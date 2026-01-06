using UnityEngine;

public class EnemyBulletScript : MonoBehaviour
{
    [SerializeField] private float force = 10f;
    [SerializeField] private int damage = 10;
    [SerializeField] private float lifetime;
    [SerializeField] private GameObject reflectedBulletPrefab; // assign PlayerBullet prefab
    private Vector2 initialVelocity;
    private Rigidbody2D rb;
    private GameObject player;
    [SerializeField] private GameObject bullet;
    

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0; // ensures straight flight
       

        player = GameObject.FindGameObjectWithTag("Player");

        Vector3 targetPos = player.GetComponent<Collider2D>().bounds.center;
        Vector3 direction = targetPos - transform.position;
        initialVelocity = direction.normalized * force;
        rb.linearVelocity = initialVelocity;

        Destroy(gameObject, lifetime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
       if (!collision.gameObject.CompareTag("Player")) return;

    PlayerMovement playerMovement =
        collision.gameObject.GetComponent<PlayerMovement>();

    if (!playerMovement.isParrying)
    {
        Physics2D.IgnoreCollision(
            collision.collider,
            GetComponent<Collider2D>()
        );

        collision.gameObject
            .GetComponent<health_player>()
            .TakeDamage(damage);

        Destroy(gameObject);
    }
    else
    {
        playerMovement.ReflectBullet(initialVelocity);
        Destroy(gameObject);
    }
    }
}