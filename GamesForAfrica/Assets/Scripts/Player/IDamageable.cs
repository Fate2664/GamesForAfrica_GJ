using UnityEngine;

public interface IDamageable
{
    public DeathScreen DeathScreen { get; }
    public int MaxHealth { get; }
    public int CurrentHealth { get; }
    public bool IsDead { get; }

    public void TakeDamage(int damage);
    public void Die();
    
    
}
