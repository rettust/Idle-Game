using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItemData : MonoBehaviour
{
    public string itemId;
    public int iLvl;
    public string itemNameInInventory;
    public float itemStr;
    public float itemDef;
    public float itemHealth;
    public float itemSpeed;
    public float cpsMulti;
    public float shipSpeed;
    public string equipToSlot;

    public void GenerateItemId()
    {
        itemId = System.Guid.NewGuid().ToString();
    }
}
