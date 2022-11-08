using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;        //폴더 안 저장된 파일 확인
using System.Linq;
using TMPro;
using UnityEngine;
public class GameData
{                                                                                                               //모든 배열은 0이 콩 1이 진 2가 앰버
    public int myProgress = 0;          //진행도
    public string savedTime;            //저장한 시간
    public string mapName = "FirstVillage";           //현재 마을



    public Vector3 currentPosition = Vector3.zero;          //현재 캐릭터 위치
    public Vector3 currentRotation;

   // public List<GameObject> currentItems = new List<GameObject>();          //현재까지 얻은 아이템
    public List<List<GameObject>> partyItems=new List<List<GameObject>>();          //파티원마다 장착한 장비
    public Dictionary<GameObject, Vector3> savedInventory = new Dictionary<GameObject, Vector3>();          //아이템과 몇번째 슬롯인지 저장


    //public int[] questProgress = Enumerable.Repeat(0, 2).ToArray();     //퀘스트 진행도
    public int questID; // 퀘스트순서
    public int questActionIndex; // 퀘스트대화순서.
    public bool isBackAttack; // 빽어택으로 전투돌입인지 아닌지



    public bool[] isLeader = { true,false,false };                  //누가 리더인지
    public int[] partyHP = Enumerable.Repeat(100,3).ToArray();          //파티원 개개인의 HP
    public int[] partySpeed = {10,20,30 };              //파티원 개개인의 speed
    public bool[] partyMember = Enumerable.Repeat(false, 3).ToArray(); //게임 중에 파티원이 추가되면 TRUE로 바꿔줘야함, 죽으면 false?

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
        }
        else if (instance != this)
        {
            Destroy(instance.gameObject);
        }

        for(int i = 0; i < 3; i++)
        {
            gameData.partyItems.Add(new List<GameObject>());
        }
        DontDestroyOnLoad(this.gameObject);
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

        // Player position
        gameData.currentPosition = thePlayer.transform.position; //플레이어좌표값.
        gameData.currentRotation = thePlayer.transform.eulerAngles; // 플레이어 rot값.
         
        // Quest ~ing
        gameData.questID = theQuestManager.questId;
        gameData.questActionIndex = theQuestManager.questActionIndex;

        // BackAttack Battle ? true : false
        gameData.isBackAttack = theActionController.isBackAttack; // 빽어택으로 전투돌입인가?


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
        gameData.isLeader[0] = false;
        gameData.isLeader[1] = true;
        gameData.savedTime = DateTime.Now.ToString();
        string ToJsonData = JsonUtility.ToJson(gameData);     
                                                             
        File.WriteAllText(filePath + curSlot.ToString(), ToJsonData);
        Debug.Log("저장");       
    }
  /*  private void OnApplicationQuit()
    {
        SaveGameData();     
    }                           // 게임 종료 시 자동 저장
  */
    
    //다른 부분에서 저장을 해야될 경우
   // DataController.Instance.SaveGameData();
}
