using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            FireMenu.isMenu = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            FireMenu.isMenu = false;

            if (FireMenu.isMenu)
            {
                FindObjectOfType<FireMenu>().LeaveFire();
            }
        }
    }
}