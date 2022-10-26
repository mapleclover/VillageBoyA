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
        if (PointerInfo.instance.transform.gameObject.layer == 6)       //��� ĳ���Ϳ��� ����
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
            obj.transform.localPosition = pos;                          //UI�� ���� � ������ ���� �ִ��� ǥ��
            obj.GetComponent<RawImage>().raycastTarget = false;
            //���ӵ����Ϳ� ������ �� ���⼭
        }
        else if (PointerInfo.instance.transform.gameObject.layer == 7)          //�Ҹ�ǰ�� ĳ���Ϳ��� ���
        {
            myText.text = $"{this.transform.name} has gained +10hp!";      //value������ �����ؾ� ��
            myAlert.SetActive(true);
            Destroy(PointerInfo.instance.transform.gameObject);
        }
        myPanel.SetActive(false);
        
    }
  

}
