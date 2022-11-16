using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public struct ItemDataSample
{
    public Item orgData;
    public ItemLevel level;
    public float GetUpgradeProb()
    {
        return orgData.possibility[(int)level];
    }
}

public enum ItemLevel //��ȭ ���� 5����
{
    Level_1, Level_2, Level_3, Level_4, Level_5
}

public enum EnhanceableItem // ��ȭ ���� ����������
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

    // ��ȭ ---------------------------------

    [Header("------Enhancement-------")]
    // �ܺμ����Ұ�
    [SerializeField] EnhanceableItem _enhanceableItem;
    public EnhanceableItem enhanceableItem // ��ȭ ���� �� ����������
    {
        get => _enhanceableItem;
    }
    [SerializeField] float[] _possibility; // Ȯ��
    public float[] possibility
    {
        get => _possibility;
    }

    // �ܺμ���
    public int[] AP; // ���ݷ�
    public int[] EnchantCost; // ��ȭ���
    public ItemLevel itemLevel; // ������ ���� 5���� ����
    public bool CheckSuccess(int level)
    {
        float rnd = UnityEngine.Random.Range(0f, 100f);
        if (rnd < _possibility[level])
        {
            return true;
        }
        return false;
    }

    // ��ȭ ----------------------------------


    
    public enum NpcType
    {
        Weapon,
        Armor,      
        Used,
        Ingredient
    }
}
