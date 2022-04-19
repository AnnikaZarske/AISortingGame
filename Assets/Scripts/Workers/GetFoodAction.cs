﻿using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class GetFoodAction : GoapAction
{
	public float workDuration = 2;
	private float startTime = 0;
	
    private bool hasFood = false;
	private FarmComponent targetFarm;

	private Vector3 foodIconPos = new Vector3(0.1f, 0.27f, 0);

	public GetFoodAction () {
		addPrecondition ("hasFood", false);
		addEffect ("hasFood", true); 
	}
	
	public override void reset ()
	{
		hasFood = false;
		targetFarm = null;
		startTime = 0;
	}
	
	public override bool isDone ()
	{
		return hasFood;
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
			if (targetFarm.numFood > 0) 
			{
				targetFarm.numFood -= 1;
				hasFood = true;

				ResourceCount resourceCount = FindObjectOfType<ResourceCount>().GetComponent<ResourceCount>();
				resourceCount.numCoins += 1;
				
				BackpackComponent backpack = (BackpackComponent)agent.GetComponent(typeof(BackpackComponent));
				GameObject prefab = Resources.Load<GameObject> (backpack.foodType);
				GameObject food = Instantiate (prefab, transform.position, quaternion.identity);
				backpack.food = food;
				food.transform.parent = transform;
				food.transform.localPosition = foodIconPos;

				return true;
			} else {
			
				return false;
			}
		}
		return true;
	}
}