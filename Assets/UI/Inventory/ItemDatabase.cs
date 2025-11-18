using UnityEngine;
using System.Collections;
using LitJson;
using System.Collections.Generic;
using System.IO;
public class ItemDatabase : MonoBehaviour
{
    private List<Item> database = new List<Item>();
    private JsonData itemData;

    void Start()
    {
        //Item item = new Item(0, "ball", 5);
        //database.Add(item);
        //Debug.Log(database[0].Title); 
        itemData = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + "/StreamingAssets/Items.json"));
    }
    void ConstructItemDatabase()
    {
        for (int i = 0; i < itemData.Count; i++)
        {
            //database.Add(new Item((int)itemData[i]["id"], itemData[i]["title"].ToString,itemData[i]["value"]));
        }
    }
}
public class Item
{
    public int ID { get; set; }
    public string Title { get; set; }
    public int Value { get; set; }
    public Item(int id, string title, int value)
    {
          this.ID = id; 
          this.Title = title;
          this.Value = value;
    }
}