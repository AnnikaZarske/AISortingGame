using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public abstract class Worker : MonoBehaviour, IGoap
{

    public BackpackComponent backpack;
    public float moveSpeed = 1f;
    public Vector3 foodIconPos = new Vector3(0.1f, 0.27f, 0);
    public Vector3 toolIconPos = new Vector3(-0.1f, 0.27f, 0);
    
    void Start()
    {
        if (backpack == null)
            backpack = gameObject.AddComponent(typeof(BackpackComponent)) as BackpackComponent;

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

    public bool moveAgent(GoapAction nextAction)
    {
        float step = moveSpeed * Time.deltaTime;
        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, nextAction.target.transform.position, step);
        
        if (gameObject.transform.position.Equals(nextAction.target.transform.position) ) {
            // we are at the target location, we are done
            nextAction.setInRange(true);
            return true;
        } else
            return false;
    }
}
