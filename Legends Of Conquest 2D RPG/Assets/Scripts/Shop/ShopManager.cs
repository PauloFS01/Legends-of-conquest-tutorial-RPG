using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public static ShopManager instance;

    public GameObject shopMenu, buyPanel, sellPanel;
    public List<ItemsManager> itemsForSale;

    [SerializeField] Text currentCoin;
    [SerializeField] GameObject itemSlotContainer;
    [SerializeField] Transform itemSlotBuyContainerParent;
    [SerializeField] Transform itemSlotSellContainerParent;

    [SerializeField] ItemsManager selectedItem;
    [SerializeField] Text buyItemName, buyItemDescription, buyItemValue;
    [SerializeField] Text sellItemName, sellItemDescription, sellItemValue;

    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenShopMenu()
    {
        shopMenu.SetActive(true);
        GameManager.instance.shopOpened = true;

        currentCoin.text = "R$: " +  GameManager.instance.currentCoin;
        buyPanel.SetActive(true);
    }

    public void CloseShopMenu()
    {
        shopMenu.SetActive(false);
        GameManager.instance.shopOpened = false;
    }

    public void OpenBuyPanel()
    {
        buyPanel.SetActive(true);
        sellPanel.SetActive(false);

        UpdateIemsShop(itemSlotBuyContainerParent, itemsForSale);

    }

    public void OpenSellPanel()
    {
        buyPanel.SetActive(false);
        sellPanel.SetActive(true);

        UpdateIemsShop(itemSlotSellContainerParent, Inventory.instance.GetItemsList());
    }

    private void UpdateIemsShop(Transform itemSlotContainerParent, List<ItemsManager> itemsToLookThrough)
    {
        foreach (Transform itemSlot in itemSlotContainerParent)
        {
            Destroy(itemSlot.gameObject);
        };
        foreach (ItemsManager item in itemsToLookThrough)
        {
            RectTransform itemSlot = Instantiate(itemSlotContainer, itemSlotContainerParent).GetComponent<RectTransform>();

            Image itemImage = itemSlot.Find("Items Image").GetComponent<Image>();
            itemImage.sprite = item.itemsImage;

            //Text itemsAmoutText = itemSlot.Find("Amount Text").GetComponent<Text>();

            /*        if(item.amount > 1)
                    {
                        itemsAmoutText.text = "";
                    }else
                    {
                        itemsAmoutText.text = "";
                    }*/

            itemSlot.GetComponent<ItemButton>().itemOnButton = item;

        }
    }

    public void SelectedBuyItem(ItemsManager itemsToBuy)
    {
        selectedItem = itemsToBuy;
        buyItemName.text = selectedItem.itemName;
        buyItemDescription.text = selectedItem.itemDescription;
        buyItemValue.text = "Value: " + selectedItem.valueIncoins;

    }

    public void SelectedSellItem(ItemsManager itemsToSell)
    {
        selectedItem = itemsToSell;
        sellItemName.text = selectedItem.itemName;
        sellItemDescription.text = selectedItem.itemDescription;
        sellItemValue.text = "Value: " + (int)selectedItem.valueIncoins * 0.75f;
    }

    public void BuyItem()
    {
        if(GameManager.instance.currentCoin >= selectedItem.valueIncoins)
        {
            GameManager.instance.currentCoin -= selectedItem.valueIncoins;
            Inventory.instance.AddItems(selectedItem);

            currentCoin.text = "R$: " + GameManager.instance.currentCoin;
        }
    }

    public void SellItem()
    {
        if (selectedItem)
        {
            GameManager.instance.currentCoin += (int)(selectedItem.valueIncoins * 0.75f);
            Inventory.instance.RemoveItem(selectedItem);

            currentCoin.text = "R$: " + GameManager.instance.currentCoin;
            selectedItem = null;

            OpenSellPanel();
        }
    }
}
