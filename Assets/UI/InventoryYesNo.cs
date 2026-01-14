using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class InventoryYesNo : MonoBehaviour
{
    public GameObject InventoryPanel;
    public static bool isOpen = true;
    //Start is called before the first frame update
    void Start()
    {
        InventoryPanel.SetActive(true);
    }
    //Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
           ToggleInventory();
        }

    }

    public void ToggleInventory()
    {
        if(isOpen = true)
        {
            isOpen = false;
            InventoryPanel.SetActive(false);
        }
        if(isOpen = false)
        {
            isOpen = true;
            InventoryPanel.SetActive(true);
        }
    }
}
