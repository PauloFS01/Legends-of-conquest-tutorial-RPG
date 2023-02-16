using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemButton : MonoBehaviour
{
    public ItemsManager itemOnButton;

    public void Press()
    {
        MenuManager.instance.itemName.text = itemOnButton.name;
        MenuManager.instance.itemDesctiption.text = itemOnButton.itemDescription;

        MenuManager.instance.activeItem = itemOnButton;
    }
}
