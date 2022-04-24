using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class BackpackComponent : MonoBehaviour
{
    public GameObject food;
    public string foodType = "Food";
    public GameObject tool;
    public string toolType = "Tool";
    public int numFoods;
    public int numLogs;
    public int numRocks;
    public int numOres;
    public int numPlanks;
    public int numBricks;
    
    public GameObject sprite;
    public Vector3 foodIconPos = new Vector3(0.1f, 0.27f, 0);
    public Vector3 toolIconPos = new Vector3(-0.1f, 0.27f, 0);

    public void AddFood()
    {
        GameObject prefab = Resources.Load<GameObject> (foodType);
        food = Instantiate (prefab, transform.position, quaternion.identity);
        food.transform.parent = transform;
    }

    public void AddTool()
    {
        GameObject prefab = Resources.Load<GameObject> (toolType);
        tool = Instantiate (prefab, transform.position, quaternion.identity);
        tool.transform.parent = transform;
    }
    
    
    private void Update()
    {
        if (food != null)
        {
            food.transform.localPosition = sprite.transform.localPosition + foodIconPos;
        }
        if (tool != null)
        {
            tool.transform.localPosition = sprite.transform.localPosition + toolIconPos;
        }
    }
}
