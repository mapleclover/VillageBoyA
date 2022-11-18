using System.Collections;
using System.Collections.Generic;
using System.Text;
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
        StringBuilder sb = new StringBuilder();
        Color color = myInfoBox.GetComponent<Image>().color;
        switch (temp.transform.gameObject.layer)
        {
            case 7:                         //7: 장비, 8:소모품,9:퀘스트
                color= Color.yellow;
                break;
            case 8:
                color = Color.green;
                break;
            case 9:
               color = Color.grey;
                break;

        }
        myInfoBox.GetComponent<Image>().color=color;
        sb.Append(PointerInfo.instance.transform.GetComponent<Pickup>().item.itemName);     //볼드체 처리 해야됨
        
        sb.AppendLine();sb.AppendLine();
        sb.Append(temp.transform.GetComponent<Pickup>().item.itemInfo);
        myInfoText.text = sb.ToString();

        myInfoBox.SetActive(true);
    }
    public void pointerexit()
    {
        myInfoBox.SetActive(false);
    }
    public void pointerclick()
    {
        Transform clickedItem = PointerInfo.instance.transform;
        //  instance = this;
        if (clickedItem.gameObject.layer == 7 || clickedItem.gameObject.layer == 8)
        {
            myPanel.SetActive(true);
            pos.x = clickedItem.parent.localPosition.x;
            pos.y = clickedItem.parent.localPosition.y + 150;
            myParty.transform.localPosition = pos;
            myInfoBox.SetActive(false);
            myParty.SetActive(true);
        }
    }


}
