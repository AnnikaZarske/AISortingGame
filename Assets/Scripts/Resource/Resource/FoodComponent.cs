using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodComponent : MonoBehaviour
{
    public float foodAmount;

    private void Start()
    {
        foodAmount = 1;
    }

    public void Use(float eaten)
    {
        float random = Random.Range(0, 0.2f);
        foodAmount -= eaten;
        foodAmount -= random;
    }

    public bool Destroyed()
    {
        return foodAmount <= 0;
    }
}
