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
    public enum ItemLevel // �����۷���
    {
        Level_1, Level_2, Level_3, Level_4, Level_5
    }


    //public int itemLevel; // �����۷���

    public int[] AP; // ���ݷ�
    public int[] EnchantCost; // ���
    


    [SerializeField] float[] _possibility; // Ȯ��
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
