using UnityEngine;

public interface IDamageable
{
    public float Health { get; set; }
    public bool IsAlive { get; }
<<<<<<< HEAD
    public void TakeDamage(float amount, Vector2 attackDirection);
    public void ApplyKnockback(Vector2 direction, float force );
=======
    public void TakeDamage(float amount);
>>>>>>> parent of 4b60155 (Refactored your player code.)
    public void Die();
}
