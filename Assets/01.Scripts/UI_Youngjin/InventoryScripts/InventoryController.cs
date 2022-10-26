using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    public GameObject myParty;
    public GameObject myInventory;
    public GameObject myPanel;
    public GameObject[] mySlots;
    public GameObject myAlert;
    bool v = true;
    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            myInventory.SetActive(v);
            if (v) v = false;
            else v = true;          //�κ��丮 ����
        }
    }
    public void OnClickX()
    {
        myPanel.SetActive(false);
    }
    public void OnClickSort()
    {
       Vector2 temp=Vector2.zero;
        GameObject temp2;
       for(int i = 0; i < mySlots.Length-1; i++)
        {
            for(int j = i + 1; j < mySlots.Length; j++)
            {
                if (mySlots[j].transform.childCount > 0 && mySlots[i].transform.childCount == 0)
                {
                    temp= mySlots[j].transform.localPosition;
                   mySlots[j].transform.localPosition = mySlots[i].transform.localPosition;
                    mySlots[i].transform.localPosition = temp;
                    temp2 = mySlots[j];
                    mySlots[j] = mySlots[i];
                    mySlots[i] = temp2;
                }
            }
        }               //�������� ���� ������ �ڷ� �̵�         
        List<GameObject> list = new List<GameObject>();
        for(int i = 0; i < mySlots.Length; i++)
        {
            if (mySlots[i].transform.childCount > 0)
            {
                list.Add(mySlots[i]);
            }
        }
       for(int i=0; i < list.Count-1; i++)
        {
            for(int j = i + 1; j < list.Count; j++)
            {
                if (list[j].transform.GetChild(0).gameObject.layer < list[i].transform.GetChild(0).gameObject.layer)
                {
                    
                    temp = list[j].transform.localPosition;
                    list[j].transform.localPosition = list[i].transform.localPosition;
                    list[i].transform.localPosition = temp;
                    temp2 = list[j];
                    list[j] = list[i];
                    list[i] = temp2;                 //���, ����Ʈ, �Ҹ�ǰ ������ ����   

                }
            }
        }
       for(int i = 0; i < list.Count; i++)
        {
            mySlots[i] = list[i];
        }
    }
    public void ClickOK()
    {
        myAlert.SetActive(false);
    }
}
