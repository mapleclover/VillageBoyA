//�ۼ��� : �̿���
//���� : 

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
    public GameObject myInven;
    public static AboutItem instance = null;



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
            case 7: //7: ���, 8:�Ҹ�ǰ,9:����Ʈ
                color = Color.yellow;
                break;
            case 8:
                color = Color.green;
                break;
            case 9:
                color = Color.grey;
                break;
            case 10:
                color = Color.magenta;
                break;
        }

        myInfoBox.GetComponent<Image>().color = color;
        sb.Append(temp.transform.GetComponent<Pickup>().item.itemName);
        if (temp.GetComponent<Pickup>().item.enhanceableItem.Equals(Item.EnhanceableItem.Possible))
        {
            sb.Append("   lv.");        
            sb.Append(temp.GetComponent<EnhanceableItems>().myData.Level);          //��ȭ ������ �������� ��� ���� AP DP ǥ��
        }
        sb.AppendLine();sb.AppendLine();
        sb.Append(temp.transform.GetComponent<Pickup>().item.itemInfo);
        if (temp.GetComponent<Pickup>().item.enhanceableItem.Equals(Item.EnhanceableItem.Possible))
        {
            sb.Append("���ݷ� +");
            sb.Append(temp.GetComponent<EnhanceableItems>().myData.AP);sb.Append(' ');
            sb.Append("���� +");
            sb.Append(temp.GetComponent<EnhanceableItems>().myData.DP);
        }
        myInfoText.text = sb.ToString();
        Transform tr = myInven.transform;
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
        if (clickedItem.gameObject.layer.Equals(7)|| clickedItem.gameObject.layer.Equals(8))     //���� Ȥ�� ��� Ŭ���� ���
        {
            myPanel.SetActive(true);        //������ ��Ƽ���� ����� UI Ȱ��ȭ

            myInfoBox.SetActive(false);
            myParty.SetActive(true);
        }
       
    }
}
