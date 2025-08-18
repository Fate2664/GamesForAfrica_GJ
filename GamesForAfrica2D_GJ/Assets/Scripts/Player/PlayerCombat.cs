using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [Header("Player Attack")]
    [SerializeField] private Projectile bulletPrefab;

    [Space(10)]
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private PlayerStats playerStats;

    private float LastAttackTime = 0f;

    private void Awake()
    {
        LastAttackTime = 0;
    }

    void FixedUpdate()
    {
       // if (!playerStats.IsAlive) { return; }
        Attack();
    }

    private void Attack()
    {
        if (Time.time - LastAttackTime >= 1f / playerStats.attackSpeed)
        {
            FindTarget();
            LastAttackTime = Time.time;
        }
    }

    void FindTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemies.Length == 0)
        {
            return;
        }
        GameObject nearestEnemy = null;
        float closestDistance = Mathf.Infinity;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector2.Distance(transform.position, enemy.transform.position);
            if (distance < playerStats.attackRange && distance < closestDistance)
            {
                closestDistance = distance;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && closestDistance <= playerStats.attackRange)
        {
            Shoot(nearestEnemy.transform.position);
        }
    }

    void Shoot(Vector3 target)
    {
        float angleStep = playerStats.spreadAngle / (playerStats.bulletCount - 1);
        float startAngle = -playerStats.spreadAngle / 2f;
        Vector2 baseDirection = (target - transform.position).normalized;

        for (int i = 0; i < playerStats.bulletCount; i++)
        {
            float currentAngle = startAngle + (angleStep * i);
            Quaternion rotation = Quaternion.AngleAxis(currentAngle, Vector3.forward);
            Vector2 pelletDirection = rotation * baseDirection;

            Projectile projectile = Instantiate(
                bulletPrefab,
                transform.position,
                Quaternion.identity
            );
            Projectile projScript = projectile.GetComponent<Projectile>();
            projScript.direction = pelletDirection;
            projScript.speed = playerStats.bulletSpeed;
            projScript.damage = playerStats.attackDamage / playerStats.bulletCount;
            projScript.maxDistance = playerStats.attackRange;
        }
    }

}
