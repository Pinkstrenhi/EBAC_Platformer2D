using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBase : MonoBehaviour
{
    public ProjectileBase projectileBase;
    public Transform positionToShoot;
    public float timeBetweenShoot = 0.2f;
    public Transform playerSideReference;
    private Coroutine _currentCoroutine;

    private void Awake()
    {
        playerSideReference = GameObject.FindObjectOfType<Player>().transform;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            _currentCoroutine = StartCoroutine(nameof(StartShoot));
        }
        else if(Input.GetKeyUp(KeyCode.S))
        {
            if (_currentCoroutine != null)
            {
                StopCoroutine(nameof(StartShoot));
            }
        }
    }

    IEnumerator StartShoot()
    {
        while (true)
        {
           Shoot();
           yield return new WaitForSeconds(timeBetweenShoot);
        }
    }
    private void Shoot()
    {
        var projectile = Instantiate(projectileBase);
        projectile.transform.position = positionToShoot.position;
        projectile.side = playerSideReference.transform.localScale.x;
    }
}
