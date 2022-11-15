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
   //datacontroller�� �߰�
   //
   public Dictionary<GameObject, int>curItems;
  // public  Dictionary<string, int> myItem = new Dictionary<string, int>();               //key�� item name, value�� ����
    public TMPro.TMP_Text countUI;
    public int defaultCount = 1;
    public List<GameObject> curItem=new List<GameObject>();
    bool v = true;
    Vector3 currentRotation;

    private void Awake()
    {
        Instance = this;
        DataController.instance.gameData.gold = 0;
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
                //  myItem["�ذ�"]++;
                //�ذ�� �׽�Ʈ
                ShowNumbertoUI();
                ShowMyGold();
            }
            myInventory.SetActive(v);
            if (v) v = false;
            else v = true;          //�κ��丮 ����
            myInfoBox.SetActive(false);
          
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
                   mySlots[i] = temp2;                          //�������� ���� ������ �ڷ� �̵�       
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
                    mySlots[i].transform.localPosition = mySlots[j].transform.localPosition;            //������ ����
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

                //���ĵ� ��ġ���� �����ͷ� ����
        }
    }


    public void GetItem(GameObject theItem)
    {
        if ( DataController.instance.gameData.myItemCount.ContainsKey(theItem.GetComponent<Pickup>().item.itemName))
        {
            Debug.Log("�̹�����");
            DataController.instance.gameData.myItemCount[theItem.GetComponent<Pickup>().item.itemName]++;
            ShowNumbertoUI();

        }                                       //�̹� �� �κ��丮�� ������ ���ڸ� ����
       else
        {
            switch (theItem.GetComponent<Pickup>().item.itemName)             //�� �κ��丮�� ������ �߰�
            {
             /*   case "���":
                    //100: ���
                    Debug.Log("�׽�Ʈ������");
                    ItemAppears(theItem);
                    break;
             */
                case "���":
                    ItemAppears(theItem);
                    break;
                case "�ڽ�":
                    ItemAppears(theItem);
                    break;
                case "����":
                    ItemAppears(theItem);
                    break;
                case "����":
                    ItemAppears(theItem);
                    break;
                case "����":
                    ItemAppears(theItem);
                    break;
                case "�ذ�":
                    ItemAppears(theItem);
                    break;
                case "â":
                    ItemAppears(theItem);
                    break;
                case "Į":
                    ItemAppears(theItem);
                    break;
                case "���첿��":
                    ItemAppears(theItem);
                    break;

            }
        }
       
    }

    public void ItemAppears(GameObject theItem)
    {
        for (int i = 0; i < mySlots.Length; i++)
        {
            if (mySlots[i].transform.childCount == 0)       //�� ������ ã��
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
            if (mySlots[i].transform.childCount > 0)                //���Կ� �������� ������
            {

                GameObject obj = mySlots[i].transform.GetChild(0).gameObject;
                if (obj.GetComponent<Pickup>().item.itemType == Item.NpcType.Ingredient || obj.layer == 8)      // �Ҹ�ǰ Ȥ�� ����� ��츸 ����
                {

                    itemCount.GetComponent<RectTransform>().localScale = new Vector3(1.0f, 1.0f, 1.0f);
                    GameObject count = Instantiate(itemCount);
                    count.transform.SetParent(mySlots[i].transform.GetChild(0).transform);
                    count.GetComponent<RectTransform>().localScale = new Vector3(1.0f, 1.0f, 1.0f);

                    
                  if (DataController.instance.gameData.savedInventory.ContainsKey(obj.GetComponent<Pickup>().item.itemName))
                    {
                        count.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = DataController.instance.gameData.myItemCount[obj.GetComponent<Pickup>().item.itemName].ToString();
                    }
                    //�������� ������ UI�� ǥ��
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


