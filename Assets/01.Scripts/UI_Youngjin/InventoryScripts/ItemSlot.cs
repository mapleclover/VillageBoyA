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
        if (icon != null)
        {
            ItemSlot slot = movingObject.parent.GetComponent<ItemSlot>();
            slot.SetChildren(icon.transform);
        }

<<<<<<< HEAD
        eventData.pointerDrag.transform.SetParent(transform);
        eventData.pointerDrag.transform.localPosition = Vector3.zero;
=======
        movingObject.SetParent(transform);
        movingObject.localPosition = Vector3.zero;

>>>>>>> 09f870078b428a8e57993763e5ade4e5dab5b814
    }

    public void SetChildren(Transform child)
    {
        child.SetParent(transform);
        child.localPosition = Vector3.zero;
    }
}