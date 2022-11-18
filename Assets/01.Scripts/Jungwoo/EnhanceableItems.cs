//작성자 : 전정우
//설명 :
using System;
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