using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextHandler : MonoBehaviour
{
    public TMP_Text creditPerSecText;
    public TMP_Text planetText;
    public TMP_Text credText;
    public TMP_Text statText;

        void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        credText.text = "creddies: " + DataHandler.creditAmount.ToString("0");
        creditPerSecText.text = "you are generating " + DataHandler.creditPerSecond.ToString("F2") + " credits per second";
        statText.text = "max hp: " + DataHandler.playerMaxHealthPoints + "\nstrength: " + DataHandler.playerStrengthStat +
                        "\ndefense: " + DataHandler.playerDefenseStat + "\nshpeed: " + DataHandler.playerSpeedValue +
                        "\ncreddy multipliey: " + DataHandler.inventoryCreditMultiplier + "\nadd'l shipspeed: " + DataHandler.playerShipSpeed;
    }
}
