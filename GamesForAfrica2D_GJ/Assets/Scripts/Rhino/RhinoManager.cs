using UnityEngine;

public class RhinoManager : MonoBehaviour, IDamageable
{
    private float currentHealth;
    public bool IsAlive => currentHealth > 0;
    public float Health
    {
        get => currentHealth;
        set => currentHealth = value;
    }
    [SerializeField] private float maxHealth = 100f;

    private void Start()
    {
        currentHealth = maxHealth;
    }
 
    public void TakeDamage(float amount, Vector2 attackDirection)
    {
        if (IsAlive)
        {
            currentHealth -= amount;
            Debug.Log($"Rhino took {amount} damage. Current health: {currentHealth}");
            if (!IsAlive)
            {
                Die();
            }
        }
    }

    public void Die()
    {
        Debug.Log("Rhino has died.");
        // Add death logic here, such as playing an animation or dropping loot
    }

    public void ApplyKnockback(Vector2 direction, float force)
    {
        throw new System.NotImplementedException();
    }
}
