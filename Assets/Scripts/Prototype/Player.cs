using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player : MonoBehaviour
{
    [Header("References")]
        public Rigidbody2D myRigidbody2D;
        public Vector2 friction;
    [Header("Move and Jump")]
        public float speed;
        public float speedRun;
        public float jumpForce;
    [Header("Animation")] 
        public float jumpScaleY = 1.5f;
        public float jumpScaleX = 0.7f;
        public float animationDuration = 0.3f;
        public Ease ease;
    
        
    private float _currentSpeed;

    private void Awake()
    {
        jumpForce = 15f;
        speed = 5f;
        speedRun = 10f;
        friction = new Vector2(0.1f, 0);
        ease = Ease.OutBack;
    }

    private void Update()
    {
        PlayerJump();
        PlayerMovement();
    }

    private void PlayerMovement()
    {
       _currentSpeed = Input.GetKey(KeyCode.LeftControl) ? speedRun : speed;

        #region Input Keys

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                myRigidbody2D.velocity = new Vector2(-_currentSpeed,myRigidbody2D.velocity.y);
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                myRigidbody2D.velocity = new Vector2(_currentSpeed,myRigidbody2D.velocity.y);
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
            myRigidbody2D.velocity = Vector2.up * jumpForce;
            
            var rigidbody2DTransform = myRigidbody2D.transform;
            rigidbody2DTransform.localScale = Vector2.one;
            DOTween.Kill(rigidbody2DTransform);
            
            PlayerJumpScale();
        }
    }

    private void PlayerJumpScale()
    {
        /*fazer scale quando o player cai*/
        myRigidbody2D.transform.DOScaleY(jumpScaleY,animationDuration).SetLoops(2,
            LoopType.Yoyo).SetEase(ease);
        myRigidbody2D.transform.DOScaleX(jumpScaleX,animationDuration).SetLoops(2,
            LoopType.Yoyo).SetEase(ease);
    }
}
