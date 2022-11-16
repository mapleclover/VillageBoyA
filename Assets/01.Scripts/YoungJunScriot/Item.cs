using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ItemLevel //강화 레벨 5까지
{
    Level_1, Level_2, Level_3, Level_4, Level_5
}

public enum EnhanceableItem // 강화 가능 아이템인지
{
    Possible, Impossible
}


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

    // 강화 ---------------------------------

    [Header("------Enhancement-------")]
    // 외부수정불가
    [SerializeField] EnhanceableItem _enhanceableItem;
    public EnhanceableItem enhanceableItem // 강화 가능 한 아이템인지
    {
        get => _enhanceableItem;
    }
    [SerializeField] float[] _possibility; // 확률
    public float[] possibility
    {
        get => _possibility;
    }

    // 외부수정
    public int[] AP; // 공격력
    public int[] EnchantCost; // 강화비용
    public ItemLevel itemLevel; // 아이템 레벨 5까지 설정
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


    
    public enum NpcType
    {
        Weapon,
        Armor,      
        Used,
        Ingredient
    }
}
