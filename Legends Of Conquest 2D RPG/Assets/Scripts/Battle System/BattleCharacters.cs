using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleCharacters : MonoBehaviour
{
    [SerializeField] bool isPlayer;
    [SerializeField] string[] attacksAvaiable;

    public string characterName;
    public int currentHP, maxHP, currentMana, dexterity, defence, wpnPower, armorDefence;

    public bool IsPlayer()
    {
        return isPlayer;
    }

    public string[] AttackMovesAvaiable()
    {
        return attacksAvaiable;
    }

    public void TakeHPDamage(int damageToRecieve)
    {
        currentHP -= damageToRecieve;

        if(currentHP < 0)
        {
            currentHP = 0;
        }
    }
}
