using System.Linq;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    public Projectile BulletPrefab;
    private Vector2 movementInput;
    public PlayerStats PlayerStats;
    private float LastAttackTime;
    private Animator animator;
    private Vector2 lastPosition;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        lastPosition = transform.position;
        LastAttackTime = 0;
    }
    void Start()
    {
    }

    void Update()
    {
        GetInput();
        if(Input.GetAxisRaw("Horizontal")!=0|| Input.GetAxisRaw("Vertical")!=0)
            animator.SetBool("Walking", true);
        else
            animator.SetBool("Walking", false);
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
        Vector3 movement = new Vector3(movementInput.x, movementInput.y, 0f) * PlayerStats.moveSpeed * Time.deltaTime;
        transform.Translate(movement,Space.World);
    }
    void Attack()
    {
        if (Time.time - LastAttackTime >= 1f / PlayerStats.attackSpeed)
        {
            //Debug.Log("LookingForTarget");
            FindTarget();
            LastAttackTime = Time.time;
        }
    }

    void FindTarget()
    {
        //Debug.Log("finding");
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
            if (distance < PlayerStats.attackRange && distance < closestDistance)
            {
                closestDistance = distance;
                nearestEnemy = enemy;
            }
        }
        if (nearestEnemy != null&&closestDistance<=PlayerStats.attackRange)
        {
            Shoot(nearestEnemy.transform.position);
        }
    }
    void Shoot(Vector3 target)
    {
        float angleStep = PlayerStats.spreadAngle / (PlayerStats.bulletCount - 1);
        float startAngle = -PlayerStats.spreadAngle / 2f;
        Vector2 baseDirection = (target - transform.position).normalized;
        for (int i = 0; i < PlayerStats.bulletCount; i++)
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
            if (projScript == null)
            {
                Debug.Log("No projectile script found");
            }
            else
            {
                projScript.direction = pelletDirection;
                projScript.speed = PlayerStats.bulletSpeed;
                projScript.damage = PlayerStats.attackDamage;
                projScript.maxDistance = PlayerStats.attackRange;
                projScript.transform.localScale = new Vector3(PlayerStats.bulletSize, PlayerStats.bulletSize, 1);
            }
        }
    }
}
