using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;        //폴더 안 저장된 파일 확인
using System.Linq;
using TMPro;
using UnityEngine;
public class GameData
{
    public int myProgress = 0;
    public string savedTime;

    public string mapName="FirstVillage";
    public Vector3 currentPosition = Vector3.zero;
    public List<GameObject> currentItems = new List<GameObject>();
    //1. public Dictionary<GameObject, GameObject> whoHasWhat = new Dictionary<GameObject, GameObject>();
    //key가 아이템, value가 파티원
   //2. List<List<GameObject>> partyItems = new List<List<GameObject>>();
    //  partyItems[0]이 메인캐릭터 1이 진 2가 앰버
    public List<List<GameObject>> partyItems=new List<List<GameObject>>();


    public int[] questProgress = Enumerable.Repeat(0, 3).ToArray();


    public bool[] isLeader = { true,false,false };
    public int[] partyHP = Enumerable.Repeat(100,3).ToArray();
    public bool[] partyMember = Enumerable.Repeat(false, 3).ToArray(); //게임 중에 파티원이 추가되면 TRUE로 바꿔줘야함

}
public class DataController: MonoBehaviour
{
    static GameObject _container;
    public string gamedataFilename = "VillageBoyA.json";       //.json 앞에 게임 데이터 파일 이름 설정
   public string filePath;
    public int nowSlot;
  public  GameData gameData=new GameData();
    public static DataController instance;

  
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
