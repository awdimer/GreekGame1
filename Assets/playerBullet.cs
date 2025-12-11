using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    [SerializeField] private int damage = 10;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0; // straight line
    }

    // Called when the bullet is reflected
    public void ShootOut(Vector2 originalVelocity, float speedMultiplier = 1f)
    {
        rb.linearVelocity = -originalVelocity * speedMultiplier;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("enemy"))
        {
            enemyHealth enemyHP = collision.gameObject.GetComponent<enemyHealth>();
            if (enemyHP != null)
                enemyHP.TakeDamage(damage);

            Destroy(gameObject);
        }
    }
}