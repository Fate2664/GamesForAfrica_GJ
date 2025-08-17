using UnityEngine;

public class RhinoManager : MonoBehaviour, IDamageable
{
    [Header("Health")]
    [SerializeField] private float maxHealth = 100f;

    [Header("Particle Effects")]
    [SerializeField] private ParticleSystem damageParticles;
    [SerializeField] private ParticleSystem deathParticles;

    private float currentHealth;
    public bool IsAlive => currentHealth > 0;
    public float Health
    {
        get => currentHealth;
        set => currentHealth = value;
    }
    

    private void Start()
    {
        currentHealth = maxHealth;
    }
 
    public void TakeDamage(float amount, Vector2 attackDirection)
    {
        if (IsAlive)
        {
            currentHealth -= amount;
            if (SettingsManager.Instance.ParticlesEnabled)
            {
                SpawnDamageParticles(attackDirection);
            }

            if (!IsAlive)
            {
                if (SettingsManager.Instance.ParticlesEnabled)
                {
                    SpawnDeathParticles();
                }
                Die();
            }
        }
    }

    private void SpawnDamageParticles(Vector2 attackDirection)
    {
        if (damageParticles != null)
        {
            Quaternion spawnRotation = Quaternion.FromToRotation(Vector2.right, attackDirection);
            Instantiate(damageParticles, transform.position, spawnRotation);
        }
    }

    private void SpawnDeathParticles()
    {
        if (deathParticles != null)
        {
            ParticleSystem ps = Instantiate(deathParticles, transform.position, Quaternion.identity);
            ps.Play();

            // Ensure particle system has time to finish
            Destroy(ps.gameObject, ps.main.duration + ps.main.startLifetime.constantMax);
        }
    }

    public void Die()
    {
        Debug.Log("Rhino has died.");
        //show death screen and trigger game over
    }

    public void ApplyKnockback(Vector2 direction, float force)
    {
        throw new System.NotImplementedException();
    }
}
