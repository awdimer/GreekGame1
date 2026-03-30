using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Cinemachine;

public class RespawnScript : MonoBehaviour
{
    public GameObject player;
    public GameObject respawnPoint;
    public Animator transition;
    [SerializeField] private float transitiontime;
    [HideInInspector]public CinemachineCamera cameraOnSpawn;
    [SerializeField] private CameraManager cameraManager;
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
        StartCoroutine(Respawn());
    }
    IEnumerator Respawn()
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitiontime);
        player.transform.position = respawnPoint.transform.position;
        cameraManager.currentCamera.Priority = 10;
        cameraOnSpawn.Priority = 20;
        yield return new WaitForSeconds(transitiontime);
        transition.SetTrigger("End");
    }
}