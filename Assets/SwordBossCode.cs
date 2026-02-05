using UnityEngine;

public class SwordBossCode : MonoBehaviour
{
    [Header("Detection")]
    [SerializeField] private float detectionRadius;
    [SerializeField] private LayerMask playerLayer;

    [Header("Attack Ranges")]
    [SerializeField] private float shortRange;
    [SerializeField] private float mediumRange;
    [SerializeField] private float longRange;
    [SerializeField] private float xRangeForUpAttack;
    //attackCoolDowns will be specific per attack
    private float attackCooldownTimer;

    void Update()
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

    private void AttackLogic(Vector2 distance)
    {
        if (attackCooldownTimer > 0)
            return;
//returns abolute distance, meaning it can't be negative.
        float absX = Mathf.Abs(distance.x);
        float absY = Mathf.Abs(distance.y);

        // Up attack first (priority attack)
        if (absY > 1.5f && absX <= xRangeForUpAttack)
        {
            upAttack();
        }
        else if (absX <= shortRange)
        {
            shortRangeAttack();
        }
        else if (absX <= mediumRange)
        {
            mediumRangeAttack();
        }
        else if (absX <= longRange)
        {
            longRangeAttack();
        }
    }

    private void shortRangeAttack()
    {
        Debug.Log("Short Range Attack");
    }

    private void mediumRangeAttack()
    {
        Debug.Log("Medium Range Attack");
    }

    private void longRangeAttack()
    {
        Debug.Log("Long Range Attack");
    }

    private void upAttack()
    {
        Debug.Log("Up Attack");
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, shortRange);
        
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, mediumRange);
        
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, longRange);

        Gizmos.color = Color.purple;

        // Draw a ray upward from the boss
        Vector3 start = transform.position;
        Vector3 end = transform.position + Vector3.up * xRangeForUpAttack;

        Gizmos.DrawLine(start, end);
    }
}
