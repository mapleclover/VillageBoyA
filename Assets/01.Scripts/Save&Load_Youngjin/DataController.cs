//작성자 : 이영진
//설명 :

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
        get => Level < orgData.GetMaxLevel(); // 작다면 트루
    }
    public bool GetEnhanceableItem
    {
        get => orgData.CheckEnhanceableItem(); // 0 이라면 트루
    }

    // 현재 문제점
    // 완료 1. 골드를 소모하지 않음 (재료)
    // 2. orgData를 인벤토리에서 끌어왔을 때 자동으로 바인딩 되도록 바꿔야 함.
    // 완료 3. Level 배열 최댓값이 아직 잡히지 않음 (집에가서 해)
    // 4. 강화 후 데이터가 출력되는 툴팁 필요 그리고 캐릭터 스텟에 적용@@@@@@
    // 5. 스크립터블에서 필요 재료 혹은 재화를 연동 해야함. >> 슬롯에 자동추가 >> 내 아이템 리스트에서
    // 완료 6. 강화 버튼 함수를 따로 만들어야 함
    // 완료 7. 강화 가능 유무 로직을 만들어야 함 (아에 강화 슬롯에 올라가지 않거나 올라가면 버튼 UI가 false)

    // 재료강화 >> 텍스트로

    // item.cs 는 스크립터블오브젝트인데 cs안에 스트럭트 구조가 포함되어도 되는가?

    // 일단 여기에 스트럭트 구조를 만들었지만 다 모였을 때 EnhanceableItems.cs 를
    // 스크립터블로 만들고 거기에서 아이템데이터 구조를 만들 수 있도록 해야할 것 같음
    // 왜냐면 아이템 전부가 이 스크립트 자식으로 만드는 것 보다 강화 가능 아이템에
    // EnhanceableItems.cs 를 추가하는게 저렴할 것 같음
}




[Serializable]
public class GameData
{
    public int curSlot;

    //모든 배열은 0이 콩 1이 진 2가 앰버
    public int turnBattleTimeSpeed = 0; // 턴배틀게임속도
    public int myProgress = 0; //진행도
    public string savedTime; //저장한 시간
    public string mapName = "FirstVillage"; //현재 마을
    public bool[] victoryComplete = Enumerable.Repeat(false, 6).ToArray();


    public Vector3 currentPosition = new Vector3(-136, 0, -90); //현재 캐릭터 위치
    public Vector3 currentRotation;

    //  public Dictionary<string, int> savedInventory = new Dictionary<string, int>(); //아이템과 몇번째 슬롯인지 저장
    // public Dictionary<string, int> myItemCount = new Dictionary<string, int>(); // 아이템과 개수
    public List<string> itemList = new List<string>();
    public List<int> slotNum = new List<int>();
    public List<int> itemCount = new List<int>();

    public int gold = 0;

    public bool isFirstTime = true;
    //아이템 구조체: 슬롯 번호, 개수, 강화 횟수, 강화된 스탯

    //포션 개수: DataController.instance.gameData.myItemCount["포션"]

    public struct myPartyStats
    {
        public bool isLeader; //리더 유무
        public int strength; //공격력
        public int defPower; //방어력
        public int HP; //체력
        public bool isAlive; //생존 여부
        public List<string> myUsedItems;
    }

  

    //폴더 안 저장된 파일 확인
    public myPartyStats Kong;
    public myPartyStats Jin;
    public myPartyStats Ember;
    public SaveItemData apple;
    public SaveItemData gloves;
    public SaveItemData goldring;
    public SaveItemData necklace;


    //public int[] questProgress = Enumerable.Repeat(0, 2).ToArray();     //퀘스트 진행도
    public int questID = 30; // 퀘스트순서
    public int questActionIndex = 0; // 퀘스트대화순서.
    public bool questClear = true; // 퀘스트클리어 유무
    public bool isBackAttack; // 빽어택으로 전투돌입인지 아닌지

    public bool[] partyMember = Enumerable.Repeat(true, 3).ToArray(); //게임 중에 파티원이 추가되면 TRUE로 바꿔줘야함, 죽으면 false?
}

public class DataController : MonoBehaviour
{
    // static GameObject _container;
    public string gamedataFilename = "VillageBoyA.json"; //.json 앞에 게임 데이터 파일 이름 설정
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
            Debug.Log("불러오기");
            string FromJsonData = File.ReadAllText(filePath + nowSlot.ToString());
            gameData = JsonUtility.FromJson<GameData>(FromJsonData); //파일이 있으면 불러옴
                                                                     //Json을 data클래스로 복구
        }
        else
        {
            Debug.Log("새로운 파일 생성");
            gameData = new GameData(); //저장된 파일이 없으면 새로 만듦
           
        }
    }



    public void SaveData()
    {
        thePlayer = GameObject.FindWithTag("Player");
        theQuestManager = FindObjectOfType<QuestManager>();
        theActionController = FindObjectOfType<ActionController>();
        // Player position
        gameData.currentPosition = thePlayer.transform.position; //플레이어좌표값.
        gameData.currentRotation = thePlayer.transform.eulerAngles; // 플레이어 rot값.
        //Quest ~ing
        gameData.questID = theQuestManager.questId;
        gameData.questClear = theQuestManager.questComplete;
        gameData.questActionIndex = theQuestManager.questActionIndex;
        //BackAttack Battle ? true : false
        gameData.isBackAttack = theActionController.isBackAttack; // 빽어택으로 전투돌입인가?
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
        gameData.currentPosition = thePlayer.transform.position; //플레이어좌표값.
        gameData.currentRotation = thePlayer.transform.eulerAngles; // 플레이어 rot값.

        //Quest ~ing
        gameData.questID = theQuestManager.questId;
        gameData.questActionIndex = theQuestManager.questActionIndex;

        //BackAttack Battle ? true : false
        gameData.isBackAttack = theActionController.isBackAttack; // 빽어택으로 전투돌입인가?

        for (int i = 0; i < InventoryController.Instance.mySlots.Length; i++) //인벤토리 저장
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
        Debug.Log("저장");
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
      }                           // 게임 종료 시 자동 저장
    */

    //다른 부분에서 저장을 해야될 경우
    // DataController.Instance.SaveGameData();
}