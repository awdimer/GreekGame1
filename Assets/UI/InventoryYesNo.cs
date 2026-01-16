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
        isOpen = true;
    }
    //Update is called once per frame

    public void InventoryOn()
    {
        if(isOpen = false)
        {
            
        }
    }
    public void InventoryOff()
    {
        
    }
}
