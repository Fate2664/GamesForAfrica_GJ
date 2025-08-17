using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [Header("Player Attack")]
    [SerializeField] float attackRange = 2f;
    [SerializeField] float attackSpeed = 1f;
    [SerializeField] int bulletNum = 3;
    [SerializeField] float bulletSpeed = 5f;
    [SerializeField] float spreadAngle = 30f;
    [SerializeField] private float attackDamage = 10f;
    [SerializeField] private Projectile bulletPrefab;

    [Space(10)]
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private PlayerStats playerStats;

    private float nextAttackTime = 0f;
    private int attackTrigger = Animator.StringToHash("isAttacking");
    private static bool isAttacking = false;
    private float LastAttackTime = 0f;

    private void Awake()
    {
        LastAttackTime = 0;
    }

    void FixedUpdate()
    {
        if (!playerStats.IsAlive) { return; }
        Attack();
    }

    private void Attack()
    {
        isAttacking = true;
        if (Time.time - LastAttackTime >= 1f / attackSpeed)
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
            Debug.Log("No enemies");
            return;
        }
        GameObject nearestEnemy = null;
        float closestDistance = Mathf.Infinity;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector2.Distance(transform.position, enemy.transform.position);
            if (distance < attackRange && distance < closestDistance)
            {
                closestDistance = distance;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && closestDistance <= attackRange)
        {
            Shoot(nearestEnemy.transform.position);
        }
    }

    void Shoot(Vector3 target)
    {
        float angleStep = spreadAngle / (bulletNum - 1);
        float startAngle = -spreadAngle / 2f;
        Vector2 baseDirection = (target - transform.position).normalized;

        for (int i = 0; i < bulletNum; i++)
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
            projScript.speed = bulletSpeed;
            projScript.damage = attackDamage / bulletNum;
            projScript.maxDistance = attackRange;
        }
    }

}
