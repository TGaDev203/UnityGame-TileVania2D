using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    //! Components
    [Header("Collision For Being Damaged")]
    [SerializeField] private LayerMask _layerTakenDamage;

    [SerializeField] private int maxHealth;
    [SerializeField] private int currentHealth;
    [SerializeField] private float damageCooldown;
    private CapsuleCollider2D playerCollider;
    private HealthBarManager healthBar;
    private float lastDamageTime;

    private void Awake()
    {
        InitializeComponents();
    }

    //! Initialization
    private void InitializeComponents()
    {
        playerCollider = GetComponent<CapsuleCollider2D>();
        healthBar = GetComponent<HealthBarManager>();
    }

    private void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartGame();
        }
        TouchingEnemy();
    }

    private void TouchingEnemy()
    {
        if (IsTouchingEnemy() && Time.time - lastDamageTime > damageCooldown)
        {
            TakeDamage(3);
            lastDamageTime = Time.time;
        }
    }

    //! Other Method To Handle Damage
    private void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        SoundManager.Instance.PlayerHitSound();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private bool IsTouchingEnemy()
    {
        return playerCollider.IsTouchingLayers(_layerTakenDamage);
    }

    private void Die()
    {
        UnityEditor.EditorApplication.isPlaying = false;
    }

    private void StartGame()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
    }
}