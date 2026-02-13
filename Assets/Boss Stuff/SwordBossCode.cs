using UnityEngine;

public class SwordBossCode : BossCode

{
    [SerializeField] private float moveSpeed;
    private Vector2 playerPos;
    void Update()
    {
        //detects which range player is in whilst also getting player position
        playerPos = UpdateMethod();

        if (isInShortRange == true)
        {
            shortRangeAttack();
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
    //if player out of range, move towards player until it is in range
    private void moveTowardsPlayer(Vector2 playerPos)
    {
        transform.position = Vector2.MoveTowards(transform.position,playerPos,moveSpeed * Time.deltaTime);
    }


    private void shortRangeAttack()
    {
        Debug.Log("Short Range Attack");
        
        int randomInt = Random.Range(0, 1);
        if(randomInt == 0)
        {
            quickSlash();
        }
        if(randomInt == 1)
        {
            bigSwordAttack();
        }
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
}
