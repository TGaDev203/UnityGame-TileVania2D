using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    //! Components
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rigidBody;

    private void Awake()
    {
        InitializeComponents();
    }
    
    //! Initialization
    private void InitializeComponents()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidBody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        HandleAnimation();
    }

    //! Handle All Animations
    private void HandleAnimation()
    {
        FlipSprite();
    }

    //! Flip Sprite
    private void FlipSprite()
    {
        float moveDirection = rigidBody.velocity.x;

        if (moveDirection != 0)
        {
            transform.localScale = new Vector2(Mathf.Sign(moveDirection), 1f);
        }
    }
}
