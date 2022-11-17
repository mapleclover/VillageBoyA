using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Playables;

public class InventoryController : MonoBehaviour
{
    public static InventoryController Instance;
    public GameObject myParty;
    public GameObject myInventory;
    public GameObject myPanel;
    public GameObject[] mySlots;
    public GameObject myInfoBox;
    public GameObject itemCount;
    public TMPro.TMP_Text myGold;
    public GameObject myMenu;
   //datacontroller에 추가
   //
   public Dictionary<GameObject, int>curItems;
  // public  Dictionary<string, int> myItem = new Dictionary<string, int>();               //key는 item name, value는 개수
    public TMPro.TMP_Text countUI;
    public int defaultCount = 1;
    public List<GameObject> curItem=new List<GameObject>();
   public bool v = true;
    Vector3 currentRotation;

    private void Awake()
    {
        Instance = this;
        foreach (GameObject obj in mySlots)
        {
            if (obj.transform.childCount > 0)
            {
                DataController.instance.gameData.myItemCount[obj.transform.GetChild(0).GetComponent<Pickup>().item.itemName]= 1;
                
                //curItems[obj] = obj.transform.GetChild(0).GetComponent<Pickup>().item.count;
            }
        }
        
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Tab)||Input.GetKeyDown(KeyCode.I))
        {
            if (v)
            {
                //  myItem["해골"]++;
                //해골로 테스트
                ShowNumbertoUI();
                ShowMyGold();
            }
            if(!myMenu.activeSelf)myInventory.SetActive(v);
            else
            {
                v = false;
                myInventory.SetActive(v);
            }
            if (v) v = false;
            else v = true;          //인벤토리 열기
            myInfoBox.SetActive(false);
          
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (myInventory.activeSelf)
            {
                v = false;
                myInventory.SetActive(v);
            }
        }
        if (Input.GetKeyDown(KeyCode.F1))
        {
            GetItem(curItem[1]);
        }
        if (Input.GetKeyDown(KeyCode.F2))
        {
            GetItem(curItem[2]);
        }
        if (Input.GetKeyDown(KeyCode.F3))
        {
            GetItem(curItem[3]);
        }
        if (Input.GetKeyDown(KeyCode.F4))
        {
            GetItem(curItem[4]);
        }
        if (Input.GetKeyDown(KeyCode.F5))
        {
            GetItem(curItem[5]);
        }
        if (Input.GetKeyDown(KeyCode.F6))
        {
            GetItem(curItem[6]);
        }
        if (Input.GetKeyDown(KeyCode.F7))
        {
            GetItem(curItem[7]);
        }
        if (Input.GetKeyDown(KeyCode.F5))
        {
            GetItem(curItem[0]);
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
            DataController.instance.gameData.savedInventory[mySlots[i].transform.GetChild(0).gameObject.GetComponent<Pickup>().item.itemName] = i;

                //정렬된 위치값을 데이터로 저장
        }
    }


    public void GetItem(GameObject theItem)
    {
        if ( DataController.instance.gameData.myItemCount.ContainsKey(theItem.GetComponent<Pickup>().item.itemName))
        {
            Debug.Log("이미있음");
            DataController.instance.gameData.myItemCount[theItem.GetComponent<Pickup>().item.itemName]++;
            ShowNumbertoUI();

        }                                       //이미 내 인벤토리에 있으면 숫자만 더함
       else
        {
            switch (theItem.GetComponent<Pickup>().item.itemName)             //내 인벤토리에 없으면 추가
            {
             /*   case "사과":
                    //100: 사과
                    Debug.Log("테스트아이템");
                    ItemAppears(theItem);
                    break;
             */
                case "사과":
                    ItemAppears(theItem);
                    break;
                case "박스":
                    ItemAppears(theItem);
                    break;
                case "도끼":
                    ItemAppears(theItem);
                    break;
                case "포션":
                    ItemAppears(theItem);
                    break;
                case "방패":
                    ItemAppears(theItem);
                    break;
                case "해골":
                    ItemAppears(theItem);
                    break;
                case "창":
                    ItemAppears(theItem);
                    break;
                case "칼":
                    ItemAppears(theItem);
                    break;
                case "여우꼬리":
                    ItemAppears(theItem);
                    break;

            }
        }
       
    }

    public void ItemAppears(GameObject theItem)
    {
        for (int i = 0; i < mySlots.Length; i++)
        {
            if (mySlots[i].transform.childCount == 0)       //빈 슬롯을 찾음
            {

               
                GameObject obj = Instantiate(theItem);
               
               obj.transform.SetParent(mySlots[i].transform);
                obj.transform.GetComponent<RectTransform>().localScale = new Vector3(1.0f, 1.0f, 1.0f);
                obj.transform.GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(70, 70);
                obj.transform.localPosition = Vector2.zero;
                DataController.instance.gameData.savedInventory[obj.GetComponent<Pickup>().item.itemName] = i;
                DataController.instance.gameData.myItemCount[obj.GetComponent <Pickup>().item.itemName] = 1;
                break;
            }
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

                    
                  if (DataController.instance.gameData.savedInventory.ContainsKey(obj.GetComponent<Pickup>().item.itemName))
                    {
                        count.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = DataController.instance.gameData.myItemCount[obj.GetComponent<Pickup>().item.itemName].ToString();
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
    public void ShowMyGold()
    {
        myGold.text = $"x{DataController.instance.gameData.gold}";
    }
    
}


