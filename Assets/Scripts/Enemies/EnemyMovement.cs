using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    //! Components
    [SerializeField] private float moveSpeed = 1f;

    private Rigidbody2D rigidBody;

    private void Awake()
    {
        InitializeComponents();
    }

    //! Initialization
    private void InitializeComponents()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Move();
    }

    //! Moving Control
    private void Move()
    {
        rigidBody.velocity = new Vector2(moveSpeed, rigidBody.velocity.y);
    }

    //! On Trigger Exit
    void OnTriggerExit2D(Collider2D other)
    {
        moveSpeed = -moveSpeed;
    }
}
