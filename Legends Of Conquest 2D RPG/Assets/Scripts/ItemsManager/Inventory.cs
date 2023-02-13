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

    public void AddItems(ItemsManager item)
    {
        if (item.isStackable)
        {
            bool itemAlreadyInInventory = false;

            foreach(ItemsManager itemInventory in itemsList)
            {
                if(itemInventory.itemName == item.itemName)
                {
                    itemInventory.amount += item.amount;
                }
            }

            if (!itemAlreadyInInventory)
            {
                itemsList.Add(item);
            }
        }
        else
        {
            itemsList.Add(item);
        }
    }

    public List<ItemsManager> GetItemsList()
    {
        return itemsList;
    }
}
