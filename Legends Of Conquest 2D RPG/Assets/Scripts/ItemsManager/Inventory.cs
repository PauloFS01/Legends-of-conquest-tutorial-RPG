using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour

{
    public static Inventory inventory;
    private List<ItemsManager> itemsList;
    void Start()
    {
        itemsList = new List<ItemsManager>();
        Debug.Log("Adding item");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddItems(ItemsManager item)
    {
        itemsList.Add(item);
    }
}
