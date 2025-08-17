using UnityEngine;
using UnityEngine.Rendering;

public class PlayerStats : MonoBehaviour, IDamageable
{
    [Header("Player Stats")]
    [SerializeField] private float moveSpeed = 3f;

    [Header("UI/Animations")]  
    [SerializeField] private DeathScreen deathScreen;
    [SerializeField] private Animator animator;

    private float currentHealth;
    private float maxHealth;
    private Rigidbody2D rb;
    private bool isDead = false;

    public float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }
    public float Health { get => currentHealth; set => currentHealth = value; }
    public float MaxHealth { get => maxHealth; set => maxHealth = value; }

    public bool IsAlive => currentHealth > 0;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        switch (SettingsManager.Instance.Difficulty)
        {
            case 0:
                maxHealth = 200; break;
            case 1:
                maxHealth = 100; break;
            case 2:
                maxHealth = 50; break;
            case 3:
                maxHealth = 10; break;
        }

        currentHealth = maxHealth;
    }

    public void Die()
    {
        if (isDead)
        {
            return;
        }

        isDead = true;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        // Play death animation
        if (animator != null)
        {
            animator.SetTrigger("isDead");
        }
        deathScreen.ShowDeathScreen();
    }

    public void TakeDamage(float damage, Vector2 attackDirection)
    {
        currentHealth -= damage;
        attackDirection = Vector2.zero; // Ignore attack direction for player
        if (currentHealth <= 0)
        {
            Die();
        }
    }
}
