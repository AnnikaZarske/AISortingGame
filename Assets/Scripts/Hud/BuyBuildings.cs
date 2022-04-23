using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.UI;

public class BuyBuildings : MonoBehaviour
{
    [Header("Building Costs")] 
    public int sawmillCostLog = 10;
    public int stoneMasonCostRocks = 15;
    public int smithCostLog = 5;
    public int smithCostRock = 5;
    public int farmFieldCostCoins = 10;
    public int farmFieldFoodAmount;
    
    [Header("Connected components")]
    public StorageComponent storage;
    public SpriteRenderer sawmillBuilding;
    public SpriteRenderer stoneMasonBuilding;
    public SpriteRenderer smithBuilding;
    public SpriteRenderer farmFieldBuilding;
    public Button carpenterButton, masonButton, smithButton;
    public Button sawmillBuyButton, stoneMasonBuyButton, smithBuyButton, farmFieldBuyButton;
    
    void Start()
    {
        storage = FindObjectOfType<StorageComponent>();
    }

    public void BuySawmill()
    {
        if (sawmillCostLog <= storage.numLogs)
        {
            storage.numLogs -= sawmillCostLog;
            carpenterButton.interactable = true;
            sawmillBuyButton.interactable = false;
            sawmillBuilding.color = Color.white;
        } 
    }
    
    public void BuyStoneMason()
    {
        if (stoneMasonCostRocks <= storage.numRocks)
        {
            storage.numRocks -= stoneMasonCostRocks;
            masonButton.interactable = true;
            stoneMasonBuyButton.interactable = false;
            stoneMasonBuilding.color = Color.white;
        } 
    }

    public void BuySmith()
    {
        if (smithCostRock <= storage.numRocks && smithCostLog <= storage.numLogs)
        {
            storage.numRocks -= smithCostRock;
            storage.numLogs -= smithCostLog;
            smithButton.interactable = true;
            smithBuyButton.interactable = false;
            smithBuilding.color = Color.white;
        } 
    }
    
    public void BuyFarmField()
    {
        if (farmFieldCostCoins <= storage.numCoins)
        {
            storage.numCoins -= farmFieldCostCoins;
            farmFieldBuyButton.interactable = false;
            farmFieldBuilding.color = Color.white;
            farmFieldBuilding.gameObject.AddComponent<FarmComponent>();
            farmFieldBuilding.GetComponent<FarmComponent>().numFood = farmFieldFoodAmount;
        } 
    }
}
