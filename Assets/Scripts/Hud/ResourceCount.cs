using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Experimental;
using UnityEngine;

public class ResourceCount : MonoBehaviour
{
    [Header("Basic material")]
    public TextMeshProUGUI logCount;
    public TextMeshProUGUI rockCount;
    public TextMeshProUGUI oreCount;
    public TextMeshProUGUI foodCount;
    public TextMeshProUGUI coinCount;
    public int numCoins; // Replace with coins
    
    [Header("Crafted material")]
    public TextMeshProUGUI plankCount;
    public TextMeshProUGUI brickCount;
    public TextMeshProUGUI toolCount;
    
    [Header("Connected components")] // Only works for one of each atm
    public StorageComponent storage;
    public FarmComponent farm;
    public SmithComponent smith;
    
    
    
    
    void Start()
    {
        logCount.text = "00";
        rockCount.text = "00";
        oreCount.text = "00";
        plankCount.text = "00";
        brickCount.text = "00";
        coinCount.text = "00";
        foodCount.text = "00";
        toolCount.text = "00";
    }
    
    void Update()
    {
        coinCount.text = numCoins.ToString("00");
        
        logCount.text = storage.numLogs.ToString("00");

        rockCount.text = storage.numRocks.ToString("00");
        oreCount.text = storage.numOres.ToString("00");
        
        plankCount.text = storage.numPlanks.ToString("00");
        brickCount.text = storage.numBricks.ToString("00");

        foodCount.text = farm.numFood.ToString("00");
        toolCount.text = smith.numTools.ToString("00");
    }
}
