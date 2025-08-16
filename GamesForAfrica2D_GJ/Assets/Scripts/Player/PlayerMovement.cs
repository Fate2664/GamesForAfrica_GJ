using System.Linq;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.UIElements;
public class PlayerStats
{
    public float health = 100f;
    public float maxHealth = 100f;
    public float moveSpeed = 5f;
    public float attackDamage = 10f;
    public float attackRange = 2f;
    public float attackSpeed = 1f;
    public float bullets= 3f;
    public float bulletSpeed=5f;
    public float spreadAngle = 30f;
}
public class PlayerMovement : MonoBehaviour
{
    public PlayerStats DefaultStats;
    public PlayerStats CurrentStats;
    private float LastAttackTime;
    public Projectile BulletPrefab;

    private void Awake()
    {
        ResetStats();
    }
    void Start()
    {
    }

    void Update()
    {
        Attack();
    }


    void Attack()
    {
        if (Time.time - LastAttackTime >= 1f / CurrentStats.attackSpeed)
        {
            FindTarget();
        }

    }
    void FindTarget()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, CurrentStats.attackRange);
        Enemy closestEnemy = hits
        .Where(h => h.CompareTag("Enemy"))
        .Select(h => h.GetComponent<Enemy>())
        .OrderBy(e => Vector3.Distance(transform.position, e.transform.position))
        .FirstOrDefault();
        Shoot(closestEnemy.transform.position);
    }
    void Shoot(Vector3 target)
    {
        float angleStep = CurrentStats.spreadAngle / (CurrentStats.bullets - 1);
        float startAngle = -CurrentStats.spreadAngle / 2f;
        Vector2 baseDirection = (target - transform.position).normalized;

        for (int i = 0; i < CurrentStats.bullets; i++)
        {
            float currentAngle = startAngle + (angleStep * i);
            Quaternion rotation = Quaternion.AngleAxis(currentAngle, Vector3.forward);
            Vector2 pelletDirection = rotation * baseDirection;

            Projectile projectile = Instantiate(
                BulletPrefab,
                transform.position,
                Quaternion.identity
            );
            Projectile projScript = projectile.GetComponent<Projectile>();
            projScript.direction = pelletDirection;
            projScript.speed = CurrentStats.bulletSpeed;
            projScript.damage = CurrentStats.attackDamage / CurrentStats.bullets;
            projScript.maxDistance = CurrentStats.attackRange;
        }
    }
        void ResetStats()
    {
            CurrentStats = DefaultStats;
    }
}
