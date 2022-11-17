using UnityEngine;

// �ڿ��� Npc ��ũ���ͺ�.

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
