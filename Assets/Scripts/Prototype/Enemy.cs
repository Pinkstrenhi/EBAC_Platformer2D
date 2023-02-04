using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int damage = 10;
    public Animator animator;
    public string triggerAttack = "Attack";
    public HealthBase healthBase;
    private void OnCollisionEnter2D(Collision2D other)
    {
        //Debug.Log(other.transform.name);
        var health = other.gameObject.GetComponent<HealthBase>();
        if (health != null)
        {
            health.Damage(damage);
            PlayerAttackAnimation();
        }
    }

    private void PlayerAttackAnimation()
    {
        animator.SetTrigger(triggerAttack);
    }

    public void Damage(int amount)
    {
        healthBase.Damage(amount);
    }
}
