using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//���� NPC
[CreateAssetMenu(fileName = "NPC", menuName = "NPC/npc")]
public class NPC : ScriptableObject
{
    public string npcName;
    public GameObject npcPrefab; 
}
