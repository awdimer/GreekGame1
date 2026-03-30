using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Cinemachine;

public class CheckpointScript : MonoBehaviour
{
    [SerializeField] public CinemachineCamera spawnCamera;
    private RespawnScript respawn;
    void Start()
    {
        respawn = GameObject.FindGameObjectWithTag("Respawn").GetComponent<RespawnScript>();
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            respawn.respawnPoint = this.gameObject;
            respawn.cameraOnSpawn = spawnCamera;
        }
    }

}
