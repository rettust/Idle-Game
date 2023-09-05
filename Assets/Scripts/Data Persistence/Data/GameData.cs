using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[System.Serializable]
public class GameData
{
// all of the data being received before it hits the json immediately after calling save function
#region ListOfData
    // credit variables to serialize
    public float creditAmount;
    public float creditPerSecond;
    public float playerStrengthStat;
    public float playerDefenseStat;
    public float playerHealthPoints;
    public float playerMaxHealthPoints;
    public float playerShipSpeed;
    public float[] genNumCount, genBaseMultiplier;
    public float planetPos;
    public Vector3 shipPos;
    public bool isShipTraveling;
    public string planetDestination;
    public SerializableDictionary<string, bool> upgradesBought;


#endregion ListOfData

    //these will be your starting values on a new game
    public GameData()
    {
        // credit / generator values
        creditAmount = 0f;
        genNumCount = new float[5] {1,0,0,0,0};
        genBaseMultiplier = new float[5] {1,1,1,1,1};
        // inventory serialization
        // player stat values
        playerStrengthStat = 1f;
        playerDefenseStat = 1f;
        playerMaxHealthPoints = 25f;
        playerHealthPoints = 25f;
        playerShipSpeed = 0.5f;
        // "map" screen values; planetPos = timer constant from MapHandler.cs
        planetPos = 1.0f;
        isShipTraveling = false;
        // checks all upgrades to tick off the ones that are purchased
        upgradesBought = new SerializableDictionary<string, bool>();
    }

    public void NewGameData()
    {
        // credit / generator values
        DataHandler.creditAmount = 0f;
        DataHandler.genNumCount = new float[5] {1,0,0,0,0};
        DataHandler.genBaseMultiplier = new float[5] {1,1,1,1,1};
        // inventory serialization
        // player stat values
        DataHandler.playerStrengthStat = 1f;
        DataHandler.playerDefenseStat = 1f;
        DataHandler.playerMaxHealthPoints = 25f;
        DataHandler.playerHealthPoints = 25f;
        DataHandler.playerShipSpeed = 0.5f;
        // "map" screen values; planetPos = timer constant from MapHandler.cs
        planetPos = 1.0f;
        isShipTraveling = false;
        // checks all upgrades to tick off the ones that are purchased
        upgradesBought = new SerializableDictionary<string, bool>();
    }

}
