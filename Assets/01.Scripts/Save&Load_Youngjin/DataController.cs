//�ۼ��� : �̿���
//���� :

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    public int DP
    {
        get => orgData.GetDP(Level);
    }
    public int EnchantCost
    {
        get => orgData.GetEnchantCost(Level);
    }
    public float Possibility
    {
        get => orgData.GetPossibility(Level);
    }
    public bool GetMaxLevel
    {
        get => Level < orgData.GetMaxLevel(); // �۴ٸ� Ʈ��
    }
    public bool GetEnhanceableItem
    {
        get => orgData.CheckEnhanceableItem(); // 0 �̶�� Ʈ��
    }

    // ���� ������
    // �Ϸ� 1. ��带 �Ҹ����� ���� (���)
    // 2. orgData�� �κ��丮���� ������� �� �ڵ����� ���ε� �ǵ��� �ٲ�� ��.
    // �Ϸ� 3. Level �迭 �ִ��� ���� ������ ���� (�������� ��)
    // 4. ��ȭ �� �����Ͱ� ��µǴ� ���� �ʿ� �׸��� ĳ���� ���ݿ� ����@@@@@@
    // 5. ��ũ���ͺ��� �ʿ� ��� Ȥ�� ��ȭ�� ���� �ؾ���. >> ���Կ� �ڵ��߰� >> �� ������ ����Ʈ����
    // �Ϸ� 6. ��ȭ ��ư �Լ��� ���� ������ ��
    // �Ϸ� 7. ��ȭ ���� ���� ������ ������ �� (�ƿ� ��ȭ ���Կ� �ö��� �ʰų� �ö󰡸� ��ư UI�� false)

    // ��ᰭȭ >> �ؽ�Ʈ��

    // item.cs �� ��ũ���ͺ������Ʈ�ε� cs�ȿ� ��Ʈ��Ʈ ������ ���ԵǾ �Ǵ°�?

    // �ϴ� ���⿡ ��Ʈ��Ʈ ������ ��������� �� ���� �� EnhanceableItems.cs ��
    // ��ũ���ͺ�� ����� �ű⿡�� �����۵����� ������ ���� �� �ֵ��� �ؾ��� �� ����
    // �ֳĸ� ������ ���ΰ� �� ��ũ��Ʈ �ڽ����� ����� �� ���� ��ȭ ���� �����ۿ�
    // EnhanceableItems.cs �� �߰��ϴ°� ������ �� ����
}




[Serializable]
public class GameData
{
    public int curSlot;

    //��� �迭�� 0�� �� 1�� �� 2�� �ڹ�
    public int turnBattleTimeSpeed = 0; // �Ϲ�Ʋ���Ӽӵ�
    public int myProgress = 0; //���൵
    public string savedTime; //������ �ð�
    public string mapName = "FirstVillage"; //���� ����
    public bool[] victoryComplete = Enumerable.Repeat(false, 6).ToArray();


    public Vector3 currentPosition = new Vector3(-136, 0, -90); //���� ĳ���� ��ġ
    public Vector3 currentRotation;

    //  public Dictionary<string, int> savedInventory = new Dictionary<string, int>(); //�����۰� ���° �������� ����
    // public Dictionary<string, int> myItemCount = new Dictionary<string, int>(); // �����۰� ����
    public List<string> itemList = new List<string>();
    public List<int> slotNum = new List<int>();
    public List<int> itemCount = new List<int>();

    public int gold = 0;

    public bool isFirstTime = true;
    //������ ����ü: ���� ��ȣ, ����, ��ȭ Ƚ��, ��ȭ�� ����

    //���� ����: DataController.instance.gameData.myItemCount["����"]

    public struct myPartyStats
    {
        public bool isLeader; //���� ����
        public int strength; //���ݷ�
        public int defPower; //����
        public int HP; //ü��
        public bool isAlive; //���� ����
        public List<string> myUsedItems;
    }

  

    //���� �� ����� ���� Ȯ��
    public myPartyStats Kong;
    public myPartyStats Jin;
    public myPartyStats Ember;
    public SaveItemData apple;
    public SaveItemData gloves;
    public SaveItemData goldring;
    public SaveItemData necklace;


