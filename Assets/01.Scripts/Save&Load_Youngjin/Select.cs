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
using System.Runtime.ConstrainedExecution;

[System.Serializable]

public class Select : MonoBehaviour
{
    public static Select instance=null;
    public TextMeshProUGUI[] slotText;// 슬롯 버튼 아래 텍스트
    public bool[] savefile = new bool[3];//세이브파일 존재 유무
    public GameObject[] buttonList;
    public GameObject[] myMember;
    public GameObject mySaveLoad;
    public GameObject[][] partyPortrait = new GameObject[3][];
    
    void Start()        
    {
        for(int i = 0; i < 3; i++)
        {
            partyPortrait[i] = new GameObject[3];
        }
        ShowUI();
        instance = this;
    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Slot(Button_New.cur);

        }

    }
    public void Slot(int num)
    {
        DataController.instance.nowSlot = num;
        if (savefile[num])
        {
            DataController.instance.LoadGameData();
     //해당 슬롯에 데이터가 존재하면 게임씬으로 이동
        }
        Game();

    }

    public void Game()      
    {
        if (!savefile[DataController.instance.nowSlot])     //현재 슬롯에 데이터 없으면 
        {
            DataController.instance.gameData.savedTime = DateTime.Now.ToString(("yyyy-MM-dd HH:mm:ss tt"));

            savefile[DataController.instance.nowSlot] = true;
            DataController.instance.SaveGameData(); //입력한 이름 복사 후 현재 정보 저장
        }
        SceneManager.LoadScene(8);  //게임씬으로 이동
    }
    public void ShowUI()
    {
        for (int i = 0; i < 3; i++)
        {
            if (File.Exists(DataController.instance.filePath + $"{i}")) //데이터 있음
            {
                savefile[i] = true;
                DataController.instance.nowSlot = i;
                DataController.instance.LoadGameData();
                Vector2 temp = new Vector2(-110, 0);
                slotText[i].transform.localPosition = temp;
                slotText[i].text = "Saved Date:" + DataController.instance.gameData.savedTime;   //저장한 시간 표시      
                                                                                                 //  slotText[i].text+="\n"+ $"<color=blue>{DataController.instance.gameData.currentVillage}</color>";//현재 있는 마을 표시
                slotText[i].text += $"\nMy Progress={DataController.instance.gameData.myProgress}%";
                DataController.instance.gameData.Kong.isAlive = true;
                DataController.instance.gameData.Jin.isAlive = true;
                DataController.instance.gameData.Ember.isAlive = true;
                    if (DataController.instance.gameData.Kong.isAlive)
                    {
                       
                        Vector3 position = myMember[0].transform.localPosition;
                        position.x = 140;
                        position.y = buttonList[i].transform.localPosition.y;
                        GameObject obj = Instantiate(myMember[0], position, Quaternion.identity);
                        

                        if(partyPortrait[i][0]!=null)
                        {
                            Destroy(partyPortrait[i][0]);
                        }
                        partyPortrait[i][0] = obj;
                        if (DataController.instance.gameData.Kong.isLeader)
                        {
                            obj.GetComponent<RectTransform>().sizeDelta = new Vector2(70.0f, 70.0f);
                        }
                        obj.transform.SetParent(mySaveLoad.transform);
                        obj.transform.localPosition = position;
                        obj.SetActive(true);
                        //파티원이 있으면 사진이 뜸
                    }
                    if (DataController.instance.gameData.Kong.isAlive)
                    {

                        Vector3 position = myMember[1].transform.localPosition;
                        position.x = 220;
                        position.y = buttonList[i].transform.localPosition.y;
                        GameObject obj = Instantiate(myMember[1], position, Quaternion.identity);
                       

                        if (partyPortrait[i][1] != null)
                        {
                            Destroy(partyPortrait[i][1]);
                        }
                        partyPortrait[i][1] = obj;
                        if (DataController.instance.gameData.Kong.isLeader)
                        {
                            obj.GetComponent<RectTransform>().sizeDelta = new Vector2(70.0f, 70.0f);
                        }
                        obj.transform.SetParent(mySaveLoad.transform);
                        obj.transform.localPosition = position;
                        obj.SetActive(true);
                        //파티원이 있으면 사진이 뜸
                    }
                    if (DataController.instance.gameData.Kong.isAlive)
                    {

                        Vector3 position = myMember[2].transform.localPosition;
                        position.x = 300;
                        position.y = buttonList[i].transform.localPosition.y;
                        GameObject obj = Instantiate(myMember[2], position, Quaternion.identity);
                      

                        if (partyPortrait[i][2] != null)
                        {
                            Destroy(partyPortrait[i][2]);
                        }
                        partyPortrait[i][2] = obj;
                        if (DataController.instance.gameData.Kong.isLeader)
                        {
                            obj.GetComponent<RectTransform>().sizeDelta = new Vector2(70.0f, 70.0f);
                        }
                        obj.transform.SetParent(mySaveLoad.transform);
                        obj.transform.localPosition = position;
                        obj.SetActive(true);
                        //파티원이 있으면 사진이 뜸
                    }
                }
            
            else
            {
                slotText[i].text = "<color=grey>No Saved Data</color> ";

            }
        }
        DataController.instance.DataClear();//불러온 데이터 초기화(시간만 표기만 함)
    }

   
}
//경로: C:/Users/user/AppData/LocalLow/DefaultCompany/New Unity ProjectVillageBoyA.json