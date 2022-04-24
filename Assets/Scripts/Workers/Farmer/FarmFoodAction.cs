using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmFoodAction : GoapAction
{
    private bool farmed = false;
    private FieldComponent targetField;

    private float startTime = 0;
    public float workDuration = 2;
    public int farmedFood = 1;

    public AudioSource audioSource;
    public AudioClip foodFarmedSound;

    public FarmFoodAction()
    {
        addPrecondition("hasFoods", false);
        addPrecondition("hasTool", true);
        addEffect("hasFoods", true);
    }

    public override void reset()
    {
        farmed = false;
        targetField = null;
        startTime = 0;
    }
    
    public override bool isDone ()
    {
        return farmed;
    }
	
    public override bool requiresInRange ()
    {
        return true;
    }

    public override bool checkProceduralPrecondition(GameObject agent)
    {
        FieldComponent[] fields = (FieldComponent[])FindObjectsOfType(typeof(FieldComponent));
        FieldComponent closest = null;
        float closestDist = 0;
        
        foreach (FieldComponent field in fields) {
            if (closest == null) {
                closest = field;
                closestDist = (field.gameObject.transform.position - agent.transform.position).magnitude;
            } else {
                float dist = (field.gameObject.transform.position - agent.transform.position).magnitude;
                if (dist < closestDist) {
                    closest = field;
                    closestDist = dist;
                }
            }
        }
        if (closest == null)
            return false;

        targetField = closest;
        target = targetField.gameObject;
		
        return closest != null;
    }
    
    public override bool perform (GameObject agent)
    {
        if (startTime == 0)
            startTime = Time.time;
		
        if (Time.time - startTime > workDuration) {
            FieldComponent field = targetField;

            if(targetField != null)
            {
                field.Use(farmedFood);
                BackpackComponent backpack = (BackpackComponent)agent.GetComponent(typeof(BackpackComponent));
                backpack.numFoods += farmedFood;
                audioSource.PlayOneShot(foodFarmedSound);
                farmed = true;

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
        return true;
    }
}