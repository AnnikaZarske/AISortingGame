using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropOffFoodAction : GoapAction
{
    public float workDuration = 2;
	private float startTime = 0;

	private bool droppedOffFood = false;
	private FarmComponent targetFarm;

	public DropOffFoodAction () {
		addPrecondition ("hasFoods", true);
		addEffect ("hasFoods", false); 
		addEffect("makeFood", true);
	}
	
	public override void reset ()
	{
		droppedOffFood = false;
		targetFarm = null;
		startTime = 0;
	}
	
	public override bool isDone ()
	{
		return droppedOffFood;
	}

	public override bool requiresInRange ()
	{
		return true;
	}

	public override bool checkProceduralPrecondition (GameObject agent)
	{
		FarmComponent[] farms = (FarmComponent[]) FindObjectsOfType ( typeof(FarmComponent) );
		FarmComponent closest = null;
		float closestDist = 0;

		foreach (FarmComponent farm in farms) {
			if (farm.numFood > 0) {
				if (closest == null) {
					
					closest = farm;
					closestDist = (farm.gameObject.transform.position - agent.transform.position).magnitude;
				} else {
					float dist = (farm.gameObject.transform.position - agent.transform.position).magnitude;
					if (dist < closestDist) {
						closest = farm;
						closestDist = dist;
					}
				}
			}
		}
		if (closest == null)
			return false;

		targetFarm = closest;
		target = targetFarm.gameObject;

		return closest != null;
	}

	public override bool perform (GameObject agent)
	{
		if (startTime == 0)
			startTime = Time.time;
        
		if (Time.time - startTime > workDuration) 
		{
			BackpackComponent backpack = (BackpackComponent)agent.GetComponent(typeof(BackpackComponent));
			targetFarm.numFood += backpack.numFoods;
			droppedOffFood = true;
			backpack.numFoods = 0;
		}
		return true;
	}
}
