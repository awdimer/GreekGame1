using UnityEngine;

public class player_health
{
    public int maxHealth = 100;
    public int health;
    void start()
    {
        health = maxHealth;
    }
public void takeDamage(int damage)
{
    health -= damage;
    if(health <= 0){
        Destroy(Player);
    }
}
}
