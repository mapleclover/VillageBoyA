using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class InventoryController : MonoBehaviour
{
    public static InventoryController Instance;
    public GameObject myParty;
    public GameObject myInventory;
    public GameObject myPanel;
    public GameObject[] mySlots;
    public GameObject myAlert;
    public GameObject myInfoBox;
    public GameObject itemCount;
   public  Dictionary<string, int> myItem = new Dictionary<string, int>();               //key는 item name, value는 개수
    public TMPro.TMP_Text countUI;
    public int defaultCount = 1;
    public List<GameObject> curItem;
    bool v = true;

    private void Awake()
    {
        Instance = this;
       foreach(GameObject obj in mySlots)
        {
            if (obj.transform.childCount > 0)
            {
                myItem[obj.transform.GetChild(0).GetComponent<Pickup>().item.itemName] = 1;
            }
        }
        myItem["포션"] = 2;
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (v)
            {
                //  myItem["해골"]++;
                //해골로 테스트
                ShowNumbertoUI();
              
            }
            myInventory.SetActive(v);
            if (v) v = false;
            else v = true;          //인벤토리 열기
            myInfoBox.SetActive(false);
          
        }
        if (Input.GetKeyDown(KeyCode.F1))
        {
            GameObject test = null;
            GetItem(test);
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
        if (myItem.ContainsKey(theItem.GetComponent<Pickup>().item.itemName))
        {
            Debug.Log("사과2");
            myItem[theItem.GetComponent<Pickup>().item.itemName]++;
        }                                       //이미 내 인벤토리에 있으면 숫자만 더함
       else
        {
            switch (theItem.GetComponent<ObjData>().id)             //내 인벤토리에 없으면 추가
            {
                case 100:
                    //100: 사과
                    Debug.Log("사과1");
                    for (int i = 0; i < mySlots.Length; i++)
                    {
                        if (mySlots[i].transform.childCount == 0)
                        {

                            //GameObject obj = Instantiate(itemIcons[0], mySlots[i].transform.localPosition, Quaternion.identity);
                            GameObject obj = Instantiate(curItem[0]);
                            myItem[curItem[0].GetComponent<Pickup>().item.itemName] = 1;
                          //  curItem.Add(obj);
                            obj.transform.SetParent(mySlots[i].transform);
                            obj.transform.GetComponent<RectTransform>().localScale = new Vector3(1.0f, 1.0f, 1.0f);
                            obj.transform.GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(70, 70);
                            obj.transform.localPosition = Vector2.zero;     
                            break;
                        }
                    }

                    break;
            }

        }
    }
    public void ShowNumbertoUI()
    {
        for (int i = 0; i < mySlots.Length; i++)
        {
            if (mySlots[i].transform.childCount > 0)
            {

                GameObject obj = mySlots[i].transform.GetChild(0).gameObject;
                if (obj.GetComponent<Pickup>().item.itemType == Item.NpcType.Ingredient || obj.layer == 8)
                {

                    itemCount.GetComponent<RectTransform>().localScale = new Vector3(1.0f, 1.0f, 1.0f);
                    GameObject count = Instantiate(itemCount);
                    count.transform.SetParent(mySlots[i].transform.GetChild(0).transform);
                    count.GetComponent<RectTransform>().localScale = new Vector3(1.0f, 1.0f, 1.0f);


                    if (myItem.ContainsKey(obj.GetComponent<Pickup>().item.itemName))
                    {
                        count.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = myItem[obj.GetComponent<Pickup>().item.itemName].ToString();
                    }
                    count.transform.localPosition = new Vector2(20, 20);
                    if (obj.transform.childCount > 2)
                    {
                        Destroy(obj.transform.GetChild(1).gameObject);
                    }
                    //  count.SetActive(true);
                }
            }
        }
    }
    
}


