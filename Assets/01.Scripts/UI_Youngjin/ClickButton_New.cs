//작성자 : 이영진
//설명 : 
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickButton_New : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public int i;

    public void OnPointerEnter(PointerEventData eventData)
    {
        //i = this.transform.localPosition.y;
        Button_New.cur = i;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
    }
    public void OnClick()
    {
        SoundTest.instance.PlaySE("SFX_Button");
    }
}