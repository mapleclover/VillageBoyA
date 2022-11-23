//�ۼ��� : ����ȣ
//���� : ������ �ൿ�� ���� �� UIǥ�� Script

using UnityEngine;
using UnityEngine.EventSystems;

public class TestButtonScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        manager.OnHoverSkill(cost, previousCost);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        manager.OnHoverSkillExit(previousCost);
    }

    public int previousCost;
    public int cost;
    public ActionCost manager;
}
