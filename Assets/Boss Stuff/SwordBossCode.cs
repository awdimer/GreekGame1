
using UnityEngine;
using System.Collections;
public class SwordBossCode : BossCode

{

    [SerializeField] private float jumpHeight;
    [SerializeField] private float jumpDuration;
    [SerializeField] private float timeTillJump;
    [SerializeField] private float jumpAttackCoolDown;
    [SerializeField] private float jumpAttackRadius;
    [SerializeField] private GameObject jumpAttackPoint;
    private float timePassed = 0;

    private Vector2 jumpStartPos;
    private Vector2 jumpTargetPos;
    private float jumpTimer;
    private bool isJumping;

    [SerializeField] private float moveSpeed;
    [SerializeField] private GameObject attackPoint;
    [SerializeField] private float shortAttackRadius;
    [SerializeField] private float mediumAttackRadius;
    [SerializeField] private int damage;
    [SerializeField] private int stunTime;
    private float stunTimer = 0f;
    private Vector2 playerPos;
    private bool isAttacking;
   

    private SwordBossHealth bossHealth;
    private Animator anim;
    [SerializeField] public Animator animator;
    private bool facingRight = false;

    private void Awake()
    {
        bossHealth = GetComponent<SwordBossHealth>();
    }

    void Update()
    {
        //detects which range player is in whilst also getting player position
        playerPos = UpdateMethod();
        Debug.Log("Jumping: " + isJumping + " Attacking: " + isAttacking + " time count " + timePassed);
        FacePlayer(playerPos);
        if (isJumping)
        {
            HandleJump();
            return;
        }
        if (bossHealth.isStunned || isAttacking)
        return;
            if (isInShortRange == true)
            {
                animator.SetBool("isWalking", false);
                shortRangeAnimBegin();
                timePassed = 0;
            }
            else if (isInMediumRange == true)
            {
                animator.SetBool("isWalking", false);
                mediumRangeAttack();
                timePassed = 0;
            }
            else if (isInLongRange == true)
            {
                
                longRangeAttack();
                moveTowardsPlayer(playerPos);
            }
            else if (isAbove == true)
            {
                animator.SetBool("isWalking", false);
                upAttack();
            }
            else
            {
                animator.SetBool("isWalking", true);
                Debug.Log("Player Outside of range!");
                moveTowardsPlayer(playerPos);
                
            }
    }

        
    
    //if player out of range, move towards player until it is in range
    private void moveTowardsPlayer(Vector2 playerPos)
    {
        transform.position = Vector2.MoveTowards(transform.position,playerPos,moveSpeed * Time.deltaTime);
    }


    private void shortRangeAnimBegin()
    {
        //Debug.Log("Short Range Attack");
        
        //int randomInt = Random.Range(0, 1);
        //if(randomInt == 0)
        //{
        //    quickSlash();
        //}
        //if(randomInt == 1)
        //{
        //    bigSwordAttack();
        //}
    if (isAttacking)
        return;

    isAttacking = true;
    animator.SetTrigger("shortRangeAttack");
    }

    private void mediumRangeAttack()
    {
        //int randomInt = Random.Range(0, 1);
        
    }

    private void longRangeAttack()
    {
        //int randomInt = Random.Range(0, 1);
        if (!isAttacking && !isJumping)
        {
            timePassed += Time.deltaTime;
            if(timePassed >= timeTillJump)
            {
                jumpAttack();
                timePassed = 0;
            }
            
        }
        
    }

    private void upAttack()
    {
        
    }

    private void jumpAttack()
    {
    
    if (isJumping) return;

    
    isJumping = true;

    jumpStartPos = transform.position;
    jumpTargetPos = playerPos;   // Player position already stored in Update()
    jumpTimer = 0f;

    //animator.SetTrigger("jumpAttack"); /
    }

    private void bigSwordAttack()
    {
        
    }

    private void upwardSlash()
    {
        
    }
    private void quickSlash()
    {
        
    }


    public void shortRangeAttack()
{
    
    

    Collider2D[] targets = Physics2D.OverlapCircleAll(attackPoint.transform.position,shortAttackRadius,playerLayer);

    foreach (Collider2D target in targets)
    {
        testPlayerMovement player = target.GetComponent<testPlayerMovement>();
        int staminaDamage = player.SwordDamage;
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
            bossHealth.DrainStamina(staminaDamage);
            Debug.Log("Stamina Drained " + staminaDamage);
        }
    }
}

    public void endShortRangeAttack()
        {
            StartCoroutine(attackCooldown(1));
        }

   // public void stunned()
    //{
    //    animator.SetBool("isStunned",true);
    //    isStunned = true;


    //}

    public void jumpSmashAttack()
        {
        
        isAttacking = true;
        

        Collider2D[] targets = Physics2D.OverlapCircleAll(jumpAttackPoint.transform.position,jumpAttackRadius,playerLayer);

        foreach (Collider2D target in targets)
            {
                testPlayerMovement player = target.GetComponent<testPlayerMovement>();
                int staminaDamage = player.SwordDamage;
                if (player == null)
                {
                    continue;
                }


                health_player health = target.GetComponent<health_player>();

                if (health != null)
                    health.TakeDamage(damage);
                
            }
            StartCoroutine(attackCooldown(1));
        }

    public IEnumerator attackCooldown(int cooldownTime)
    {
        yield return new WaitForSeconds(cooldownTime);
        animator.SetTrigger("attackEnd");
        isAttacking = false;
    }


    private void HandleJump()
{
    jumpTimer += Time.deltaTime;
    float progress = jumpTimer / jumpDuration;

    if (progress >= 1f)
    {
        transform.position = jumpTargetPos;
        isJumping = false;
        
        jumpSmashAttack();
        // You can trigger damage here if it's a slam attack
        

        return;
    }

    // Horizontal movement (linear)
    Vector2 horizontalPos = Vector2.Lerp(jumpStartPos, jumpTargetPos, progress);

    // Vertical arc (parabola)
    float height = 4 * jumpHeight * progress * (1 - progress);

    transform.position = horizontalPos + Vector2.up * height;
}

public void ResetAttackState()
{
    isAttacking = false;
}


public void OnDrawGizmosSelected()
    {

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.transform.position, detectionRadius);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(attackPoint.transform.position, shortRange);
        
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(attackPoint.transform.position, mediumRange);
        
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(attackPoint.transform.position, longRange);

        Gizmos.color = Color.cyan;

        // Draw a ray upward from the boss
        Vector3 start = transform.position;
        Vector3 end = transform.position + Vector3.up * xRangeForUpAttack;

        Gizmos.DrawLine(start, end);
    }

    public void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackPoint.transform.position, shortAttackRadius);
        Gizmos.DrawWireSphere(jumpAttackPoint.transform.position, jumpAttackRadius);
    }


private void FacePlayer(Vector2 playerPos)
{
    if (playerPos.x > transform.position.x && !facingRight)
        Flip();
    else if (playerPos.x < transform.position.x && facingRight)
        Flip();
}

private void Flip()
{
    facingRight = !facingRight;
    Vector3 scale = transform.localScale;
    scale.x *= -1;
    transform.localScale = scale;
}

}


