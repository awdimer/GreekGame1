using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

public class enemyHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    [SerializeField] private int health;
    [SerializeField] private float currentHealth;

    [SerializeField] private int maxStamina;
    [SerializeField] private int stamina;
    [SerializeField] private float currentStamina;

    public bool isStunned = false;
    [SerializeField] public int stunnedTime;
    private Animator anim;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim = GetComponent<Animator>();
        health = maxHealth;
        currentHealth = health;
        stamina = maxStamina;          
        currentStamina = stamina;
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

    public void DrainStamina(int damage)
    {
        stamina -= damage;
        if (stamina < currentStamina)
        {
           currentStamina = stamina;
        }
        if (stamina <= 0)
        {
            StunnedState();
        }
    }


    public virtual void StunnedState()
    {
        Debug.Log("stunnedState caused");
        isStunned = true;
        StartCoroutine(stunCoroutine(stunnedTime));
    }



    private IEnumerator stunCoroutine(int stunnedTime)
{
    anim.SetBool("isStunned", true);

    yield return new WaitForSeconds(stunnedTime);

    isStunned = false;
    currentStamina = maxStamina;
    anim.SetBool("isStunned", false);

    enemy_mov enemyAI = GetComponent<enemy_mov>();
    if (enemyAI != null)
    {
        enemyAI.ResetState(); // 👈 NEW
    }
}
}
