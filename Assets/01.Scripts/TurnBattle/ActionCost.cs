//�ۼ��� : ����ȣ
//���� : ������ �ൿ�� ���� �� UIǥ�� Script

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

    public void OnHoverSkillExit(int SelectedSkillCost) //ĳ���� ��ų ���� ��ư ����Ʈ�� ������ ĳ���Ͱ� ���õǾ��ִ� ��ų�� �ڽ�Ʈ��ŭ ��¦ ���ϰ� ����
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

    public void OnHoverSkill(int newlySelectedCost, int previouslySelectedCost)//���� �����Ϸ��� ���콺�� �ø� ��ų �ڽ�Ʈ & ���� ���õǾ��ִ� ��ų �ڽ�Ʈ
    {
        int costDiff = newlySelectedCost - previouslySelectedCost;
        
        if (costDiff <= 0)//���� ������ ��ų�� �ڽ�Ʈ�� ���� ���õǾ��ִ� �ڽ�Ʈ ���� ������... 1 6 2 4
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
            //Debug.Log($"{i}��° ũ����Ż ��Ȱ��ȭ");
            Crystal[i - 1].GetComponent<Image>().color = color;
        }
    }

    private void ChangeToUnusedCost(int start, int end)
    {
        Color color = Color.white;
        color.a = 1.0f;
        for (int i = start; i <= end; i++)
        {
            //Debug.Log($"{i}��° ũ����Ż Ȱ��ȭ");
            Crystal[i - 1].GetComponent<Image>().color = color;
        }
    }

    private void ChangeToTempCost(int start, int end)
    {
        Color color = Color.white;
        color.a = 0.5f;
        for (int i = start; i <= end; i++)
        {
            //Debug.Log($"{i}��° ũ����Ż �ӽú�Ȱ��ȭ");
            Crystal[i - 1].GetComponent<Image>().color = color;
        }
    }
}