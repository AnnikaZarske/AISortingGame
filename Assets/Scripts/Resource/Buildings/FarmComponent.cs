﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmComponent : MonoBehaviour
{
    public int numFood;
    public GameObject numberPopup;
    public AudioSource audioSource;
    public AudioClip coinSound, foodDropoffSound;
    public Vector3 numberPos;

    private void Start()
    {
        numberPos = new Vector3(-6f, -1.8f, 0);
    }

    public void DisplayText(int numberAmount)
    {
        NumberPopup.Create(numberPos, numberAmount, numberPopup);
    }

    public void PlaySound(AudioClip sound)
    {
        audioSource.PlayOneShot(sound);
    }
}
