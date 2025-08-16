using System.Linq;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{

    public PlayerStats CurrentStats;
    private float LastAttackTime;
    public Projectile BulletPrefab;
    private Vector2 movementInput;

    private void Awake()
    {
        ResetStats();
        LastAttackTime = 0;

    }
    void Start()
    {
    }

    void Update()
    {
        GetInput();
        Debug.DrawRay(transform.position, Vector2.right * CurrentStats.attackRange, Color.red, 1f);
    }

    void GetInput()
    {
        movementInput.x = Input.GetAxisRaw("Horizontal");
        movementInput.y = Input.GetAxisRaw("Vertical");
        if (movementInput.magnitude > 1f)
        {
            movementInput.Normalize();
        }
    }
    private void FixedUpdate()
    {
        GetInput();
        Move();
        Attack();
    }
    private void Move()
    {
        Vector3 movement = new Vector3(movementInput.x, movementInput.y, 0f) * CurrentStats.moveSpeed * Time.deltaTime;
        transform.Translate(movement,Space.World);
    }
    void Attack()
    {
        if (Time.time - LastAttackTime >= 1f / CurrentStats.attackSpeed)
        {
            //Debug.Log("LookingForTarget");
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
            if (distance < CurrentStats.attackRange && distance < closestDistance)
            {
                closestDistance = distance;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null&&closestDistance<=CurrentStats.attackRange)
        {
            Shoot(nearestEnemy.transform.position);
        }
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
        CurrentStats = GetComponent<PlayerStats>();
    }
}
