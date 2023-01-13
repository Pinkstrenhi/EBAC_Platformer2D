using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int damage;

    private void Awake()
    {
        damage = 10;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log(other.transform.name);
        var health = other.gameObject.GetComponent<HealthBase>();
        if (health != null)
        {
            health.Damage(damage);
        }
    }
}
