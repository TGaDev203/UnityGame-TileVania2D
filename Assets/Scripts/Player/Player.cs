using UnityEngine;

public class Player : MonoBehaviour
{
    //! Components
    [Header("Collision For Being Damaged")]
    [SerializeField] private LayerMask _layerEnemy;

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
        return playerCollider.IsTouchingLayers(_layerEnemy);
    }

    private void Die()
    {
        UnityEditor.EditorApplication.isPlaying = false;
    }
}