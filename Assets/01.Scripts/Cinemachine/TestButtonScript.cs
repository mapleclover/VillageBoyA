//작성자 : 유은호
//설명 : 전투시 행동력 관리 및 UI표시 Script

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
