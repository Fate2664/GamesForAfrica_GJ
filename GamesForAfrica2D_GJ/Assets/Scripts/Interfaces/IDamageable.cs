using UnityEngine;

public interface IDamageable
{
    public float Health { get; set; }
    public float MaxHealth { get; set; }
    public bool IsAlive { get; }
    public void TakeDamage(float amount, Vector2 attackDirection);
    public void Die();
}
