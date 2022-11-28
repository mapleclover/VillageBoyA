//작성자 : 이영진
//설명 : 

using TMPro;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class AboutItem : MonoBehaviour
{
    public GameObject myInfoBox;
    public TMP_Text myInfoText;
    public GameObject myParty;
    public GameObject myPanel;
    public static AboutItem instance = null;
    public GameObject myEnhance;
    Vector2 pos = Vector2.zero;

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
            case 7: //7: 장비, 8:소모품,9:퀘스트
                color = Color.yellow;
                break;
            case 8:
                color = Color.green;
                break;
            case 9:
                color = Color.grey;
                break;
            case 10:
                color = Color.green;
                break;
        }

        myInfoBox.GetComponent<Image>().color = color;
        sb.Append(temp.transform.GetComponent<Pickup>().item.itemName);
        if (temp.GetComponent<Pickup>().item.enhanceableItem.Equals(Item.EnhanceableItem.Possible))
        {
            sb.Append("   lv.");
            sb.Append(temp.GetComponent<EnhanceableItems>().myData.Level);
        }
        sb.AppendLine();sb.AppendLine();
        sb.Append(temp.transform.GetComponent<Pickup>().item.itemInfo);
        myInfoText.text = sb.ToString();
        Transform tr = this.transform.GetChild(0).transform;
        myInfoBox.transform.localPosition = new Vector2(tr.localPosition.x,tr.localPosition.y-300);
        myInfoBox.SetActive(true);
    }

    public void pointerexit()
    {
        myInfoBox.SetActive(false);
    }

    public void pointerclick()
    {
        Transform clickedItem = PointerInfo.instance.transform;
        if (clickedItem.gameObject.layer == 7 || clickedItem.gameObject.layer == 8)
        {
            myPanel.SetActive(true);
           // if(myEnhance.activeSelf)pos.x= clickedItem.parent.transform.parent.localPosition.x+200;
          //  else pos.x = clickedItem.parent.transform.parent.localPosition.x;
           // pos.y = clickedItem.parent.transform.parent.localPosition.y + 150;
          //  myParty.transform.localPosition = pos;
            myInfoBox.SetActive(false);
            myParty.SetActive(true);
        }
    }
}
