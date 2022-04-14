using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolComponent : MonoBehaviour
{
    public float toolUsage;

    private void Start()
    {
        toolUsage = 1;
    }

    public void Use(float usage)
    {
        toolUsage -= usage;
    }

    public bool Destroyed()
    {
        return toolUsage <= 0;
    }
}
