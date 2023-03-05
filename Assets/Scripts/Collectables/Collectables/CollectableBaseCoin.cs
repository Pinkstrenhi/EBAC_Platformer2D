using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableBaseCoin : CollectableBase
{
    public Collider2D collider;
    protected override void OnCollect()
    {
        base.OnCollect();
        CollectableManager.Instance.AddCoins();
        collider.enabled = false;
    }
}
