using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CurrencyHandler : MonoBehaviour
{
    [SerializeField] private GameObject[] genOverlayArr;
    [SerializeField] private TMP_Text[] genOverlayText;
    [SerializeField] private GameObject[] buy1Butt;
    [SerializeField] private GameObject[] buy10Butt;
    [SerializeField] private GameObject[] buy100Butt;
    [SerializeField] private GameObject[] buyMaxbutt;
    [SerializeField] private TooltipTrigger[] ttip1;
    [SerializeField] private TooltipTrigger[] ttip10;
    [SerializeField] private TooltipTrigger[] ttip100;
    [SerializeField] private TooltipTrigger[] ttipMax;
    private float[] multiCheck = { 1, 1, 1, 1, 1 };
    public GameObject creditScreen;
    private string[] genName;
    private int lastOverlay = 0;
    public float[] buyMultiGen;
    public float[] multiGen10Cost, multiGen100Cost;
    public float[] buyMaxGen;
    public float prestigePoints;

    void Start()
    {
        genName = new string[5] { "Shitters Clogged", "Sexy Chris", "Pickard", "Shoenice", "Abigail Shapiro, Tradwife" };
        buyMultiGen = new float[5];
        buyMaxGen = new float[5];
        multiCheck = new float[5];
        multiGen100Cost = new float[5];
        multiGen10Cost = new float[5];
        ttip1 = new TooltipTrigger[5];
        ttip10 = new TooltipTrigger[5];
        ttip100 = new TooltipTrigger[5];
        ttipMax = new TooltipTrigger[5];
        foreach(GameObject _overlay in genOverlayArr){
            _overlay.SetActive(false);
        }
        foreach(float _num in DataHandler.genNumCount){
            int i = 0;
            if(_num == 0){
                DataHandler.genFinalCost[i] = DataHandler.genCost[i];
                i++;
            }
        }
        FetchTheBoltCutters();
        InvokeRepeating("CalcMaxGen", 1, 1);
        InvokeRepeating("CalcMulti100", 1, 1);
        InvokeRepeating("CalcMulti10", 1, 1);
        InvokeRepeating("BuyGenButtonTooltipUpdate", 1, 1);
        InvokeRepeating("CalcFinalMultipliers", 1, 1);
    }
    void Update()
    {
        cpsGeneration();
        DataHandler.creditPerSecond = cpsSumArray();
        DataHandler.creditAmount += DataHandler.creditPerSecond * Time.deltaTime;
        for(int i = 0; i < genOverlayText.Length; i++){
            UpdateGenText(i);
            UpdateGenStats(i);
        }
    }

    public void GimmeMoney(float moneys)
    {
        DataHandler.creditAmount += moneys;
    }

// cps calculations are being handled here inside of update()
// functions: cpsGeneration(), cpsSumArray()
#region CPS Calcs
    public void cpsGeneration()
    {
        for(int i = 0; i < DataHandler.genCps.Length; i++)
        {
            DataHandler.genCps[i] = DataHandler.genBaseCps[i] * DataHandler.genNumCount[i] * DataHandler.genFinalMultiplier[i];
        }
    }

    public float cpsSumArray()
    {
        var cpsArrSum = 0f;
        foreach (float i in DataHandler.genCps)
        {
            cpsArrSum += i;
        }
        return cpsArrSum;
    }
#endregion CPS Calcs

// handles updating prices, text, etc.
// functions: UpdateGenStats(x), CalcFinalMultipliers(), UpdateGenText(x), UpdateGenOverlay(x)
#region Gen Stats

    // calculates the multiplier for (# gens owned / 25), final cost, and final multipliers
    public void UpdateGenStats(int x)
    {
        multiCheck[x] = 1 + Mathf.FloorToInt(DataHandler.genNumCount[x] / 25);
        DataHandler.genFinalCost[x] = DataHandler.genCost[x] * Mathf.Pow(DataHandler.genCostIncreaseRate[x], DataHandler.genNumCount[x]);
        CalcFinalMultipliers();
    }

    // append any additional upgrades or multipliers that will effect generator multis here
    public void CalcFinalMultipliers()
    {
        for(int i = 0; i < DataHandler.genNumCount.Length; i++){
            DataHandler.genFinalMultiplier[i] = DataHandler.genBaseMultiplier[i] * multiCheck[i];
        }
    }

    // does what it says on the tin
    public void UpdateGenText(int x)
    {
        genOverlayText[x].text = ("You currently own " + DataHandler.genNumCount[x] + " " + genName[x] + "." +
                                 "\nThey are generating: " + DataHandler.genCps[x] + " credits per second." +
                                 "\nTheir hard work is being multiplied by " + DataHandler.genFinalMultiplier[x] + "." +
                                 "\nThe next " + genName[x] + " will cost " + DataHandler.genFinalCost[x].ToString("0.00") + ".");
    }

    // moves screens around (disable/enable overlay GOs)
    public void UpdateGenOverlay(int x)
    {
        genOverlayArr[lastOverlay].SetActive(false);
        lastOverlay = x;
        genOverlayArr[x].SetActive(true);
    }
#endregion Gen Stats

//logic for buying generators from 1-n
// functions: Buy1Gen(x), BuyMultiGen(x, y)
#region Generator Buyers
    // no real reason for this, could just pass x=1 to the function after, but w/e
    public void Buy1Gen(int x)
    {
        DataHandler.genNumCount[x] += 1;
        DataHandler.creditAmount -= DataHandler.genFinalCost[x];
        UpdateGenStats(x);
    }

    // x = amount of generators to buy, y is the gen[y] being referenced/calculated for
    public void BuyMultiGen(int x, int y)
    {
        // formula for multibuy: basecost * (increaserate^numgen * (increaserate^x - 1) / (increaserate - 1))
        for(int i = 0; i < buyMultiGen.Length; i++){
            buyMultiGen[i] = DataHandler.genCost[i] * ((Mathf.Pow(DataHandler.genCostIncreaseRate[i], DataHandler.genNumCount[i]) *
                                                    (Mathf.Pow(DataHandler.genCostIncreaseRate[i], x) - 1)) /
                                                    (DataHandler.genCostIncreaseRate[i] - 1));
        }

        if(DataHandler.creditAmount >= buyMultiGen[y]){
            DataHandler.genNumCount[y] += x;
            DataHandler.creditAmount -= buyMultiGen[y];
            UpdateGenStats(y);
        }

    }
#endregion Generator Buyers

    // calculates the total NUMBER of generators you can buy based on current credits, passes that int to buyMaxGen array, and that number just gets passed to
    // BuyMultiGen array to get purchased
    //functions: CalcMaxGen, CalcMulti10, CalcMulti100, FetchTheBoltCutters, BuyButtonTooltipUpdate
#region Multigen Calcs
    public void CalcMaxGen()
    {
        // formula for maxbuy: floor(log_increaserate((currentCredit(increaserate-1) / base(increaserate^numgen)) + 1))
        for(int i = 0; i < buyMaxGen.Length; i++){
            buyMaxGen[i] = Mathf.Floor(Mathf.Log((((DataHandler.creditAmount * (DataHandler.genCostIncreaseRate[i] - 1))) /
                                                 (DataHandler.genCost[i] * (Mathf.Pow(DataHandler.genCostIncreaseRate[i], DataHandler.genNumCount[i])))+1),
                                                  DataHandler.genCostIncreaseRate[i]));
        }
    }

    // calc'd purely to display on tooltip
    public void CalcMulti10(){
        // formula for multibuy: basecost * (increaserate^numgen * (increaserate^x - 1) / (increaserate - 1))
        for(int i = 0; i < multiGen10Cost.Length; i++){
            multiGen10Cost[i] = DataHandler.genCost[i] * ((Mathf.Pow(DataHandler.genCostIncreaseRate[i], DataHandler.genNumCount[i]) *
                                                    (Mathf.Pow(DataHandler.genCostIncreaseRate[i], 10) - 1)) /
                                                    (DataHandler.genCostIncreaseRate[i] - 1));
        }
    }

    // calc'd purely to display on tooltip
        public void CalcMulti100(){
        // formula for multibuy: basecost * (increaserate^numgen * (increaserate^x - 1) / (increaserate - 1))
        for(int i = 0; i < multiGen100Cost.Length; i++){
            multiGen100Cost[i] = DataHandler.genCost[i] * ((Mathf.Pow(DataHandler.genCostIncreaseRate[i], DataHandler.genNumCount[i]) *
                                                    (Mathf.Pow(DataHandler.genCostIncreaseRate[i], 100) - 1)) /
                                                    (DataHandler.genCostIncreaseRate[i] - 1));
        }
    }

    // pulling the tooltip trigger components from each button (1x, 10x, 100x, max)
    public void FetchTheBoltCutters(){
        for(int i = 0; i < ttip1.Length; i++){
            ttip1[i] = buy1Butt[i].GetComponent<TooltipTrigger>();
            ttip10[i] = buy10Butt[i].GetComponent<TooltipTrigger>();
            ttip100[i] = buy100Butt[i].GetComponent<TooltipTrigger>();
            ttipMax[i] = buyMaxbutt[i].GetComponent<TooltipTrigger>();
        }
    }

    // generates the tooltips (components pulled in FetchTheBoltCutters()) and populates them with OnMouseOver strings passed along to TooltipTrigger class
    public void BuyGenButtonTooltipUpdate()
    {
        for(int i = 0; i < ttip1.Length; i++){
            ttip1[i].content = "this will cost u: " + DataHandler.genFinalCost[i];
            ttip1[i].header = "buy ya a single";
            ttip10[i].content = "this will cost u: " + multiGen10Cost[i];
            ttip10[i].header = "10, thats almost a dozen";
            ttip100[i].content = "this will cost u: " + multiGen100Cost[i];
            ttip100[i].header = "one HUNDRED";
            ttipMax[i].content = "u can afford to buy:  " + buyMaxGen[i];
            ttipMax[i].header = "spend ALL creddies";
        }
    }
#endregion Multigen Calcs
}
