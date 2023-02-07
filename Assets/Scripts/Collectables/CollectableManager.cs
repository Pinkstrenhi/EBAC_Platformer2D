using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Singleton;

public class CollectableManager : Singleton<CollectableManager>
{
    public int coins;
    private void Start()
    {
        Reset();
    }

    private void Reset()
    {
        coins = 0;
        UpdateUI();
    }
    public void AddCoins(int amount = 1)
    {
        coins += amount;
        UpdateUI();
    }

    private void UpdateUI()
    {
        UiGameManager.UpdateTextCoins("x " + coins.ToString());
    }
}
