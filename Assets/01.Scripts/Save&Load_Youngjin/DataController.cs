using System;
using System.Collections;
using System.Collections.Generic;
using System.IO; //���� �� ����� ���� Ȯ��
using System.Linq;
using TMPro;
using UnityEngine;

[Serializable]
public class GameData
{
    //��� �迭�� 0�� �� 1�� �� 2�� �ڹ�
    public int myProgress = 0; //���൵
    public string savedTime; //������ �ð�
    public string mapName = "FirstVillage"; //���� ����
    public bool[] victoryComplete = Enumerable.Repeat(false, 6).ToArray();


    public Vector3 currentPosition = new Vector3(-136, 0, -90); //���� ĳ���� ��ġ
    public Vector3 currentRotation;

    public Dictionary<string, int> savedInventory = new Dictionary<string, int>(); //�����۰� ���° �������� ����
    public Dictionary<string, int> myItemCount = new Dictionary<string, int>(); // �����۰� ����
    public int gold = 0;

    //������ ����ü: ���� ��ȣ, ����, ��ȭ Ƚ��, ��ȭ�� ����

    //���� ����: DataController.instance.gameData.myItemCount["����"]

    public struct myPartyStats
    {
        public bool isLeader; //���� ����
        public int strength; //���ݷ�
        public int defPower; //����
        public int speed; //�ӵ�
        public int HP; //ü��
        public bool isAlive; //���� ����
        public List<string> myUsedItems;
    }

    public myPartyStats Kong;
    public myPartyStats Jin;
    public myPartyStats Ember;


    //public int[] questProgress = Enumerable.Repeat(0, 2).ToArray();     //����Ʈ ���൵
    public int questID = 30; // ����Ʈ����
    public int questActionIndex = 0; // ����Ʈ��ȭ����.
    public bool questClear = true; // ����ƮŬ���� ����
    public bool isBackAttack; // ���������� ������������ �ƴ���

    public bool[]
        partyMember = Enumerable.Repeat(true, 3).ToArray(); //���� �߿� ��Ƽ���� �߰��Ǹ� TRUE�� �ٲ������, ������ false?
}

public class DataController : MonoBehaviour
{
    // static GameObject _container;
    public string gamedataFilename = "VillageBoyA.json"; //.json �տ� ���� ������ ���� �̸� ����
    public string filePath;
    public int nowSlot;
    public GameData gameData = new GameData();
    public static DataController instance;
    private PlayerMovement thePlayer;
    private QuestManager theQuestManager;
    private ActionController theActionController;

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

        DontDestroyOnLoad(gameObject);
        //  gameData.myInventory = new List<GameData.myPartyItems>();

        gameData.Kong.isLeader = true;
        gameData.Kong.strength = 10;
        gameData.Kong.defPower = 0;
        gameData.Kong.speed = 10;
        gameData.Kong.HP = 100;
        gameData.Kong.isAlive = true;
        gameData.Kong.myUsedItems = new List<string>();

        gameData.Jin.isLeader = false;
        gameData.Jin.strength = 20;
        gameData.Jin.defPower = 0;
        gameData.Jin.speed = 20;
        gameData.Jin.HP = 100;
        gameData.Jin.isAlive = true;
        gameData.Jin.myUsedItems = new List<string>();

        gameData.Ember.isLeader = false;
        gameData.Ember.strength = 30;
        gameData.Ember.defPower = 0;
        gameData.Ember.speed = 30;
        gameData.Ember.HP = 100;
        gameData.Ember.isAlive = true;
        gameData.Ember.myUsedItems = new List<string>();
        filePath = Application.persistentDataPath + gamedataFilename;

        gameData.gold = 0;
    }


    public void LoadGameData()
    {
        if (File.Exists(filePath + nowSlot.ToString()))
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

    public void SaveGameData()
    {
        thePlayer = FindObjectOfType<PlayerMovement>();
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
                Debug.Log(obj.transform.GetChild(0).name);
                if (!gameData.savedInventory.ContainsKey(obj.transform.GetChild(0).GetComponent<Pickup>().item
                        .itemName))
                    gameData.savedInventory[obj.transform.GetChild(0).GetComponent<Pickup>().item.itemName] = i;
            }
        }

        gameData.savedTime = DateTime.Now.ToString();
        string ToJsonData = JsonUtility.ToJson(gameData); //Json���� ��ȯ
        //  filePath = Application.persistentDataPath + gamedataFilename;
        File.WriteAllText(filePath + nowSlot.ToString(), ToJsonData);
        Debug.Log("����"); //����� ������ ������ ���
        //���� ���� �� ����� ���� ������ ������ ������ ����� ������ ��� ���� ��
    }

    public void DataClear()
    {
        nowSlot = -1;
        gameData = new GameData();
    }

    public void SaveGameDataByESC(int curSlot)
    {
        gameData.savedTime = DateTime.Now.ToString();
        string ToJsonData = JsonUtility.ToJson(gameData);

        File.WriteAllText(filePath + curSlot.ToString(), ToJsonData);
        Debug.Log("����");
    }

    public void SaveData()
    {
        thePlayer = FindObjectOfType<PlayerMovement>();
        theQuestManager = FindObjectOfType<QuestManager>();
        // Player position
        gameData.currentPosition = thePlayer.transform.position; //�÷��̾���ǥ��.
        gameData.currentRotation = thePlayer.transform.eulerAngles; // �÷��̾� rot��.
        //Quest ~ing
        gameData.questID = theQuestManager.questId;
        gameData.questClear = theQuestManager.questComplete;
        gameData.questActionIndex = theQuestManager.questActionIndex;
        for (int i = 0; i < InventoryController.Instance.mySlots.Length; i++)
        {
            GameObject obj = InventoryController.Instance.mySlots[i];
            if (obj.transform.childCount > 0)
            {
                GameObject thisitem = obj.transform.GetChild(0).gameObject;
                if (!gameData.savedInventory.ContainsKey(thisitem.GetComponent<Pickup>().item.itemName))
                    gameData.savedInventory[thisitem.GetComponent<Pickup>().item.itemName] = i;

                //DontDestroyOnLoad(thisitem);

                Debug.Log($"{thisitem.name} �����");
            }
        }
    }


    /*  private void OnApplicationQuit()
      {
          SaveGameData();     
      }                           // ���� ���� �� �ڵ� ����
    */

    //�ٸ� �κп��� ������ �ؾߵ� ���
    // DataController.Instance.SaveGameData();
}