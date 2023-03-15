using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsAssets : MonoBehaviour
{
    public static ItemsAssets instance;
    [SerializeField] ItemsManager[] itemsAvaliable;
    void Start()
    {
        if(instance !=null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

  public ItemsManager GetItemAsset(string itemToGetName)
    {
        foreach(ItemsManager item in itemsAvaliable)
        {
            if(item.itemName == itemToGetName)
            {
                return item;
            }
        }
        return null;
    }
}
