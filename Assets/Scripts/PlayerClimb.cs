using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClimb : MonoBehaviour
{
    private float gravityAtStart = 5f;

    private CapsuleCollider2D capsuleCollider;

    private Rigidbody2D rigidBody;

    private float climbSpeed = 5f;

    private PlayerMovement playerMovement;

    private void Awake()
    {
        capsuleCollider = GetComponent<CapsuleCollider2D>();

        rigidBody = GetComponent<Rigidbody2D>();

        playerMovement = GetComponent<PlayerMovement>();

    }

    private void Start()
    {

    }

    private void Update()
    {
        Climb();
    }

    private void Climb()
    {
        if (capsuleCollider.IsTouchingLayers(LayerMask.GetMask("Ladder")))
        {
            Vector2 inputVectorClimb = InputManager.Instance.GetInputVectorClimb();

            rigidBody.velocity = new Vector2(rigidBody.velocity.x, inputVectorClimb.y * climbSpeed);
            
            rigidBody.gravityScale = 0f;

            if (IsAtTopOfLadder())
            {
                if (InputManager.Instance.IsJumping())
                {
                    rigidBody.gravityScale = gravityAtStart;
                    
                    rigidBody.velocity = new Vector2(rigidBody.velocity.x, playerMovement.GetJumpForce());
                }
            }
            else{}
        }

        else
        {
            rigidBody.gravityScale = gravityAtStart;
        }
    }

    private bool IsAtTopOfLadder()
    {
        return capsuleCollider.IsTouchingLayers(LayerMask.GetMask("TopLadder"));
    }
}
