using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClimb : MonoBehaviour
{
    [SerializeField] float gravityAtStart;

    private CapsuleCollider2D playerCollider;

    private Rigidbody2D rigidBody;

    private float climbSpeed = 5f;

    private PlayerMovement playerMovement;

    private void Awake()
    {
        playerCollider = GetComponent<CapsuleCollider2D>();

        rigidBody = GetComponent<Rigidbody2D>();

        playerMovement = GetComponent<PlayerMovement>();

    }

    private void Update()
    {
        Climb();
    }

    private void Climb()
    {
        if (playerCollider.IsTouchingLayers(LayerMask.GetMask("Ladder")))
        {
            Vector2 inputVectorClimb = InputManager.Instance.GetInputVectorClimb();

            rigidBody.velocity = new Vector2(rigidBody.velocity.x, inputVectorClimb.y * climbSpeed);

            rigidBody.gravityScale = 0f;

            if (playerCollider.IsTouchingLayers(LayerMask.GetMask("TopLadder")))
            {
                if (InputManager.Instance.IsJumping())
                {
                    rigidBody.gravityScale = gravityAtStart;

                    rigidBody.velocity = new Vector2(rigidBody.velocity.x, playerMovement.GetPlayerJumpForce());
                }
            }

            else { }
        }

        else
        {
            rigidBody.gravityScale = gravityAtStart;
        }
    }
}