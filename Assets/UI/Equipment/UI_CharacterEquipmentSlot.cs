using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class UI_CharacterEquipmentSlot : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        Item item = GetComponent<ItemData>().item;
        Debug.Log(item);
    }
}