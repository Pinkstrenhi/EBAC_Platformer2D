using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableBaseCoin : CollectableBase
{
    protected override void OnCollect()
    {
        base.OnCollect();
        CollectableManager.Instance.AddCoins();
    }
}
