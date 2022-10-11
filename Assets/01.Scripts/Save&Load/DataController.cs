using System.Collections;
using System.Collections.Generic;
using System.IO;        //���� �� ����� ���� Ȯ��
using TMPro;
using UnityEngine;
public class GameData
{
    public string name;
}
public class DataController : MonoBehaviour
{
    static GameObject _container;
    public string gamedataFilename = "VillageBoyA.json";       //.json �տ� ���� ������ ���� �̸� ����
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
                DontDestroyOnLoad(_container);  //�� �̵��� �־ ������ ��Ʈ�ѷ� ���� ������Ʈ�� ������
            }
            return _instance;
        }
    }
    public GameData _gameData;
    public GameData gameData
    {
        get
        {
            if (_gameData == null)     //������ �����ϸ� �ڵ����� ����
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
            Debug.Log("�ҷ�����");
            string FromJsonData=File.ReadAllText(filePath+nowSlot.ToString());
            gameData = JsonUtility.FromJson<GameData>(FromJsonData);   //������ ������ �ҷ���
               //Json�� dataŬ������ ����
        }
        else
        {
            Debug.Log("���ο� ���� ����");
            gameData = new GameData();     //����� ������ ������ ���� ����
        }
    }
    public void SaveGameData()
    {
        string ToJsonData=JsonUtility.ToJson(gameData);     //Json���� ��ȯ
      //  filePath = Application.persistentDataPath + gamedataFilename;
        File.WriteAllText(filePath + nowSlot.ToString(), ToJsonData);
        Debug.Log("����");        //����� ������ ������ ���
                                 //���� ���� �� ����� ���� ������ ������ ������ ����� ������ ��� ���� ��
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

    ���� ���� �� �ڵ� ����
    */


    //�ٸ� �κп��� ������ �ؾߵ� ���
   // DataController.Instance.SaveGameData();
}
