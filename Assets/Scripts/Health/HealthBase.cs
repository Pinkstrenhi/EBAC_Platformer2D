using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class HealthBase : MonoBehaviour
{
    public Action OnKill;
    public int startLife;
    public bool destroyOnKill;
    public float delayToKill;
    private int _currentLife;
    private bool _isDead;
    [SerializeField] private FlashColor _flashColor;
    private void Awake()
    {
        Init();
        if (_flashColor == null)
        {
            _flashColor = GetComponent<FlashColor>();
        }
    }

    private void Init()
    {
        startLife = 10;
        delayToKill = 1f;
        _isDead = false;
        _currentLife = startLife;
    }
    public void Damage(int damage)
    {
        if (_isDead)
        {
            return;
        }
        _currentLife -= damage;
        if (_currentLife <= 0)
        {
            Kill();
        }

        if (_flashColor != null)
        {
            _flashColor.Flash();
        }
    }

    private void Kill()
    {
        _isDead = true;
        if (destroyOnKill)
        {
            Destroy(gameObject,delayToKill);
        }
        OnKill?.Invoke();
    }
}
