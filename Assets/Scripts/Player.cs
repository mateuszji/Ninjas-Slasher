using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rb;
    public enum Players { WSAD, Arrows };
    public Players playerType;
    private string horizontalInput, verticalInput;
    private Vector2 movement;
    public Transform chainTarget;
    private KeyCode chainSpawnKey;
    [HideInInspector] public bool spawnedChain = false;

    void Start()
    {
        if (!animator) animator = GetComponent<Animator>();
        if (!rb) rb = GetComponent<Rigidbody2D>();

        if (playerType == Players.WSAD)
        {
            horizontalInput = "HorizontalWSAD";
            verticalInput = "VerticalWSAD";
            chainSpawnKey = KeyCode.Space;
        }
        else
        {
            horizontalInput = "HorizontalArrows";
            verticalInput = "VerticalArrows";
            chainSpawnKey = KeyCode.RightShift;
        }

        //Physics2D.IgnoreLayerCollision(6, 6);
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * PlayersManager.Instance.playerSpeed * Time.fixedDeltaTime);
    }

    void Update()
    {
        if (!GameManager.Instance.gameStarted) return;
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

        if (Input.GetKey(chainSpawnKey))
        {
            spawnedChain = true;
            Chain.Instance.ChainCheck();
        }
        else
        {
            spawnedChain = false;
            Chain.Instance.ChainCheck();
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == 8) // Enemy
        {
            GameManager.Instance.GameOver(this.playerType);
        }
    }
}
