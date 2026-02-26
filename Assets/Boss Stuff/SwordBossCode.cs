using UnityEngine;

public class SwordBossCode : BossCode

{
    [SerializeField] private float moveSpeed;
    [SerializeField] private GameObject attackPoint;
    [SerializeField] private float radius;
    [SerializeField] private int damage;
    [SerializeField] private int stunTime;
    private float stunTimer = 0f;
    private Vector2 playerPos;
    private bool isAttacking;
    private bool isStunned;

    private SwordBossHealth bossHealth;
    private Animator anim;
    [SerializeField] public Animator animator;


    private void Awake()
    {
        bossHealth = GetComponent<SwordBossHealth>();
    }

    void Update()
    {
        //detects which range player is in whilst also getting player position
        playerPos = UpdateMethod();
        if(!isStunned)
        {
            if (isInShortRange == true)
            {
                shortRangeAnimBegin();
            }
            else if (isInMediumRange == true)
            {
                mediumRangeAttack();
            }
            else if (isInLongRange == true)
            {
                longRangeAttack();
            }
            else if (isAbove == true)
            {
                upAttack();
            }
            else
            {
                Debug.Log("Player Outside of range!");
                moveTowardsPlayer(playerPos);
            }
        }
        else
        {
            stunTimer += Time.deltaTime;
            if (stunTimer >= stunTime)
            {
                isStunned = false;
                stunTimer = 0f;
            }
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
       shortRangeAttack();
    }

    private void mediumRangeAttack()
    {
        int randomInt = Random.Range(0, 1);
        Debug.Log("Medium Range Attack");
    }

    private void longRangeAttack()
    {
        int randomInt = Random.Range(0, 1);
        Debug.Log("Long Range Attack");
    }

    private void upAttack()
    {
        Debug.Log("Up Attack");
    }

    private void jumpAttack()
    {
        
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
    isAttacking = true;
    animator.SetTrigger("shortRangeAttack");

    Collider2D[] targets = Physics2D.OverlapCircleAll(attackPoint.transform.position,radius,playerLayer);

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
        }
    }
}

    public void endShortRangeAttack()
        {
            isAttacking = false;
        }

    public void stunned()
    {
        animator.SetBool("isStunned",true);
        isStunned = true;


    }
}


