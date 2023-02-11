using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableBasePlanets : CollectableBase
{
    protected override void OnCollect()
    {
        base.OnCollect();
        CollectableManager.Instance.AddPlanets();
    }
}
