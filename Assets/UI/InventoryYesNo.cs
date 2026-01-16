using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class InventoryYesNo : MonoBehaviour
{
    public GameObject InventoryPanel;
    public static bool isOpen = true;
    public void ToggleInventory()
    {
        isOpen = !isOpen;
        InventoryPanel.SetActive(isOpen);
    }
}
