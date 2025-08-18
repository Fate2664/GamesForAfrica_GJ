using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    public float damage;
    public float maxDistance;    
    public Vector2 direction;
    private Vector2 spawnPosition;
    private float distanceTraveled;
    [SerializeField] public string EnemyTag;

    private void Start()
    {
        spawnPosition = transform.position;
        distanceTraveled = 0;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(EnemyTag))
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            Vector2 attackDirection = (collision.transform.position - transform.position).normalized;
            if (enemy != null)
            {
                enemy.TakeDamage(damage, attackDirection);
                enemy.ApplyKnockback(attackDirection, 5f);
            }
            DestroyProjectile();
        }
    }

    private void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}