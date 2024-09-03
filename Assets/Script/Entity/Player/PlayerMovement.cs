using System;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 10f;
    private bool isGround = true;

    private PlayerInput playerInput;
    private Animator animator;
    private Rigidbody2D playerRb;
    
    public float MoveSpeed
    {
        get { return moveSpeed; }
        set { moveSpeed = value; }
    }
    
    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        animator = GetComponent<Animator>();
        playerRb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (playerInput.Axis != 0f)
        {
            playerRb.velocity = 
                new Vector2(playerInput.Axis * moveSpeed, playerRb.velocity.y);
            animator.SetBool("walk", true);
        }
        else animator.SetBool("walk", false);

        if (playerInput.Jump && isGround)
        {
            playerRb.velocity = Vector2.zero;
            playerRb.AddForce(Vector2.up * 700);
            animator.SetBool("jump", playerInput.Jump);
            AudioManager.Instance.playJump();
            isGround = false;
        }
        else animator.SetBool("jump", playerInput.Jump);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Obstacle")
        {
            playerRb.velocity = Vector2.zero;
        }
        if(collision.collider.tag == "Ground")
        {
            isGround = true;
        }
    }
}
