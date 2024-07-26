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
    [Header("Input Value")]
    [SerializeField] float runSpeed = 1f;
    [SerializeField] float jumpForce = 2f;
    [SerializeField] float climbSpeed = 5f;
    private Rigidbody2D rigidBody;
    private CapsuleCollider2D capsuleCollider;
    private float gravityAtStart = 3f;
    private bool isClimbing;
    // private Animator playerAnimator;
    // private Vector2 moveInput;
    // private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        gravityAtStart = rigidBody.gravityScale;
        // playerAnimator = GetComponent<Animator>();
        // capsuleCollider = GetComponent<CapsuleCollider2D>();
        // gravityScaleAtStart = rigidBody.gravityScale;
        // spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        Move();
        Climb();
        // FlipSprite();
        // ClimbLadder();
    }

    private void Start()
    {
        InputManager.Instance.OnJump += HandleJump;
    }

    private void OnDisable()
    {
        if (InputManager.Instance != null)
        {
            InputManager.Instance.OnJump -= HandleJump;
        }
    }

    // void OnMove(InputValue value)
    // {
    //     moveInput = value.Get<Vector2>();
    // }

    // void OnJump(InputValue value)
    // {
    //     if (!capsuleCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
    //     {
    //         return;
    //     }

    //     if (value.isPressed)
    //     {
    //         rigidBody.velocity += new Vector2(rigidBody.velocity.x, jumpForce);
    //     }
    // }

    private void Move()
    {
        // bool playerHasHorizontalSpeed = Mathf.Abs(rigidBody.velocity.x) > Mathf.Epsilon;
        // Vector2 playerVelocity = new Vector2(moveInput.x * runSpeed, rigidBody.velocity.y);
        // rigidBody.velocity = playerVelocity;
        // playerAnimator.SetBool("isRunning", playerHasHorizontalSpeed);
        // float moveHorizontal = Input.GetAxis("Horizontal") * runSpeed * Time.deltaTime;
        // Vector2 position = new Vector2(moveHorizontal, 0);
        // transform.Translate(position);
        // Vector2 inputVector = InputManager.Instance.GetInputVectorNormalize();
        // rigidBody.velocity = new Vector2(inputVector.x * runSpeed, rigidBody.velocity.y);

        Vector2 inputVectorMove = InputManager.Instance.GetInputVectorMove();
        rigidBody.velocity = new Vector2(inputVectorMove.x * runSpeed, rigidBody.velocity.y);
    }

    private void Climb()
    {
        if (capsuleCollider.IsTouchingLayers(LayerMask.GetMask("Ladder")))
        {
            Vector2 inputVectorClimb = InputManager.Instance.GetInputVectorClimb();
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, inputVectorClimb.y * climbSpeed);
            rigidBody.gravityScale = 0f;
            isClimbing = true;
            if (InputManager.Instance.IsJumping())
            {
                rigidBody.gravityScale = gravityAtStart;
                rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpForce);
            }
        }

        else 
        {
            rigidBody.gravityScale = gravityAtStart;
            isClimbing = false;
        }        
    }

    private void HandleJump(object sender, EventArgs e)
    {
        // if (isClimbing)
        // {
        //     rigidBody.gravityScale = gravityAtStart;
        //     rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpForce);
        // }

        if (capsuleCollider.IsTouchingLayers(LayerMask.GetMask("Ground")) || capsuleCollider.IsTouchingLayers(LayerMask.GetMask("Mushroom")))
        {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpForce);
        }
    }

    // void FlipSprite()
    // {
    //     bool playerHasHorizontalSpeed = Mathf.Abs(rigidBody.velocity.x) > Mathf.Epsilon;

    //     if (playerHasHorizontalSpeed)
    //     {
    //         spriteRenderer.flipX = rigidBody.velocity.x < 0;
    //         // transform.localScale = new Vector2(Mathf.Sign(rb.velocity.x), 1f);
    //         playerAnimator.SetBool("isRunning", true);
    //     }

    //     else
    //     {
    //         playerAnimator.SetBool("isRunning", false);
    //     }
    // }

    // void ClimbLadder()
    // {
    // if (!capsuleCollider.IsTouchingLayers(LayerMask.GetMask("Climbing")))
    // {
    //     rigidBody.gravityScale = gravityScaleAtStart;
    //     playerAnimator.SetBool("isClimbing", false);
    //     return;
    // }

    // Check if the player is trying to jump
    // if (!isClimbing)
    // {
    // Vector2 jumpVelocity = new Vector2(rb.velocity.x, jumpForce);
    // rb.velocity = jumpVelocity;
    // return;
    // rb.velocity += new Vector2(rb.velocity.x, jumpForce);
    // }

    // Calculate climb velocity
    // Vector2 climbVelocity = new Vector2(rigidBody.velocity.x, moveInput.y * climbSpeed);
    // rigidBody.velocity = climbVelocity;

    // Disable gravity while climbing
    // rigidBody.gravityScale = 0f;

    // Update animator based on vertical speed
    // bool playerHasVerticalSpeed = Mathf.Abs(rigidBody.velocity.y) > Mathf.Epsilon;
    // playerAnimator.SetBool("isClimbing", playerHasVerticalSpeed);
    // }
}