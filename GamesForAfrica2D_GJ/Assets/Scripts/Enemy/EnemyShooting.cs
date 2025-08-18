using UnityEngine;

public class EnemyShooting : MonoBehaviour
{

    [SerializeField] private float AttackDamage;
    [SerializeField] private float AttackRange;
    [SerializeField] private float AttackSpeed;
    [SerializeField] private Projectile bullet;
    [SerializeField] private float BulletSpeed;
    [SerializeField] private float BulletSize;
    [SerializeField] private string enemyTag;
    private float LastAttackTime;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        LastAttackTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void Attack()
    {
        if (Time.time - LastAttackTime >= 1f / AttackSpeed)
        {
            FindTarget();
            LastAttackTime = Time.time;
        }
    }

    void FindTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("EnemyTag");
        if (enemies.Length == 0)
        {
            return;
        }
        GameObject nearestEnemy = null;
        float closestDistance = Mathf.Infinity;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector2.Distance(transform.position, enemy.transform.position);
            if (distance < AttackRange && distance < closestDistance)
            {
                closestDistance = distance;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && closestDistance <= AttackRange)
        {
            Shoot(nearestEnemy.transform.position);
        }
    }

    void Shoot(Vector3 target)
    {
        Vector2 direction = (target - transform.position).normalized;
        Projectile projectile = Instantiate(
            bullet,
            transform.position,
            Quaternion.identity
        );

        Projectile projScript = projectile.GetComponent<Projectile>();
        projScript.direction = direction;
        projScript.speed = BulletSpeed;
        projScript.damage = AttackDamage;
        projScript.maxDistance = AttackRange;
        projScript.transform.localScale = new Vector3(BulletSize, BulletSize, 1);
        projScript.EnemyTag = enemyTag;
    }
}
