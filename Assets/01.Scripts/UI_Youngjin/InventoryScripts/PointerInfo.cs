using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PointerInfo : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler,IPointerClickHandler, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public GameObject myInfoBox;
    public TMPro.TMP_Text myInfoText;
    public string itemName;
    public string itemInfo;
    public GameObject myParty;
    public GameObject myPanel;
    Vector2 pos = Vector2.zero;
    Vector2 dragOffset = Vector2.zero;
    public static PointerInfo instance = null;


    public void OnPointerEnter(PointerEventData eventData)
    {

            switch (this.transform.gameObject.layer)
            {
                case 6:                         //6: ���, 7:����Ʈ, 8:�Ҹ�ǰ
                    myInfoBox.GetComponent<Image>().color = Color.yellow;
                    break;
                case 7:
                    myInfoBox.GetComponent<Image>().color = Color.green;
                break;
                case 8:
                myInfoBox.GetComponent<Image>().color = Color.grey;
                break;

        }
         myInfoText.text =$"<size=32><b>{itemName}</size></b> \n\n {itemInfo} ";
         pos.x = this.transform.parent.localPosition.x;
         pos.y = this.transform.parent.localPosition.y+170 ;
         myInfoBox.transform.localPosition = pos;
        
       //  myInfoBox.transform.SetParent(this.transform);
        myInfoBox.SetActive(true);

    }
    public void OnPointerExit(PointerEventData eventData)
    {
        myInfoBox.SetActive(false);
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        instance = this;
        if (this.transform.gameObject.layer == 6 || this.transform.gameObject.layer ==7)
        {
            myPanel.SetActive(true);
            pos.x = this.transform.parent.localPosition.x;
            pos.y = this.transform.parent.localPosition.y+150 ;
            myParty.transform.localPosition = pos;
            myInfoBox.SetActive(false);
            myParty.SetActive(true);
        }
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
