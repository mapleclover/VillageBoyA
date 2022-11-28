using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShowStatus : MonoBehaviour
{
    [SerializeField] private PC[] PCs;
    [SerializeField] private int CharacterIndex;
    public TextMeshProUGUI[] Texts;
    private GameData.myPartyStats character;
    public Image CharacterImage;
    [SerializeField]private Sprite[] images;

    public string IsAOEToString(int i)
    {
        if (PCs[CharacterIndex].IsAOE[i])
            return "전체공격";
        return "단일공격";
    }
    public string GetEquipmentName()
    {
        if (character.myUsedItems.Count == 0)
            return "장착된 장비 없음";
        else
            return character.myUsedItems[0];
    }
    
    public void OpenUpStatus()
    {
        if (DataController.instance.gameData.Kong.isLeader)
        {
            Show("Kong");
        }
        else if(DataController.instance.gameData.Jin.isLeader)
        {
            Show("Jin");
        }
        else if(DataController.instance.gameData.Ember.isLeader)
        {
            Show("Ember");
        }
    }

    public void Show(string characterName)
    {
        character = new GameData.myPartyStats();
        switch (characterName)
        {
            case "Kong":
                character = DataController.instance.gameData.Kong;
                CharacterIndex = 0;
                Texts[4].text = "검";
                break;
            case "Jin":
                character = DataController.instance.gameData.Jin;
                CharacterIndex = 1;
                Texts[4].text = "활";
                break;
            case "Ember":
                character = DataController.instance.gameData.Ember;
                CharacterIndex = 2;
                Texts[4].text = "창";
                break;
        }

        CharacterImage.sprite = images[CharacterIndex];
        Texts[0].text = $"{character.HP} / {PCs[CharacterIndex].Health}";
        Texts[1].text = $"{PCs[CharacterIndex].BaseAttackDamage+character.strength} (+{character.strength})";
        Texts[2].text = $"{character.defPower} (+{character.defPower})";
        Texts[3].text = $"{PCs[CharacterIndex].Speed}";
        Texts[5].text = $"{GetEquipmentName()}";
        Texts[6].text = $"{PCs[CharacterIndex].SkillName[0]}({IsAOEToString(0)})\n" +
            $"{PCs[CharacterIndex].SkillName[1]}({IsAOEToString(1)})\n" +
            $"{PCs[CharacterIndex].SkillName[2]}({IsAOEToString(2)})";
    }
}
