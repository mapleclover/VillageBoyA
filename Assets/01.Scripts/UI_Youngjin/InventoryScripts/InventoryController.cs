//작성자 : 이영진
//설명 : 
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class InventoryController : MonoBehaviour
{
    public static InventoryController Instance;
    public GameObject myInventory;
    public GameObject myPanel;
    public GameObject[] mySlots;
    public GameObject myInfoBox;
    public GameObject itemCount;
    public TMP_Text myGold;

    public GameObject myMenu;

    //datacontroller에 추가
    //
    public Dictionary<GameObject, int> curItems;

    // public  Dictionary<string, int> myItem = new Dictionary<string, int>();               //key는 item name, value는 개수
    public TMP_Text countUI;
    public int defaultCount = 1;
    public List<GameObject> curItem = new List<GameObject>();
    public bool v = true;
    Vector3 currentRotation;

    private void Awake()
    {
        Instance = this;
        for (int i = 0; i < mySlots.Length; i++)
        {
            if(mySlots[i].transform.childCount > 0)
            {
                DataController.instance.gameData.myItemCount[mySlots[i].transform.GetChild(0).GetComponent<Pickup>().item.itemName] = 1;
            }
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) || Input.GetKeyDown(KeyCode.I))
        {
            if (v)
            {
                ShowNumbertoUI();
                ShowMyGold();
            }

            if (!myMenu.activeSelf) myInventory.SetActive(v);
            else
            {
                v = false;
                myInventory.SetActive(v);
            }

            if (v) v = false;
            else v = true; //인벤토리 열기
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
            GetItem(curItem[0]);
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

    }

    public void OnClickX()
    {
        myPanel.SetActive(false);
    }

    public void OnClickSort()
    {
        Vector2 temp = Vector2.zero;
        GameObject temp2;
        for (int i = 0; i < mySlots.Length - 1; i++)
        {
            for (int j = i + 1; j < mySlots.Length; j++)
            {
                if (mySlots[j].transform.childCount > 0 && mySlots[i].transform.childCount.Equals(0))
                {
                    temp = mySlots[j].transform.localPosition;
                    mySlots[j].transform.localPosition = mySlots[i].transform.localPosition;
                    mySlots[i].transform.localPosition = temp;
                    temp2 = mySlots[j];
                    mySlots[j] = mySlots[i];
                    mySlots[i] = temp2; //아이템이 없는 슬롯을 뒤로 이동       
                }
            }
        }

        int count = 0;
        for (int i = 0; i < mySlots.Length; i++)
        {
            if (mySlots[i].transform.childCount > 0)
            {
                count++;
            }
        }

        for (int i = 0; i < count - 1; i++)
        {
            for (int j = i + 1; j < count; j++)
            {
                GameObject obj1 = mySlots[j].transform.GetChild(0).gameObject;
                GameObject obj2 = mySlots[i].transform.GetChild(0).gameObject;
                if (obj1.layer < obj2.layer)
                {
                    temp = mySlots[i].transform.localPosition;
                    mySlots[i].transform.localPosition = mySlots[j].transform.localPosition; //아이템 정렬
                    mySlots[j].transform.localPosition = temp;
                    temp2 = mySlots[i];
                    mySlots[i] = mySlots[j];
                    mySlots[j] = temp2;
                }
            }
        }

        for (int i = 0; i < count; i++)
        {
            DataController.instance.gameData.savedInventory[
                mySlots[i].transform.GetChild(0).gameObject.GetComponent<Pickup>().item.itemName] = i;
            //정렬된 위치값을 데이터로 저장
        }
    }


    public void GetItem(GameObject theItem)
    {
        string st = theItem.GetComponent<Pickup>().item.itemName;
        if ( DataController.instance.gameData.myItemCount.ContainsKey(st))
        {
            DataController.instance.gameData.myItemCount[st]++;
            if (theItem.GetComponent<Pickup>().item.itemType==Item.ItemType.Ingredient||theItem.layer==8)
            {
                GameObject temp= mySlots[DataController.instance.gameData.savedInventory[st]].transform.GetChild(0).gameObject;
                GameObject obj = temp.transform.GetChild(1).gameObject;
                obj.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = DataController.instance.gameData.myItemCount[st].ToString();
            }
        } //이미 내 인벤토리에 있으면 숫자만 더함
        else
        {
            switch (st)             //내 인벤토리에 없으면 추가
            {
                case "사과":
                    ItemAppears(theItem);
                    ShowNumbertoUI();
                    break;
                case "박스":
                    ItemAppears(theItem);
                    break;
                case "금반지":
                    ItemAppears(theItem);
                    break;
                case "포션":
                    ItemAppears(theItem);
                    ShowNumbertoUI();
                    break;
                case "방패":
                    ItemAppears(theItem);
                    break;
                case "해골":
                    ItemAppears(theItem);
                    break;
                case "팔찌":
                    ItemAppears(theItem);
                    break;
                case "장갑":
                    ItemAppears(theItem);
                    break;
                case "여우꼬리":
                    ItemAppears(theItem);
                    break;
                case "여우머리":
                    ItemAppears(theItem);
                    break;
                case "뿔":          
                    ItemAppears(theItem);
                    break;
                case "우유":
                    ItemAppears(theItem);
                    break;
                case "기어부품":
                    ItemAppears(theItem);
                    break;
            }
        }
    }

    public void ItemAppears(GameObject theItem)
    {
        for (int i = 0; i < mySlots.Length; i++)
        {

            if (mySlots[i].transform.childCount.Equals(0))       //빈 슬롯을 찾음
            {       
                GameObject obj = Instantiate(theItem);
                RectTransform rt = obj.transform.GetComponent<RectTransform>();
                RectTransform rtforchild = obj.transform.GetChild(0).GetComponent<RectTransform>();

                string itemname = obj.GetComponent<Pickup>().item.itemName;

                obj.transform.SetParent(mySlots[i].transform);
                rt.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                if (theItem.GetComponent<Pickup>().item.itemType == Item.ItemType.Ingredient)
                {
                     rtforchild.sizeDelta = new Vector2(60, 60);
                }
                else rtforchild.sizeDelta = new Vector2(70, 70);
                obj.transform.localPosition = Vector2.zero;
                DataController.instance.gameData.savedInventory[itemname] = i;
                DataController.instance.gameData.myItemCount[itemname] = 1;
                break;
            }
        }
    }

    public void ShowNumbertoUI()
    {
        for (int i = 0; i < mySlots.Length; i++)
        {
            if (mySlots[i].transform.childCount > 0) //슬롯에 아이템이 있으면
            {
                GameObject obj = mySlots[i].transform.GetChild(0).gameObject;
                if (obj.GetComponent<Pickup>().item.itemType == Item.ItemType.Ingredient ||
                    obj.layer == 8) // 소모품 혹은 재료일 경우만 실행
                {
                    if (obj.transform.childCount == 1)
                    {
                        GameObject count = Instantiate(itemCount);
                        itemCount.GetComponent<RectTransform>().localScale = new Vector3(0.7f, 0.7f, 0.7f);
                        count.transform.SetParent(mySlots[i].transform.GetChild(0).transform);
                        count.GetComponent<RectTransform>().localScale = new Vector3(0.7f, 0.7f, 0.7f);
                        count.GetComponent<Image>().raycastTarget = false;

                        //    else Destroy(count);
                        if (DataController.instance.gameData.savedInventory.ContainsKey(obj.GetComponent<Pickup>().item
                                .itemName))
                        {
                            count.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = DataController.instance
                                .gameData.myItemCount[obj.GetComponent<Pickup>().item.itemName].ToString();
                        }

                        //아이템의 개수를 UI로 표기
                        count.transform.localPosition = new Vector2(15, 15);
                    }
                    else
                    {
                        obj.transform.GetChild(1).GetComponentInChildren<TextMeshProUGUI>().text = DataController.instance
                                .gameData.myItemCount[obj.GetComponent<Pickup>().item.itemName].ToString();
                    }
                }
                //  count.SetActive(true);
            }
        }
    }

    public void ShowMyGold()
    {
        myGold.text = $"x{DataController.instance.gameData.gold}";
    }
}