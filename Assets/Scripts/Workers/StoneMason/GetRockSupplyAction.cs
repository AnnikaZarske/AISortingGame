using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetRockSupplyAction : GoapAction
{
    private bool gatheredRockSupply = false;
    private StorageComponent targetSupplyPile;
    
    private float startTime = 0;
    public float workDuration = 3;
    public int brickCost = 5;
	
    public GetRockSupplyAction () {
        addPrecondition ("hasRocks", false); 
        addEffect ("hasRocks", true);
    }
    
    public override void reset ()
    {
        gatheredRockSupply = false;
        targetSupplyPile = null;
        startTime = 0;
    }
	
    public override bool isDone ()
    {
        return gatheredRockSupply;
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
            if (targetSupplyPile.numRocks >= brickCost)
            {
                BackpackComponent backpack = (BackpackComponent)agent.GetComponent(typeof(BackpackComponent));
                backpack.numRocks += brickCost;
                targetSupplyPile.numRocks -= brickCost;
                gatheredRockSupply = true;
            }
        }
        return true;
    }
}