    //public int[] questProgress = Enumerable.Repeat(0, 2).ToArray();     //����Ʈ ���൵
    public int questID = 30; // ����Ʈ����
    public int questActionIndex = 0; // ����Ʈ��ȭ����.
    public bool questClear = true; // ����ƮŬ���� ����
    public bool isBackAttack; // ���������� ������������ �ƴ���

    public bool[] partyMember = Enumerable.Repeat(true, 3).ToArray(); //���� �߿� ��Ƽ���� �߰��Ǹ� TRUE�� �ٲ������, ������ false?
}

public class DataController : MonoBehaviour
{
    // static GameObject _container;
    public string gamedataFilename = "VillageBoyA.json"; //.json �տ� ���� ������ ���� �̸� ����
    public string filePath;
    public int nowSlot; //0 1 2 
    public GameData gameData = new GameData();
    public static DataController instance;
    private GameObject thePlayer;
    private QuestManager theQuestManager;
    private ActionController theActionController;
    private GameObject myInven;
    private GameObject mySlots;
    private void Awake()
    {
        // File.Delete(filePath);
        if (instance == null)
        {
            instance = this;
           
        }
        else if (instance != null)
        {
            Destroy(this.gameObject);
            return;
        }

        if (gameData.isFirstTime)
        {
            gameData.Kong.isLeader = true;
            gameData.Kong.strength = 0;
            gameData.Kong.defPower = 0;
            gameData.Kong.HP = 150;
            gameData.Kong.isAlive = true;
            gameData.Kong.myUsedItems = new List<string>();

            gameData.Jin.isLeader = false;
            gameData.Jin.strength = 0;
            gameData.Jin.defPower = 0;
            gameData.Jin.HP = 100;
            gameData.Jin.isAlive = true;
            gameData.Jin.myUsedItems = new List<string>();

            gameData.Ember.isLeader = false;
            gameData.Ember.strength = 0;
            gameData.Ember.defPower = 0;
            gameData.Ember.HP = 125;
            gameData.Ember.isAlive = true;
            gameData.Ember.myUsedItems = new List<string>();
            gameData.isFirstTime = false;
        }


        DontDestroyOnLoad(gameObject);
        //  gameData.myInventory = new List<GameData.myPartyItems>();
        filePath = Application.persistentDataPath + gamedataFilename;
    }

    public void Save()
    {
        gameData.Kong.isLeader = true;
        gameData.Kong.strength = 0;
        gameData.Kong.defPower = 0;
        gameData.Kong.HP = 150;
        gameData.Kong.isAlive = true;
        gameData.Kong.myUsedItems = new List<string>();

        gameData.Jin.isLeader = false;
        gameData.Jin.strength = 0;
        gameData.Jin.defPower = 0;
        gameData.Jin.HP = 100;
        gameData.Jin.isAlive = true;
        gameData.Jin.myUsedItems = new List<string>();

        gameData.Ember.isLeader = false;
        gameData.Ember.strength = 0;
        gameData.Ember.defPower = 0;
        gameData.Ember.HP = 125;
        gameData.Ember.isAlive = true;
        gameData.Ember.myUsedItems = new List<string>();

        gameData.curSlot=nowSlot;
        gameData.savedTime = DateTime.Now.ToString();
        string ToJsonData = JsonUtility.ToJson(gameData);

        File.WriteAllText(filePath + nowSlot.ToString(), ToJsonData);   //nowSlot.ToString()
    }
   
    public void LoadGameData()
    {

        if (File.Exists(filePath + nowSlot.ToString()))//File.Exists(filePath + nowSlot.ToString())
        {
            Debug.Log("�ҷ�����");
            string FromJsonData = File.ReadAllText(filePath + nowSlot.ToString());
            gameData = JsonUtility.FromJson<GameData>(FromJsonData); //������ ������ �ҷ���
                                                                     //Json�� dataŬ������ ����
        }
        else
        {
            Debug.Log("���ο� ���� ����");
            gameData = new GameData(); //����� ������ ������ ���� ����
           
        }
    }



