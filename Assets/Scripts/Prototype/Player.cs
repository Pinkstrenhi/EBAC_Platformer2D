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
        public Vector2 friction = new Vector2(0.1f, 0);
        public HealthBase healthBase;
    [Header("Move")]
        public SO_Float soSpeed;
        public SO_Float soSpeedRun;
    [Header("Jump")]
        public SO_Float soJumpForce;
        public SO_Float soJumpScaleX;
        public SO_Float soJumpScaleY;
    [Header("Fall")]
        public SO_Float soFallScaleX;
        public SO_Float soFallScaleY;
    [Header("Animation References")]
        public SO_Float soAnimationDurationJump;
        public SO_Float soAnimationDurationFall;
        public Ease ease = Ease.OutBack;
    [Header("Animation Player")] 
        public string boolRun = "PlayerRun";
        public string boolJump = "PlayerJump";
        public string triggerDeath = "PlayerDeath";
        public Animator animator;
        public float playerSwipeDuration = 0.1f;
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
        animator.SetTrigger(triggerDeath);
    }

    private void Update()
    {
        PlayerJump();
        PlayerFall();
        PlayerMovement();
    }

    private void PlayerMovement()
    {
       _currentSpeed = Input.GetKey(KeyCode.LeftShift) ? soSpeedRun.value : soSpeed.value;

        #region Input Keys

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                myRigidbody2D.velocity = new Vector2(-_currentSpeed,myRigidbody2D.velocity.y);
                if (myRigidbody2D.transform.localScale.x != -1)
                {
                    myRigidbody2D.transform.DOScaleX(-1f,playerSwipeDuration);
                }

                animator.SetBool(boolRun, true);
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                myRigidbody2D.velocity = new Vector2(_currentSpeed,myRigidbody2D.velocity.y);
                if (myRigidbody2D.transform.localScale.x != 1)
                {
                    myRigidbody2D.transform.DOScaleX(1f,playerSwipeDuration);
                }
                animator.SetBool(boolRun,true);
            }
            else
            {
                animator.SetBool(boolRun,false);
            }
            
        #endregion

        #region Friction

            switch (myRigidbody2D.velocity.x)
            {
                case < 0: 
                    myRigidbody2D.velocity += friction;
                    break; 
                case > 0: 
                    myRigidbody2D.velocity -= friction;
                    break;
            }

         #endregion
        
    }

    private void PlayerJump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            myRigidbody2D.velocity = Vector2.up * soJumpForce.value;
            
            var rigidbody2DTransform = myRigidbody2D.transform;
            rigidbody2DTransform.localScale = Vector2.one;
            DOTween.Kill(rigidbody2DTransform);
            
            animator.SetBool(boolJump,true);
            PlayerJumpScale();
        }
    }

    private void PlayerJumpScale()
    {
        myRigidbody2D.transform.DOScaleY(soJumpScaleY.value,soAnimationDurationJump.value).SetLoops(2,
            LoopType.Yoyo).SetEase(ease);
        myRigidbody2D.transform.DOScaleX(soJumpScaleX.value,soAnimationDurationJump.value).SetLoops(2,
            LoopType.Yoyo).SetEase(ease);
    }

    private void PlayerFall()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            myRigidbody2D.transform.DOScaleY(soFallScaleY.value,soAnimationDurationFall.value).SetLoops(2,
                LoopType.Yoyo).SetEase(ease);
            myRigidbody2D.transform.DOScaleX(soFallScaleX.value,soAnimationDurationFall.value).SetLoops(2,
                LoopType.Yoyo).SetEase(ease);
            animator.SetBool(boolJump,false);
        }
    }

    public void DestroyMe()
    {
        Destroy(gameObject);
    }
}
