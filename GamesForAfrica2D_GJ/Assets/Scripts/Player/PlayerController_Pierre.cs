using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float walkSpeed = 3f;
    public float sprintSpeed = 5f;

    [Header("Keybinds")]
    public KeyCode sprintKey = KeyCode.LeftShift;

    public GameObject playerCharacter;

    float horizontalInput;
    float verticalInput;

    private Animator animator;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
        animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        // Get input
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        // Animation
        if (animator != null)
        {
            float inputMagnitude = new Vector2(horizontalInput, verticalInput).magnitude;
            animator.SetFloat("Speed", inputMagnitude);
            animator.SetFloat("MoveX", horizontalInput);
            animator.SetFloat("MoveY", verticalInput);
        }

        HandleAttack();
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        float currentSpeed = Input.GetKey(sprintKey) ? sprintSpeed : walkSpeed;
        Vector2 moveDirection = new Vector2(horizontalInput, verticalInput).normalized;
        rb.linearVelocity = moveDirection * currentSpeed;
        if (moveDirection.sqrMagnitude > 0.01f)
        {
            float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
            playerCharacter.transform.rotation = Quaternion.Euler(0,0, angle-90f);
        }        

    }

    private void HandleAttack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (animator != null)
            {
                animator.SetTrigger("Attack");
            }
        }
    }
}