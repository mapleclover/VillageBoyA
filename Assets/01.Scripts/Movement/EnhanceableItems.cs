using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct SaveItemData
{
    public Item orgData;

    public int Itemlevel; // �ν����Ϳ� ���� �� �ְ� �����
    public float GetPossibility(int level)
    {
        return orgData.possibility[(int)level];
    }
}

public class EnhanceableItems : MonoBehaviour
{


}
