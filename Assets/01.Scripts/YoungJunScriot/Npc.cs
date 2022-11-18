//작성자 : 박영준
//설명 : NPC

using UnityEngine;

[CreateAssetMenu(fileName = "new Npc", menuName = "ScriptableObject/NPC")]
public class Npc : ScriptableObject
{
    public string npcName;
    public NpcType npcType;
    public Sprite npcImage;
    public GameObject npcPrefab;

    public enum NpcType
    {
        Npc
    }
}