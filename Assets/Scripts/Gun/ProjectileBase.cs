using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBase : MonoBehaviour
{
    public Vector3 direction;
    public float timeToDestroy = 1f;
    public float side = 1f;
    public int damageAmount = 1;
    private void Awake()
    {
        Destroy(gameObject,timeToDestroy);
    }

    private void Update()
    {
        transform.Translate(direction * (Time.deltaTime * side));
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        var enemy = other.transform.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.Damage(damageAmount);
            Destroy(gameObject);
        }
    }
}
