using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour, IDataPersistence
{
    private GameObject itemGO;
    public EquipmentManager equip;
    public SliderController slider;
    private InventoryItemData _itemData;
    private CurrencyHandler curr;
    public GameObject currencyManager;

    void Start()
    {
        curr = currencyManager.GetComponent<CurrencyHandler>();
    }

    // handles all logic for 'Credit' screen - ewww get this code away from me
    #region CreditButtons
    public void CreditButtonOverlays(string type)
    {
        switch (type)
        {
            case "Gen1":
                curr.UpdateGenOverlay(0);
                curr.UpdateGenText(0);
                break;
            case "Gen2":
                curr.UpdateGenOverlay(1);
                curr.UpdateGenText(1);
                break;
            case "Gen3":
                curr.UpdateGenOverlay(2);
                curr.UpdateGenText(2);
                break;
            case "Gen4":
                curr.UpdateGenOverlay(3);
                curr.UpdateGenText(3);
                break;
            case "Gen5":
                curr.UpdateGenOverlay(4);
                curr.UpdateGenText(4);
                break;
        }
    }

    public void Buy1GeneratorButton(string type)
    {
        switch (type)
        {
            case "Gen1Buy1":
                if (DataHandler.creditAmount >= DataHandler.genFinalCost[0] && DataHandler.creditAmount >= DataHandler.genCost[0])
                {
                    curr.Buy1Gen(0);
                    curr.UpdateGenStats(0);
                }
                break;
            case "Gen2Buy1":
                if (DataHandler.creditAmount >= DataHandler.genFinalCost[1] && DataHandler.creditAmount >= DataHandler.genCost[1])
                {
                    curr.Buy1Gen(1);
                    curr.UpdateGenStats(1);
                }
                break;
            case "Gen3Buy1":
                if (DataHandler.creditAmount >= DataHandler.genFinalCost[2] && DataHandler.creditAmount >= DataHandler.genCost[2])
                {
                    curr.Buy1Gen(2);
                    curr.UpdateGenStats(2);
                }
                break;
            case "Gen4Buy1":
                if (DataHandler.creditAmount >= DataHandler.genFinalCost[3] && DataHandler.creditAmount >= DataHandler.genCost[3])
                {
                    curr.Buy1Gen(3);
                    curr.UpdateGenStats(3);
                }
                break;
            case "Gen5Buy1":
                if (DataHandler.creditAmount >= DataHandler.genFinalCost[4] && DataHandler.creditAmount >= DataHandler.genCost[4])
                {
                    curr.Buy1Gen(4);
                    curr.UpdateGenStats(4);
                }
                break;
        }
    }

    public void Buy10Gen(string type)
    {
        switch (type)
        {
            case "Gen1Buy10":
                curr.BuyMultiGen(10, 0);
                curr.UpdateGenStats(0);
                break;
            case "Gen2Buy10":
                curr.BuyMultiGen(10, 1);
                curr.UpdateGenStats(1);
                break;
            case "Gen3Buy10":
                curr.BuyMultiGen(10, 2);
                curr.UpdateGenStats(2);
                break;
            case "Gen4Buy10":
                curr.BuyMultiGen(10, 3);
                curr.UpdateGenStats(3);
                break;
            case "Gen5Buy10":
                curr.BuyMultiGen(10, 4);
                curr.UpdateGenStats(4);
                break;
        }
    }

    public void Buy100Gen(string type)
    {
        switch (type)
        {
            case "Gen1Buy100":
                curr.BuyMultiGen(100, 0);
                curr.UpdateGenStats(0);
                break;
            case "Gen2Buy100":
                curr.BuyMultiGen(100, 1);
                curr.UpdateGenStats(1);
                break;
            case "Gen3Buy100":
                curr.BuyMultiGen(100, 2);
                curr.UpdateGenStats(2);
                break;
            case "Gen4Buy100":
                curr.BuyMultiGen(100, 3);
                curr.UpdateGenStats(3);
                break;
            case "Gen5Buy100":
                curr.BuyMultiGen(100, 4);
                curr.UpdateGenStats(4);
                break;
        }
    }

    public void BuyMaxGenButton(string type)
    {
        switch (type)
        {
            case "Gen1BuyMax":
                curr.BuyMultiGen(Mathf.FloorToInt(curr.buyMaxGen[0]), 0);
                curr.UpdateGenStats(0);
                break;
            case "Gen2BuyMax":
                curr.BuyMultiGen(Mathf.FloorToInt(curr.buyMaxGen[1]), 1);
                curr.UpdateGenStats(1);
                break;
            case "Gen3BuyMax":
                curr.BuyMultiGen(Mathf.FloorToInt(curr.buyMaxGen[2]), 2);
                curr.UpdateGenStats(2);
                break;
            case "Gen4BuyMax":
                curr.BuyMultiGen(Mathf.FloorToInt(curr.buyMaxGen[3]), 3);
                curr.UpdateGenStats(3);
                break;
            case "Gen5BuyMax":
                curr.BuyMultiGen(Mathf.FloorToInt(curr.buyMaxGen[4]), 4);
                curr.UpdateGenStats(4);
                break;
        }
    }


#endregion CreditButtons

// handles the button logic for each equipment slot: when you click, it checks to see if the slot is empty, moves instantiated clone from item to slot GO
// then adjusts stats & checks to see if the slot is empty
#region EquipItem
    public void EquipItem(string id, string type)
    {
        itemGO = GameObject.Find(id.ToString());
        switch (type)
        {
            case "Head":
                equip.SendEmBackCheck(equip.headSlot);
                itemGO.transform.SetParent(equip.headSlot.transform);
                equip.AdjustStatOnEquip(equip.headSlot);
                equip.CheckIfEmpty();
                break;
            case "Armor":
                equip.SendEmBackCheck(equip.armorSlot);
                itemGO.transform.SetParent(equip.armorSlot.transform);
                equip.AdjustStatOnEquip(equip.armorSlot);
                equip.CheckIfEmpty();
                break;
            case "Weapon":
                equip.SendEmBackCheck(equip.wpnSlot);
                itemGO.transform.SetParent(equip.wpnSlot.transform);
                equip.AdjustStatOnEquip(equip.wpnSlot);
                equip.CheckIfEmpty();
                break;
            case "Feet":
                equip.SendEmBackCheck(equip.feetSlot);
                itemGO.transform.SetParent(equip.feetSlot.transform);
                equip.AdjustStatOnEquip(equip.feetSlot);
                equip.CheckIfEmpty();
                break;
            case "Ring":
                // do stuff
                break;
            case "Shield":
                // do stuff
                break;
        }
    }
#endregion EquipItem

// regular save/load script present in most places
#region Save / Load
    public void SaveData(GameData data)
    {

    }

    public void LoadData(GameData data)
    {

    }
#endregion Save / Load
}
