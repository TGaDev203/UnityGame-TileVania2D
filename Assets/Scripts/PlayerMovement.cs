using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class PlayerMovement : MonoBehaviour
{
    [Header("Move Value")]
    [SerializeField] float runSpeed = 1f;
    [SerializeField] float jumpForce = 2f;
    private Rigidbody2D rigidBody;
    private CapsuleCollider2D capsuleCollider;

    public TilemapCollider2D ladderCollider;

    // private Animator playerAnimator;
    // private SpriteRenderer spriteRenderer;

    public float GetJumpForce()
    {
        return this.jumpForce;
    }

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();

        capsuleCollider = GetComponent<CapsuleCollider2D>();
        rigidBody = GetComponent<Rigidbody2D>();

        ladderCollider = GameObject.FindWithTag("Ladder").GetComponent<TilemapCollider2D>();

        // playerAnimator = GetComponent<Animator>();
        // spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        Move();
    }

    private void Start()
    {
        InputManager.Instance.OnJump += HandleJump;
    }

    private void Move()
    {
        Vector2 inputVectorMove = InputManager.Instance.GetInputVectorMove();

        rigidBody.velocity = new Vector2(inputVectorMove.x * runSpeed, rigidBody.velocity.y);
    }

    private void HandleJump(object sender, EventArgs e)
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(rigidBody.velocity.x) > Mathf.Epsilon;

        bool playerHasVerticalSpeed = Mathf.Abs(rigidBody.velocity.y) > Mathf.Epsilon;

        if (capsuleCollider.IsTouchingLayers(LayerMask.GetMask("Ground")) || capsuleCollider.IsTouchingLayers(LayerMask.GetMask("Mushroom")))
        {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpForce);

            if (playerHasHorizontalSpeed || playerHasVerticalSpeed)
            {

                if (!capsuleCollider.IsTouchingLayers(LayerMask.GetMask("Ground")) || InputManager.Instance.IsJumping())
                {
                    Physics2D.IgnoreCollision(capsuleCollider, ladderCollider, true);

                    ladderCollider.enabled = false;

                    Invoke("StopIgnoringCollision", 1f);
                }
                else
                {
                    Physics2D.IgnoreCollision(capsuleCollider, ladderCollider, false);

                }
            }
        }
    }

    private void StopIgnoringCollision()
    {
        ladderCollider.enabled = true;
        
        Physics2D.IgnoreCollision(capsuleCollider, ladderCollider, false);
    }
}