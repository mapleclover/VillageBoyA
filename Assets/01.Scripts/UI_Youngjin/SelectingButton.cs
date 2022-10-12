using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class SelectingButton : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    public GameObject myEmphasis;

    public void OnPointerEnter(PointerEventData eventData)
    {
        myEmphasis.SetActive(true);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        myEmphasis.SetActive(false);
    }
}
