using UnityEngine;
using System.Collections;
using LitJson;
using System.Collections.Generic;
public class ItemDatabase : MonoBehaviour
{
    private List<Item> database = new List<Item>();
    private JsonData itemData;
    void Start()
    {
        itemData = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + "/Items/Items.json"));
    }
}
public class Item
{
    
}