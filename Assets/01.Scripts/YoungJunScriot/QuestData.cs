using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//박영준 퀘스트 생성자
public class QuestData
{
    public string questName; // 퀘스트명.
    public int[] npcId; // 퀘스트 관련된 NPC id 모음.


    public QuestData(string name, int[] npc) // 생성자.
    {
        questName = name;
        npcId = npc;
    }
}
