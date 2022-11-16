using System;
using System.Collections;
using System.Collections.Generic;                                           
using System.IO;        //폴더 안 저장된 파일 확인
using System.Linq;
using TMPro;
using UnityEngine;
[Serializable]
public class GameData
{                                                                                                               //모든 배열은 0이 콩 1이 진 2가 앰버
    public int myProgress = 0;          //진행도
    public string savedTime;            //저장한 시간
    public string mapName = "FirstVillage";           //현재 마을
    public bool[] victoryComplete = Enumerable.Repeat(false, 6).ToArray();


    public Vector3 currentPosition = new Vector3(-136, 0, -90);          //현재 캐릭터 위치
    public Vector3 currentRotation;

    public Dictionary<string, int> savedInventory = new Dictionary<string, int>();          //아이템과 몇번째 슬롯인지 저장
    public Dictionary<string, int> myItemCount = new Dictionary<string, int>();      // 아이템과 개수
    public int gold=0;

    //아이템 구조체: 슬롯 번호, 개수, 강화 횟수, 강화된 스탯

    //포션 개수: DataController.instance.gameData.myItemCount["포션"]

    public struct myPartyStats
    {
       public  bool isLeader;  //리더 유무
        public int strength;   //공격력
        public int defPower;   //방어력
        public int speed;  //속도
        public int HP;  //체력
        public bool isAlive; //생존 여부
        public List<string> myUsedItems;
    }
    public myPartyStats Kong;
   public myPartyStats Jin;
    public myPartyStats Ember;




    //public int[] questProgress = Enumerable.Repeat(0, 2).ToArray();     //퀘스트 진행도
    public int questID = 30; // 퀘스트순서
    public int questActionIndex = 0; // 퀘스트대화순서.
    public bool questClear = true; // 퀘스트클리어 유무
    public bool isBackAttack; // 빽어택으로 전투돌입인지 아닌지

    public bool[] partyMember = Enumerable.Repeat(true, 3).ToArray(); //게임 중에 파티원이 추가되면 TRUE로 바꿔줘야함, 죽으면 false?
    
}
public class DataController: MonoBehaviour
{
    
   // static GameObject _container;
    public string gamedataFilename = "VillageBoyA.json";       //.json 앞에 게임 데이터 파일 이름 설정
    public string filePath;
    public int nowSlot;
    public  GameData gameData=new GameData();
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
            gameData.gold = 0;
        }
        else if (instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        //  gameData.myInventory = new List<GameData.myPartyItems>();
            filePath = Application.persistentDataPath + gamedataFilename;
 
    }

    

    public void LoadGameData()
    {
        if (File.Exists(filePath+nowSlot.ToString()))
        {
            Debug.Log("불러오기");
            string FromJsonData=File.ReadAllText(filePath+nowSlot.ToString());
            gameData = JsonUtility.FromJson<GameData>(FromJsonData);   //파일이 있으면 불러옴
               //Json을 data클래스로 복구
        }
        else
        {
            Debug.Log("새로운 파일 생성");
            gameData = new GameData();     //저장된 파일이 없으면 새로 만듦
        }
    }
    public void SaveGameData()
    {
        thePlayer = FindObjectOfType<PlayerMovement>();
        theQuestManager = FindObjectOfType<QuestManager>();
        theActionController = FindObjectOfType<ActionController>();

        //Player position
        gameData.currentPosition = thePlayer.transform.position; //플레이어좌표값.
        gameData.currentRotation = thePlayer.transform.eulerAngles; // 플레이어 rot값.

        //Quest ~ing
        gameData.questID = theQuestManager.questId;
        gameData.questActionIndex = theQuestManager.questActionIndex;

       //BackAttack Battle ? true : false
        gameData.isBackAttack = theActionController.isBackAttack; // 빽어택으로 전투돌입인가?

        for (int i = 0; i < InventoryController.Instance.mySlots.Length; i++)       //인벤토리 저장
        {
            GameObject obj = InventoryController.Instance.mySlots[i];
            if (obj.transform.childCount > 0)
            {
                Debug.Log(obj.transform.GetChild(0).name);
                if (!gameData.savedInventory.ContainsKey(obj.transform.GetChild(0).GetComponent<Pickup>().item.itemName))
                    gameData.savedInventory[obj.transform.GetChild(0).GetComponent<Pickup>().item.itemName] = i;
            }
        }
        gameData.savedTime = DateTime.Now.ToString();
        string ToJsonData=JsonUtility.ToJson(gameData);     //Json으로 변환
                                                            //  filePath = Application.persistentDataPath + gamedataFilename;
        File.WriteAllText(filePath + nowSlot.ToString(), ToJsonData);
        Debug.Log("저장");        //저장된 파일이 있으면 덮어씀
                                 //게임 실행 후 저장된 파일 없으면 데이터 파일을 만들기 때문에 계속 덮어 씀
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
        Debug.Log("저장");       
    }

    public void SaveData()
    {
        thePlayer = FindObjectOfType<PlayerMovement>();
        theQuestManager = FindObjectOfType<QuestManager>();
        // Player position
        gameData.currentPosition = thePlayer.transform.position; //플레이어좌표값.
        gameData.currentRotation = thePlayer.transform.eulerAngles; // 플레이어 rot값.
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
                if (!gameData.savedInventory.ContainsKey(thisitem.GetComponent<Pickup>().item.itemName)) gameData.savedInventory[thisitem.GetComponent<Pickup>().item.itemName] = i;

                //DontDestroyOnLoad(thisitem);

                Debug.Log($"{thisitem.name} 저장됨");
            }
        }

    }

   


    /*  private void OnApplicationQuit()
      {
          SaveGameData();     
      }                           // 게임 종료 시 자동 저장
    */

    //다른 부분에서 저장을 해야될 경우
    // DataController.Instance.SaveGameData();
}
