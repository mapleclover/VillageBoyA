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
    public TextMeshProUGUI[] slotText;// ���� ��ư �Ʒ� �ؽ�Ʈ
    public bool[] savefile = new bool[3];//���̺����� ���� ����
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
            if (File.Exists(DataController.instance.filePath + $"{i}")) //������ ����
            {
                savefile[i] = true;
                DataController.instance.nowSlot = i;
                DataController.instance.LoadGameData();
                Vector2 temp = new Vector2(-110, 0);
                slotText[i].transform.localPosition = temp;
                slotText[i].text="Saved Date:"+DataController.instance.gameData.savedTime;   //������ �ð� ǥ��      
              //  slotText[i].text+="\n"+ $"<color=blue>{DataController.instance.gameData.currentVillage}</color>";//���� �ִ� ���� ǥ��
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
            Game();     //�ش� ���Կ� �����Ͱ� �����ϸ� ���Ӿ����� �̵�
        }
        else
        {
            Create();       //������ UI Ȱ��ȭ
            
        }

    }
    public void Create()
    {
        create.transform.SetAsLastSibling();
        create.gameObject.SetActive(true);

    }
    public void Game()      
    {
        if (!savefile[DataController.instance.nowSlot])     //���� ���Կ� ������ ������ 
        {
            DataController.instance.gameData.savedTime = DateTime.Now.ToString(("yyyy-MM-dd HH:mm:ss tt"));
            DataController.instance.gameData.isLeader[0] = true;    //�� ó���� ó�� ������ ��Ƽ���� ����
            savefile[DataController.instance.nowSlot] = true;
            DataController.instance.SaveGameData(); //�Է��� �̸� ���� �� ���� ���� ����
        }
        SceneManager.LoadScene(1);  //���Ӿ����� �̵�
    }
}
//���: C:/Users/user/AppData/LocalLow/DefaultCompany/New Unity ProjectVillageBoyA.json