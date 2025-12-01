using System.Reflection;
using UnityEngine;

public class enemyHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    [SerializeField] private int health;
    [SerializeField] private float currentHealth;
    private Animator anim;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim = GetComponent<Animator>();
        health = maxHealth;
        currentHealth = health;
    }


    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health < currentHealth)
        {
            currentHealth = health;
            anim.SetTrigger("attacked");
        }
        if (health <= 0)
        {
            anim.SetTrigger("die");
        }
    }
    
    public void die()
    {
        Destroy(gameObject);
    }
}
