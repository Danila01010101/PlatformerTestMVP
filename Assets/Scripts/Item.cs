using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[CreateAssetMenu]
public class Item:ScriptableObject
{
    public enum ItemType
    {
        Sword,
        Knife,
        HealPotion,
        Talisman
    }
    public ItemType itemType;
    public int amount;
}