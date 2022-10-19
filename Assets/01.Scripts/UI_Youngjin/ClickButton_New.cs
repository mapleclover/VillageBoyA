using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickButton_New : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
   
    public float i;
    public void OnPointerEnter(PointerEventData eventData)
    {
        i = this.transform.localPosition.y;
        switch (i)
        {
            case 153.0f:
                Button_New.cur = 0;
                Button_New.Instance.Move();
                break;
            case 0.0f:
                Button_New.cur = 1;
                Button_New.Instance.Move();
                break;
            case -153.0f:
                Button_New.cur = 2;
                Button_New.Instance.Move();
                break;
        }
    }
    public void OnPointerExit(PointerEventData eventData)
    {

    }
}
