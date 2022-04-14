using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetLogSupplyAction : GoapAction
{
    private bool gatheredLogSupply = false;
    private StorageComponent targetSupplyPile;
    
    private float startTime = 0;
    public float workDuration = 3;
	
    public GetLogSupplyAction () {
        addPrecondition ("hasLogs", false); 
        addEffect ("hasLogs", true);
    }
    
    public override void reset ()
    {
        gatheredLogSupply = false;
        targetSupplyPile = null;
        startTime = 0;
    }
	
    public override bool isDone ()
    {
        return gatheredLogSupply;
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
            if (targetSupplyPile.numLogs > 0)
            {
                BackpackComponent backpack = (BackpackComponent)agent.GetComponent(typeof(BackpackComponent));
                backpack.numLogs += 1;
                targetSupplyPile.numLogs -= 1;
                gatheredLogSupply = true;
            }
        }
        return true;
    }
}
