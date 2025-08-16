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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
            DestroyProjectile();
        }
        else if (!other.isTrigger)
        { 
            DestroyProjectile();
        }
    }

    private void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}