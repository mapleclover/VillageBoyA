using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickButton_New : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
   
    public int i;
    public void OnPointerEnter(PointerEventData eventData)
    {
        Button_New.cur = i;
    }
    public void OnPointerExit(PointerEventData eventData)
    {

    }
}