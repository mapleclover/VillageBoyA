using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using TMPro;
using static UnityEngine.UIElements.UxmlAttributeDescription;

public class Select : MonoBehaviour
{
    public GameObject create;
    public TMP_Text[] slotText;// 슬롯 버튼 아래 텍스트
    public TMPro.TMP_InputField newPlayerName;//새로 입력되는 닉네임
    bool[] savefile = new bool[3];//세이브파일 존재 유무
    bool t = false;
    void Start()
    {
        
        for(int i = 0; i < 3; i++)
        {
            if (File.Exists(DataController.instance.filePath + $"{i}")) //데이터 있음
            {
                savefile[i] = true;
                DataController.instance.nowSlot = i;
                DataController.instance.LoadGameData();
                slotText[i].text =DataController.instance.gameData.name;   //닉네임 표시
                Debug.Log(savefile[i]);
            }
            else
            {
                slotText[i].text = "Empty";
            }
        }
        DataController.instance.DataClear();//불러온 데이터 초기화(닉네임 표기만 함)
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
        if (savefile[num])
        {
            DataController.instance.LoadGameData();
            Game();     //해당 슬롯에 데이터가 존재하면 게임씬으로 이동
        }
        else
        {
            Create();       //없으면 플레이어 닉네임 입력 UI 활성화
            
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
           DataController.instance.gameData.name = newPlayerName.text;
            DataController.instance.SaveGameData(); //입력한 이름 복사 후 현재 정보 저장
        }
        SceneManager.LoadScene(DataController.instance.nowSlot + 1);  //게임씬으로 이동
    }
}
//경로: C:/Users/user/AppData/LocalLow/DefaultCompany/New Unity ProjectVillageBoyA.json