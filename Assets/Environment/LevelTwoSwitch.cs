using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Cinemachine;

public class MoveCheckpoint : MonoBehaviour
{
    public GameObject LevelTwoStartThing;
    private RespawnScript respawn;
    [SerializeField] public CinemachineCamera spawnCamera;
    void Awake()
    {
        respawn = GameObject.FindGameObjectWithTag("Respawn").GetComponent<RespawnScript>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            respawn.respawnPoint = LevelTwoStartThing;
            respawn.cameraOnSpawn = spawnCamera;
        }
    }
}
