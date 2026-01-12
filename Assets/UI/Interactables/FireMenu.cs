using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class FireMenu : MonoBehaviour
{
    public GameObject firePopUp;
    public static bool isMenu;
    public static bool isRest;

    void Start()
    {
        firePopUp.SetActive(false);
        isRest = false;
        isMenu = false;
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (isMenu)
                LeaveFire();
            else
                popUp();
        }
    }

    public void popUp()
    {
        firePopUp.SetActive(true);
        Time.timeScale = 0f;
        isMenu = true;
    }
    public void Rest()
    {
        firePopUp.SetActive(true);
        Time.timeScale = 0f;
        isMenu = true;
        LeaveFire();
    }

  public void LeaveFire()
{
    firePopUp.SetActive(false);
    isMenu = false;
    Time.timeScale = 1f;
}
}
