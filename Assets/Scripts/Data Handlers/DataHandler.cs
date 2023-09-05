using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DataHandler : MonoBehaviour, IDataPersistence
{
// anything that needs saved or accessed from many different places for some reason needs to make it's way here
#region List of Data
    public static bool isDataLoaded = false;
    public static float creditAmount = 0;
    public static float creditPerSecond;
    public static float playerStrengthStat = 1f;
    public static float playerDefenseStat = 1f;
    public static float playerHealthPoints = 25f;
    public static float playerMaxHealthPoints = 25f;
    public static float playerSpeedValue = 1.0f;
    public static float inventoryCreditMultiplier = 1.0f;
    public static float playerShipSpeed = 1.0f;
    // Update these arrays any time a new generator is added////////////////////////////////////////////
    public static float[] genNumCount = { 1, 0, 0, 0, 0 };
    public static float[] genCost = { 5, 500, 5000, 20000, 100000 };
    public static float[] genBaseCps = { 1, 25, 100, 500, 2000 };
    public static float[] genBaseMultiplier = { 1, 1, 1, 1, 1 };
    public static float[] genFinalMultiplier =  { 1, 1, 1, 1, 1 };
    public static float[] genCostIncreaseRate = { 1.07f, 1.14f, 1.21f, 1.28f, 1.35f };
    public static float[] genFinalCost = new float[genNumCount.Length];
    public static float[] genCps = { (1*genNumCount[0]*genFinalMultiplier[0]),
                                      25*genNumCount[1]*genFinalMultiplier[1],
                                      100*genNumCount[2]*genFinalMultiplier[2],
                                      500*genNumCount[3]*genFinalMultiplier[3],
                                      2000*genNumCount[4]*genFinalMultiplier[4] };
    /////////////////////////////////////////////////////////////////////////////////////////////////////

#endregion List of Data

// normal save/load call present in most places - needs updated any time there is a new piece of data to track inside of DataHandler
#region Save / Load
    public void LoadData(GameData data)
    {
        // credit/generator values
        creditAmount = data.creditAmount;
        creditPerSecond = data.creditPerSecond;
        genNumCount = new float[] { data.genNumCount[0], data.genNumCount[1], data.genNumCount[2], data.genNumCount[3], data.genNumCount[4] };
        genBaseMultiplier = new float[] { data.genBaseMultiplier[0], data.genBaseMultiplier[1], data.genBaseMultiplier[2], data.genBaseMultiplier[3], data.genBaseMultiplier[4] };
        // player stat values
        playerStrengthStat = data.playerStrengthStat;
        playerDefenseStat = data.playerDefenseStat;
        playerHealthPoints = data.playerHealthPoints;
        playerShipSpeed = data.playerShipSpeed;
        playerMaxHealthPoints = data.playerMaxHealthPoints;
    }

    public void SaveData(GameData data)
    {
        // credit/generator values
        data.creditAmount = creditAmount;
        data.creditPerSecond = creditPerSecond;
        data.genNumCount = new float[] { genNumCount[0], genNumCount[1], genNumCount[2], genNumCount[3], genNumCount[4] };
        data.genBaseMultiplier = new float[] { genBaseMultiplier[0], genBaseMultiplier[1], genBaseMultiplier[2], genBaseMultiplier[3], genBaseMultiplier[4] };
        // player stat values
        data.playerStrengthStat = playerStrengthStat;
        data.playerDefenseStat = playerDefenseStat;
        data.playerHealthPoints = playerHealthPoints;
        data.playerShipSpeed = playerShipSpeed;

    }
#endregion Save / Load
}
