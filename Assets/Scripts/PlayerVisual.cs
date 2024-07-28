using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerVisual : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    private Rigidbody2D rigidBody;

    private CapsuleCollider2D capsuleCollider;

    private Animator playerAnimation;

    public TilemapCollider2D ladderCollider;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        rigidBody = GetComponent<Rigidbody2D>();

        capsuleCollider = GetComponent<CapsuleCollider2D>();

        playerAnimation = GetComponent<Animator>();

        ladderCollider = GetComponent<TilemapCollider2D>();
    }

    private void Start()
    {

    }

    private void Update()
    {
        FlipSprite();

        RunAnimation();

        ClimbAnination();
    }

    private void FlipSprite()
    {
        bool playerHasBackwardSpeed = rigidBody.velocity.x < 0;
        bool playerHasForwardSpeed = rigidBody.velocity.x > 0;
        if (playerHasBackwardSpeed)
        {
            spriteRenderer.flipX = true;
        }

        else if (playerHasForwardSpeed)
        {
            spriteRenderer.flipX = false;
        }
    }

    private void RunAnimation()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(rigidBody.velocity.x) > Mathf.Epsilon;

        if (capsuleCollider.IsTouchingLayers(LayerMask.GetMask("Ground")) && playerHasHorizontalSpeed)
        {
            playerAnimation.SetBool("isRunning", true);
        }

        else
        {
            playerAnimation.SetBool("isRunning", false);
        }
    }

    private void ClimbAnination()
    {
        bool playerHasVerticalSpeed = Mathf.Abs(rigidBody.velocity.y) > Mathf.Epsilon;

        if (capsuleCollider.IsTouchingLayers(LayerMask.GetMask("Ladder")) && playerHasVerticalSpeed)
        {
            Debug.Log("OK");
            
            playerAnimation.SetBool("isClimbing", true);
        }

        else 
        {
            playerAnimation.SetBool("isClimbing", false);
        }
    }
}
