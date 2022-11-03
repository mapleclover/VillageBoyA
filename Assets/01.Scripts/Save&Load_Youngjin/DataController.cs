using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;        //���� �� ����� ���� Ȯ��
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
    //key�� ������, value�� ��Ƽ��
   //2. List<List<GameObject>> partyItems = new List<List<GameObject>>();
    //  partyItems[0]�� ����ĳ���� 1�� �� 2�� �ڹ�
    public List<List<GameObject>> partyItems=new List<List<GameObject>>();


    public int[] questProgress = Enumerable.Repeat(0, 3).ToArray();


    public bool[] isLeader = { true,false,false };
    public int[] partyHP = Enumerable.Repeat(100,3).ToArray();
    public bool[] partyMember = Enumerable.Repeat(false, 3).ToArray(); //���� �߿� ��Ƽ���� �߰��Ǹ� TRUE�� �ٲ������

}
public class DataController: MonoBehaviour
{
    static GameObject _container;
    public string gamedataFilename = "VillageBoyA.json";       //.json �տ� ���� ������ ���� �̸� ����
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
        gameData.savedTime = DateTime.Now.ToString();
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
   
  public void SaveGameDataByESC(int curSlot)
    {
        gameData.isLeader[0] = false;
        gameData.isLeader[1] = true;
        gameData.savedTime = DateTime.Now.ToString();
        string ToJsonData = JsonUtility.ToJson(gameData);     
                                                             
        File.WriteAllText(filePath + curSlot.ToString(), ToJsonData);
        Debug.Log("����");       
    }
  /*  private void OnApplicationQuit()
    {
        SaveGameData();     
    }                           // ���� ���� �� �ڵ� ����
  */
    
    //�ٸ� �κп��� ������ �ؾߵ� ���
   // DataController.Instance.SaveGameData();
}
