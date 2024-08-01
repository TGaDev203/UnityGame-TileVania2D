using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerMovement : MonoBehaviour
{
    //! Component Variables
    [Header("Set Value")]

    [SerializeField] private float runSpeed;

    [SerializeField] private float playerJumpForce;

    [SerializeField] private float bouncingJumpForce;

    [SerializeField] private float buoyancyForce;

    [SerializeField] private float waterDrag;

    [SerializeField] private float waterAngularDrag;

    [Header("Collision For Jumping")]
    [SerializeField] LayerMask _layersPlayerCanJump;

    [Header("Collider To Avoid Collider: Player And Ladder")]
    [SerializeField] LayerMask _layerIgnorePlayerLadder;

    private Rigidbody2D rigidBody;

    private CapsuleCollider2D playerCollider;

    private BoxCollider2D feetCollider;

    public TilemapCollider2D ladderCollider;

    private bool isInWater = false;

    //! Lifecycle Methods
    private void Awake()
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

    //! Moving Control
    private void Move()
    {
        Vector2 inputVectorMove = InputManager.Instance.GetInputVectorMove();

        rigidBody.velocity = new Vector2(inputVectorMove.x * runSpeed, rigidBody.velocity.y);
    }

    //! Jumping Comtrol
    private void Jump(object sender, EventArgs e)
    {
        if (!feetCollider.IsTouchingLayers(_layersPlayerCanJump))
        {
            return;
        }

        rigidBody.velocity = new Vector2(rigidBody.velocity.x, playerJumpForce);

        bool playerHasHorizontalSpeed = Mathf.Abs(rigidBody.velocity.x) > Mathf.Epsilon;

        bool playerHasVerticalSpeed = Mathf.Abs(rigidBody.velocity.y) > Mathf.Epsilon;

        if (!playerHasHorizontalSpeed || !playerHasVerticalSpeed)
        {
            return;
        }

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

    public float GetPlayerJumpForce()
    {
        return this.playerJumpForce;
    }

    //! Bouncing Player When Jumping In Mushroom
    private void BouncingMushroom()
    {
        bool isAtTopBouncing = playerCollider.IsTouchingLayers(LayerMask.GetMask("TopBouncing"));

        if (feetCollider.IsTouchingLayers(LayerMask.GetMask("Mushroom")) && isAtTopBouncing)
        {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, bouncingJumpForce);
        }
    }

    //! On Trigger Enter
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isInWater && other.CompareTag("Water"))
        {
            Debug.Log(other.gameObject.name + " Collision With: " + gameObject.name);

            isInWater = true;

            rigidBody.gravityScale = 0.5f;

            playerJumpForce = 40;

            Debug.Log("Gravity Changed");

            rigidBody.drag = waterDrag;

            rigidBody.angularDrag = waterAngularDrag;
        }

        if (rigidBody.IsTouchingLayers(LayerMask.GetMask("Platform")))
        {
            playerJumpForce = 17;
        }
    }

    //! On Trigger Exit
    private void OnTriggerExit2D(Collider2D other)
    {
        if (isInWater && other.CompareTag("Water"))
        {
            isInWater = false;

            rigidBody.gravityScale = default;

            rigidBody.drag = default;

            rigidBody.angularDrag = default;

            playerJumpForce = default;
        }
    }
}