using UnityEngine;

public class SwordBossCode : BossCode
{
    void Update()
    {
        UpdateMethod();

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
