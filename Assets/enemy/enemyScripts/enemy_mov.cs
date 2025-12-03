using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_mov : MonoBehaviour
{
    public Transform[] patrolPoints;
    [SerializeField] public float moveSpeed;
    [SerializeField] public int patrolDestination;
    [SerializeField] private LayerMask groundLayer;

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
        else if(isPatrolling && KBcounter <= 0 && !isGrounded())
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

        KBcounter = KBtotaltime;

        // Clear existing velocity so knockback is consistent
        rb.linearVelocity = Vector2.zero;

        // Decide knockback direction
        Vector2 direction = knockFromRight ? Vector2.left : Vector2.right;

        // Apply knockback force
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
}
