using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.UI;
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
        data = "<color=#0473f0><b>" + item.Title + "</b></color>\n\n" + item.Description + "<color=#C94262>"+"\nPower: " + item.Power +
        "</color>" + "<color=#7EA5B7><b>" + "\nDefense: " + item.Defence + "</color>" + "<color=#2F9879>" + "\nVitality: " + item.Vitality;
        tooltip.transform.GetChild(0).GetComponent<TMP_Text>().text = data;
    }
}