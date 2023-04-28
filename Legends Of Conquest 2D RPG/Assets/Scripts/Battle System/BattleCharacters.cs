using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleCharacters : MonoBehaviour
{
    [SerializeField] bool isPlayer;
    [SerializeField] string[] attacksAvaiable;

    public string characterName;
    public int currentHP, maxHP, maxMana, currentMana, dexterity, defence, wpnPower, armorDefence;

    public Sprite deadSprite;
    public ParticleSystem deathParticle;
    public bool isDead;

    private void Update()
    {
        if(!isPlayer && isDead)
        {
            FadeOutEnemy();
        }
    }

    public void FadeOutEnemy()
    {
        GetComponent<SpriteRenderer>().color = new Color(
            Mathf.MoveTowards(GetComponent<SpriteRenderer>().color.r, 1f, 0.3f * Time.deltaTime),
            Mathf.MoveTowards(GetComponent<SpriteRenderer>().color.g, 0f, 0.3f * Time.deltaTime),
            Mathf.MoveTowards(GetComponent<SpriteRenderer>().color.b, 0f, 0.3f * Time.deltaTime),
            Mathf.MoveTowards(GetComponent<SpriteRenderer>().color.a, 0f, 0.3f * Time.deltaTime)
            );

        if(GetComponent<SpriteRenderer>().color.a == 0)
        {
            gameObject.SetActive(false);
        }
    }

    public void KillEnemy()
    {
        isDead = true;
    }

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
    public void UseItemInTheBattle(ItemsManager itemToUse)
    {
        if(itemToUse.itemType == ItemsManager.ItemType.Item)
        {
            if(itemToUse.affectType == ItemsManager.AffectType.HP)
            {
                AddHP(itemToUse.amoutOfAffect);
            }
            else if(itemToUse.affectType == ItemsManager.AffectType.Mana)
            {
                AddMana(itemToUse.amoutOfAffect);
            }
        }
    }

    private void AddHP(int amoutOfAffect)
    {
        currentHP += amoutOfAffect;
    }
    private void AddMana(int amoutOfAffect)
    {
        currentMana += amoutOfAffect;
    }

    public void KillPlayer()
    {
        if (deadSprite)
        {
            GetComponent<SpriteRenderer>().sprite = deadSprite;
            Instantiate(deathParticle, transform.position, transform.rotation);
            isDead = true;
        }
    }
}
