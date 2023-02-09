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
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
