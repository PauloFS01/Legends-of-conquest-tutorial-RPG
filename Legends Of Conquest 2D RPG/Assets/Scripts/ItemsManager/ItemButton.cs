using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemButton : MonoBehaviour
{
    public ItemsManager itemOnButton;

    public void Press()
    {
        if (MenuManager.instance.menu.activeInHierarchy)
        {
        MenuManager.instance.itemName.text = itemOnButton.name;
        MenuManager.instance.itemDesctiption.text = itemOnButton.itemDescription;

        MenuManager.instance.activeItem = itemOnButton;
        }

        if (ShopManager.instance.shopMenu.activeInHierarchy)
        {
            if (ShopManager.instance.buyPanel.activeInHierarchy)
            {
                ShopManager.instance.SelectedBuyItem(itemOnButton);
            }
            else if (ShopManager.instance.sellPanel.activeInHierarchy)
            {
                ShopManager.instance.SelectedSellItem(itemOnButton);
            }
        }
        if (BattleManager.instance.itemsToUseMenu.activeInHierarchy)
        {
            BattleManager.instance.SelectedItemToUse(itemOnButton);
        }
    }
}
