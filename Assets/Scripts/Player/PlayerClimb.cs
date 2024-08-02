using UnityEngine;


public class PlayerClimb : MonoBehaviour
{
    //! Component Variables
    [SerializeField] LayerMask _topLadderPoint;

    [SerializeField] private float gravityAtStart;

    [SerializeField] private float climbSpeed;

    private CapsuleCollider2D playerCollider;

    private Rigidbody2D rigidBody;

    private PlayerMovement playerMovement;

    //! Component References For Player Physics And Collision Detection
    [SerializeField] LayerMask _layerPlayerCanClimb;
    


    //! Lifecycle Methods
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

    //! Climbing Control
    private void Climb()
    {
        if (playerCollider.IsTouchingLayers(_layerPlayerCanClimb))
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
        }

        else
        {
            rigidBody.gravityScale = gravityAtStart;
        }
    }
}
