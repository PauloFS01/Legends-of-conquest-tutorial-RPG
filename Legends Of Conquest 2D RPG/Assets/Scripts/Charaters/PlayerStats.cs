using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats instance;

    public string playerName;

    public Sprite characterImage;

    [SerializeField] int maxLevel = 50;
    public int playerLevel = 1;
    public int currentXP;
    
    [SerializeField] int baseLevelXp = 100;

    public int maxHP=100;
    public int currentHP;

    public int maxMana=30;
    public int currentMana=30;

    public int dexterity;
    public int defence;

    public int[] xpForEachLevel;


    void Start()
    {
        instance = this;

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

    public void AddHp(int amountHPToAdd)
    {
        currentHP += amountHPToAdd;
        if(currentHP > maxHP)
        {
            currentHP = maxHP;
        }
    }

    public void AddMana(int amountManaToAdd)
    {
        currentMana += amountManaToAdd;
        if (currentMana > maxMana)
        {
            currentMana = maxMana;
        }
    }
}
