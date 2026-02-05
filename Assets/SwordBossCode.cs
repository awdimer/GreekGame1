using UnityEngine;

public class SwordBossCode : MonoBehaviour
{
    [SerializeField] private float detectionRadius;
    [SerializeField] private float attackRadius;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private float xRangeForUpAttack;
    [SerializeField] private float shortRange;
    [SerializeField] private float mediumRange;
    

    private Vector2 distanceFromPlayer;
    private float attackCooldownTime;

    void Update()
    {
        // Only run logic if player is inside detection radius
        if (DetectPlayer(out Vector2 playerPos))
        {
            distanceFromPlayer = CalculateDistanceFromPlayer(playerPos);

            Debug.Log("X distance: " + distanceFromPlayer.x +"  Y distance: " + distanceFromPlayer.y);
        }
    }

    // Detects player ONLY if inside radius
    public bool DetectPlayer(out Vector2 playerPos)
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, detectionRadius, playerLayer);

        if (hit != null)
        {
            playerPos = hit.transform.position;
            return true;
        }

        playerPos = Vector2.zero;
        return false;
    }

    // Calculates distance from boss to player
    public Vector2 CalculateDistanceFromPlayer(Vector2 playerPos)
    {
        return (Vector2)transform.position - playerPos;
    }

    // Visualize detection radius in Scene view
    
    
    private void AttackLogic(Vector2 distanceFromPlayer)
    {
        if(distanceFromPlayer.y <= 20 )
        {
            if(distanceFromPlayer.x <=shortRange)
            {
                shortRangeAttack();
            }
            else if(distanceFromPlayer.x <= mediumRange)
            {
                mediumRangeAttack();
            }
            else if(distanceFromPlayer.x > mediumRange)
            {
                longRangeAttack();
            }
        }
        else if(distanceFromPlayer.y <= 20 && distanceFromPlayer.x <= xRangeForUpAttack)
        {
           upAttack(); 
        }
    }

    private void shortRangeAttack()
    {
        
    }
    private void mediumRangeAttack()
    {
        
    }
    private void longRangeAttack()
    {
        
    }
    
    
    private void jumpAttack()
    {
        
    }
    private void comboAttack()
    {
        
    }
    private void upAttack()
    {
        
    }
    
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
