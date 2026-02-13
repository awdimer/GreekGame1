using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;
public class ItemData : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Item item;
    public int amount;
    public int slot;
    private Inventory inv;
    private Tooltip tooltip;
    private Vector2 offset;
    
    void Start()
    {
        inv = GameObject.Find("FakeInventory").GetComponent<Inventory>();
        tooltip = inv.GetComponent<Tooltip>();
        
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        if(item != null)
        {
            offset = eventData.position - new Vector2(this.transform.position.x, this.transform.position.y);
            this.transform.SetParent(this.transform.parent.parent);
            this.transform.position = eventData.position - offset;
            GetComponent<CanvasGroup>().blocksRaycasts = false;
        }
    }
    public void OnDrag(PointerEventData eventData)
    {
        if(item != null)
        {
            this.transform.position = eventData.position - offset;
        }
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        this.transform.SetParent(inv.slots[slot].transform);
        Debug.Log("Ending drag — slot = " + slot);
        this.transform.position = inv.slots[slot].transform.position;
        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }
    public void OnPointerDown(PointerEventData eventData)
    { 
        if(item != null)
        {
            offset = eventData.position - new Vector2(this.transform.position.x, this.transform.position.y);
        }
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        tooltip.Activate(item);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        tooltip.Deactivate();
    }
} 