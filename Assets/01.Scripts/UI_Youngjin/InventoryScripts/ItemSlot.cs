using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IDropHandler
{

    public void OnDrop(PointerEventData eventData)
    {
        PointerInfo icon = transform.GetComponentInChildren<PointerInfo>();
        if (icon != null)
        {
            ItemSlot slot = eventData.pointerDrag.transform.parent.GetComponent<ItemSlot>();
            slot.SetChildren(icon.transform);
        }

        eventData.pointerDrag.transform.SetParent(transform);
        eventData.pointerDrag.transform.localPosition = Vector3.zero;

    }
    public void SetChildren(Transform child)
    {
        child.SetParent(transform);
        child.localPosition = Vector3.zero;
    }

    
}
