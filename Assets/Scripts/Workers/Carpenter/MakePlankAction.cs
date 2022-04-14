using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakePlankAction : GoapAction
{
    private bool crafted = false;
    private SawMillComponent targetSawmill;

    private float startTime = 0;
    public float workDuration = 10;

    public MakePlankAction()
    {
        addPrecondition("hasFood", true);
        addPrecondition("hasTool", true);
        addPrecondition("hasLogs", true);
        addEffect("hasLogs", false);
        addEffect("hasPlanks", true);
    }

    public override void reset()
    {
        crafted = false;
        targetSawmill = null;
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
        SawMillComponent[] sawmills = (SawMillComponent[])FindObjectsOfType(typeof(SawMillComponent));
        SawMillComponent closest = null;
        float closestDist = 0;
        
        foreach (SawMillComponent sawmill in sawmills) {
            if (closest == null) {
                closest = sawmill;
                closestDist = (sawmill.gameObject.transform.position - agent.transform.position).magnitude;
            } else {
                float dist = (sawmill.gameObject.transform.position - agent.transform.position).magnitude;
                if (dist < closestDist) {
                    closest = sawmill;
                    closestDist = dist;
                }
            }
        }
        if (closest == null)
            return false;

        targetSawmill = closest;
        target = targetSawmill.gameObject;
		
        return closest != null;
    }
    
    public override bool perform (GameObject agent)
    {
        if (startTime == 0)
            startTime = Time.time;
		
        if (Time.time - startTime > workDuration) {
            if(targetSawmill != null)
            {
                BackpackComponent backpack = (BackpackComponent)agent.GetComponent(typeof(BackpackComponent));
                backpack.numLogs -= 1;
                backpack.numPlanks += 1;
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
