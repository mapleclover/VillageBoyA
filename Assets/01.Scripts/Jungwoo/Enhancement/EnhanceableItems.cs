//작성자 : 전정우
//설명 :
using System;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;





public class EnhanceableItems : MonoBehaviour
{
    public SaveItemData myData;
    
    // public SaveItemData myData;

    public int _AP
    {
        get => myData.AP;
    }
    public int _EnchantCost
    {
        get => myData.EnchantCost;
    }
    public float _Possibility
    {
        get => myData.Possibility;
    }
    public bool _GetMaxLevel
    {
        get => myData.GetMaxLevel;
    }
    public bool _EnhanceableItem
    {
        get => myData.GetEnhanceableItem;
    }

 
    public void ConsumeGold()
    {
        DataController.instance.gameData.gold -= myData.EnchantCost;
    }

    public bool CheckSuccess()
    {
        float rnd = Random.Range(0, 100);
        if (rnd <= _Possibility)
        {
            Debug.Log($"랜덤숫자{rnd}");
            return true;
        }
        Debug.Log($"랜덤숫자{rnd}");

        return false;
    }
}