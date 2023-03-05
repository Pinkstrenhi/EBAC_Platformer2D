using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableBasePlanets : CollectableBase
{
    public Collider2D collider;
    protected override void OnCollect()
    {
        base.OnCollect();
        CollectableManager.Instance.AddPlanets();
        collider.enabled = false;
    }
}
