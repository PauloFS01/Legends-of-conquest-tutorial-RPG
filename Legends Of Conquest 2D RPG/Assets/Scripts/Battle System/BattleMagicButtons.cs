using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BattleMagicButtons : MonoBehaviour
{
    public string spellName;
    public int spellCost;

    public TextMeshProUGUI spellNameText, spellCostText;

    public void Press()
    {
        if (BattleManager.instance.GetCurrentActiveCaaracter().currentMana >= spellCost)
        {
            BattleManager.instance.magicChoicePannel.SetActive(false);
            BattleManager.instance.OpenTargetMenu(spellName);
            BattleManager.instance.GetCurrentActiveCaaracter().currentMana -= spellCost;
        }
    }
}
