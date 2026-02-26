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

    [SerializeField] private GameObject enemyAttackPoint;
    [SerializeField] private float radius;
    [SerializeField] private LayerMask players;
    [SerializeField] private int damage;

    private bool attackingplayer = false;
    private bool attacking = false;

    private float lostPlayerTimer = 0f;
    [SerializeField] private float lostPlayerTime = 1f;

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
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        // PATROLLING
        if (isPatrolling && KBcounter <= 0 && isGrounded() && lostPlayerTimer <= 0 && !attackingplayer)
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

        detectPlayer();
    }

    // NEW: Automatically face left/right
    private void FaceDirection(Vector2 direction)
    {
        if (direction.x > 0.01f)
            transform.localScale = new Vector3(1, 1, 1);   // face right
        else if (direction.x < -0.01f)
            transform.localScale = new Vector3(-1, 1, 1);  // face left
    }

    private void Patrol()
    {
        Vector2 targetPos = patrolPoints[patrolDestination].position;
        Vector2 moveDirection = targetPos - (Vector2)transform.position;

        FaceDirection(moveDirection);

        transform.position = Vector2.MoveTowards(
            transform.position,
            targetPos,
            moveSpeed * Time.deltaTime
        );

        if (Vector2.Distance(transform.position, targetPos) < .2f)
        {
            patrolDestination = (patrolDestination == 0 ? 1 : 0);
        }
    }

    // APPLY KNOCKBACK
    public void knockBack(Vector2 attackerPosition)
    {
        isPatrolling = false;
    isGettingAttacked = true;
    attacking = false;

    KBcounter = KBtotaltime;
    rb.linearVelocity = Vector2.zero;

    float direction = transform.position.x > attackerPosition.x ? 1 : -1;

    rb.AddForce(new Vector2(direction * KBforce, KBforce),ForceMode2D.Impulse);
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

        if (hit.collider != null && !isGettingAttacked)
        {
            playerPos = hit.collider.transform.position;
            attacking = true;
            isPatrolling = false;
            lostPlayerTimer = 0f;

            return true;
        }

        return false;
    }

    // NEW: Use overlap circle to detect if player is in attack radius
    private bool PlayerInsideAttackRadius()
    {
        Collider2D hit = Physics2D.OverlapCircle(
            enemyAttackPoint.transform.position,
            radius,
            players
        );

        return hit != null;
    }

    private void CalculateDirections()
    {
        directions[0] = transform.right;
        directions[1] = -transform.right;
    }

    private void detectPlayer()
    {
        CalculateDirections();

        bool sawPlayerThisFrame = false;
        bool playerAttackable = PlayerInsideAttackRadius();

        // Raycasts for sight (chase)
        for (int i = 0; i < directions.Length; i++)
        {
            if (playerInSight(i))
            {
                sawPlayerThisFrame = true;
                break;
            }
        }

        // Lost player timer
        if (!sawPlayerThisFrame && attacking)
        {
            lostPlayerTimer += Time.deltaTime;

            if (lostPlayerTimer >= lostPlayerTime)
            {
                attacking = false;
                isPatrolling = true;
                lostPlayerTimer = 0;
            }
        }

        // If chasing and not in attack radius → move toward player
        if (attacking && !playerAttackable)
        {
            Vector2 moveDirection = playerPos - (Vector2)transform.position;
            FaceDirection(moveDirection);

            transform.position = Vector2.MoveTowards(
                transform.position,
                playerPos,
                moveSpeed * Time.deltaTime
            );
        }

        // If player inside attack radius → trigger animation
        if (playerAttackable)
        { 
            Vector2 moveDirection = playerPos - (Vector2)transform.position;
            FaceDirection(moveDirection);
            anim.SetTrigger("attackingPlayer");
        }
    }

    // Animation Event — deals damage
    public void attack()
{
    attackingplayer = true;

    Collider2D[] targets = Physics2D.OverlapCircleAll(enemyAttackPoint.transform.position,radius,players);

    foreach (Collider2D target in targets)
    {
        testPlayerMovement player = target.GetComponent<testPlayerMovement>();

        if (player == null)
            continue;

        if (!player.isParrying)
        {
            health_player health = target.GetComponent<health_player>();

            if (health != null)
                health.TakeDamage(damage);
        }
        else
        {
            // Player parried → knock enemy back away from player
            knockBack(player.transform.position);
        }
    }
}


    public void endAttack()
    {
        attackingplayer = false;
        anim.SetTrigger("attackEnd");
        
    }

    private void OnDrawGizmos()
    {
        if (enemyAttackPoint != null)
            Gizmos.DrawWireSphere(enemyAttackPoint.transform.position, radius);
    }
}
