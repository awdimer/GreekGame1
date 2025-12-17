using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class health_player : MonoBehaviour
{
    public static event Action OnPlayerDeath;
    private PlayerMovement playerMovement;
    public int maxHealth = 100;
    public int health;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        health = maxHealth;
        playerMovement = GetComponent<PlayerMovement>();
    }


    public void TakeDamage(int damage)
    {
        if (playerMovement.isDodging)
        {
            Debug.Log("Player Dodged!");
            return; // exit before damage is applied
        }

        health -= damage;

        if (health <= 0)
        {
            Destroy(gameObject);
            OnPlayerDeath?.Invoke();
        }
    }
}
