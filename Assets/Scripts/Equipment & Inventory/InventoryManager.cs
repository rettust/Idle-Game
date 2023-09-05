using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class InventoryManager : MonoBehaviour, IDataPersistence
{
   public GameObject prefab;
   public GameObject parentGO;
   public TMP_Text itemText;
   public LootTable lootTable;
   public InventoryItemData invData;
   [HideInInspector] public string itemName;
   public GameObject buttonMan;
   private ButtonManager butt;



   public void Start()
   {
      butt = buttonMan.GetComponent<ButtonManager>();
   }

   public void AddNewItem()
   {
      ItemData item = lootTable.GetRandomItem();
      SetMods(item, "");
      ApplyModValues(item, "");
      setItemText(item);
      GameObject clone = Instantiate(prefab, parentGO.transform);
      clone.SetActive(true);
      clone.name = invData.itemId;
      Button button = clone.GetComponent<Button>();
      SetItemTooltip(item, clone);
      InventoryItemData item2 = clone.GetComponentInChildren<InventoryItemData>();
      button.onClick.AddListener(delegate{butt.EquipItem(item2.itemId, item2.equipToSlot);});

   }

   public void setItemText(ItemData item)
   {
      itemText.text = item.itemName;
   }

   public void SetItemTooltip(ItemData item, GameObject clone)
   {
      var sb = new System.Text.StringBuilder();
      for(int i = 0; i < item.attributeMods.Count; i++){
         sb.AppendLine(item.attributeMods[i] + ": +" + item.attributeValues[i]);
      }
      TooltipTrigger tooltip = clone.AddComponent<TooltipTrigger>();
      tooltip.header = item.itemName;
      tooltip.content = sb.ToString();
   }

   public void SetMods(ItemData item, string mod)
   {
      invData.GenerateItemId();
      invData.itemNameInInventory = item.itemName;
      invData.equipToSlot = item.itemType;
      invData.iLvl = item.iLvl;
      item.attributeMods.Clear();
      item.attributeValues.Clear();
      foreach(Attributes _attribute in item.attributes){
         mod = _attribute.name;
         item.attributeValues.Add(_attribute.value);
         item.attributeMods.Add(_attribute.name);
      }
   }

   public void ApplyModValues(ItemData item, string mod){
      foreach(string _attribute in item.attributeMods){
         mod = _attribute;
         var i = 0;
         switch(mod){
            case "Health":
               invData.itemHealth = item.attributeValues[i];
               break;
            case "Strength":
               invData.itemStr = item.attributeValues[i];
               break;
            case "Defense":
               invData.itemDef = item.attributeValues[i];
               break;
            case "Speed":
               invData.itemSpeed = item.attributeValues[i];
               break;
            case "Credit Production":
               invData.cpsMulti = item.attributeValues[i];
               break;
            case "Ship Speed":
               invData.shipSpeed = item.attributeValues[i];
               break;
         }
         i++;
      }
   }


    public void LoadData(GameData data)
    {

    }

    public void SaveData(GameData data)
    {

    }
}

