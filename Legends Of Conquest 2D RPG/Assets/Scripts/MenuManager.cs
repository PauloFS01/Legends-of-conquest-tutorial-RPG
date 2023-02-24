using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuManager : MonoBehaviour
{
    [SerializeField] Image imageToFade;
    public GameObject menu;
    [SerializeField] GameObject[] statsButtons;

    public static MenuManager instance;

    private PlayerStats[] playerStats;
    [SerializeField] TextMeshProUGUI[] nameText, hpText, manaText, currentXpText, xpText;
    [SerializeField] Slider[] xpSlider;
    [SerializeField] Image[] characterImage;
    [SerializeField] GameObject[] characterPanel;

    [SerializeField] Text statName, statHP, statMana, statDex, statDef, equipedWeapons, statEquipedArmor;
    [SerializeField] Text statWeaponPower, statsArmorDefence;
    [SerializeField] Image characterStatImage;

    [SerializeField] GameObject itemSlotContainer;
    [SerializeField] Transform itemSlotContainerParent;

    public Text itemName, itemDesctiption;

    public ItemsManager activeItem;

    [SerializeField] GameObject characterChoicePanel;
    [SerializeField] TextMeshProUGUI[] itemsCharacterChoiceName;

    private void Start()
    {
        instance = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (menu.activeInHierarchy)
            {
                UpdateStats();
                menu.SetActive(false);
                GameManager.instance.gameMenuOpened = false;
            }else
            {
                menu.SetActive(true);
                GameManager.instance.gameMenuOpened = true;
            }
        }
    }

    public void UpdateStats()
    {
        playerStats = GameManager.instance.GetPlayerStats();
        for(int i = 0; i < playerStats.Length; i++)
        {
            characterPanel[i].SetActive(true);
            nameText[i].text = playerStats[i].playerName;
            hpText[i].text = "HP: " + playerStats[i].currentHP + "/" + playerStats[i].maxHP;
            manaText[i].text = "Mana: " + playerStats[i].currentMana + "/" + playerStats[i].maxMana;
            currentXpText[i].text = "CurrentXP: " + playerStats[i].currentXP;

            characterImage[i].sprite = playerStats[i].characterImage;

            xpText[i].text = playerStats[i].currentXP.ToString() + "/" + playerStats[i].xpForEachLevel[playerStats[i].playerLevel];
            xpSlider[i].maxValue = playerStats[i].xpForEachLevel[playerStats[i].playerLevel];
            xpSlider[i].value = playerStats[i].currentXP;
        }
    }

    public void StatsMenu()
    {
        for(int i = 0; i < playerStats.Length; i++)
        {
            statsButtons[i].SetActive(true);

            statsButtons[i].GetComponentInChildren<Text>().text = playerStats[i].playerName;
        }

        StatsMenuUpdate(0);
    }

    public void StatsMenuUpdate(int playerSelectedNumber)
    {
        PlayerStats playerSelected = playerStats[playerSelectedNumber];

        statName.text = playerSelected.playerName;

        statHP.text = playerSelected.currentHP.ToString() + "/" + playerSelected.maxHP;
        statMana.text = playerSelected.currentMana.ToString() + "/" + playerSelected.maxMana;

        statDex.text = playerSelected.dexterity.ToString();
        statDef.text = playerSelected.defence.ToString();

        characterStatImage.sprite = playerSelected.characterImage;

        equipedWeapons.text = playerSelected.equipedWeaponName;
        statEquipedArmor.text = playerSelected.equipedArmorName;

        statWeaponPower.text = playerSelected.weaponPower.ToString();
        statsArmorDefence.text = playerSelected.armorDefence.ToString();

    }
    public void UpdateItemsInventory()
    {
        foreach (Transform itemSlot in itemSlotContainerParent)
        {
            Destroy(itemSlot.gameObject);
        };
        foreach(ItemsManager item in Inventory.instance.GetItemsList())
        {
            RectTransform itemSlot = Instantiate(itemSlotContainer, itemSlotContainerParent).GetComponent<RectTransform>();

            Image itemImage = itemSlot.Find("Items Image").GetComponent<Image>();
            itemImage.sprite = item.itemsImage;

            //Text itemsAmoutText = itemSlot.Find("Amount Text").GetComponent<Text>();

            /*        if(item.amount > 1)
                    {
                        itemsAmoutText.text = item.amount.ToString();
                    }else
                    {
                        itemsAmoutText.text = "";
                    }*/

            itemSlot.GetComponent<ItemButton>().itemOnButton = item;

        }
    }
    public void DiscartItem()
    {
        // print(activeItem.itemName);
        Inventory.instance.RemoveItem(activeItem);
        UpdateItemsInventory();
    }

    public void UseItem(int selectedChatacter)
    {
        activeItem.UseItem(selectedChatacter);
        OpenCharacterChoicePanel();
        //DiscartItem();
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit from game");
    }

    public void FadeImage()
    {
        imageToFade.GetComponent<Animator>().SetTrigger("Start fading");
    }

    
    public void OpenCharacterChoicePanel()
    {
        characterChoicePanel.SetActive(true);
        if (activeItem)
        {
            for(int i = 0; i < playerStats.Length; i++)
            {
                PlayerStats activePlayer = GameManager.instance.GetPlayerStats()[i];
                itemsCharacterChoiceName[i].text = activePlayer.playerName;

                bool activePlayerAvaiable = activePlayer.gameObject.activeInHierarchy;
                itemsCharacterChoiceName[i].transform.parent.gameObject.SetActive(activePlayerAvaiable);
            }
        }

    }

    public void CloseChatacterChoicePanel()
    {
        characterChoicePanel.SetActive(false);
    }

}
