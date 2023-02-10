using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour

{
    public static Inventory instance;
    private List<ItemsManager> itemsList;

    void Start()
    {
        instance = this;
        itemsList = new List<ItemsManager>();
        // Debug.Log("Adding item");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddItems(ItemsManager item)
    {
        print(item.itemName + " has be added to inventory");
        itemsList.Add(item);
        print(itemsList.Count);
    }
}
