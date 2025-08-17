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
        //Debug.Log("Hit something");
        if (collision.collider.CompareTag("Enemy"))
        {
            Enemy enemy = collision.collider.GetComponent<Enemy>();
            if (enemy != null)
            {
<<<<<<< HEAD
                damageable.TakeDamage(damage, attackDirection);
                damageable.ApplyKnockback(attackDirection, 5f);
=======
                enemy.TakeDamage(damage);
>>>>>>> parent of 4b60155 (Refactored your player code.)
            }
            DestroyProjectile();
        }
    }

    private void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}