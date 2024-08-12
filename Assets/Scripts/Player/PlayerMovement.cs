using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerMovement : MonoBehaviour
{
    //! Component
    [Header("Collision For Jumping")]
    [SerializeField] LayerMask _layersPlayerCanJump;
    [Header("Collision To Avoid Collider: Player And Ladder")]
    [SerializeField] LayerMask _layerIgnorePlayerLadder;
    [Header("Collision For TopBouncing Point")]
    [SerializeField] LayerMask _layerTopBouncingPoint;
    [Header("Set Value")]

    [SerializeField] private float runSpeed;
    [SerializeField] private float playerJumpForceAtStart;
    [SerializeField] private float playerJumpForce;
    [SerializeField] private float bouncingJumpForce;
    [SerializeField] private float buoyancyForce;
    [SerializeField] private float waterDrag;
    [SerializeField] private float waterAngularDrag;


    private Rigidbody2D rigidBody;
    private CapsuleCollider2D playerCollider;
    private BoxCollider2D feetCollider;
    public TilemapCollider2D ladderCollider;
    private bool isInWater = false;

    private void Awake()
    {
        InitializeComponents();
    }

    //! Initialization
    private void InitializeComponents()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<CapsuleCollider2D>();
        rigidBody = GetComponent<Rigidbody2D>();
        ladderCollider = GameObject.FindWithTag("Ladder").GetComponent<TilemapCollider2D>();
        feetCollider = gameObject.GetComponent<BoxCollider2D>();
    }

    private void Start()
    {
        InputManager.Instance.OnJump += Jump;
    }

    private void Update()
    {
        HandleMovementAndBouncing();
    }

    //! Handle Movement and Bouncing
    private void HandleMovementAndBouncing()
    {
        Move();
        BouncingMushroom();
    }

    private void FixedUpdate()
    {
        if (isInWater)
        {
            rigidBody.AddForce(Vector2.up * buoyancyForce, ForceMode2D.Force);
        }
    }

    public float GetPlayerJumpForce()
    {
        return this.playerJumpForce;
    }

    //! Moving Control
    private void Move()
    {
        // Get the input vector for movement from the InputManager
        Vector2 inputVectorMove = InputManager.Instance.GetInputVectorMove();
        rigidBody.velocity = new Vector2(inputVectorMove.x * runSpeed, rigidBody.velocity.y);
    }

    //! Jumping Comtrol
    private void Jump(object sender, EventArgs e)
    {
        if (!CanPlayerJump())
        {
            return;
        }

        ApplyJumpForce();

        if (!HasPlayerSpeed())
        {
            return;
        }
        HandleLadderCollision();
    }

    //! Bouncing Player When Jumping In Mushroom
    private void BouncingMushroom()
    {
        if (IsPlayerOnTopBouncingPoint())
        {
            ApplyBouncingJumpForce();
        }
    }

    //! On Trigger Enter
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Water"))
        {
            EnterWater();
        }
    }

    //! On Trigger Exit
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Water"))
        {
            ExitWater();
        }
    }

    //! Other Methods To Handle Jumping Logic
    private bool CanPlayerJump()
    {
        return feetCollider.IsTouchingLayers(_layersPlayerCanJump);
    }

    private void ApplyJumpForce()
    {
        rigidBody.velocity = new Vector2(rigidBody.velocity.x, playerJumpForce);
    }

    private bool HasPlayerSpeed()
    {
        bool isMovingHorizontally = Mathf.Abs(rigidBody.velocity.x) > Mathf.Epsilon;
        bool isMovingVertically = Mathf.Abs(rigidBody.velocity.y) > Mathf.Epsilon;
        return isMovingHorizontally && isMovingVertically;
    }

    //! Avoid Ladder When Jumping
    private void HandleLadderCollision()
    {
        if (!playerCollider.IsTouchingLayers(_layerIgnorePlayerLadder) || InputManager.Instance.IsJumping())
        {
            Physics2D.IgnoreCollision(playerCollider, ladderCollider, true);
            ladderCollider.enabled = false;
            Invoke("StopIgnoringCollisionAfterJumping", 1f);
        }

        else
        {
            Physics2D.IgnoreCollision(playerCollider, ladderCollider, false);
        }
    }

    private void StopIgnoringCollisionAfterJumping()
    {
        Physics2D.IgnoreCollision(playerCollider, ladderCollider, false);
        ladderCollider.enabled = true;
    }

    //! Other Methods To Handle Jumping If On Bouncing Mushroom
    private bool IsPlayerOnTopBouncingPoint()
    {
        return playerCollider.IsTouchingLayers(_layerTopBouncingPoint);
    }

    private void ApplyBouncingJumpForce()
    {
        rigidBody.velocity = new Vector2(rigidBody.velocity.x, bouncingJumpForce);
    }

    //! Other Methods To Handle Movement In Water
    private void EnterWater()
    {
        SoundManager.Instance.PlayWaterSplashSound();
        SoundManager.Instance.PlayWaterWalkingSound();
        playerJumpForce = 40;
        rigidBody.drag = waterDrag;
        rigidBody.angularDrag = waterAngularDrag;
    }

    private void ExitWater()
    {
        SoundManager.Instance.StopWaterWalkingSound();
        rigidBody.drag = default;
        rigidBody.angularDrag = default;
        playerJumpForce = playerJumpForceAtStart;
    }
}