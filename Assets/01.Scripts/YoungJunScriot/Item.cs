//작성자 : 박영준
//설명 :  
//수정자 : 전정우
using System;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public struct SaveItemStat
{
    public Item orgData;


    public int[] AP; // 공격력
    public int[] EnchantCost; // 비용





    //public ItemLevel level;
    public float GetUpgradeProb(int level)
    {
        return orgData.possibility[(int)level];
    }
}


[CreateAssetMenu(fileName = "new item", menuName = "ScriptableObject/Item")]
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
    public enum EnhanceableItem // 강화 가능한 아이템인지
    {
        Possible,
        Impossible
    }

    [Header("------Enhancement-------")] [SerializeField]
    EnhanceableItem _enhanceableItem;

    public EnhanceableItem enhanceableItem // 강화 가능 아이템
    {
        get => _enhanceableItem;
    }
/*
    public enum ItemLevel // 아이템레벨
    {
        Level_1,
        Level_2,
        Level_3,
        Level_4,
        Level_5
    }*/

    //public int itemLevel; // 아이템레벨



    [SerializeField] float[] _possibility; // 확률

    public float[] possibility
    {
        get => _possibility;
    }

    public bool CheckSuccess(int level)
    {
        float rnd = Random.Range(0f, 100f);
        if (rnd < _possibility[level])
        {
            return true;
        }

        return false;
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