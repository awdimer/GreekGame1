using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireMenu : MonoBehaviour
{
    public health_player playerHealth;
    public GameObject firePopUp;
    public static bool isMenu;
    public static bool isRest;
    bool playerInside;
    public DiscoveryMenu discoveryMenu;
    public GameObject discoveryPopUp;
    public static bool isDiscoverMenu;
    void Start()
    {
        firePopUp.SetActive(false);
        isRest = false;
        isMenu = false;
        discoveryMenu = FindObjectOfType<DiscoveryMenu>();
        //discoveryMenu.SetActive(false);
        discoveryPopUp.SetActive(false);

    }

    void Update()
    {
        // Only allow E when player is inside the box
        if (playerInside && Input.GetKeyDown(KeyCode.Tab))
        {
            if (discoveryMenu != null && discoveryMenu.hasDiscovered) //if is true
            {
                
                if (isMenu)
                    LeaveFire();
                else
                    popUp();
            }
            else
            {
                discoverPopUp();
            }
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
            leaveDiscover();
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
        playerHealth = GameObject.FindWithTag("Player")?.GetComponent<health_player>();
        playerHealth.health = playerHealth.maxHealth;
        LeaveFire();
    }

    public void LeaveFire()
    {
        firePopUp.SetActive(false);
        isMenu = false;
        Time.timeScale = 1f;
    }
  public void discoverPopUp()
{
    discoveryPopUp.SetActive(true);
}

public void Discover()
{
    discoveryPopUp.SetActive(false);
    popUp();
}
public void leaveDiscover()
    {
        discoveryPopUp.SetActive(false);
    }
}
