using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentManager : MonoBehaviour
{
    public GameObject headSlot;
    public GameObject wpnSlot;
    public GameObject offhandSlot;
    public GameObject armorSlot;
    public GameObject feetSlot;
    public GameObject ringSlotRight;
    public GameObject ringSlotLeft;
    private InventoryItemData g;
    public GameObject inventoryParent;
    private Image invImage;
    public Transform[] inventorySlots;

    public void Start()
    {
        CheckIfEmpty();
    }
    public void CheckIfEmpty()
    {
        foreach (Transform invSlot in inventorySlots){
            if(invSlot.transform.childCount == 0){
                invImage = invSlot.GetComponent<Image>();
                invImage.color = Color.red;
            } else if (invSlot.transform.childCount > 0){
                invImage = invSlot.GetComponent<Image>();
                invImage.color = Color.green;
            }
        }
    }

    public void AdjustStatOnEquip(GameObject itemSlot)
    {
        g = itemSlot.GetComponentInChildren<InventoryItemData>();
        DataHandler.playerDefenseStat += g.itemDef;
        DataHandler.playerStrengthStat += g.itemStr;
        DataHandler.playerMaxHealthPoints += g.itemHealth;
        DataHandler.playerSpeedValue += g.itemSpeed;
        DataHandler.inventoryCreditMultiplier += g.cpsMulti;
        DataHandler.playerShipSpeed += g.shipSpeed;
        CheckIfEmpty();
    }

    public void SendEmBackCheck(GameObject itemSlot)
    {
        if(itemSlot.transform.childCount > 0)
        {
            foreach(Transform item in itemSlot.transform)
            {
                g = itemSlot.GetComponentInChildren<InventoryItemData>();
                DataHandler.playerDefenseStat -= g.itemDef;
                DataHandler.playerStrengthStat -= g.itemStr;
                item.transform.SetParent(inventoryParent.transform);
                CheckIfEmpty();
            }
        }
    }
}
