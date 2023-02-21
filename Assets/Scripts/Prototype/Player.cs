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
        //public Animator animator;
        public SO_Player soPlayer;
    [Header("Jump Collision Check")] 
        public Collider2D collider2D;
        public float distanceToGround;
        public float spaceToGround = 0.1f;
        public ParticleSystem jumpVFX;
    
    
    private float _currentSpeed;
    private Animator _currentPlayer;

    private void Awake()
    {
        if (healthBase != null)
        {
            healthBase.OnKill += OnPayerKill;
        }

        if (collider2D != null)
        {
            distanceToGround = collider2D.bounds.extents.y;
        }
        _currentPlayer = Instantiate(soPlayer.player,transform);
        _currentPlayer.GetComponentInChildren<GunBase>().playerSideReference = transform;
        _currentPlayer.GetComponentInChildren<PlayerDestroyHelper>().player = this;
    }

    private bool IsGrounded()
    {
        Debug.DrawRay(transform.position,-Vector2.up, Color.cyan,
            distanceToGround + spaceToGround);
        return Physics2D.Raycast(transform.position,-Vector2.up,
            distanceToGround + spaceToGround);
    }
    private void OnPayerKill()
    {
        healthBase.OnKill -= OnPayerKill; 
        _currentPlayer.SetTrigger(soPlayer.triggerDeath);
    }

    private void Update()
    {
        IsGrounded();
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

                _currentPlayer.SetBool(soPlayer.boolRun, true);
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                myRigidbody2D.velocity = new Vector2(_currentSpeed,myRigidbody2D.velocity.y);
                if (myRigidbody2D.transform.localScale.x != 1)
                {
                    myRigidbody2D.transform.DOScaleX(1f,soPlayer.playerSwipeDuration);
                }
                _currentPlayer.SetBool(soPlayer.boolRun,true);
            }
            else
            {
                _currentPlayer.SetBool(soPlayer.boolRun,false);
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
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            myRigidbody2D.velocity = Vector2.up * soPlayer.soJumpForce.value;
            
            var rigidbody2DTransform = myRigidbody2D.transform;
            rigidbody2DTransform.localScale = Vector2.one;
            DOTween.Kill(rigidbody2DTransform);
            
            _currentPlayer.SetBool(soPlayer.boolJump,true);
            PlayerJumpScale();
            PlayJumpVFX();
        }
    }

    private void PlayJumpVFX()
    {
        if (jumpVFX != null)
        {
            jumpVFX.Play();
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
            _currentPlayer.SetBool(soPlayer.boolJump,false);
        }
    }

    public void DestroyMe()
    {
        Destroy(gameObject);
    }
}
