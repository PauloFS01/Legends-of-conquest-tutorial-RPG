using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] string playerName;

    [SerializeField] int maxLevel = 50;
    [SerializeField] int playerLevel = 1;
    [SerializeField] int currentXP;
    
    [SerializeField] int baseLevelXp = 100;

    [SerializeField] int maxHP=100;
    [SerializeField] int currentHP;

    [SerializeField] int maxMana=30;
    [SerializeField] int currentMana=30;

    [SerializeField] int dexterity;
    [SerializeField] int defence;

    [SerializeField] int[] xpForEachLevel;
    void Start()
    {
        xpForEachLevel = new int[maxLevel];
        xpForEachLevel[1] = baseLevelXp;

        for(int i = 2; i < xpForEachLevel.Length; i++)
        {
            xpForEachLevel[i] = (int)(0.02f * i * i + 3.06f * i * i + 105.6f * i);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            AddXP(100);
        }
    }

    public void AddXP(int amountXp)
    {
        currentXP += amountXp;
        if(currentXP > xpForEachLevel[playerLevel])
        {
            currentXP -= xpForEachLevel[playerLevel];
            playerLevel++;

            if(playerLevel % 2  == 0)
            {
                dexterity++;
            }
            else
            {
                defence++;
            }

            maxHP = Mathf.FloorToInt(maxHP * 1.18f);
            currentHP = maxHP;

            maxMana = Mathf.FloorToInt(maxMana * 1.06f);
            currentMana = maxMana;
        }
    }
}
