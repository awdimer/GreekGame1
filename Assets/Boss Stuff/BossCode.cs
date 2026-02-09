using UnityEngine;

public class BossCode : MonoBehaviour
{
    [Header("Detection")]
    [SerializeField] protected float detectionRadius;
    [SerializeField] protected LayerMask playerLayer;

    [Header("Attack Ranges")]
    [SerializeField] protected float shortRange;
    [SerializeField] protected float mediumRange;
    [SerializeField] protected float longRange;
    [SerializeField] protected float xRangeForUpAttack;
    //attackCoolDowns will be specific per attack
    private protected float attackCooldownTimer;
    private protected bool isInShortRange;
    private protected bool isInMediumRange;
    private protected bool isInLongRange;
    private protected bool isAbove;

    void Update()
    {
        
    }


    public void UpdateMethod()
    {
        attackCooldownTimer -= Time.deltaTime;

        if (DetectPlayer(out Vector2 playerPos))
        {
            Vector2 distance = CalculateDistanceFromPlayer(playerPos);
            AttackLogic(distance);
        }
    }

    public bool DetectPlayer(out Vector2 playerPos)
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position,detectionRadius,playerLayer);

        if (hit != null)
        {
            playerPos = hit.transform.position;
            return true;
        }

        playerPos = Vector2.zero;
        return false;
    }

    public Vector2 CalculateDistanceFromPlayer(Vector2 playerPos)
    {
        return playerPos - (Vector2)transform.position;
    }

    public void AttackLogic(Vector2 distance)
    {
        if (attackCooldownTimer > 0)
            return;
//returns abolute distance, meaning it can't be negative.
        float absX = Mathf.Abs(distance.x);
        float absY = Mathf.Abs(distance.y);

        // Up attack first (priority attack)
        if (absY > 1.5f && absX <= xRangeForUpAttack)
        {
            isAbove = true;
            isInShortRange = false;
            isInMediumRange = false;
            isInLongRange = false;
        }
        else if (absX <= shortRange)
        {
            isAbove = false;
            isInShortRange = true;
            isInMediumRange = false;
            isInLongRange = false;
        }
        else if (absX <= mediumRange)
        {
            isAbove = false;
            isInShortRange = false;
            isInMediumRange = true;
            isInLongRange = false;
        }
        else if (absX <= longRange)
        {
            isAbove = false;
            isInShortRange = false;
            isInMediumRange = false;
            isInLongRange = true;
        }
    }


    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, shortRange);
        
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, mediumRange);
        
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, longRange);

        Gizmos.color = Color.cyan;

        // Draw a ray upward from the boss
        Vector3 start = transform.position;
        Vector3 end = transform.position + Vector3.up * xRangeForUpAttack;

        Gizmos.DrawLine(start, end);
    }
}
