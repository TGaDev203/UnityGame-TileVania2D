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
