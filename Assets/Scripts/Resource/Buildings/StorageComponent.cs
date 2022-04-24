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

   [SerializeField] private Transform NumberPopup;
   
   public void DisplayText()
   {
      Instantiate(NumberPopup);
   }
}
