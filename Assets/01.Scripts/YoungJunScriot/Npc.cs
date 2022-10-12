using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �ڿ��� Npc ��ũ���ͺ�.

[CreateAssetMenu(fileName = "new Npc",menuName = "new Npc/npc")]
public class Npc : ScriptableObject
{

    public string npcName;
    public NpcType npcType;
    public GameObject npcPrefab;

    public enum NpcType
    {
        Npc
    }

}
