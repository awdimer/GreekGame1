using UnityEngine;

public class enemydammage : enemyHealth
{
    [SerializeField] private int damage = 10;

    public void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerMovement playerMovement = collision.gameObject.GetComponent<PlayerMovement>();
            health_player playerHealth = collision.gameObject.GetComponent<health_player>();
            PlayerCombat playerCombat = collision.gameObject.GetComponent<PlayerCombat>();

            if (playerCombat.isParrying != true)
            {
                playerMovement.KBcounter = playerMovement.KBtotaltime;
                playerMovement.knockFromRight = collision.transform.position.x <= transform.position.x;

                playerHealth.TakeDamage(damage);

                Object.FindFirstObjectByType<Hitstop>().Stop(0.5f);
            }
            else if(playerCombat.isParrying)
            {
                base.TakeDamage(playerCombat.SwordDamage);
            }
        }
    }
}