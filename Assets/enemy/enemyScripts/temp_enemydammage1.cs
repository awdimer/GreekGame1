using UnityEngine;

public class temp_enemydammage : MonoBehaviour
{
    [SerializeField] private int damage = 10;

    private health_player playerHealth; // cache reference



    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            testPlayerMovement testPlayerMovement = collision.gameObject.GetComponent<testPlayerMovement>();
            health_player playerHealth = collision.gameObject.GetComponent<health_player>();

            
            playerHealth.TakeDamage(10);
            Object.FindFirstObjectByType<Hitstop>().Stop(0.5f);
            
        }
    }
}