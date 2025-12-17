using UnityEngine;

    
public class projectileEnemy : MonoBehaviour
{
    private Vector2 playerPos;
    [SerializeField] private float range;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private GameObject enemyAttackPoint;
    [SerializeField] private GameObject bulletPos;
    [SerializeField] private int damage;
    [SerializeField] private GameObject bullet;
    private float timer;
    private Animator anim;
    private Rigidbody2D rb;
    private BoxCollider2D box;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        box = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer >= 2)
        {
            timer = 0;
            detectPlayer();
        }
        
    }

    private void detectPlayer()
    {
        bool playerAttackable = PlayerInsideAttackRadius();
        if (playerAttackable)
        {
            shoot();
        }
    }

    private bool PlayerInsideAttackRadius()
{
    Collider2D hit = Physics2D.OverlapCircle(enemyAttackPoint.transform.position, range, playerLayer);

    if (hit != null)
    {
        playerPos = hit.transform.position;
        return true;
    }

    return false;
}
    private void shoot()
    {
        Instantiate(bullet, bulletPos.transform.position, Quaternion.identity);
        Debug.Log("player in range");
    }

    private void OnDrawGizmos()
    {
        if (enemyAttackPoint != null)
            Gizmos.DrawWireSphere(enemyAttackPoint.transform.position, range);
    }
}
