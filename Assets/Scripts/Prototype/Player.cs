using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody2D myRigidbody2D;
    public Vector2 friction;
    public float speed;
    public float speedRun;
    public float jumpForce;

    private float _currentSpeed;

    private void Awake()
    {
        jumpForce = 15f;
        speed = 5f;
        speedRun = 15f;
        friction = new Vector2(0.1f, 0);
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
                    myRigidbody2D.velocity -= friction;
                    break; 
                case > 0: 
                    myRigidbody2D.velocity += friction;
                    break;
            }

         #endregion
        
    }

    private void PlayerJump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            myRigidbody2D.velocity = Vector2.up * jumpForce;
        }
    }
}
