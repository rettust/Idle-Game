using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigationHandler : MonoBehaviour
{
    public GameObject CreditScreen;
    public GameObject ShopScreen;
    public GameObject BarScreen;
    public GameObject EquipScreen;
    public GameObject MapScreen;
    public GameObject CombatScreen;
    public GameObject PrestigeScreen;
    public void Navigate(string location)
    {
        CreditScreen.SetActive(false);
        ShopScreen.SetActive(false);
        BarScreen.SetActive(false);
        EquipScreen.SetActive(false);
        MapScreen.SetActive(false);
        CombatScreen.SetActive(false);
        PrestigeScreen.SetActive(false);

        switch (location)
        {
            case "Credit":
                CreditScreen.SetActive(true);
                break;
            case "Shop":
                ShopScreen.SetActive(true);
                break;
            case "Bars":
                BarScreen.SetActive(true);
                break;
            case "Equip":
                EquipScreen.SetActive(true);
                break;
            case "Map":
                MapScreen.SetActive(true);
                break;
            case "Combat":
                CombatScreen.SetActive(true);
                break;
            case "Prestige":
                PrestigeScreen.SetActive(true);
                break;
        }
    }
}
