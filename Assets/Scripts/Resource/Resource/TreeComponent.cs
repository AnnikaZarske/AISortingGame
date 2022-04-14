using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeComponent : MonoBehaviour
{
    public Sprite stumpSprite;
    public int logCount;

    private new SpriteRenderer renderer;

    private void Start()
    {
        renderer = this.GetComponent<SpriteRenderer>();
        logCount = 4;
    }

    public void Use(int chopped = 1)
    {
        logCount -= chopped;
        if (Destroyed())
        {
            renderer.sprite = stumpSprite;
            Destroy(this);
        }
    }

    public bool Destroyed()
    {
        return logCount <= 0;
    }
}
