using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldComponent : MonoBehaviour
{
    public int foodCount;

    private void Start()
    {
        foodCount = 15;
    }

    public void Use(int harvested = 1)
    {
        foodCount -= harvested;
    }

    public bool Destroyed()
    {
        return foodCount <= 0;
    }
}
