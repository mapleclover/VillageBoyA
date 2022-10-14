using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using TMPro;
//using static UnityEngine.UIElements.UxmlAttributeDescription;
using System.Data;
[System.Serializable]

public class Select : MonoBehaviour
{
    public static Select instance;
    public GameObject create;
    public TextMeshProUGUI[] slotText;// 슬롯 버튼 아래 텍스트
    bool[] savefile = new bool[3];//세이브파일 존재 유무
    bool t = false;
    //  public GameObject[] myParty;
    //   public GameObject[] myMember; 
    /*
    [Serializable]
    public class Party
    {
        public static GameObject[]myMember=new GameObject[3];
    }
    
    public Party[] myParty=new Party[3];
    */
    public GameObject[] myMember;
    void Start()        
    {
       
        for(int i = 0; i < 3; i++)
        {
            if (File.Exists(DataController.instance.filePath + $"{i}")) //데이터 있음
            {
                savefile[i] = true;
                DataController.instance.nowSlot = i;
                DataController.instance.LoadGameData();
                slotText[i].text="Saved Date:"+DataController.instance.gameData.savedTime;   //저장한 시간 표시                                                                           //  myParty[i].SetActive(true);
                DataController.instance.gameData.partyMember[0] = true;
                for(int j=0;j< DataController.instance.gameData.partyMember.Length;j++)
                {
                    if (DataController.instance.gameData.partyMember[j])
                    {
                        Vector3 position = myMember[j].transform.localPosition;
                        switch (j)
                        {
                            case 0:
                                position.x = 200;
                                break;
                            case 1:
                                position.x = 265;
                                break;
                            case 2:
                                position.x = 330;
                                break;
                        }
                        switch (i)
                        {
                            case 0:
                                position.y = 124;
                                break;
                            case 1:
                                position.y = 0;
                                break;
                            case 2:
                                position.y = -124;
                                break;
                        }
                        GameObject obj = Instantiate(myMember[j],position,Quaternion.identity);
                        obj.transform.parent = GameObject.Find("SaveLoad").transform;
                        obj.transform.localPosition = position;
                        obj.SetActive(true);
                                                //파티원이 있으면 사진이 뜸
                    }
                }
            }
            else
            {
                slotText[i].text = "Empty";
               
            }
        }
        DataController.instance.DataClear();//불러온 데이터 초기화(시간만 표기만 함)
    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && t == true)
        {
            t = false;
            create.gameObject.SetActive(false);
        }
    }
    public void Slot(int num)
    {
        DataController.instance.nowSlot = num;
        if (savefile[num] && t==true)
        {
            DataController.instance.LoadGameData();
            create.gameObject.SetActive(false);
            Game();     //해당 슬롯에 데이터가 존재하면 게임씬으로 이동
        }
        else
        {
            Create();       //없으면 UI 활성화
            
        }

    }
    public void Create()
    {
        create.gameObject.SetActive(true);
   
        t = true;
    }
    public void Game()      
    {
        if (!savefile[DataController.instance.nowSlot])     //현재 슬롯에 데이터 없으면 
        {
            DataController.instance.gameData.savedTime = DateTime.Now.ToString(("yyyy-MM-dd HH:mm:ss tt"));
            DataController.instance.SaveGameData(); //입력한 이름 복사 후 현재 정보 저장
        }
        SceneManager.LoadScene(DataController.instance.nowSlot + 1);  //게임씬으로 이동
    }
}
//경로: C:/Users/user/AppData/LocalLow/DefaultCompany/New Unity ProjectVillageBoyA.json