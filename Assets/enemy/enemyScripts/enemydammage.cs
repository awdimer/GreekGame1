using UnityEngine;

public class enemydammage : MonoBehaviour
{
    [SerializeField] private int damage = 10;

    private enemyHealth enemyHealth; // cache reference
    private enemy_mov enemy_mov;

    private void Awake()
    {
        // get the component once when the object loads
        enemyHealth = GetComponent<enemyHealth>();
        enemy_mov = GetComponent<enemy_mov>();
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerMovement playerMovement = collision.gameObject.GetComponent<PlayerMovement>();
            health_player playerHealth = collision.gameObject.GetComponent<health_player>();

            if (!playerMovement.isParrying)
            {
                playerMovement.KBcounter = playerMovement.KBtotaltime;
                playerMovement.knockFromRight = collision.transform.position.x <= transform.position.x;

                playerHealth.TakeDamage(damage);

                Object.FindFirstObjectByType<Hitstop>().Stop(0.5f);
            }
            else
            {
                // use the cached reference
                enemyHealth.TakeDamage(playerMovement.SwordDamage);
                enemy_mov.knockBack();
            }
        }
    }
}