//작성자 : 박영준
//설명 :  
//수정자 : 전정우
using System;
using UnityEngine;
using Random = UnityEngine.Random;




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

    public int[] AP; // 공격력
    public int[] EnchantCost; // 비용
    public int[] Possibility; // 확률
    public int GetPossibility(int lv)
    {
        return Possibility[lv - 1];
    }
    public int GetAP(int lv)
    {
        return AP[lv - 1];
    }
    public int GetEnchantCost(int lv)
    {
        return EnchantCost[lv - 1];
    }
    public bool CheckSuccess(int level)
    {
        float rnd = Random.Range(0f, 100f);
        if (rnd < Possibility[level])
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
        Ingredient,
        Accessory
    }
}