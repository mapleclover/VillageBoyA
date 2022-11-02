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
    public TextMeshProUGUI[] slotText;// ���� ��ư �Ʒ� �ؽ�Ʈ
    public bool[] savefile = new bool[3];//���̺����� ���� ����
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
     //�ش� ���Կ� �����Ͱ� �����ϸ� ���Ӿ����� �̵�
        }
        Game();

    }

    public void Game()      
    {
        if (!savefile[DataController.instance.nowSlot])     //���� ���Կ� ������ ������ 
        {
            DataController.instance.gameData.savedTime = DateTime.Now.ToString(("yyyy-MM-dd HH:mm:ss tt"));
         
            DataController.instance.gameData.partyMember[1] = true;
            DataController.instance.gameData.partyMember[2] = true;
            savefile[DataController.instance.nowSlot] = true;
            DataController.instance.SaveGameData(); //�Է��� �̸� ���� �� ���� ���� ����
        }
        SceneManager.LoadScene(6);  //���Ӿ����� �̵�
    }
    public void ShowUI()
    {
        for (int i = 0; i < 3; i++)
        {
            if (File.Exists(DataController.instance.filePath + $"{i}")) //������ ����
            {
                savefile[i] = true;
                DataController.instance.nowSlot = i;
                DataController.instance.LoadGameData();
                Vector2 temp = new Vector2(-110, 0);
                slotText[i].transform.localPosition = temp;
                slotText[i].text = "Saved Date:" + DataController.instance.gameData.savedTime;   //������ �ð� ǥ��      
                                                                                                 //  slotText[i].text+="\n"+ $"<color=blue>{DataController.instance.gameData.currentVillage}</color>";//���� �ִ� ���� ǥ��
                slotText[i].text += $"\nMy Progress={DataController.instance.gameData.myProgress}%";
                DataController.instance.gameData.partyMember[0] = true;
                DataController.instance.gameData.partyMember[1] = true;
                DataController.instance.gameData.partyMember[2] = true;
                for (int j = 0; j < DataController.instance.gameData.partyMember.Length; j++)
                {
                    if (DataController.instance.gameData.partyMember[j])
                    {
                       
                        Vector3 position = myMember[j].transform.localPosition;
                        switch (j)
                        {
                            case 0:
                                position.x = 140;
                                break;
                            case 1:
                                position.x = 220;
                                break;
                            case 2:
                                position.x = 300;
                                break;
                        }
                        position.y = buttonList[i].transform.localPosition.y;

                        GameObject obj = Instantiate(myMember[j], position, Quaternion.identity);
                        if(partyPortrait[i][j]!=null)
                        {
                            Destroy(partyPortrait[i][j]);

                        }
                        partyPortrait[i][j] = obj;
                        if (DataController.instance.gameData.isLeader[j] == true)
                        {
                            obj.GetComponent<RectTransform>().sizeDelta = new Vector2(70.0f, 70.0f);
                        }
                        obj.transform.SetParent(mySaveLoad.transform);
                        obj.transform.localPosition = position;
                        obj.SetActive(true);
                        //��Ƽ���� ������ ������ ��
                    }
                }
            }
            else
            {
                slotText[i].text = "<color=grey>No Saved Data</color> ";

            }
        }
        DataController.instance.DataClear();//�ҷ��� ������ �ʱ�ȭ(�ð��� ǥ�⸸ ��)
    }
<<<<<<< Updated upstream
=======
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
     //�ش� ���Կ� �����Ͱ� �����ϸ� ���Ӿ����� �̵�
        }
        Game();

    }

    public void Game()      
    {
        if (!savefile[DataController.instance.nowSlot])     //���� ���Կ� ������ ������ 
        {
            DataController.instance.gameData.savedTime = DateTime.Now.ToString(("yyyy-MM-dd HH:mm:ss tt"));
            DataController.instance.gameData.isLeader[0] = true;    //�� ó���� ó�� ������ ��Ƽ���� ����
            DataController.instance.gameData.partyMember[1] = true;
            DataController.instance.gameData.partyMember[2] = true;
            savefile[DataController.instance.nowSlot] = true;
            DataController.instance.SaveGameData(); //�Է��� �̸� ���� �� ���� ���� ����
        }
        SceneManager.LoadScene(7);  //���Ӿ����� �̵�
    }
>>>>>>> Stashed changes
}
//���: C:/Users/user/AppData/LocalLow/DefaultCompany/New Unity ProjectVillageBoyA.json