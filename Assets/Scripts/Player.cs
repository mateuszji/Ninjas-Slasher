using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rb;
    public enum Players { WSAD, Arrows };
    public Players playerType;
    private string horizontalInput, verticalInput;
    private Vector2 movement;

    void Start()
    {
        if (!animator) animator = GetComponent<Animator>();
        if (!rb) rb = GetComponent<Rigidbody2D>();

        if (playerType == Players.WSAD)
        {
            horizontalInput = "HorizontalWSAD";
            verticalInput = "VerticalWSAD";
        }
        else
        {
            horizontalInput = "HorizontalArrows";
            verticalInput = "VerticalArrows";
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * 10 * Time.fixedDeltaTime);
    }

void Update()
    {
        movement.x = Input.GetAxisRaw(horizontalInput);
        movement.y = Input.GetAxisRaw(verticalInput);

        if (movement.x != 0 || movement.y != 0)
            animator.SetBool("isWalking", true);
        else
            animator.SetBool("isWalking", false);

        if (movement.x == -1)
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        if (movement.x == 1)
            transform.localRotation = Quaternion.Euler(0, 0, 0);
    }
}
