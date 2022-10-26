using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


//ÀüÁ¤¿ì
//1025

public class TooltipController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    //public Tooltip tooltip;
    public GameObject myTool;

    public void OnPointerEnter(PointerEventData eventData)
    {
        myTool.SetActive(true);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        myTool.SetActive(false);
    }

}
