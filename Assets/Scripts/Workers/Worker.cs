using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public abstract class Worker : MonoBehaviour, IGoap
{
    public BackpackComponent backpack;
    public Pathfinding pathfinding;
    public float moveSpeed = 1f;
    public Vector3 foodIconPos = new Vector3(0.1f, 0.27f, 0);
    public Vector3 toolIconPos = new Vector3(-0.1f, 0.27f, 0);
    
    void Start()
    {
        if (backpack == null)
            backpack = gameObject.AddComponent(typeof(BackpackComponent)) as BackpackComponent;
        
        if (pathfinding == null)
            pathfinding = gameObject.AddComponent(typeof(Pathfinding)) as Pathfinding;

        if (backpack.food == null)
        {
            GameObject prefab = Resources.Load<GameObject> (backpack.foodType);
            GameObject food = Instantiate (prefab, transform.position, quaternion.identity);
            backpack.food = food;
            food.transform.parent = transform;
            food.transform.localPosition = foodIconPos;
        }
        
        if (backpack.tool == null)
        {
            GameObject prefab = Resources.Load<GameObject> (backpack.toolType);
            GameObject tool = Instantiate (prefab, transform.position, quaternion.identity);
            backpack.tool = tool;
            tool.transform.parent = transform;
            tool.transform.localPosition = toolIconPos;
        }
    }

    public HashSet<KeyValuePair<string, object>> getWorldState()
    {
        HashSet<KeyValuePair<string, object>> worldData = new HashSet<KeyValuePair<string, object>>();

        worldData.Add(new KeyValuePair<string, object>("hasLogs", backpack.numLogs > 0 ));
        worldData.Add(new KeyValuePair<string, object>("hasRocks", backpack.numRocks > 0 ));
        worldData.Add(new KeyValuePair<string, object>("hasOres", backpack.numOres > 0 ));
        worldData.Add(new KeyValuePair<string, object>("hasPlanks", backpack.numPlanks > 0 ));
        worldData.Add(new KeyValuePair<string, object>("hasBricks", backpack.numBricks > 0 ));
        worldData.Add(new KeyValuePair<string, object>("hasFoods", backpack.numFoods > 0 ));
        worldData.Add(new KeyValuePair<string, object>("hasFood", backpack.food != null));
        worldData.Add(new KeyValuePair<string, object>("hasTool", backpack.tool != null));
        worldData.Add(new KeyValuePair<string, object>("hasToolSupplies", (backpack.numLogs > 0 && backpack.numOres > 0)));
        worldData.Add(new KeyValuePair<string, object>("makeTools", (backpack.numLogs <= 0 && backpack.numOres <= 0)));

        return worldData;
    }

    public abstract HashSet<KeyValuePair<string, object>> createGoalState();
    
    public void planFailed(HashSet<KeyValuePair<string, object>> failedGoal){}
    
    public void planFound (HashSet<KeyValuePair<string, object>> goal, Queue<GoapAction> actions)
    {
        Debug.Log ("<color=green>Plan found</color> "+GoapAgent.prettyPrint(actions));
    }

    public void actionsFinished ()
    {
        Debug.Log ("<color=blue>Actions completed</color>");
    }

    public void planAborted (GoapAction aborter)
    {
        Debug.Log ("<color=red>Plan Aborted</color> "+GoapAgent.prettyPrint(aborter));
    }

    private Vector3[] path;
    private bool pathSuccess;
    private bool targetReached;
    private int pathIndex;
    
    public bool moveAgent(GoapAction nextAction)
    {
        float step = moveSpeed * Time.deltaTime;
        if (path == null) {
            pathSuccess = pathfinding.FindPath(gameObject.transform.position, nextAction.target.transform.position);
            if (pathSuccess) {
                path = pathfinding.waypoints;
                targetReached = false;
            }else {
                Debug.LogWarning("<color=red>No path found in moveAgent</color>");
            }
            pathIndex = 0;
            
        }else {
            Vector3 currentWaypoint = path[pathIndex];
            transform.position = Vector3.MoveTowards(gameObject.transform.position, currentWaypoint, step);
            if (Vector3.Distance(transform.position, currentWaypoint) < 0.01f) {
                pathIndex++;
                if (pathIndex == path.Length) {
                    targetReached = true;
                }
            }
        }
        
        if (Vector3.Distance(gameObject.transform.position, nextAction.target.transform.position) < 0.3f || targetReached) {
            // we are at the target location, we are done
            nextAction.setInRange(true);
            pathIndex = 0;
            path = null;
            targetReached = false;
            return true;
        } else
            return false;
    }

    public void OnDrawGizmos()
    {
        if (path != null) {
            for(int i = pathIndex; i < path.Length; i++) {
                Gizmos.color = Color.black;
                Gizmos.DrawCube(path[i], Vector3.one * 0.1f);

                if (i == pathIndex) {
                    Gizmos.DrawLine(transform.position, path[i]);
                }else {
                    Gizmos.DrawLine(path[i-1], path[i]);
                }
            }
        }
    }
}
