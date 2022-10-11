using System.Collections;
using System.Collections.Generic;
using System.IO;        //폴더 안 저장된 파일 확인
using TMPro;
using UnityEngine;
public class GameData
{
    public string name;
}
public class DataController : MonoBehaviour
{
    static GameObject _container;
    public string gamedataFilename = "VillageBoyA.json";       //.json 앞에 게임 데이터 파일 이름 설정
   public string filePath;
    public int nowSlot;
  public GameData gameData=new GameData();
    public static DataController instance;

    /*
    static GameObject Container
    {
        get
        {
            return _container;
        }
    }
    static DataController _instance;
    public static DataController Instance
    {
        get
        {
            if (!_instance)
            {
                _container = new GameObject();
               // _container.name = "DataController";
                _instance=_container.AddComponent(typeof(DataController))as DataController;
                DontDestroyOnLoad(_container);  //씬 이동이 있어도 데이터 컨트롤러 게임 오브젝트가 유지됨
            }
            return _instance;
        }
    }
    public GameData _gameData;
    public GameData gameData
    {
        get
        {
            if (_gameData == null)     //게임이 시작하면 자동으로 실행
            {
                LoadGameData();
                SaveGameData();
            }
            return _gameData;
        }

    }
    */
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
        DontDestroyOnLoad(this.gameObject);
        filePath = Application.persistentDataPath + gamedataFilename;
    }

    public void LoadGameData()
    {
        if (File.Exists(filePath))
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
    /*
    private void OnApplicationQuit()
    {
        SaveGameData();     
    }

    게임 종료 시 자동 저장
    */


    //다른 부분에서 저장을 해야될 경우
   // DataController.Instance.SaveGameData();
}
