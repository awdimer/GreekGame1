using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class UIManager : MonoBehaviour
{
    public GameObject gameOverMenu;
    private void OnEnable()
    {
        health_player.OnPlayerDeath += EnableGameOverMenu;
    }
    private void OnDisable()
    {
        health_player.OnPlayerDeath -= EnableGameOverMenu;
    }
    public void EnableGameOverMenu()
    {
        gameOverMenu.SetActive(true);
    }
    public void RestartLevel()
    {
        SceneManager.LoadScene("scene1");
        Time.timeScale = 1f;
    }
    public void GoToMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
}