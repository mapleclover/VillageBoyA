using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;
public class UsingItemtoParty : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler,IPointerClickHandler
{
    Vector2 pos = Vector2.zero;
    public GameObject myArrow;
    public GameObject myPanel;
    public Transform myInventory;
    public GameObject myAlert;
    public TMPro.TMP_Text myText;
    GameObject obj;
    public void OnPointerEnter(PointerEventData eventData)
    {
        pos.x = this.transform.localPosition.x;
        pos.y=this.transform.localPosition.y+100;
        myArrow.transform.localPosition=pos;    
        myArrow.SetActive(true);

    }
    public void OnPointerExit(PointerEventData eventData)
    {
        myArrow.SetActive(false);
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (PointerInfo.instance.transform.gameObject.layer == 6)       //장비를 캐릭터에게 장착
        {
            obj = Instantiate(this.gameObject);
            obj.GetComponent<RectTransform>().sizeDelta = new Vector2(25, 25);
            pos.x = PointerInfo.instance.transform.localPosition.x + 20;
            pos.y = PointerInfo.instance.transform.localPosition.y - 20;

            obj.transform.SetParent(PointerInfo.instance.transform);
            if (PointerInfo.instance.transform.childCount > 2)
            {
                Destroy(PointerInfo.instance.transform.GetChild(1).gameObject);
            }
            obj.transform.localPosition = pos;                          //UI로 누가 어떤 아이템 갖고 있는지 표시
            obj.GetComponent<RawImage>().raycastTarget = false;
            //게임데이터에 적용할 때 여기서
        }
        else if (PointerInfo.instance.transform.gameObject.layer == 7)          //소모품을 캐릭터에게 사용
        {
            myText.text = $"{this.transform.name} has gained +10hp!";      //value값으로 변경해야 함
            myAlert.SetActive(true);
            Destroy(PointerInfo.instance.transform.gameObject);
        }
        myPanel.SetActive(false);
        
    }
  

}
