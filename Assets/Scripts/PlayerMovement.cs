using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float runSpeed = 1f;
    [SerializeField] float jumpForce = 2f;
    [SerializeField] float climbSpeed = 5f;
    private Animator playerAnimator;
    private Rigidbody2D rigidBody;
    private Vector2 moveInput;
    private CapsuleCollider2D capsuleCollider;
    private float gravityScaleAtStart;
    private SpriteRenderer spriteRenderer;
    // bool isClimbing = false;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        gravityScaleAtStart = rigidBody.gravityScale;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        Run();
        FlipSprite();
        ClimbLadder();
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void OnJump(InputValue value)
    {
        if (!capsuleCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            return;
        }

        if (value.isPressed)
        {
            rigidBody.velocity += new Vector2(rigidBody.velocity.x, jumpForce);
        }
    }

    void Run()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(rigidBody.velocity.x) > Mathf.Epsilon;
        Vector2 playerVelocity = new Vector2(moveInput.x * runSpeed, rigidBody.velocity.y);
        rigidBody.velocity = playerVelocity;
        playerAnimator.SetBool("isRunning", playerHasHorizontalSpeed);
    }

    void FlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(rigidBody.velocity.x) > Mathf.Epsilon;

        if (playerHasHorizontalSpeed)
        {
            spriteRenderer.flipX = rigidBody.velocity.x < 0;
            // transform.localScale = new Vector2(Mathf.Sign(rb.velocity.x), 1f);
            playerAnimator.SetBool("isRunning", true);
        }

        else
        {
            playerAnimator.SetBool("isRunning", false);
        }
    }

    void ClimbLadder()
    {
        if (!capsuleCollider.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            rigidBody.gravityScale = gravityScaleAtStart;
            playerAnimator.SetBool("isClimbing", false);
            return;
        }

        // Check if the player is trying to jump
        // if (!isClimbing)
        // {
        // Vector2 jumpVelocity = new Vector2(rb.velocity.x, jumpForce);
        // rb.velocity = jumpVelocity;
        // return;
        // rb.velocity += new Vector2(rb.velocity.x, jumpForce);
        // }

        // Calculate climb velocity
        Vector2 climbVelocity = new Vector2(rigidBody.velocity.x, moveInput.y * climbSpeed);
        rigidBody.velocity = climbVelocity;

        // Disable gravity while climbing
        rigidBody.gravityScale = 0f;

        // Update animator based on vertical speed
        bool playerHasVerticalSpeed = Mathf.Abs(rigidBody.velocity.y) > Mathf.Epsilon;
        playerAnimator.SetBool("isClimbing", playerHasVerticalSpeed);
    }
}
