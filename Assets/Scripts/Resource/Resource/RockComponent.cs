using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockComponent : MonoBehaviour
{
    public int rockCount;

    private void Start()
    {
        rockCount = 25;
    }

    public void Use(int mined = 3)
    {
        rockCount -= mined;
        if (Destroyed())
        {
            Destroy(this.gameObject);
        }
    }

    public bool Destroyed()
    {
        return rockCount <= 0;
    }
}
