using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireMenu : MonoBehaviour
{
    public GameObject firePopUp;
    public static bool isMenu;
    public static bool isRest;
    public int heaf;
    public int maxHealth;
    bool playerInside;
    
    void Start()
    {
        firePopUp.SetActive(false);
        isRest = false;
        isMenu = false;
    }

    void Update()
    {
        // Only allow E when player is inside the box
        if (playerInside && Input.GetKeyDown(KeyCode.E))
        {
            if (isMenu)
                LeaveFire();
            else
                popUp();
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInside = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInside = false;
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
        LeaveFire();
    }

    public void LeaveFire()
    {
        firePopUp.SetActive(false);
        isMenu = false;
        Time.timeScale = 1f;
    }
}
