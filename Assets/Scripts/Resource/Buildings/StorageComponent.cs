using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageComponent : MonoBehaviour
{
   public int numLogs;
   public int numRocks;
   public int numOres;
   public int numPlanks;
   public int numBricks;
   public int numCoins;
   public GameObject numberPopup;
   public Vector3 numberPos;
   public AudioSource audioSource;
   public AudioClip logDropOffSound;
   public AudioClip RockAndOreDropSound;

   private void Start()
   {
      numberPos = new Vector3(-3.5f, 2f, 0);
   }

   public void DisplayText(int numberAmount)
   {
      NumberPopup.Create(numberPos, numberAmount, numberPopup);
   }

   public void DropOffSound(AudioClip sound)
   {
      audioSource.PlayOneShot(sound);
   }
}
