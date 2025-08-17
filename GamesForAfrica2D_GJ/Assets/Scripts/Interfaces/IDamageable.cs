using UnityEngine;

public interface IDamageable
{
    public float Health { get; set; }
    public bool IsAlive { get; }
    public void TakeDamage(float amount);
    public void Die();
}
