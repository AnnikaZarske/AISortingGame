using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OreComponent : MonoBehaviour
{
    public int oreCount;

    private void Start()
    {
        oreCount = 15;
    }

    public void Use(int mined = 1)
    {
        oreCount -= mined;
        if (Destroyed())
        {
            Destroy(this.gameObject);
        }
    }

    public bool Destroyed()
    {
        return oreCount <= 0;
    }
}
