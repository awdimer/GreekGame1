using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class menuManager : MonoBehaviour
{
    [SerializeField] private GameObject loadingBarObject;
    [SerializeField] private Image loadingBar;
    [SerializeField] private GameObject[] objectsToHide;
    [SerializeField] private string persistentGameplay = "persistent";
    [SerializeField] private string room1 = "room1";
    private List<AsyncOperation> scenesToLoad = new List<AsyncOperation>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        loadingBarObject.SetActive(false);
    }
    public void StartGame()
    {
        loadingBarObject.SetActive(true);
        HideMenu();
        scenesToLoad.Add(SceneManager.LoadSceneAsync(persistentGameplay));
        scenesToLoad.Add(SceneManager.LoadSceneAsync(room1,LoadSceneMode.Additive));
        StartCoroutine(progressLoadingBar());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void HideMenu()
    {
        for(int i = 0; i<objectsToHide.Length; i++)
        {
            objectsToHide[i].SetActive(false);
        }
    }
    private IEnumerator progressLoadingBar()
    {
        float loadProgress = 0f;
        for(int i = 0; i < scenesToLoad.Count; i++)
        {
            while (scenesToLoad[i].isDone)
            {
                loadProgress += scenesToLoad[i].progress;
                loadingBar.fillAmount = loadProgress / scenesToLoad.Count;
                yield return null;

            }
        }
    }
}
