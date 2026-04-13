using UnityEngine;

public class AttackBoxScript : MonoBehaviour
{
    private BoxCollider2D box;

    [SerializeField] int damage = 10;

    private enemyHealth EnemyHealth;
    private enemy_mov Enemy_Mov;

    private bool hasHit = false;

    void Start()
    {
        box = GetComponent<BoxCollider2D>();

        // Get scripts from parent (enemy)
        EnemyHealth = GetComponentInParent<enemyHealth>();
        Enemy_Mov = GetComponentInParent<enemy_mov>();

        // Disable hitbox by default
        box.enabled = false;
    }

    // Called from animation
    public void EnableHitbox()
    {
        hasHit = false;
        box.enabled = true;
    }

    // Called from animation
    public void DisableHitbox()
    {
        box.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (hasHit) return;

        // Only react if it's actually the player
        testPlayerMovement player = other.GetComponentInParent<testPlayerMovement>();
        if (player == null) return;

        hasHit = true;
        attack(other);
    }

    public void attack(Collider2D Target)
    {
        testPlayerMovement player = Target.GetComponentInParent<testPlayerMovement>();
        if (player == null) return;

        health_player health = Target.GetComponentInParent<health_player>();

        int staminaDamage = player.SwordDamage;

        if (!player.isParrying)
        {
            if (health != null)
            {
                Debug.Log("Player takes damage");
                health.TakeDamage(damage);
            }
        }
        else
        {
            Debug.Log("Player parried");

            if (Enemy_Mov != null)
                Enemy_Mov.knockBack(player.transform.position);

            if (EnemyHealth != null)
                EnemyHealth.DrainStamina(staminaDamage);
        }
    }
}