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
    public GameObject myInfoBox;
    public GameObject itemCount;
  // public  Dictionary<string, int> myItem = new Dictionary<string, int>();               //key는 item name, value는 개수
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
                DataController.instance.gameData.myItem[obj.transform.GetChild(0).GetComponent<Pickup>().item.itemName] = 1;
            }
        }
        DataController.instance.gameData.myItem["포션"] = 2;
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
                   mySlots[i] = temp2;                          //아이템이 없는 슬롯을 뒤로 이동       
                }
            }
        }
        int count = 0;   
        for(int i = 0; i < mySlots.Length; i++)
        {
            if (mySlots[i].transform.childCount > 0)
            {
                count++;
            }
        }
        for(int i = 0; i < count - 1; i++)
        {
            for(int j = i+1; j < count; j++)
            {
                GameObject obj1 = mySlots[j].transform.GetChild(0).gameObject;
                GameObject obj2 = mySlots[i].transform.GetChild(0).gameObject;
                if (obj1.layer <obj2.layer)
                {
                   temp= mySlots[i].transform.localPosition;
                    mySlots[i].transform.localPosition = mySlots[j].transform.localPosition;            //아이템 정렬
                    mySlots[j].transform.localPosition = temp;
                    temp2 = mySlots[i];
                    mySlots[i] = mySlots[j];
                    mySlots[j] = temp2;

                }
            }
        }
        for(int i = 0; i < count; i++)
        {
            DataController.instance.gameData.savedInventory[mySlots[i].transform.GetChild(0).gameObject] = mySlots[i].transform.position;
                //정렬된 위치값을 데이터로 저장
        }



   
    }


    public void GetItem(GameObject theItem)
    {
        if (DataController.instance.gameData.myItem.ContainsKey(theItem.GetComponent<Pickup>().item.itemName))
        {
            Debug.Log("사과2");
            DataController.instance.gameData.myItem[theItem.GetComponent<Pickup>().item.itemName]++;
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
                        if (mySlots[i].transform.childCount == 0)       //빈 슬롯을 찾음
                        {

                            //GameObject obj = Instantiate(itemIcons[0], mySlots[i].transform.localPosition, Quaternion.identity);
                            GameObject obj = Instantiate(curItem[0]);
                            DataController.instance.gameData.myItem[curItem[0].GetComponent<Pickup>().item.itemName] = 1;
                          //  curItem.Add(obj);
                            obj.transform.SetParent(mySlots[i].transform);
                            obj.transform.GetComponent<RectTransform>().localScale = new Vector3(1.0f, 1.0f, 1.0f);
                            obj.transform.GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(70, 70);
                            obj.transform.localPosition = Vector2.zero;
                            DataController.instance.gameData.savedInventory[obj] =mySlots[i].transform.localPosition;
                            //Destroy(obj);
                            break;
                        }
                    }

                    break;
            }
        }
        if (theItem.GetComponent<Pickup>().item.itemName == "포션")
        {
            // DataController.instance.gameData.myPotions++;
            //DataController.instance.gameData.savedInventory[theItem];
        }
       
    }
    public void ShowNumbertoUI()
    {
        for (int i = 0; i < mySlots.Length; i++)
        {
            if (mySlots[i].transform.childCount > 0)                //슬롯에 아이템이 있으면
            {

                GameObject obj = mySlots[i].transform.GetChild(0).gameObject;
                if (obj.GetComponent<Pickup>().item.itemType == Item.NpcType.Ingredient || obj.layer == 8)      // 소모품 혹은 재료일 경우만 실행
                {

                    itemCount.GetComponent<RectTransform>().localScale = new Vector3(1.0f, 1.0f, 1.0f);
                    GameObject count = Instantiate(itemCount);
                    count.transform.SetParent(mySlots[i].transform.GetChild(0).transform);
                    count.GetComponent<RectTransform>().localScale = new Vector3(1.0f, 1.0f, 1.0f);


                    if (DataController.instance.gameData.myItem.ContainsKey(obj.GetComponent<Pickup>().item.itemName))
                    {
                        count.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = DataController.instance.gameData.myItem[obj.GetComponent<Pickup>().item.itemName].ToString();
                    }
                    //아이템의 개수를 UI로 표기
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


