using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour, IDamageable
{
    [Header("Health & Damage")]
    [SerializeField] private float maxHealth = 100;
    [SerializeField] public int contactDamage = 10;
    [SerializeField] private float damageCooldown = 1f;

    [Header("Knockback")]
    [SerializeField] public float knockbackX = 5f;
    [SerializeField] public float knockbackY = 2f;

    [Header("Particles & Visuals")]
    [SerializeField] private GameObject currencyPrefab;
    [SerializeField] private ParticleSystem damageParticles;
    [SerializeField] private ParticleSystem deathParticles;
    [SerializeField] private Color damageColor = Color.red;

    [Header("Connections")] 
    [SerializeField] private PlayerStats playerStats;

    private float currentHP;
    private float lastDamageTime = -999f;
    private Coroutine damageCoroutine;
    private bool isKnockback = false;
    private Rigidbody2D rb;

    public float Health
    {
        get => currentHP;
        set => currentHP = value;
    }

    public float MaxHealth
    {
        get => maxHealth;
        set => maxHealth = value;
    }

    public bool IsAlive => currentHP > 0;


    void Start()
    {
        currentHP = maxHealth;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (currentHP <= 0)
        {
            Die();
        }
    }
    public void TakeDamage(float damage, Vector2 attackDirection)
    {
        currentHP -= damage;
        if (SettingsManager.Instance.ParticlesEnabled)
        {
            SpawnDamageParticles(attackDirection);
        }

        if (currentHP <= 0)
        {
            if (SettingsManager.Instance.ParticlesEnabled)
            {
                SpawnDeathParticles();
            }
            SpawnCurrency();
            Die();
        }
    }
    public void Die()
    {
        Destroy(gameObject);
    }

    private void SpawnDamageParticles(Vector2 attackDirection)
    {
        if (damageParticles != null)
        {
            Quaternion spawnRotation = Quaternion.FromToRotation(Vector2.right, attackDirection);
            Instantiate(damageParticles, transform.position, spawnRotation);
        }
    }

    private void SpawnCurrency()
    {
        Vector2 spawnPos = (Vector2)transform.position - new Vector2(0, 1f); // Adjust spawn position as needed
        if (currencyPrefab != null)
        {
            GameObject orb = Instantiate(currencyPrefab, spawnPos, Quaternion.identity);
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

    public void ApplyKnockback(Vector2 direction, float force)
    {
        if (!playerStats.IsAlive) return;

        isKnockback = true;
        Invoke(nameof(ResetKnockback), 0.8f); // Reset knockback after 0.8 seconds

        if (damageCoroutine != null)
        {
            StopCoroutine(damageCoroutine);
        }
        damageCoroutine = StartCoroutine(Damage(0.5f)); // Flash for 0.5 seconds

        rb.linearVelocity = Vector2.zero;
        rb.AddForce(direction.normalized * force, ForceMode2D.Impulse);

        // Debug.DrawRay(transform.position, knockback, Color.red, 1f);
    }
    private void ResetKnockback()
    {
        isKnockback = false;
    }

    private IEnumerator Damage(float duration, float flashSpeed = 0.1f)
    {
        SpriteRenderer spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        float elapsed = 0f;
        while (elapsed < duration)
        {
            spriteRenderer.color = damageColor;
            yield return new WaitForSeconds(flashSpeed);
            spriteRenderer.color = Color.white;
            yield return new WaitForSeconds(flashSpeed);
            elapsed += flashSpeed * 2;
        }
        spriteRenderer.color = Color.white;
    }

}
