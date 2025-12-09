using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;
public class Tooltip : MonoBehaviour
{
    private Item item;
    private string data;
    private GameObject tooltip;
    void Start()
    {
        tooltip = GameObject.Find("Tooltip");
        tooltip.SetActive(false);
    }
    void Update()
    {
        if (tooltip.activeSelf)
        {
            tooltip.transform.position = Input.mousePosition;
        }
    }
    public void Activate(Item item)
    {
        this.item = item;
        ConstructDataString();
        tooltip.SetActive(true);
    }
    public void Deactivate()
    {
        tooltip.SetActive(false);
    }
    public void ConstructDataString()
    {
        data = "<color=#0473f0><b>" + item.Title + "</b></color>\n\n" + item.Description + "";
        tooltip.transform.GetChild(0).GetComponent<TMP_Text>().text = data;
    }

}