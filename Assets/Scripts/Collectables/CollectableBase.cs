using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableBase : MonoBehaviour
{
    public string compareTag = "Player";
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.CompareTag(compareTag))
        {
            Collect();
        }
    }

    protected virtual void Collect()
    {
       //Debug.Log("Collect");
       gameObject.SetActive(false);
       OnCollect();
    }
    protected virtual void OnCollect()
    {
        
    }
}
