using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_mov : MonoBehaviour
{
    public Transform[] patrolPoints;
    [SerializeField] public float moveSpeed;
    [SerializeField] public int patrolDestination;
    [SerializeField] private LayerMask groundLayer;

    [Header("Attack Settings")]
    private Vector2 playerPos;
    private Vector3[] directions = new Vector3[2];
    [SerializeField] private float range;
    [SerializeField] private LayerMask playerLayer;
    private bool attacking = false;

    private float lostPlayerTimer = 0f;
    [SerializeField] private float lostPlayerTime = 0.5f;

    // Components
    private Animator anim;
    private Rigidbody2D rb;
    private bool isPatrolling = true;
    private BoxCollider2D box;

    [Header("Knockback Settings")]
    [SerializeField] public float KBforce;
    [SerializeField] public float KBtotaltime;
    [SerializeField] public float gravityForce;
    private float KBcounter;
    public bool knockFromRight;
    private bool isGettingAttacked = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        box = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        // PATROLLING
        if (isPatrolling && KBcounter <= 0 && isGrounded())
        {
            Patrol();
        }
        else if (isPatrolling && KBcounter <= 0 && !isGrounded())
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, -gravityForce);
        }
        else if (isGettingAttacked)
        {
            KBcounter -= Time.deltaTime;

            if (KBcounter <= 0)
            {
                isGettingAttacked = false;
                isPatrolling = true;
            }
        }

        attackPlayer();
    }

    private void Patrol()
    {
        if (patrolDestination == 0)
        {
            transform.position = Vector2.MoveTowards(transform.position, patrolPoints[0].position, moveSpeed * Time.deltaTime);

            if (Vector2.Distance(transform.position, patrolPoints[0].position) < .2f)
            {
                transform.localScale = new Vector3(1, 1, 1);
                patrolDestination = 1;
            }
        }
        else if (patrolDestination == 1)
        {
            transform.position = Vector2.MoveTowards(transform.position, patrolPoints[1].position, moveSpeed * Time.deltaTime);

            if (Vector2.Distance(transform.position, patrolPoints[1].position) < .2f)
            {
                transform.localScale = new Vector3(-1, 1, 1);
                patrolDestination = 0;
            }
        }
    }

    // APPLY KNOCKBACK USING AddForce()
    public void knockBack()
    {
        isPatrolling = false;
        isGettingAttacked = true;
        attacking = false;

        KBcounter = KBtotaltime;

        rb.linearVelocity = Vector2.zero;

        Vector2 direction = knockFromRight ? Vector2.left : Vector2.right;

        rb.AddForce(new Vector2(direction.x * KBforce, KBforce), ForceMode2D.Impulse);
    }

    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(
            box.bounds.center,
            box.bounds.size,
            0,
            Vector2.down,
            0.1f,
            groundLayer
        );
        return raycastHit.collider != null;
    }

    private bool playerInSight(int i)
    {
        Debug.DrawRay(transform.position, directions[i], Color.red);

        RaycastHit2D hit = Physics2D.Raycast(transform.position, directions[i], range, playerLayer);

        if (hit.collider != null && isGettingAttacked == false)
        {
            Debug.Log("Detected Player");

            playerPos = hit.collider.transform.position;
            attacking = true;
            isPatrolling = false;
            lostPlayerTimer = 0f; // reset timer since we saw player

            return true;
        }

        return false;
    }

    private void CalculateDirections()
    {
        directions[0] = transform.right;          // right
        directions[1] = -transform.right;         // left
    }

    private void attackPlayer()
    {
        CalculateDirections();

        bool sawPlayerThisFrame = false;

        // Check all directions
        for (int i = 0; i < directions.Length; i++)
        {
            if (playerInSight(i))
            {
                sawPlayerThisFrame = true;
                break;
            }
        }

        // If no player detected this frame
        if (!sawPlayerThisFrame && attacking)
        {
            lostPlayerTimer += Time.deltaTime;

            if (lostPlayerTimer >= lostPlayerTime)
            {
                Debug.Log("Lost player, returning to patrol.");
                attacking = false;
                isPatrolling = true;
            }
        }

        // If still attacking, chase player
        if (attacking)
        {
            transform.position = Vector2.MoveTowards(
                transform.position,
                playerPos,
                moveSpeed * Time.deltaTime
            );
        }
    }
}
