using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Input input;
    public float moveSpeed;
    public Vector2 moveDeltaInput;
    public Vector2 moveInput;
    Rigidbody2D rb;
    Animator animator;

    PlayerAttack playerAttack;
    public float animSpeed;

    void Awake()
    {
        input = new Input();

        input.Gameplay.Movement.performed += ctx => moveDeltaInput = ctx.ReadValue<Vector2>();
        input.Gameplay.Shoot.performed += ctx => playerAttack.Shoot(moveInput);

        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerAttack = GetComponent<PlayerAttack>();
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + new Vector2(moveDeltaInput.x, moveDeltaInput.y / 2f) * moveSpeed * Time.fixedDeltaTime);

        if(moveDeltaInput.x != 0 || moveDeltaInput.y != 0)
        {
            animator.SetFloat("X",moveDeltaInput.x);
            animator.SetFloat("Y",moveDeltaInput.y / 2f);

            moveInput = new Vector2(moveDeltaInput.x, moveDeltaInput.y / 2f);
        }
        animator.SetFloat("dX",moveDeltaInput.x);
        animator.SetFloat("dY",moveDeltaInput.y / 2f);

        animator.SetFloat("SPEED",Mathf.Abs(moveDeltaInput.x + moveDeltaInput.y / 2f));

        animator.speed =animSpeed;
    }

    void OnEnable()
    {
        input.Enable();
    }

    void OnDisable()
    {
        input.Disable();
    }
}
