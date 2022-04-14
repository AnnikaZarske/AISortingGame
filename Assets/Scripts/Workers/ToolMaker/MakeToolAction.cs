using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeToolAction : GoapAction
{
    private bool crafted = false;
    private SmithComponent targetSmith;

    private float startTime = 0;
    public float workDuration = 10;

    public MakeToolAction()
    {
        addPrecondition("hasFood", true);
        addPrecondition("hasLogs", true);
        addPrecondition("hasOres", true);
        addEffect("hasLogs", false);
        addEffect("hasOres", false);
        addEffect("makeTools", true);
    }

    public override void reset()
    {
        crafted = false;
        targetSmith = null;
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
        SmithComponent[] smiths = (SmithComponent[])FindObjectsOfType(typeof(SmithComponent));
        SmithComponent closest = null;
        float closestDist = 0;
        
        foreach (SmithComponent smith in smiths) {
            if (closest == null) {
                closest = smith;
                closestDist = (smith.gameObject.transform.position - agent.transform.position).magnitude;
            } else {
                float dist = (smith.gameObject.transform.position - agent.transform.position).magnitude;
                if (dist < closestDist) {
                    closest = smith;
                    closestDist = dist;
                }
            }
        }
        if (closest == null)
            return false;

        targetSmith = closest;
        target = targetSmith.gameObject;
		
        return closest != null;
    }
    
    public override bool perform (GameObject agent)
    {
        if (startTime == 0)
            startTime = Time.time;
		
        if (Time.time - startTime > workDuration) {
            if(targetSmith != null)
            {
                BackpackComponent backpack = (BackpackComponent)agent.GetComponent(typeof(BackpackComponent));
                backpack.numLogs -= 1;
                backpack.numOres -= 1;
                targetSmith.numTools += 1;
                crafted = true;
                
                FoodComponent food = backpack.food.GetComponent(typeof(FoodComponent)) as FoodComponent;
                food.Use(0.34f);
                if (food.Destroyed())
                {
                    Destroy(backpack.food);
                    backpack.food = null;
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
