//작성자 : 유은호
//설명 : 전투시 행동력 관리 및 UI표시 Script

using UnityEngine;
using UnityEngine.UI;

public class ActionCost : MonoBehaviour
{
    [SerializeField] private int RemainingCost;
    public int NumberOfCrystal;
    public GameObject CrystalPrefab;
    public Transform ContentBox;
    public GameObject NotEnough;

    [SerializeField] private GameObject[] Crystal;

    // Start is called before the first frame update
    void Start()
    {
        Crystal = new GameObject[NumberOfCrystal];
        for (int i = 0; i < NumberOfCrystal; i++)
        {
            GameObject obj = Instantiate(CrystalPrefab, ContentBox);
            Crystal[i] = obj;
        }
        TotalCost(4);
    }

    public void OnHoverSkillExit(int SelectedSkillCost) //캐릭터 스킬 선택 버튼 리스트가 떴을때 캐릭터가 선택되어있던 스킬의 코스트만큼 살짝 진하게 변경
    {
        if (RemainingCost >= 0)
        {
            if (NotEnough.activeSelf)
            {
                NotEnough.SetActive(false);
            }
        }
        ChangeToUnusedCost(1, RemainingCost);
        ChangeToTempCost(RemainingCost + 1, RemainingCost + SelectedSkillCost);
    }

    public void OnHoverSkill(int newlySelectedCost, int previouslySelectedCost)//새로 선택하려고 마우스를 올린 스킬 코스트 & 전에 선택되어있던 스킬 코스트
    {
        int costDiff = newlySelectedCost - previouslySelectedCost;
        
        if (costDiff <= 0)//새로 선택한 스킬의 코스트가 전에 선택되어있던 코스트 보다 적으면... 1 6 2 4
        {
            ChangeToUnusedCost(RemainingCost + 1, RemainingCost - costDiff);// 2 3
            ChangeToTempCost(RemainingCost - costDiff + 1, RemainingCost + previouslySelectedCost); // 4 5
        }
        else
        {
            if (RemainingCost < costDiff)//1 6 4 2
            {
                ChangeToTempCost(1, RemainingCost + previouslySelectedCost);//1 2 3
                NotEnough.SetActive(true);
            }
            else//2 5 3 2
            {
                ChangeToTempCost(RemainingCost - costDiff + 1, RemainingCost + previouslySelectedCost);//2 3 4
            }
        }
    }

    public void TotalCost(int costs)
    {
        RemainingCost -= costs;
        if (RemainingCost < 0)
        {
            ChangeToUsedCost(1, NumberOfCrystal);
            NotEnough.SetActive(true);
        }
        else
        {
            if (NotEnough.activeSelf)
            {
                NotEnough.SetActive(false);
            }
            ChangeToUnusedCost(1, RemainingCost);
            ChangeToUsedCost(RemainingCost+1, NumberOfCrystal);
        }
    }

    private void ChangeToUsedCost(int start, int end)
    {
        Color color = Color.white;
        color.a = 0.2f;
        for (int i = start; i <= end; i++)
        {
            //Debug.Log($"{i}번째 크리스탈 비활성화");
            Crystal[i - 1].GetComponent<Image>().color = color;
        }
    }

    private void ChangeToUnusedCost(int start, int end)
    {
        Color color = Color.white;
        color.a = 1.0f;
        for (int i = start; i <= end; i++)
        {
            //Debug.Log($"{i}번째 크리스탈 활성화");
            Crystal[i - 1].GetComponent<Image>().color = color;
        }
    }

    private void ChangeToTempCost(int start, int end)
    {
        Color color = Color.white;
        color.a = 0.5f;
        for (int i = start; i <= end; i++)
        {
            //Debug.Log($"{i}번째 크리스탈 임시비활성화");
            Crystal[i - 1].GetComponent<Image>().color = color;
        }
    }
}