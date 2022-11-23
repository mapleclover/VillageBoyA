using UnityEngine;
using UnityEngine.EventSystems;

public class TestButtonScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        //manager.OnHoverSKill(cost);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //manager.OnHoverSkillCancel();
    }
    public int cost;
    public ActionCost manager;
}
