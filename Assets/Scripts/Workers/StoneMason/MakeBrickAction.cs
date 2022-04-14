using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeBrickAction : GoapAction
{
    private bool crafted = false;
    private StoneMasonComponent targetStoneMason;

    private float startTime = 0;
    public float workDuration = 10;
    public int brickCost = 5;

    public MakeBrickAction()
    {
        addPrecondition("hasFood", true);
        addPrecondition("hasTool", true);
        addPrecondition("hasRocks", true);
        addEffect("hasRocks", false);
        addEffect("hasBricks", true);
    }

    public override void reset()
    {
        crafted = false;
        targetStoneMason = null;
        startTime = 0;
    }
    
    public override bool isDone ()
    {
        return crafted;
    }
	
    public override bool requiresInRange ()
    {
        return true;
    }

    public override bool checkProceduralPrecondition(GameObject agent)
    {
        StoneMasonComponent[] stoneMasons = (StoneMasonComponent[])FindObjectsOfType(typeof(StoneMasonComponent));
        StoneMasonComponent closest = null;
        float closestDist = 0;
        
        foreach (StoneMasonComponent stoneMason in stoneMasons) {
            if (closest == null) {
                closest = stoneMason;
                closestDist = (stoneMason.gameObject.transform.position - agent.transform.position).magnitude;
            } else {
                float dist = (stoneMason.gameObject.transform.position - agent.transform.position).magnitude;
                if (dist < closestDist) {
                    closest = stoneMason;
                    closestDist = dist;
                }
            }
        }
        if (closest == null)
            return false;

        targetStoneMason = closest;
        target = targetStoneMason.gameObject;
		
        return closest != null;
    }
    
    public override bool perform (GameObject agent)
    {
        if (startTime == 0)
            startTime = Time.time;
		
        if (Time.time - startTime > workDuration) {
            if(targetStoneMason != null)
            {
                BackpackComponent backpack = (BackpackComponent)agent.GetComponent(typeof(BackpackComponent));
                backpack.numRocks -= brickCost;
                backpack.numBricks += 1;
                crafted = true;
                
                FoodComponent food = backpack.food.GetComponent(typeof(FoodComponent)) as FoodComponent;
                food.Use(0.34f);
                if (food.Destroyed())
                {
                    Destroy(backpack.food);
                    backpack.food = null;
                }
                
                ToolComponent tool = backpack.tool.GetComponent(typeof(ToolComponent)) as ToolComponent;
                tool.Use(0.25f);
                if (tool.Destroyed())
                {
                    Destroy(backpack.tool);
                    backpack.tool = null;
                }
            }
            else
            {
                reset();
            }
        }
        return true;
    }
}
