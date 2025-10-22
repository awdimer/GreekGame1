using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // ───────────── Player Settings ─────────────
    [Header("Movement Settings")]
    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;
    [SerializeField] private float wallJumpPowerY;
    [SerializeField] private float wallJumpPowerX;

    [Header("Environment Layers")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;

    // ───────────── Knockback Settings ─────────────
    [Header("Knockback Settings")]
    [SerializeField] public float KBforce;           // Knockback force
    [SerializeField] public float KBtotaltime;       // Total knockback duration
    [SerializeField] public float KBcounter;         // Knockback counter
    public bool knockFromRight;                      // Direction of knockback

    // ───────────── Private Variables ─────────────
    private Rigidbody2D body;
    private BoxCollider2D boxCollider;
    private float horizontalInput;
    private float wallJumpCooldown;

    private bool isWallLeft = false;
    private bool isWallRight = false;

    // ───────────── Unity Methods ─────────────
    private void Awake()
    {
        // Cache components
        body = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        HandleInput();
        HandleDirectionFlip();
    }

    private void FixedUpdate()
    {
        if (KBcounter <= 0 && wallJumpCooldown > 0.2f)
        {
            HandleMovement();

            if (onWall() && !isGrounded())
            {
                body.gravityScale = 0;
                body.linearVelocity = Vector2.zero;
            }
            else
            {
                body.gravityScale = 5;
            }

            if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) && isGrounded())
            {
                groundJump();
            }

            if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) && onWall() && !isGrounded())
            {
                wallJump();
            }
        }
        else
        {
            HandleKnockback();
        }
    }

    // ───────────── Input & Movement ─────────────
    private void HandleInput()
    {
        horizontalInput = Input.GetAxis("Horizontal");
    }

    private void HandleDirectionFlip()
    {
        if (horizontalInput > 0.01f)
            transform.localScale = Vector3.one;
        else if (horizontalInput < -0.01f)
            transform.localScale = new Vector3(-1, 1, 1);
    }

    private void HandleMovement()
    {
        body.linearVelocity = new Vector2(horizontalInput * speed, body.linearVelocity.y);
    }

    // ───────────── Jump Methods ─────────────
    private void groundJump()
    {
        body.linearVelocity = new Vector2(body.linearVelocity.x, jumpPower);

        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow))
        {
            body.linearVelocity = new Vector2(body.linearVelocity.x, 0);
        }
    }

    private void wallJump()
    {
        if (isWallLeft && wallJumpCooldown > 0.2f)
        {
            body.linearVelocity = new Vector2(wallJumpPowerX, wallJumpPowerY);
        }
        else if (isWallRight && wallJumpCooldown > 0.2f)
        {
            body.linearVelocity = new Vector2(-wallJumpPowerX, wallJumpPowerY);
        }

        wallJumpCooldown = 0;
    }

    // ───────────── Knockback ─────────────
    private void HandleKnockback()
    {
        wallJumpCooldown += Time.deltaTime;

        if (isGrounded())
        {
            float knockDirection = knockFromRight ? -KBforce : KBforce;
            body.linearVelocity = new Vector2(knockDirection, KBforce);
        }

        KBcounter -= Time.deltaTime;
    }

    // ───────────── Collision Checks ─────────────
    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(
            boxCollider.bounds.center,
            boxCollider.bounds.size,
            0,
            Vector2.down,
            0.1f,
            groundLayer
        );

        return raycastHit.collider != null;
    }

    private bool onWall()
    {
        bool facingRight = transform.localScale.x > 0;

        if (facingRight)
        {
            RaycastHit2D hitRight = Physics2D.BoxCast(
                boxCollider.bounds.center,
                boxCollider.bounds.size,
                0,
                Vector2.right,
                0.1f,
                wallLayer
            );

            isWallRight = hitRight.collider != null;
            isWallLeft = false;
            return isWallRight;
        }
        else
        {
            RaycastHit2D hitLeft = Physics2D.BoxCast(
                boxCollider.bounds.center,
                boxCollider.bounds.size,
                0,
                Vector2.left,
                0.1f,
                wallLayer
            );

            isWallLeft = hitLeft.collider != null;
            isWallRight = false;
            return isWallLeft;
        }
    }
}