using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;

using UnityEngine;

public class Player : MonoBehaviour
{
    public float movementForce = 5f;
    public float maxMovementSpeed = 5f;
    public float jumpForce = 4f;
    public float groundRadius = 0.21f ;
    public float Timer = 1f;
    public float maxForce = 10f;
    public Transform groundDetect;
    public Transform wallBounce;
    public LayerMask groundLayerMask;
    public Vector2 size = new Vector2(0.5f, 0.5f); 

    private float movementDirection;
    private bool canJump = false;
    private bool canGround = true;
    private bool canBounce = false;
    private bool isOntheRight = true;
    private bool isHolding = false;
    private bool isWalking;
    private bool isJumping = false;
    private Rigidbody2D rb;
    //t
    private static Player instance;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {

        if (DialogueManager.GetInstance().dialogueIsPlaying)

        {
            movementDirection = 0;
            return;
        }
        movementDirection = Input.GetAxisRaw("Horizontal");
        canGround = Physics2D.OverlapCircle(groundDetect.position, groundRadius , groundLayerMask);
        if (rb.velocity.y > 0)
        {
            canBounce = Physics2D.OverlapBox(wallBounce.position, new Vector2(0.2f, 0.66f), 0f, groundLayerMask);
        }
        else
        {
            canBounce = false;
        }
        if (canBounce)
        {
            rb.velocity = new Vector2(isOntheRight ? -movementForce : movementForce, rb.velocity.y);
        }
        if (canGround && isHolding == false)
        {
            rb.velocity = new Vector2(movementForce * movementDirection, rb.velocity.y);
            isWalking = rb.velocity.x != Vector2.zero.x;
        }
        if (isOntheRight && movementDirection < 0 && canGround)
        { 
            Flip();
        }
        else if(!isOntheRight && movementDirection > 0 && canGround)
        {
            Flip();
        }
        
        if (Input.GetButtonDown("Jump") && canGround)
        {
            isHolding = true;
            rb.velocity = new Vector2(0, 0);
            if (canGround) canJump = true;
        }
        if (Input.GetButton("Jump") && isHolding)
        {
            if (Timer*jumpForce <= maxForce) 
            {
                Timer += Time.deltaTime + Time.deltaTime/2;
            }
            else
            {
                canJump = false;
                jump();
            }           
        }
        if (Input.GetButtonUp("Jump") && isHolding)
        {
            if (canGround && canJump)
            {
                jump();
            }
        }
        
        UnityEngine.Debug.Log(canBounce);
        //UnityEngine.Debug.Log(rb.velocity);
    }

    private void jump()
    {
        if(canGround)
        {
            rb.velocity = new Vector2(movementDirection * movementForce, Timer * jumpForce * 1.75f);
            isHolding = false;
            Timer = 1f;
        }
    } 

    public void FixedUpdate()
    {
        //t


        if (canGround) return;

        if (rb.velocity.y < 0)
        {           
            rb.velocity += Vector2.up * Physics2D.gravity.y * (jumpForce - 2.5f) * Time.deltaTime;       
        }
        else if (rb.velocity.y > 0 && !isHolding)
        {
            isWalking = false;
            rb.velocity += Vector2.up * Physics2D.gravity.y * (jumpForce - 2.5f) * Time.deltaTime;           
        }
    }
  
    private void Flip()
    {
        isOntheRight = !isOntheRight;
        transform.Rotate(0f, 180f, 0f);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        movementDirection = Input.GetAxisRaw("Horizontal");
    }
    public bool IsWalking()
    {
        return isWalking;
    }
    public bool IsHoding()
    {
        return isHolding;
    }

}
