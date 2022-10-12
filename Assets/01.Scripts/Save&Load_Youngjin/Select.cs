using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using TMPro;
using static UnityEngine.UIElements.UxmlAttributeDescription;
using System.Data;

public class Select : MonoBehaviour
{
    public static Select instance;
    public GameObject create;
    public TextMeshProUGUI[] slotText;// ���� ��ư �Ʒ� �ؽ�Ʈ
    bool[] savefile = new bool[3];//���̺����� ���� ����
    bool t = false;
    void Start()
    {
       
        for(int i = 0; i < 3; i++)
        {
            if (File.Exists(DataController.instance.filePath + $"{i}")) //������ ����
            {
                savefile[i] = true;
                DataController.instance.nowSlot = i;
                DataController.instance.LoadGameData();
                slotText[i].text="Saved Date:"+DataController.instance.gameData.savedTime;   //������ �ð� ǥ��
               

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