//작성자 : 이영진
//설명 : 
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        PointerInfo icon = transform.GetComponentInChildren<PointerInfo>();
        Transform movingObject = eventData.pointerDrag.transform;
        if (icon != null&&movingObject.transform.parent.TryGetComponent(out ItemSlot slot))
        {
            
            slot.SetChildren(icon.transform);
        }
        else if (icon != null && movingObject.transform.parent.TryGetComponent(out reinforceslot Slot))
        {

            Slot.SetChild(icon.transform);
        }
     
        movingObject.SetParent(transform);
        movingObject.localPosition = Vector3.zero;
    }

    public void SetChildren(Transform child)
    {
        child.SetParent(transform);
        child.localPosition = Vector3.zero;
    }
}