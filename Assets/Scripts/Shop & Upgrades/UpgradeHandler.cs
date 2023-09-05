using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeHandler : MonoBehaviour, IDataPersistence
{
    public GameObject parentGO;
    public DictIdGenerator[] dictId;
    public Button[] b;
    public SliderController slider;

    public void Awake()
    {
        b = parentGO.GetComponentsInChildren<Button>(true);
        dictId = parentGO.GetComponentsInChildren<DictIdGenerator>(true);
    }

    public void Upgrade(int upgradeid)
    {
        switch (upgradeid)
        {
            case 0:
                if (DataHandler.creditAmount >= 50)
                {
                    DataHandler.creditAmount -= 50;
                    DataHandler.genBaseMultiplier[0] += 0.1f;
                    DisableButton(upgradeid);
                    SerializeGuid(upgradeid);
                }
                break;
            case 1:
                if (DataHandler.creditAmount >= 100)
                {
                    DataHandler.creditAmount -= 100;
                    slider.sliders[0].enabled = true;
                    DisableButton(upgradeid);
                    SerializeGuid(upgradeid);
                }
                break;
            case 2:
                if (DataHandler.creditAmount >= 5000)
                {
                    DataHandler.creditAmount -= 5000;
                    DataHandler.genBaseMultiplier[1] *= 2;
                    DisableButton(upgradeid);
                    SerializeGuid(upgradeid);
                }
                break;
            case 3:
                if (DataHandler.creditAmount >= 10000)
                {
                    DataHandler.creditAmount -= 10000;
                    DataHandler.genBaseMultiplier[2]  *= 2;
                    DisableButton(upgradeid);
                    SerializeGuid(upgradeid);
                }
                break;
            case 4:
                if (DataHandler.creditAmount >= 50000)
                {
                    DataHandler.creditAmount -= 50000;
                    DataHandler.genBaseMultiplier[1]  *= 5;
                    DisableButton(upgradeid);
                    SerializeGuid(upgradeid);
                }
            break;
            case 5:
                if (DataHandler.creditAmount >= 650000)
                {
                    DataHandler.creditAmount -= 650000;
                    DataHandler.genBaseMultiplier[0]  *= 8;
                    DisableButton(upgradeid);
                    SerializeGuid(upgradeid);
                }
            break;
            case 6:
                if (DataHandler.creditAmount >= 1000000)
                {
                    DataHandler.creditAmount -= 1000000;
                    slider.sliders[0].enabled = true;
                    DisableButton(upgradeid);
                    SerializeGuid(upgradeid);
                }
            break;
            case 7:
                if (DataHandler.creditAmount >= 1500000)
                {
                    DataHandler.creditAmount -= 1500000;
                    DataHandler.genBaseMultiplier[3]  *= 0.5f;
                    DisableButton(upgradeid);
                    SerializeGuid(upgradeid);
                }
            break;
        }
    }

    private void DisableButton(int x)
    {
        b[x].interactable = false;
    }

    private void SerializeGuid(int x)
    {
        dictId[x].isCollected = true;
    }

    public void SaveData(GameData data)
    {
        dictId = parentGO.GetComponentsInChildren<DictIdGenerator>(true);
        foreach(DictIdGenerator _dict in dictId){
            if (data.upgradesBought.ContainsKey(_dict.id)){
                data.upgradesBought.Remove(_dict.id);
            }
            data.upgradesBought.Add(_dict.id, _dict.isCollected);
        }

    }

    public void LoadData(GameData data)
    {
        foreach(DictIdGenerator _dict in dictId){
            var i = 0;
            data.upgradesBought.TryGetValue(_dict.id, out _dict.isCollected);
            if(dictId[i].isCollected)
            {
                b[i].interactable = false;
            }
            i++;
        }

        for(int i = 0; i < b.Length; i++){
            data.upgradesBought.TryGetValue(dictId[i].id, out dictId[i].isCollected);
            if(dictId[i].isCollected){
                b[i].interactable = false;
            }
        }

    }
}
