using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class GetToolAction : GoapAction
{
    public float workDuration = 2;
	private float startTime = 0;
	
    private bool hasTool = false;
	private SmithComponent targetSmith;
	
	public Vector3 toolIconPos = new Vector3(-0.1f, 0.27f, 0);

	public GetToolAction () {
		addPrecondition ("hasTool", false);
		addEffect ("hasTool", true); 
	}
	
	public override void reset ()
	{
		hasTool = false;
		targetSmith = null;
		startTime = 0;
	}
	
	public override bool isDone ()
	{
		return hasTool;
	}

	public override bool requiresInRange ()
	{
		return true;
	}

	public override bool checkProceduralPrecondition (GameObject agent)
	{
		SmithComponent[] smiths = (SmithComponent[]) FindObjectsOfType ( typeof(SmithComponent) );
		SmithComponent closest = null;
		float closestDist = 0;

		foreach (SmithComponent smith in smiths) {
			if (smith.numTools > 0) {
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
        
		if (Time.time - startTime > workDuration) 
		{
			if (targetSmith.numTools > 0) 
			{
				targetSmith.numTools -= 1;
				hasTool = true;

				BackpackComponent backpack = (BackpackComponent)agent.GetComponent(typeof(BackpackComponent));
				GameObject prefab = Resources.Load<GameObject> (backpack.toolType);
				GameObject tool = Instantiate (prefab, transform.position, quaternion.identity);
				backpack.tool = tool;
				tool.transform.parent = transform;
				tool.transform.localPosition = toolIconPos;

				return true;
			} else {
			
				return false;
			}
		}
		return true;
	}
}
