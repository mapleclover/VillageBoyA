//�ۼ��� : ������
//���� :

using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
[Serializable]
public struct SaveItemData
{ 
    public Item orgData;
    public int Level;

    public int AP
    {
        get => orgData.GetAP(Level);
    }
    public int EnchantCost
    {
        get => orgData.GetEnchantCost(Level);
    }
    public float Possibility
    {
        get => orgData.GetPossibility(Level);
    }

    // ���� ������
    // 1. ��带 �Ҹ����� ���� (���)
    // 2. orgData�� �κ��丮���� ������� �� �ڵ����� ���ε� �ǵ��� �ٲ�� ��.
    // 3. Level �迭 �ִ��� ���� ������ ���� (�������� ��)
    // 4. ��ȭ �� �����Ͱ� ��µǴ� ���� �ʿ� �׸��� ĳ���� ���ݿ� ����@@@@@@
    // 5. ��ũ���ͺ��� �ʿ� ��� Ȥ�� ��ȭ�� ���� �ؾ���.
    // 6. ��ȭ ��ư �Լ��� ���� ������ ��
    // 7. ��ȭ ���� ���� ������ ������ �� (�ƿ� ��ȭ ���Կ� �ö��� �ʰų� �ö󰡸� ��ư UI�� false)

    // �ϴ� ���⿡ ��Ʈ��Ʈ ������ ��������� �� ���� �� EnhanceableItems.cs ��
    // ��ũ���ͺ�� ����� �ű⿡�� �����۵����� ������ ���� �� �ֵ��� �ؾ��� �� ����
    // �ֳĸ� ������ ���ΰ� �� ��ũ��Ʈ �ڽ����� ����� �� ���� ��ȭ ���� �����ۿ�
    // EnhanceableItems.cs �� �߰��ϴ°� ������ �� ����
}


public class EnhancementController : MonoBehaviour
{
    public GameObject myUI;
    public RectTransform myInventory;
    public GameObject setMyInventory;
    public bool onOff = false;

    public Button EnchantButton; // button UI

    public GameObject mySlot;
    public Item myItems;


    [SerializeField] SaveItemData myData;
    

    public int AP
    {
        get => myData.AP;
    }
    public int EnchantCost
    {
        get => myData.EnchantCost;
    }
    public float Possibility
    {
        get => myData.Possibility;
    }

    // Start is called before the first frame update
    void Start()
    {
        myInventory = myInventory.GetComponent<RectTransform>();
        myInventory.localPosition = new Vector2(0f, 0f);
        //myItems = mySlot.GetComponentInChildren<Item>();
    }

    // Update is called once per frame
    void Update()
    {
        EnchantLogic();

        if (Input.GetKeyDown(KeyCode.C))
        {
            
            myInventory.localPosition = new Vector2(200f, 0f);
            print("1");
            myUI.SetActive(true);
            setMyInventory.SetActive(true);
            onOff = true; // ���߿� ����� �� ����
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            myInventory.localPosition = new Vector2(0f, 0f);
            print("2");
            myUI.SetActive(false);
            setMyInventory.SetActive(false);
            onOff = false;
        }
    }

    public void EnchantLogic()
    {
        if (myItems.enhanceableItem == Item.EnhanceableItem.Possible && EnchantButton)
        {
            EnchantButton.onClick.RemoveAllListeners();
            EnchantButton.interactable = true;
            EnchantButton.onClick.AddListener(
                () =>
                {
                    // EnchantCost --; ���߿� ������ ����� �� �߰�
                    if (myItems.CheckSuccess(myData.Level))
                    {
                        myData.Level++;
                        Debug.Log("����");

                        Debug.Log(myItems.EnchantCost[myData.Level-1]);
                        Debug.Log(myItems.AP[myData.Level-1]);
                        Debug.Log(myItems.Possibility[myData.Level-1]);
                    }
                    else
                    {
                        Debug.Log("����");
                        Debug.Log(myItems.EnchantCost[myData.Level - 1]);
                        Debug.Log(myItems.AP[myData.Level - 1]);
                        Debug.Log(myItems.Possibility[myData.Level - 1]);
                    }
                });
        }
        else
        {
            EnchantButton.interactable = false;
        }


    }
       
}