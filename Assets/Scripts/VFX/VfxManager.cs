using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Singleton;

public class VfxManager : Singleton<VfxManager>
{
    public float timeToDestroy = 5f;
    public enum VfxType
    {
        Jump
    }

    public List<VfxManagerSetup> vfxSetup;

    public void PlayVfxByType(VfxType vfxType, Vector3 position)
    {
        foreach (var i in vfxSetup)
        {
            if (i.vfxType == vfxType)
            {
                var item = Instantiate(i.prefab);
                item.transform.position = position;
                Destroy(item.gameObject,timeToDestroy);
                break;
            }
        }
    }
}
[System.Serializable]
public class VfxManagerSetup
{
    public VfxManager.VfxType vfxType;
    public GameObject prefab;
}