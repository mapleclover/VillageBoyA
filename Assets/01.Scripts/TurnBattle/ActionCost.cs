//작성자 : 유은호
//설명 : 전투시 행동력 관리 및 UI표시 Script
using UnityEngine;
using UnityEngine.UI;

public class ActionCost : MonoBehaviour
{
    [SerializeField]private int RemainingCost;
    public int NumberOfCrystal;
    public GameObject CrystalPrefab;
    public Transform ContentBox;
    
    [SerializeField]private GameObject[] Crystal;
    // Start is called before the first frame update
    void Start()
    {
        Crystal = new GameObject[NumberOfCrystal];
        for(int i = 0; i < NumberOfCrystal; i++)
        {
            GameObject obj = Instantiate(CrystalPrefab, ContentBox);
            Crystal[i] = obj;
        }
    }

    public void OnCharacterSelect(int autoSelectedSkillCost)
    {
        Color color = Color.white;
        color.a = 0.5f;
        for (int i = RemainingCost; i < RemainingCost + autoSelectedSkillCost; i++)
        {
            Crystal[i].GetComponent<Image>().color = color;
        }
    }

    public void OnHoverSkill(int manuallySelectedSKillCost, int autoSelectedSkillCost)
    {
        Color color = Color.white;
        color.a = 0.5f;
        if (manuallySelectedSKillCost > autoSelectedSkillCost)
        {
            for (int i = RemainingCost - (manuallySelectedSKillCost - autoSelectedSkillCost);
                 i < RemainingCost + autoSelectedSkillCost;
                 i++)
            {
                Crystal[i].GetComponent<Image>().color = color;
            }
        }
        else if (manuallySelectedSKillCost < autoSelectedSkillCost)
        {
            
        }
    }

    public void TotalCost(int costs)
    {
        RemainingCost -= costs;
        Color color = Color.white;
        color.a = 1.0f;
        for (int i = 0; i < NumberOfCrystal - costs; i++)
        {
            Crystal[i].GetComponent<Image>().color = color;
        }
        color.a = 0.2f;
        for (int i = NumberOfCrystal; i > NumberOfCrystal - costs; i--)
        {
            Crystal[i - 1].GetComponent<Image>().color = color;
        }
    }
}
