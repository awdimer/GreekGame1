using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnScript : MonoBehaviour
{
    public GameObject player;
    public GameObject respawnPoint;
    private void OnEnable()
    {
        health_player.OnPlayerDeath += RespawnDude;
    }
    private void OnDisable()
    {
        health_player.OnPlayerDeath -= RespawnDude;
    }
    public void RespawnDude()
    {
        player.transform.position = respawnPoint.transform.position;
    }
}