using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Animations;

public class Player : MonoBehaviour
{
    [Header("Set Up")]
        public Rigidbody2D myRigidbody2D;
        public HealthBase healthBase;
        public Animator animator;
        public SO_Player soPlayer;
    private float _currentSpeed;

    private void Awake()
    {
        if (healthBase != null)
        {
            healthBase.OnKill += OnPLayerKill;
        }
    }

    private void OnPLayerKill()
    {
        healthBase.OnKill -= OnPLayerKill; 
        animator.SetTrigger(soPlayer.triggerDeath);
    }

    private void Update()
    {
        PlayerJump();
        PlayerFall();
        PlayerMovement();
    }

    private void PlayerMovement()
    {
       _currentSpeed = Input.GetKey(KeyCode.LeftShift) ? soPlayer.soSpeedRun.value : soPlayer.soSpeed.value;

        #region Input Keys

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                myRigidbody2D.velocity = new Vector2(-_currentSpeed,myRigidbody2D.velocity.y);
                if (myRigidbody2D.transform.localScale.x != -1)
                {
                    myRigidbody2D.transform.DOScaleX(-1f,soPlayer.playerSwipeDuration);
                }

                animator.SetBool(soPlayer.boolRun, true);
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                myRigidbody2D.velocity = new Vector2(_currentSpeed,myRigidbody2D.velocity.y);
                if (myRigidbody2D.transform.localScale.x != 1)
                {
                    myRigidbody2D.transform.DOScaleX(1f,soPlayer.playerSwipeDuration);
                }
                animator.SetBool(soPlayer.boolRun,true);
            }
            else
            {
                animator.SetBool(soPlayer.boolRun,false);
            }
            
        #endregion

        #region Friction

            switch (myRigidbody2D.velocity.x)
            {
                case < 0: 
                    myRigidbody2D.velocity += soPlayer.friction;
                    break; 
                case > 0: 
                    myRigidbody2D.velocity -= soPlayer.friction;
                    break;
            }

         #endregion
        
    }

    private void PlayerJump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            myRigidbody2D.velocity = Vector2.up * soPlayer.soJumpForce.value;
            
            var rigidbody2DTransform = myRigidbody2D.transform;
            rigidbody2DTransform.localScale = Vector2.one;
            DOTween.Kill(rigidbody2DTransform);
            
            animator.SetBool(soPlayer.boolJump,true);
            PlayerJumpScale();
        }
    }

    private void PlayerJumpScale()
    {
        myRigidbody2D.transform.DOScaleY(soPlayer.soJumpScaleY.value,
            soPlayer.soAnimationDurationJump.value).SetLoops(2,
            LoopType.Yoyo).SetEase(soPlayer.ease);
        myRigidbody2D.transform.DOScaleX(soPlayer.soJumpScaleX.value,
            soPlayer.soAnimationDurationJump.value).SetLoops(2,
            LoopType.Yoyo).SetEase(soPlayer.ease);
    }

    private void PlayerFall()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            myRigidbody2D.transform.DOScaleY(soPlayer.soFallScaleY.value,
                soPlayer.soAnimationDurationFall.value).SetLoops(2,
                LoopType.Yoyo).SetEase(soPlayer.ease);
            myRigidbody2D.transform.DOScaleX(soPlayer.soFallScaleX.value,
                soPlayer.soAnimationDurationFall.value).SetLoops(2,
                LoopType.Yoyo).SetEase(soPlayer.ease);
            animator.SetBool(soPlayer.boolJump,false);
        }
    }

    public void DestroyMe()
    {
        Destroy(gameObject);
    }
}
