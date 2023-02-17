using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsManager : MonoBehaviour
{
    public enum ItemType { Item, Weapon, Armor };
    public ItemType itemType;

    public string itemName, itemDescription;
    public int valueIncoins;
    public Sprite itemsImage;

    public int amoutOfAffect;

    public enum AffectType { HP, Mana };
    public AffectType affectType;

    public int weaponDexterity, armorDefence;

    public bool isStackable;
    public int amount;

    public void UseItem(int chatacterToUseOn)
    {
        PlayerStats selectCaracter = GameManager.instance.GetPlayerStats()[chatacterToUseOn];
        if(itemType == ItemType.Item)
        {
            if(affectType == AffectType.HP)
            {
                selectCaracter.AddHp(amoutOfAffect);
            }
            else if(affectType == AffectType.Mana)
            {
                selectCaracter.AddMana(amoutOfAffect);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // print("This is: " + itemName);
            Inventory.instance.AddItems(this);
            SelfDestroy();
        }
    }

    public void SelfDestroy()
    {
        gameObject.SetActive(false);
    }
}
