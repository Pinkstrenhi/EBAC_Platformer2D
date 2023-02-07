using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Core.Singleton;

public class UiGameManager : Singleton<UiGameManager>
{
    public TextMeshProUGUI uiTextCoins;

    public static void UpdateTextCoins(string coins)
    {
        Instance.uiTextCoins.text = coins;
    }
}
