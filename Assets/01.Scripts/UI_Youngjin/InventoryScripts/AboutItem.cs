<<<<<<< HEAD
//�ۼ��� : �̿���
//���� : 

using TMPro;
=======
using System.Collections;
using System.Collections.Generic;
using System.Text;
>>>>>>> 09f870078b428a8e57993763e5ade4e5dab5b814
using UnityEngine;
using UnityEngine.UI;

public class AboutItem : MonoBehaviour
{
    public GameObject myInfoBox;
    public TMP_Text myInfoText;
    public GameObject myParty;
    public GameObject myPanel;
    public static AboutItem instance = null;
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
<<<<<<< HEAD
            case 7: //7: ���, 8:�Ҹ�ǰ,9:����Ʈ
                myInfoBox.GetComponent<Image>().color = Color.yellow;
=======
            case 7:                         //7: ���, 8:�Ҹ�ǰ,9:����Ʈ
                color= Color.yellow;
>>>>>>> 09f870078b428a8e57993763e5ade4e5dab5b814
                break;
            case 8:
                color = Color.green;
                break;
            case 9:
               color = Color.grey;
                break;
        }
<<<<<<< HEAD

        myInfoText.text =
            $"<size=36><b>{PointerInfo.instance.transform.GetComponent<Pickup>().item.itemName}</size></b> \n {temp.transform.GetComponent<Pickup>().item.itemInfo} ";
=======
        myInfoBox.GetComponent<Image>().color=color;
        sb.Append(PointerInfo.instance.transform.GetComponent<Pickup>().item.itemName);     //����ü ó�� �ؾߵ�
        
        sb.AppendLine();sb.AppendLine();
        sb.Append(temp.transform.GetComponent<Pickup>().item.itemInfo);
        myInfoText.text = sb.ToString();

>>>>>>> 09f870078b428a8e57993763e5ade4e5dab5b814
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
            pos.x = clickedItem.parent.localPosition.x;
            pos.y = clickedItem.parent.localPosition.y + 150;
            myParty.transform.localPosition = pos;
            myInfoBox.SetActive(false);
            myParty.SetActive(true);
        }
    }
<<<<<<< HEAD
}
=======


}
>>>>>>> 09f870078b428a8e57993763e5ade4e5dab5b814
