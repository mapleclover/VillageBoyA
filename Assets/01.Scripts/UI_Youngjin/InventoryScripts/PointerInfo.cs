using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PointerInfo : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler,IPointerClickHandler, IDragHandler, IBeginDragHandler, IEndDragHandler
{

    Vector2 pos = Vector2.zero;
    Vector2 dragOffset = Vector2.zero;
    public static PointerInfo instance = null;

    private void Awake()
    {
        instance = this;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        instance = this;
        AboutItem.instance.pointerenter(instance);

    }
    public void OnPointerExit(PointerEventData eventData)
    {
        AboutItem.instance.pointerexit();
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        AboutItem.instance.pointerclick();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        GetComponentInChildren<RawImage>().raycastTarget = false;
        dragOffset = (Vector2)transform.position - eventData.position;
        transform.parent.SetAsLastSibling();
    }
    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position + dragOffset;


    }
    public void OnEndDrag(PointerEventData eventData)
    {
        transform.localPosition = Vector3.zero;
        GetComponentInChildren<RawImage>().raycastTarget = true;
    }
}
