using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AboutItem : MonoBehaviour
{
    public GameObject myInfoBox;
    public TMPro.TMP_Text myInfoText;
    public GameObject myParty;
    public GameObject myPanel;
    public static AboutItem instance = null;
    Vector2 pos = Vector2.zero;
    Vector2 dragOffset = Vector2.zero;
    private void Awake()
    {
          instance = this;
    }
    public void pointerenter(PointerInfo temp)
    {
        switch (PointerInfo.instance.transform.gameObject.layer)
        {
            case 7:                         //7: ���, 8:�Ҹ�ǰ,9:����Ʈ
                myInfoBox.GetComponent<Image>().color = Color.yellow;
                break;
            case 8:
                myInfoBox.GetComponent<Image>().color = Color.grey;
                break;
            case 9:
                myInfoBox.GetComponent<Image>().color = Color.green;
                break;

        }
       myInfoText.text = $"<size=36><b>{PointerInfo.instance.transform.GetComponent<Pickup>().item.itemName}</size></b> \n\n {PointerInfo.instance.transform.GetComponent<Pickup>().item.itemInfo} ";
        pos.x = temp.transform.parent.localPosition.x;
        pos.y =temp.transform.parent.localPosition.y + 170;
        myInfoBox.transform.localPosition = pos;

        //  myInfoBox.transform.SetParent(this.transform);
        myInfoBox.SetActive(true);
    }
    public void pointerexit()
    {
        myInfoBox.SetActive(false);
    }
    public void pointerclick()
    {
      //  instance = this;
        if (PointerInfo.instance.transform.gameObject.layer == 7 || PointerInfo.instance.transform.gameObject.layer == 8)
        {
            myPanel.SetActive(true);
            pos.x = PointerInfo.instance.transform.parent.localPosition.x;
            pos.y = PointerInfo.instance.transform.parent.localPosition.y + 150;
           myParty.transform.localPosition = pos;
            myInfoBox.SetActive(false);
            myParty.SetActive(true);
        }
    }


    //.transform.GetComponent<Pickup>().item.itemName
}
