using UnityEngine;

public class enemyHealth : MonoBehaviour
{
    [SerializeField] public int maxHealth;
    public int health;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        health = maxHealth;
    }


public void TakeDamage(int damage)
    {
        health -= damage;
        if(health <=0 )
        {
            Destroy(gameObject);
        }
    }
}
