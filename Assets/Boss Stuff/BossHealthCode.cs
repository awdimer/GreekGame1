using System.Data;
using UnityEngine;
public class BossHealthCode : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    [SerializeField] public int health;
    [SerializeField] private float currentHealth;
    [SerializeField] private int maxStamina;
    [SerializeField] private int stamina;
    [SerializeField] private float currentStamina;
    [SerializeField] private int staminaRegen;
    private Animator anim;
    private float timer = 0f;

  void Start()
    {
        anim = GetComponent<Animator>();
        health = maxHealth;
        currentHealth = health;
        stamina = maxStamina;
        currentStamina = stamina;
    }

    void Update()
    {
        
    }

    public void UpdateMethod()
    {
        timer += Time.deltaTime; // Add the time since last frame

        if (timer >= 1f) // 1 second passed
        {
           RegainStamina(staminaRegen);
            timer = 0f; // Reset timer
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health < currentHealth)
        {
            currentHealth = health;
//            anim.SetTrigger("attacked");
        }
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

    private void RegainStamina(int staminaRegen)
    {
        if(stamina < maxStamina)
        {
            stamina += staminaRegen;
        }
       
    }

    public virtual void StunnedState()
    {
        
    }

    public virtual void healthMonitor()
    {
        
    }
    
    public void die()
    {
        Destroy(gameObject);
    }
    
}
