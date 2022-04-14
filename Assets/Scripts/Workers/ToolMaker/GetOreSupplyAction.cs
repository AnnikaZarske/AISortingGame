using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class GetOreSupplyAction : GoapAction
{
    private bool gatheredOreSupply = false;
    private StorageComponent targetSupplyPile;
    
    private float startTime = 0;
    public float workDuration = 3;
	
    public GetOreSupplyAction () {
        addPrecondition ("hasOres", false); 
        addEffect ("hasOres", true);
    }
    
    public override void reset ()
    {
        gatheredOreSupply = false;
        targetSupplyPile = null;
        startTime = 0;
    }
	
    public override bool isDone ()
    {
        return gatheredOreSupply;
    }
	
    public override bool requiresInRange ()
    {
        return true; 
    }
	
    public override bool checkProceduralPrecondition (GameObject agent)
    {
        StorageComponent[] supplyPiles = (StorageComponent[]) FindObjectsOfType ( typeof(StorageComponent) );
        StorageComponent closest = null;
        float closestDist = 0;
		
        foreach (StorageComponent supply in supplyPiles) {
            if (closest == null) {
                closest = supply;
                closestDist = (supply.gameObject.transform.position - agent.transform.position).magnitude;
            } else {
                float dist = (supply.gameObject.transform.position - agent.transform.position).magnitude;
                if (dist < closestDist) {
                    closest = supply;
                    closestDist = dist;
                }
            }
        }
        if (closest == null)
            return false;

        targetSupplyPile = closest;
        target = targetSupplyPile.gameObject;
		
        return closest != null;
    }
	
    public override bool perform (GameObject agent)
    {
        if (startTime == 0)
            startTime = Time.time;
        
        if (Time.time - startTime > workDuration) {
            if (targetSupplyPile.numOres > 0)
            {
                BackpackComponent backpack = (BackpackComponent)agent.GetComponent(typeof(BackpackComponent));
                backpack.numOres += 1;
                targetSupplyPile.numOres -= 1;
                gatheredOreSupply = true;
            }
        }
        return true;
    }
}
