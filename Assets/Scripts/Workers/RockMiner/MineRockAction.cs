using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineRockAction : GoapAction
{
    private bool mined = false;
    private RockComponent targetRock;

    private float startTime = 0;
    public float workDuration = 2;
    public int minedRocks = 3;

    public MineRockAction()
    {
        addPrecondition("hasFood", true);
        addPrecondition("hasTool", true);
        addPrecondition("hasRocks", false);
        addEffect("hasRocks", true);
    }

    public override void reset()
    {
        mined = false;
        targetRock = null;
        startTime = 0;
    }
    
    public override bool isDone ()
    {
        return mined;
    }
	
    public override bool requiresInRange ()
    {
        return true;
    }

    public override bool checkProceduralPrecondition(GameObject agent)
    {
        RockComponent[] rocks = (RockComponent[])FindObjectsOfType(typeof(RockComponent));
        RockComponent closest = null;
        float closestDist = 0;
        
        foreach (RockComponent rock in rocks) {
            if (closest == null) {
                closest = rock;
                closestDist = (rock.gameObject.transform.position - agent.transform.position).magnitude;
            } else {
                float dist = (rock.gameObject.transform.position - agent.transform.position).magnitude;
                if (dist < closestDist) {
                    closest = rock;
                    closestDist = dist;
                }
            }
        }
        if (closest == null)
            return false;

        targetRock = closest;
        target = targetRock.gameObject;
		
        return closest != null;
    }
    
    public override bool perform (GameObject agent)
    {
        if (startTime == 0)
            startTime = Time.time;
		
        if (Time.time - startTime > workDuration) {
            RockComponent rock = targetRock;

            if(targetRock != null)
            {
                if (!rock.Destroyed())
                {
                    rock.Use(minedRocks);
                    BackpackComponent backpack = (BackpackComponent)agent.GetComponent(typeof(BackpackComponent));
                    backpack.numRocks += minedRocks;
                    mined = true;
                    
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
