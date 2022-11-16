using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*[Serializable]
public struct ItemDataSample
{
    public EnhanceableItems orgData;
    //public ItemLevel level;
    public float GetUpgradeProb()
    {
        return orgData.possibility[(int)level];
    }
}*/


public class EnhanceableItems : MonoBehaviour
{
    public enum ItemLevel // 아이템레벨
    {
        Level_1, Level_2, Level_3, Level_4, Level_5
    }


    //public int itemLevel; // 아이템레벨

    public int[] AP; // 공격력
    public int[] EnchantCost; // 비용
    


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


   /* private void Start()
    {
        ItemLevel itemLevel = 0;
    }*/
}
