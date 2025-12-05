using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;
public class ItemData : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Item item;
    public int amount;
    public int slot;
    private Transform originalParent;
    private Vector2 offset;
    public void OnBeginDrag(PointerEventData eventData)
    {
        if(item != null)
        {
            offset = eventData.position - new Vector2(this.transform.position.x, this.transform.position.y);
            originalParent = this.transform.parent;
            this.transform.SetParent(this.transform.parent.parent);
            this.transform.position = eventData.position - offset;
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
        this.transform.SetParent(originalParent);
        this.transform.position = originalParent.transform.position;
    }
} 