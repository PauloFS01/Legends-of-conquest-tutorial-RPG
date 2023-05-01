using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BattleRewardsHandler : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI XPText, itemsText;
    [SerializeField] GameObject rewardsScreen;

    [SerializeField] ItemsManager[] rewardItems;
    [SerializeField] int xpReward;
    public static BattleRewardsHandler instance;

    public bool markQuestAsComplete;
    public string questToComplete;

    private void Start()
    {
        instance = this;
    }

    public void OpenRewardScreen(int xpEarned, ItemsManager[] itemsEarned)
    {
        xpReward = xpEarned;
        rewardItems = itemsEarned;

        XPText.text = xpEarned + "XP";
        itemsText.text = "";

        foreach(ItemsManager rewardItemText in rewardItems)
        {
            itemsText.text = rewardItemText.itemName;
        }

        rewardsScreen.SetActive(true);
    }

    public void CloseBUtton()
    {
        foreach(PlayerStats activePlayer in GameManager.instance.GetPlayerStats())
        {
            if (activePlayer.gameObject.activeInHierarchy)
                activePlayer.AddXP(xpReward);
        }

        foreach(ItemsManager itemRewarded in rewardItems)
        {
            Inventory.instance.AddItems(itemRewarded);
        }

        rewardsScreen.SetActive(false);
        GameManager.instance.battleIsActive= false;

        if (markQuestAsComplete)
        {
            QuestManager.instance.MarkQuestComplete(questToComplete);
        }
    }
}
