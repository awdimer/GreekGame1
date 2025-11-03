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
            

            if (playerMovement.isParrying != true)
            {
                playerMovement.KBcounter = playerMovement.KBtotaltime;
                playerMovement.knockFromRight = collision.transform.position.x <= transform.position.x;

                playerHealth.TakeDamage(damage);

                Object.FindFirstObjectByType<Hitstop>().Stop(0.5f);
            }
            else if(playerMovement.isParrying)
            {
                base.TakeDamage(playerMovement.SwordDamage);
            }
        }
    }
}