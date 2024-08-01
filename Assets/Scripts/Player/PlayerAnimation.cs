using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerAnimation : MonoBehaviour
{
    //! Component Variables
    private SpriteRenderer spriteRenderer;

    private Rigidbody2D rigidBody;

    private CapsuleCollider2D playerCollider;

    private BoxCollider2D feetCollider;

    private Animator playerAnimation;

    //! Lifecycle Methods
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        rigidBody = GetComponent<Rigidbody2D>();

        playerCollider = GetComponent<CapsuleCollider2D>();

        feetCollider = GetComponent<BoxCollider2D>();

        playerAnimation = GetComponent<Animator>();
    }

    private void Update()
    {
        FlipSprite();

        PlayerRunAnimation();

        PlayerClimbAnination();
    }

    //! Flip Sprite
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

    //! Run Animation
    private void PlayerRunAnimation()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(rigidBody.velocity.x) > Mathf.Epsilon;

        if (feetCollider.IsTouchingLayers(LayerMask.GetMask("Platform")) && playerHasHorizontalSpeed)
        {
            playerAnimation.SetBool("isRunning", true);
        }

        else
        {
            playerAnimation.SetBool("isRunning", false);
        }
    }

    //! Climb Animation
    private void PlayerClimbAnination()
    {
        bool playerHasVerticalSpeed = Mathf.Abs(rigidBody.velocity.y) > Mathf.Epsilon;

        if (playerCollider.IsTouchingLayers(LayerMask.GetMask("Ladder")) && playerHasVerticalSpeed)
        {
            playerAnimation.SetBool("isClimbing", true);
        }

        else
        {
            playerAnimation.SetBool("isClimbing", false);
        }
    }
}
