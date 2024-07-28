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

    [SerializeField] float runSpeed;

    [SerializeField] float playerJumpForce;
    
    [SerializeField] float bouncingJumpForce;

    private Rigidbody2D rigidBody;

    private CapsuleCollider2D playerCollider;

    private BoxCollider2D feetCollider;

    public TilemapCollider2D ladderCollider;

    public float GetPlayerJumpForce()
    {
        return this.playerJumpForce;
    }

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();

        playerCollider = GetComponent<CapsuleCollider2D>();

        rigidBody = GetComponent<Rigidbody2D>();

        ladderCollider = GameObject.FindWithTag("Ladder").GetComponent<TilemapCollider2D>();

        feetCollider = gameObject.GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        Move();

        BouncingMushroom();
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

        if (feetCollider.IsTouchingLayers(LayerMask.GetMask("Platform")) || feetCollider.IsTouchingLayers(LayerMask.GetMask("Mushroom")))
        {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, playerJumpForce);

            if (playerHasHorizontalSpeed || playerHasVerticalSpeed)
            {

                if (!playerCollider.IsTouchingLayers(LayerMask.GetMask("Platform")) || InputManager.Instance.IsJumping())
                {
                    Physics2D.IgnoreCollision(playerCollider, ladderCollider, true);

                    ladderCollider.enabled = false;

                    Invoke("StopIgnoringCollision", 1f);
                }

                else
                {
                    Physics2D.IgnoreCollision(playerCollider, ladderCollider, false);
                }
            }
        }
    }

    private void StopIgnoringCollision()
    {
        Physics2D.IgnoreCollision(playerCollider, ladderCollider, false);

        ladderCollider.enabled = true;
    }

    private void BouncingMushroom()
    {
        bool isAtTopBouncing = playerCollider.IsTouchingLayers(LayerMask.GetMask("TopBouncing"));

        if (playerCollider.IsTouchingLayers(LayerMask.GetMask("Mushroom")) && isAtTopBouncing)
        {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, bouncingJumpForce);
        }
    }
}