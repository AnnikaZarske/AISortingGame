using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChopTreeAction : GoapAction
{
    private bool chopped = false;
    private TreeComponent targetTree;

    private float startTime = 0;
    public float workDuration = 2;
    public int choppedLogs = 1;

    public ChopTreeAction()
    {
        addPrecondition("hasFood", true);
        addPrecondition("hasTool", true);
        addPrecondition("hasLogs", false);
        addEffect("hasLogs", true);
    }

    public override void reset()
    {
        chopped = false;
        targetTree = null;
        startTime = 0;
    }
    
    public override bool isDone ()
    {
        return chopped;
    }
	
    public override bool requiresInRange ()
    {
        return true;
    }

    public override bool checkProceduralPrecondition(GameObject agent)
    {
        TreeComponent[] trees = (TreeComponent[])FindObjectsOfType(typeof(TreeComponent));
        TreeComponent closest = null;
        float closestDist = 0;
        
        foreach (TreeComponent tree in trees) {
            if (closest == null) {
                closest = tree;
                closestDist = (tree.gameObject.transform.position - agent.transform.position).magnitude;
            } else {
                float dist = (tree.gameObject.transform.position - agent.transform.position).magnitude;
                if (dist < closestDist) {
                    closest = tree;
                    closestDist = dist;
                }
            }
        }
        if (closest == null)
            return false;

        targetTree = closest;
        target = targetTree.gameObject;
		
        return closest != null;
    }
    
    public override bool perform (GameObject agent)
    {
        if (startTime == 0)
            startTime = Time.time;
		
        if (Time.time - startTime > workDuration) {
            TreeComponent tree = targetTree;

            if(targetTree != null)
            {
                if (!tree.Destroyed())
                {
                    tree.Use(choppedLogs);
                    BackpackComponent backpack = (BackpackComponent)agent.GetComponent(typeof(BackpackComponent));
                    backpack.numLogs += choppedLogs;
                    chopped = true;
                    
                    FoodComponent food = backpack.food.GetComponent(typeof(FoodComponent)) as FoodComponent;
                    food.Use(0.34f);
                    if (food.Destroyed())
                    {
                        Destroy(backpack.food);
                        backpack.food = null;
                    }
                    
                    ToolComponent tool = backpack.tool.GetComponent(typeof(ToolComponent)) as ToolComponent;
                    tool.Use(0.10f);
                    if (tool.Destroyed())
                    {
                        Destroy(backpack.tool);
                        backpack.tool = null;
                    }
                        
                }
                else
                {
                    doReset();
                }
            }
            else
            {
                doReset();
            }
        }
        return true;
    }
}
