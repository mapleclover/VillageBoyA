using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;




public enum EnhanceableItem // 강화 가능한 아이템인지
{
    Possible, Impossible
}


[CreateAssetMenu(fileName = "new item", menuName = "new item/item")]
public class Item : ScriptableObject
{
    public string itemName;
    public ItemType itemType;
    public GameObject itemPrefab;
    public string itemInfo;
    public int itemPrice;
    public Sprite itemImage;
    public int value;

    // 강화 ---------------------------------

    [Header("------Enhancement-------")]
    
    [SerializeField] EnhanceableItem _enhanceableItem;
    public EnhanceableItem enhanceableItem // 강화 가능 아이템
    {
        get => _enhanceableItem;
    }




    // 강화 ----------------------------------


    
    public enum ItemType
    {
        Weapon,
        Armor,      
        Used,
        Ingredient
    }
}
