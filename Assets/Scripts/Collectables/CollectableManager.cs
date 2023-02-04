using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Singleton;
using TMPro;

public class CollectableManager : Singleton<CollectableManager>
{
    public int coins;
    public TextMeshProUGUI coinsInHud;
    private void Start()
    {
        Reset();
    }

    private void Reset()
    {
        coins = 0;
    }

    private void Update()
    {
        coinsInHud.text = "x " + coins.ToString();
    }

    public void AddCoins(int amount = 1)
    {
        coins += amount;
    }
}
