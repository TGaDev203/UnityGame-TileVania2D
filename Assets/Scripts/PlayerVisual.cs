using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVisual : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    private Rigidbody2D rigidBody;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        rigidBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        FlipSprite();
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

    void FlipSprite()
    {
        bool playerHasHorizontalSpeed = rigidBody.velocity.x < Mathf.Epsilon;

        if (playerHasHorizontalSpeed)
        {
            spriteRenderer.flipX = true;
        }

        else
        {
            spriteRenderer.flipX = false;
        }
    }
}