    public void SaveData()
    {
        thePlayer = GameObject.FindWithTag("Player");
        theQuestManager = FindObjectOfType<QuestManager>();
        theActionController = FindObjectOfType<ActionController>();
        // Player position
        gameData.currentPosition = thePlayer.transform.position; //�÷��̾���ǥ��.
        gameData.currentRotation = thePlayer.transform.eulerAngles; // �÷��̾� rot��.
        //Quest ~ing
        gameData.questID = theQuestManager.questId;
        gameData.questClear = theQuestManager.questComplete;
        gameData.questActionIndex = theQuestManager.questActionIndex;
        //BackAttack Battle ? true : false
        gameData.isBackAttack = theActionController.isBackAttack; // ���������� ���������ΰ�?
        for (int i = 0; i < InventoryController.Instance.mySlots.Length; i++)
        {
            GameObject obj = InventoryController.Instance.mySlots[i];
            if (obj.transform.childCount > 0)
            {
                GameObject thisitem = obj.transform.GetChild(0).gameObject;
                if (!gameData.itemList.Contains(thisitem.GetComponent<Pickup>().item.itemName))
                {
                    gameData.itemList.Add(thisitem.GetComponent<Pickup>().item.itemName);

                }
                    
               
                
                
            }
        }
    }
    public void DataClear()
    {
       nowSlot = -1;
        gameData = new GameData();
    }

    public void SaveGameDataByESC(int curSlot)
    {
        Debug.Log(curSlot);
        //if(SceneManager.GetActiveScene().name.Equals("06.Field"))
        thePlayer = GameObject.FindWithTag("Player");
        theQuestManager = FindObjectOfType<QuestManager>();
        theActionController = FindObjectOfType<ActionController>();

        //Player position
        gameData.currentPosition = thePlayer.transform.position; //�÷��̾���ǥ��.
        gameData.currentRotation = thePlayer.transform.eulerAngles; // �÷��̾� rot��.

        //Quest ~ing
        gameData.questID = theQuestManager.questId;
        gameData.questActionIndex = theQuestManager.questActionIndex;

        //BackAttack Battle ? true : false
        gameData.isBackAttack = theActionController.isBackAttack; // ���������� ���������ΰ�?

        for (int i = 0; i < InventoryController.Instance.mySlots.Length; i++) //�κ��丮 ����
        {
            GameObject obj = InventoryController.Instance.mySlots[i];
            if (obj.transform.childCount > 0)
            {
                GameObject thisitem = obj.transform.GetChild(0).gameObject;
                if (!gameData.itemList.Contains(thisitem.GetComponent<Pickup>().item.itemName))
                {
                    gameData.itemList.Add(thisitem.GetComponent<Pickup>().item.itemName);
                    gameData.slotNum.Add(i);
                }
                if (thisitem.GetComponent<Pickup>().item.itemType.Equals(Item.ItemType.Ingredient)&&thisitem.transform.childCount>=2)
                {
                    string st = thisitem.transform.GetChild(1).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text.ToString();
                    gameData.itemCount.Add(int.Parse(st));

                }
            }
        }
        gameData.savedTime = DateTime.Now.ToString();
        string ToJsonData = JsonUtility.ToJson(gameData);

        File.WriteAllText(filePath + curSlot.ToString(), ToJsonData);
        Debug.Log("����");
    }


    void ShowPortrait(GameObject portrait, int i, int index)
    {
        portrait.GetComponent<RectTransform>().sizeDelta = new Vector2(25, 25);
        portrait.transform.SetParent(mySlots.transform.GetChild(index).transform.GetChild(0));
        portrait.transform.localPosition =
            new Vector2(InventoryController.Instance.curItem[i].transform.localPosition.x + 20,
                InventoryController.Instance.curItem[i].transform.localPosition.y - 20);
        portrait.GetComponent<RawImage>().raycastTarget = false;
    }
    /*  private void OnApplicationQuit()
      {
          SaveGameData();     
      }                           // ���� ���� �� �ڵ� ����
    */

    //�ٸ� �κп��� ������ �ؾߵ� ���
    // DataController.Instance.SaveGameData();
}