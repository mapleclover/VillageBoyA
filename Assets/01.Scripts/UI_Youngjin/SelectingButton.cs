using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class SelectingButton : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    public GameObject myEmphasis;
    Vector2 pos = Vector2.zero;
    public void OnPointerEnter(PointerEventData eventData)
    {
        pos.y=gameObject.transform.localPosition.y;
        myEmphasis.transform.localPosition = pos;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        pos.y = gameObject.transform.localPosition.y;
        myEmphasis.transform.localPosition = pos;
    }
}
