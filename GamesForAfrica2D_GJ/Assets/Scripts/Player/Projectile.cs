using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10f;
    public float damage = 5f;
    public float maxDistance = 5f;
    public Vector2 direction;
    private Vector2 spawnPosition;
    private float distanceTraveled = 0f;

    private void Start()
    {
        spawnPosition = transform.position;
    }

    private void Update()
    {
        Vector2 movement = direction * speed * Time.deltaTime;
        transform.Translate(movement, Space.World);

        distanceTraveled = Vector2.Distance(spawnPosition, transform.position);
        if (distanceTraveled >= maxDistance)
        {
            DestroyProjectile();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.collider.CompareTag("Enemy"))
        {
            IDamageable damageable = collision.collider.GetComponent<IDamageable>();
            Vector2 attackDirection = (collision.transform.position - transform.position).normalized;
            if (damageable != null)
            {
                damageable.TakeDamage(damage, attackDirection);
                damageable.ApplyKnockback(attackDirection, 5f);
            }
            DestroyProjectile();
        }
    }

    private void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}