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
        LastAttackTime = 0;

    }
    void Start()
    {
    }

    void Update()
    {
        GetInput();
        //Debug.DrawRay(transform.position, Vector2.right * CurrentStats.attackRange, Color.red, 1f);
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
    }
    private void Move()
    {
        Vector3 movement = new Vector3(movementInput.x, movementInput.y, 0f) * CurrentStats.moveSpeed * Time.deltaTime;
        transform.Translate(movement,Space.World);
    }
}
