//�ۼ��� : �̿���
//���� : 
using System;
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

    //datacontroller�� �߰�
    //
    public Dictionary<GameObject, int> curItems;

    // public  Dictionary<string, int> myItem = new Dictionary<string, int>();               //key�� item name, value�� ����
    public TMP_Text countUI;
  //  public int defaultCount = 1;
    public List<GameObject> curItem = new List<GameObject>();
    public bool v = true;


    private void Awake()
    {
        Instance = this;
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

            if (!myMenu.activeSelf)
            {
                myInventory.SetActive(v);
            }
            else
            {
                v = false;
                myInventory.SetActive(v);

               // myPanel.SetActive(false);
            }
            if (v) v = false;
            else v = true; //�κ��丮 ����
            myInfoBox.SetActive(false);
            myPanel.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (myInventory.activeSelf)
            {
                v = false;
                myInventory.SetActive(v);
                myPanel.SetActive(v);
            }
            myPanel.SetActive(false);
        }
        if (Input.GetMouseButtonDown(1))
        {
            DataController.instance.gameData.gold += 100;
            ShowMyGold();
        }
        if (Input.GetKeyDown(KeyCode.F1))
        {
            GetItem(curItem[0]);
        }

        if (Input.GetKeyDown(KeyCode.F2))           
        {
            GetItem(curItem[1]);
        }

        if (Input.GetKeyDown(KeyCode.F3))
        {
            GetItem(curItem[2]);
        }

        if (Input.GetKeyDown(KeyCode.F4))
        {
            GetItem(curItem[3]);
        }

        if (Input.GetKeyDown(KeyCode.F5))
        {
            GetItem(curItem[4]);
        }

        if (Input.GetKeyDown(KeyCode.F6))
        {
            GetItem(curItem[5]);
        }

        if (Input.GetKeyDown(KeyCode.F7))
        {
            GetItem(curItem[6]);
        }
        if (Input.GetKeyDown(KeyCode.F8))
        {
            GetItem(curItem[7]);
        }
        if (Input.GetKeyDown(KeyCode.F12))
        {
            GetItem(curItem[13]);
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
                    mySlots[i] = temp2; //�������� ���� ������ �ڷ� �̵�       
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
                    mySlots[i].transform.localPosition = mySlots[j].transform.localPosition; //������ ����
                    mySlots[j].transform.localPosition = temp;
                    temp2 = mySlots[i];
                    mySlots[i] = mySlots[j];
                    mySlots[j] = temp2;
                }
            }
        }

        for (int i = 0; i < count; i++)
        {
            string st = mySlots[i].transform.GetChild(0).gameObject.GetComponent<Pickup>().item.itemName;
            DataController.instance.gameData.slotNum[DataController.instance.gameData.itemList.IndexOf(st)] = i;
            //���ĵ� ��ġ���� �����ͷ� ����
        }
    }


    public void GetItem(GameObject theItem)
    {
        string st = theItem.GetComponent<Pickup>().item.itemName;
        if (DataController.instance.gameData.itemList.Contains(st) )
        {
            int index = DataController.instance.gameData.itemList.IndexOf(st);
            DataController.instance.gameData.itemCount[index]++;
            if (theItem.GetComponent<Pickup>().item.itemType.Equals(Item.ItemType.Ingredient)||theItem.layer.Equals(8))
            {
                GameObject temp= mySlots[DataController.instance.gameData.slotNum[index]].transform.GetChild(0).gameObject;
                /*if (temp.transform.childCount >= 2)
                {
                    GameObject obj = temp.transform.GetChild(1).gameObject;
                    obj.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = DataController.instance.gameData.itemCount[index].ToString();
                }
                else
                {*/
                    ShowNumbertoUI();
                
            }
        } //�̹� �� �κ��丮�� ������ ���ڸ� ����
        else
        {
            switch (st)             //�� �κ��丮�� ������ �߰�
            {
                case "���":
                    ItemAppears(theItem);
                    ShowNumbertoUI();
                    break;
                case "����":
                    ItemAppears(theItem);
                    ShowNumbertoUI();
                    break;


                case "���̾Ƹ��":
                    ItemAppears(theItem);
                    ShowNumbertoUI();
                    break;
                case "��":
                    ItemAppears(theItem);
                    ShowNumbertoUI();
                    break;
                case "ö":
                    ItemAppears(theItem);
                    ShowNumbertoUI();
                    break;
                case "��":
                    ItemAppears(theItem);
                    ShowNumbertoUI();
                    break;
                case "����":
                    ItemAppears(theItem);
                    ShowNumbertoUI();
                    break;
                case "��ũ��":
                    ItemAppears(theItem);
                    ShowNumbertoUI();
                    break;

                case "�ݹ���":
                    DataController.instance.gameData.goldring.Level = 1;
                    ItemAppears(theItem);
                    break;
                case "�尩":
                    DataController.instance.gameData.gloves.Level = 1;
                    ItemAppears(theItem);
                    break;
                case "�����":
                    DataController.instance.gameData.necklace.Level = 1;
                    ItemAppears(theItem);
                    break;


                case "���첿��":
                    ItemAppears(theItem);
                    break;
                case "����Ӹ�":
                    ItemAppears(theItem);
                    break;
                case "��":          
                    ItemAppears(theItem);
                    break;
                case "����":
                    ItemAppears(theItem);
                    break;
                case "����ǰ":
                    ItemAppears(theItem);
                    break;
                case "test Item":
                    Get3DObject(theItem);
                    break;
            }
        }
    }
    public void Get3DObject(GameObject obj)
    {
        GetItem(obj.GetComponent<Pickup>().item.itemPrefab);
        ShowNumbertoUI();
    }

    public void ItemAppears(GameObject theItem)
    {
        int temp = 0;
        for (int i = 0; i < mySlots.Length; i++)
        {

            if (mySlots[i].transform.childCount.Equals(0))       //�� ������ ã��
            {
                GameObject obj = Instantiate(theItem);
                RectTransform rt = obj.transform.GetComponent<RectTransform>();
                RectTransform rtforchild = obj.transform.GetChild(0).GetComponent<RectTransform>();

                string itemname = obj.GetComponent<Pickup>().item.itemName;

                obj.transform.SetParent(mySlots[i].transform);
                rt.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                if (theItem.GetComponent<Pickup>().item.itemType.Equals(Item.ItemType.Ingredient))
                {
                    rtforchild.sizeDelta = new Vector2(60, 60);
                }
                else rtforchild.sizeDelta = new Vector2(70, 70);
                obj.transform.localPosition = Vector2.zero;
                DataController.instance.gameData.itemList.Add(itemname);
                DataController.instance.gameData.slotNum.Add(i);
                DataController.instance.gameData.itemCount.Add(1);
                break;
            }
            else temp++;
        }
        if (temp.Equals(mySlots.Length))
        {

        }
    }

    public void ShowNumbertoUI()
    {
        for (int i = 0; i < mySlots.Length; i++)
        {
            if (mySlots[i].transform.childCount > 0) //���Կ� �������� ������
            {
                GameObject obj = mySlots[i].transform.GetChild(0).gameObject;
                if (obj.GetComponent<Pickup>().item.itemType.Equals(Item.ItemType.Ingredient) ||obj.layer.Equals(8)) // �Ҹ�ǰ Ȥ�� ����� ��츸 ����
                {
                    string st = obj.GetComponent<Pickup>().item.itemName;
                    int index = DataController.instance.gameData.itemList.IndexOf(st);
                    if (obj.transform.childCount <= 1)
                    {
                        GameObject count = Instantiate(itemCount);
                        itemCount.GetComponent<RectTransform>().localScale = new Vector3(0.7f, 0.7f, 0.7f);
                        count.transform.SetParent(mySlots[i].transform.GetChild(0).transform);
                        count.GetComponent<RectTransform>().localScale = new Vector3(0.7f, 0.7f, 0.7f);
                        count.GetComponent<Image>().raycastTarget = false;
                        Debug.Log($"index = {index}");
                        Debug.Log($"{DataController.instance.gameData.itemCount.Count}");
                        if (DataController.instance.gameData.itemList.Contains(st))
                        {
                            count.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = DataController.instance.gameData.itemCount[index].ToString();
                        }
                        //�������� ������ UI�� ǥ��
                        count.transform.localPosition = new Vector2(15, 15);
                    }
                    else
                    {
                        if(DataController.instance.gameData.itemList.Contains(st))
                        obj.transform.GetChild(1).GetComponentInChildren<TextMeshProUGUI>().text = DataController.instance.gameData.itemCount[index].ToString();
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