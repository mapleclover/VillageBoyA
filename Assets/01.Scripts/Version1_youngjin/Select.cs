using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class Select : MonoBehaviour
{
    public GameObject create;
    public Text[] slotText;// 슬롯 버튼 아래 텍스트
    public Text newPlayerName;//새로 입력되는 닉네임
    bool[] savefile = new bool[3];//세이브파일 존재 유무
    void Start()
    {
        for(int i = 0; i < 3; i++)
        {
            if (File.Exists(DataController.Instance.filePath + $"{i}")) //데이터 있음
            {
                savefile[i] = true;
                DataController.Instance.nowSlot = i;
                DataController.Instance.LoadGameData();
                slotText[i].text = DataController.Instance.gameData.name;   //닉네임 표시
            }
            else
            {
                slotText[i].text = "비어있음";
            }
        }
        DataController.Instance.DataClear();//불러온 데이터 초기화(닉네임 표기만 함)
    }
    public void Slot(int num)
    {
        DataController.Instance.nowSlot = num;
        if (savefile[num])
        {
            DataController.Instance.nowSlot = num;
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
    }
    public void Game()      
    {
        if (!savefile[DataController.Instance.nowSlot])     //현재 슬롯에 데이터 없으면 
        {
            DataController.Instance.gameData.name = newPlayerName.text;
            DataController.Instance.SaveGameData(); //입력한 이름 복사 후 현재 정보 저장
        }
        SceneManager.LoadScene(1);  //게임씬으로 이동
    }


}
