using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Core.Singleton;

public class UiGameManager : Singleton<UiGameManager>
{
    public TextMeshProUGUI uiTextCoins;
    public TextMeshProUGUI uiTextPlanets;

    public static void UpdateTextCollectables(string coins,string planets)
    {
        Instance.uiTextCoins.text = coins;
        Instance.uiTextPlanets.text = planets;
    }
}
