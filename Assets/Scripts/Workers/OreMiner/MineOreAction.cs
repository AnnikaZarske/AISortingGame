using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineOreAction : GoapAction
{
    private bool mined = false;
    private OreComponent targetOre;

    private float startTime = 0;
    public float workDuration = 5;
    public int minedOres = 1;

    public MineOreAction()
    {
        addPrecondition("hasFood", true);
        addPrecondition("hasTool", true);
        addPrecondition("hasOres", false);
        addEffect("hasOres", true);
    }

    public override void reset()
    {
        mined = false;
        targetOre = null;
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
        OreComponent[] ores = (OreComponent[])FindObjectsOfType(typeof(OreComponent));
        OreComponent closest = null;
        float closestDist = 0;
        
        foreach (OreComponent ore in ores) {
            if (closest == null) {
                closest = ore;
                closestDist = (ore.gameObject.transform.position - agent.transform.position).magnitude;
            } else {
                float dist = (ore.gameObject.transform.position - agent.transform.position).magnitude;
                if (dist < closestDist) {
                    closest = ore;
                    closestDist = dist;
                }
            }
        }
        if (closest == null)
            return false;

        targetOre = closest;
        target = targetOre.gameObject;
		
        return closest != null;
    }
    
    public override bool perform (GameObject agent)
    {
        if (startTime == 0)
            startTime = Time.time;
		
        if (Time.time - startTime > workDuration) {
            OreComponent ore = targetOre;

            if(targetOre != null)
            {
                if (!ore.Destroyed())
                {
                    ore.Use(minedOres);
                    BackpackComponent backpack = (BackpackComponent)agent.GetComponent(typeof(BackpackComponent));
                    backpack.numOres += minedOres;
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
