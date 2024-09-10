using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class CheckPointAnimation : MonoBehaviour
{
    //! Componnents
    private ParticleSystem endEffect;
    private Rigidbody2D rigidBody2D;

    private void Awake()
    {
        InitializeComponents();
    }

    private void InitializeComponents()
    {
        endEffect = GetComponent<ParticleSystem>();
        rigidBody2D = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (rigidBody2D.IsTouchingLayers(LayerMask.GetMask("Player")))
        {
            endEffect.Play();
        }
    }
}
