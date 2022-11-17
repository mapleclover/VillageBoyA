using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct SaveItemData
{
    public Item orgData;

    public int Itemlevel; // 인스펙터에 나올 수 있게 만들어
    public float GetPossibility(int level)
    {
        return orgData.possibility[(int)level];
    }
}

public class EnhanceableItems : MonoBehaviour
{


}
