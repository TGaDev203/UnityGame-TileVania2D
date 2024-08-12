using UnityEngine;


public class PlayerClimb : MonoBehaviour
{
    //! Component
    [SerializeField] LayerMask _layerPlayerCanClimb;
    [SerializeField] LayerMask _layerTopLadderPoint;
    private CapsuleCollider2D playerCollider;
    private Rigidbody2D rigidBody;
    private PlayerMovement playerMovement;
    [SerializeField] private float gravityAtStart;
    [SerializeField] private float climbSpeed;

    private void Awake()
    {
        InitializeComponents();
    }

    //! Initialization
    private void InitializeComponents()
    {
        playerCollider = GetComponent<CapsuleCollider2D>();
        rigidBody = GetComponent<Rigidbody2D>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        Climb();
    }

    //! Climbing Control
    private void Climb()
    {
        if (isPlayerOnClimbableLayer())
        {
            ApplyClimbSpeed();
            PerformJumpFromLadder();
        }
        else
        {
            rigidBody.gravityScale = gravityAtStart;
        }
    }

    //! Other Method To Handle Climbing Logic
    private bool isPlayerOnClimbableLayer()
    {
        return playerCollider.IsTouchingLayers(_layerPlayerCanClimb);
    }

    private void ApplyClimbSpeed()
    {
        Vector2 inputVectorClimb = InputManager.Instance.GetInputVectorClimb();
        rigidBody.velocity = new Vector2(rigidBody.velocity.x, inputVectorClimb.y * climbSpeed);
        rigidBody.gravityScale = default;
    }


    //! Allow To Jump When On Top Ladder Point
    private void PerformJumpFromLadder()
    {
        if (IsPlayerOnTopLadderPoint() && InputManager.Instance.IsJumping())
        {
            rigidBody.gravityScale = gravityAtStart;
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, playerMovement.GetPlayerJumpForce());
        }
    }
    
    private bool IsPlayerOnTopLadderPoint()
    {
        return playerCollider.IsTouchingLayers(_layerTopLadderPoint);
    }
}
