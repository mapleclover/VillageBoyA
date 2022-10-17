using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;        //���� �� ����� ���� Ȯ��
using System.Linq;
using TMPro;
using UnityEngine;
public class GameData
{
   // public string name;
    public string savedTime;
    public bool[] partyMember=Enumerable.Repeat(false,3).ToArray(); //���� �߿� ��Ƽ���� �߰��Ǹ� TRUE�� �ٲ������
    public string currentVillage="FirstVillage";
    public int myProgress = 0;
    public bool[] isLeader= Enumerable.Repeat(false, 3).ToArray();
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
   
  
  /*  private void OnApplicationQuit()
    {
        SaveGameData();     
    }                           // ���� ���� �� �ڵ� ����
  */
    
    //�ٸ� �κп��� ������ �ؾߵ� ���
   // DataController.Instance.SaveGameData();
}
