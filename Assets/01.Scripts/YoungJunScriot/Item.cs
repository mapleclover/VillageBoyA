using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[CreateAssetMenu(fileName = "new item", menuName = "new item/item")]
public class Item : ScriptableObject
{
    public string itemName;
    public int count;
    public NpcType itemType;
    public GameObject itemPrefab;
    public string itemInfo;
    public int itemPrice;
    public RawImage itemImage;
    public int value;


    public enum NpcType
    {
        Weapon,
        Armor,      
        Used,
        Ingredient
    }
}
