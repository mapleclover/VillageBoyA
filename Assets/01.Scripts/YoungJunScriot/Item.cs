using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;








[CreateAssetMenu(fileName = "new item", menuName = "new item/item")]
public class Item : ScriptableObject
{
    public string itemName;
    public NpcType itemType;
    public GameObject itemPrefab;
    public string itemInfo;
    public int itemPrice;
    public Sprite itemImage;
    public int value;

    // 강화 ------------------------------------

    public enum EnhanceableItem // 강화 가능한 아이템인지
    {
        Possible, Impossible
    }

    [Header("------Enhancement-------")]
    
    [SerializeField] EnhanceableItem _enhanceableItem;
    public EnhanceableItem enhanceableItem // 강화 가능 아이템
    {
        get => _enhanceableItem;
    }


    //public int itemLevel; // 아이템레벨

    public int[] AP; // 공격력
    public int[] EnchantCost; // 비용

    public int GetAP(int lv)
    {
        return AP[lv - 1];
    }
    public int GetEnchantCost(int lv)
    {
        return EnchantCost[lv - 1];
    }


    [SerializeField] float[] _possibility; // 확률
    public float[] possibility
    {
        get => _possibility;
    }

    public bool CheckSuccess(int level)
    {
        float rnd = UnityEngine.Random.Range(0f, 100f);
        if (rnd < _possibility[level])
        {
            return true;
        }
        return false;
    }



    // 강화 ----------------------------------



    public enum NpcType
    {
        Weapon,
        Armor,      
        Used,
        Ingredient
    }
}
