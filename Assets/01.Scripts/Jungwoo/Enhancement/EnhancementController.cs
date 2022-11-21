//�ۼ��� : ������
//���� :

using UnityEngine;

public class ItemStat
{
    public EnhanceableItems Data;
    public int Level; // ���߿� �� ����
}

public class EnhancementController : PointerInfo
{
    public GameObject myUI;

    public RectTransform myInventory;
    public GameObject setMyInventory;
    public bool onOff = false;

    public GameObject mySlot;

    public Item myItems;
    public SaveItemData myItemlevel;

    Vector2 invenPos = new Vector2(200f, 0);

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
        if (myItems.enhanceableItem == Item.EnhanceableItem.Possible)
        {
            if (Input.GetKeyDown(KeyCode.L))
            {
                // EnchantCost ��ŭ ��� ����Ʈ����

                if (myItems.CheckSuccess(myItemlevel.Itemlevel))
                {
                    myItemlevel.Itemlevel++;
                    Debug.Log("����");

                    //Debug.Log(myItems.EnchantCost[myItemlevel.Itemlevel]);
                    //Debug.Log(myItems.AP[myItemlevel.Itemlevel]);
                    //Debug.Log(myItems.possibility[myItemlevel.Itemlevel]);
                }
                else
                {
                    Debug.Log("����");
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            myInventory.localPosition = new Vector2(200f, 0f);
            print("1");
            myUI.SetActive(true);
            setMyInventory.SetActive(true);
            onOff = true;
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
}