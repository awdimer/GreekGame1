using UnityEngine; 
using System.Collections;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IDropHandler
{
    public int id;
    private Inventory inv;
    void Start()
    {
        inv = GameObject.Find("FakeInventory").GetComponent<Inventory>();
    }
    public void OnDrop(PointerEventData eventData)
    {
        ItemData droppedItem = eventData.pointerDrag.GetComponent<ItemData>();
        if (inv.items[id].ID == -1)
        {
            droppedItem.transform.SetParent(this.transform);
            droppedItem.transform.position = this.transform.position;
        }
    }
}