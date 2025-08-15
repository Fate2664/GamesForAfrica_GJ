using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    [SerializeField] public int MaxHealth {get; private set;}
    [SerializeField] private Animator animator;
    public DeathScreen DeathScreen => DeathScreen;
    public int CurrentHealth { get; private set; }
    public bool IsDead {get; private set;}

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        CurrentHealth = MaxHealth;
    }

    public void TakeDamage(int damage)
    {
        CurrentHealth -= damage;
        if (CurrentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        if (IsDead)
        {
            return;
        }

        IsDead = true;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        // Play death animation
        if (animator != null)
        {
            animator.SetTrigger("isDead");
        }
        DeathScreen.ShowDeathScreen();
    }

}
