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
    public TextMeshProUGUI[] slotText;// ���� ��ư �Ʒ� �ؽ�Ʈ
    bool[] savefile = new bool[3];//���̺����� ���� ����
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
            if (File.Exists(DataController.instance.filePath + $"{i}")) //������ ����
            {
                savefile[i] = true;
                DataController.instance.nowSlot = i;
                DataController.instance.LoadGameData();
                slotText[i].text="Saved Date:"+DataController.instance.gameData.savedTime;   //������ �ð� ǥ��                                                                           //  myParty[i].SetActive(true);
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
                                                //��Ƽ���� ������ ������ ��
                    }
                }
            }
            else
            {
                slotText[i].text = "Empty";
               
            }
        }
        DataController.instance.DataClear();//�ҷ��� ������ �ʱ�ȭ(�ð��� ǥ�⸸ ��)
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
            Game();     //�ش� ���Կ� �����Ͱ� �����ϸ� ���Ӿ����� �̵�
        }
        else
        {
            Create();       //������ UI Ȱ��ȭ
            
        }

    }
    public void Create()
    {
        create.gameObject.SetActive(true);
   
        t = true;
    }
    public void Game()      
    {
        if (!savefile[DataController.instance.nowSlot])     //���� ���Կ� ������ ������ 
        {
            DataController.instance.gameData.savedTime = DateTime.Now.ToString(("yyyy-MM-dd HH:mm:ss tt"));
            DataController.instance.SaveGameData(); //�Է��� �̸� ���� �� ���� ���� ����
        }
        SceneManager.LoadScene(DataController.instance.nowSlot + 1);  //���Ӿ����� �̵�
    }
}
//���: C:/Users/user/AppData/LocalLow/DefaultCompany/New Unity ProjectVillageBoyA.json