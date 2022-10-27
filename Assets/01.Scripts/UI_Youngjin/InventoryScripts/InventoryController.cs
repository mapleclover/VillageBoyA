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
    [SerializeField]
    private GameObject[] itemIcons;

    bool v = true;
    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            myInventory.SetActive(v);
            if (v) v = false;
            else v = true;          //인벤토리 열기
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
        }               //아이템이 없는 슬롯을 뒤로 이동         
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
                    list[i] = temp2;                 //장비, 퀘스트, 소모품 순서로 정렬   

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

    public void GetItem(GameObject theItem)
    {

        switch (theItem.GetComponent<ObjData>().id)
        {
            case 100:
                for (int i = 0; i< mySlots.Length; i++)
                {
                    if (mySlots[i].transform.childCount == 0)
                    {

                        //GameObject obj = Instantiate(itemIcons[0], mySlots[i].transform.localPosition, Quaternion.identity);
                        GameObject obj = Instantiate(itemIcons[0]);
                        obj.transform.SetParent(mySlots[i].transform);
                        obj.transform.GetComponent<RectTransform>().localScale = new Vector3(1.0f, 1.0f,1.0f);
                        obj.transform.GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(70, 70);
                        obj.transform.localPosition = Vector2.zero;
                        break;
                    }
                }
        
                break;
        }

    }
    
}
