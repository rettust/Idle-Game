using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderController : MonoBehaviour
{
    public Slider[] sliders = new Slider[5];
    private float[] timer = new float[5];
    public float[] secondsToFill = new float[] { 30, 3000, 3000, 6000, 12000 };
    public float[] amountGenAwarded = new float[] { 1, 1, 1, 1, 1 };
    public float[] sliderTimeMultiplier = new float[] { 1, 1, 1, 1, 1 };
    public float[] barSpeedCost = new float[] { 100, 1000, 10000, 100000, 1000000 };
    public float[] barGenUpgradeCost = new float[] { 100, 1000, 10000, 50000, 500000 };
    void Start()
    {
        //wtf
        for(int i = 0; i < sliders.Length; i++){
            sliders[i].enabled = false;
            sliders[i].maxValue = secondsToFill[i];
        }
    }

    void Update()
    {
        for (int i = 0; i < sliders.Length; i++)
        {
            if (sliders[i].enabled == true){
                BarFill(i);
            } else {
                sliders[i].value = 0;
            }
        }
    }

    void BarFill(int x)
    {
        timer[x] += Time.deltaTime * sliderTimeMultiplier[x];
        sliders[x].value = timer[x];

        if (sliders[x].value == sliders[x].maxValue){
            for(int i = 0; i < amountGenAwarded.Length; i++){
                if(sliders[i].enabled){
                    DataHandler.genNumCount[i] += amountGenAwarded[i];
                }

            }
            sliders[x].value = 0f;
            timer[x] = 0f;
        }
    }

    public void UpgradeSlider(string whichUpgrade)
    {
        switch(whichUpgrade)
        {
            case "bar1Speed":
                if(DataHandler.creditAmount >= barSpeedCost[0])
                {
                    DataHandler.creditAmount -= barSpeedCost[0];
                    barSpeedCost[0] *= 1.75f;
                    sliderTimeMultiplier[0] *= 1.1f;
                }
            break;
            case "bar1Credit":
                if(DataHandler.creditAmount >= barGenUpgradeCost[0])
                {
                    DataHandler.creditAmount -= barGenUpgradeCost[0];
                    amountGenAwarded[0] += 1;
                    barGenUpgradeCost[0] *= 10;
                }
            break;
            case "bar2Speed":
                if(DataHandler.creditAmount >= barSpeedCost[1])
                {
                    DataHandler.creditAmount -= barSpeedCost[1];
                    barSpeedCost[1] *= 1.75f;
                    sliderTimeMultiplier[1] *= 1.1f;
                }
            break;
            case "bar2Credit":
                if(DataHandler.creditAmount >= barGenUpgradeCost[1])
                {
                    DataHandler.creditAmount -= barGenUpgradeCost[1];
                    amountGenAwarded[1] += 1;
                    barGenUpgradeCost[1] *= 10;
                }
            break;
            case "bar3Speed":
                if(DataHandler.creditAmount >= barSpeedCost[2])
                {
                    DataHandler.creditAmount -= barSpeedCost[2];
                    barSpeedCost[2] *= 1.75f;
                    sliderTimeMultiplier[2] *= 1.1f;
                }
            break;
            case "bar3Credit":
                if(DataHandler.creditAmount >= barGenUpgradeCost[2])
                {
                    DataHandler.creditAmount -= barGenUpgradeCost[2];
                    amountGenAwarded[2] += 1;
                    barGenUpgradeCost[2] *= 10;
                }
            break;
            case "bar4Speed":
                if(DataHandler.creditAmount >= barSpeedCost[3])
                {
                    DataHandler.creditAmount -= barSpeedCost[3];
                    barSpeedCost[3] *= 1.75f;
                    sliderTimeMultiplier[3] *= 1.1f;
                }
            break;
            case "bar4Credit":
                if(DataHandler.creditAmount >= barGenUpgradeCost[3])
                {
                    DataHandler.creditAmount -= barGenUpgradeCost[3];
                    amountGenAwarded[3] += 1;
                    barGenUpgradeCost[3] *= 10;
                }
            break;
            case "bar5Speed":
                if(DataHandler.creditAmount >= barSpeedCost[4])
                {
                    DataHandler.creditAmount -= barSpeedCost[4];
                    barSpeedCost[4] *= 1.75f;
                    sliderTimeMultiplier[4] *= 1.1f;
                }
            break;
            case "bar5Credit":
                if(DataHandler.creditAmount >= barGenUpgradeCost[4])
                {
                    DataHandler.creditAmount -= barGenUpgradeCost[4];
                    amountGenAwarded[4] += 1;
                    barGenUpgradeCost[4] *= 10;
                }
            break;
        }
    }
}
