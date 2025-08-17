using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    [Header("Health & Damage")]
    [SerializeField] private float maxHealth = 100;
    [SerializeField] public int contactDamage = 10;
    [SerializeField] private float damageCooldown = 1f;
    [SerializeField] private float contacDamage = 5f;

    [Header("Knockback")]
    [SerializeField] public float knockbackX = 5f;
    [SerializeField] public float knockbackY = 2f;

    [Header("Particles & Visuals")]
    [SerializeField] private GameObject currencyPrefab;
    [SerializeField] private ParticleSystem damageParticles;
    [SerializeField] private ParticleSystem deathParticles;
    [SerializeField] private Color damageColor = Color.red;

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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Rhino"))
        {
            if (Time.time - lastDamageTime >= damageCooldown)
            {
                lastDamageTime = Time.time;
                Vector2 attackDirection = (collision.transform.position - transform.position).normalized;
                collision.gameObject.GetComponent<IDamageable>().TakeDamage(contactDamage, attackDirection);
            }
        }
    }

}
