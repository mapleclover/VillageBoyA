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
    public GameObject create;
    public TextMeshProUGUI[] slotText;// 슬롯 버튼 아래 텍스트
    public bool[] savefile = new bool[3];//세이브파일 존재 유무
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
                Vector2 temp = new Vector2(-110, 0);
                slotText[i].transform.localPosition = temp;
                slotText[i].text="Saved Date:"+DataController.instance.gameData.savedTime;   //저장한 시간 표시      
              //  slotText[i].text+="\n"+ $"<color=blue>{DataController.instance.gameData.currentVillage}</color>";//현재 있는 마을 표시
                slotText[i].text += $"\nMy Progress={DataController.instance.gameData.myProgress}%";
                DataController.instance.gameData.partyMember[0] = true;
                for(int j=0;j< DataController.instance.gameData.partyMember.Length;j++)
                {
                    if (DataController.instance.gameData.partyMember[j])
                    {
                        Vector3 position = myMember[j].transform.localPosition;
                        switch (j)
                        {
                            case 0:
                                position.x = 120;
                                break;
                            case 1:
                                position.x = 185;
                                break;
                            case 2:
                                position.x = 250;
                                break;
                        }
                        switch (i)
                        {
                            case 0:
                                position.y = 153;
                                break;
                            case 1:
                                position.y = 0;
                                break;
                            case 2:
                                position.y = -153;
                                break;
                        }
                        GameObject obj = Instantiate(myMember[j],position,Quaternion.identity);
                        if (DataController.instance.gameData.isLeader[j] == true)
                        {
                            obj.GetComponent<RectTransform>().sizeDelta = new Vector2(80.0f, 80.0f);
                        }
                        obj.transform.parent = GameObject.Find("SaveLoad").transform;
                        obj.transform.localPosition = position;
                        obj.SetActive(true);
                                                //파티원이 있으면 사진이 뜸
                    }
                }
            }
            else
            {
                slotText[i].text = "<color=grey>No Saved Data</color> ";
               
            }
        }
        DataController.instance.DataClear();//불러온 데이터 초기화(시간만 표기만 함)
    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Slot(Button_New.cur);

        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
          create.gameObject.SetActive(false);
        }
    }
    public void Slot(int num)
    {
        DataController.instance.nowSlot = num;
        if (savefile[num])
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
        create.transform.SetAsLastSibling();
        create.gameObject.SetActive(true);

    }
    public void Game()      
    {
        if (!savefile[DataController.instance.nowSlot])     //현재 슬롯에 데이터 없으면 
        {
            DataController.instance.gameData.savedTime = DateTime.Now.ToString(("yyyy-MM-dd HH:mm:ss tt"));
            DataController.instance.gameData.isLeader[0] = true;    //맨 처음에 처음 나오는 파티원이 리더
            savefile[DataController.instance.nowSlot] = true;
            DataController.instance.SaveGameData(); //입력한 이름 복사 후 현재 정보 저장
        }
        SceneManager.LoadScene(1);  //게임씬으로 이동
    }
}
//경로: C:/Users/user/AppData/LocalLow/DefaultCompany/New Unity ProjectVillageBoyA.json