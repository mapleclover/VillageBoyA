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
    public TMP_Text[] slotText;// ���� ��ư �Ʒ� �ؽ�Ʈ
    public TMPro.TMP_InputField newPlayerName;//���� �ԷµǴ� �г���
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
                slotText[i].text =DataController.instance.gameData.name;   //�г��� ǥ��
                Debug.Log(savefile[i]);
            }
            else
            {
                slotText[i].text = "Empty";
            }
        }
        DataController.instance.DataClear();//�ҷ��� ������ �ʱ�ȭ(�г��� ǥ�⸸ ��)
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
            Game();     //�ش� ���Կ� �����Ͱ� �����ϸ� ���Ӿ����� �̵�
        }
        else
        {
            Create();       //������ �÷��̾� �г��� �Է� UI Ȱ��ȭ
            
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
           DataController.instance.gameData.name = newPlayerName.text;
            DataController.instance.SaveGameData(); //�Է��� �̸� ���� �� ���� ���� ����
        }
        SceneManager.LoadScene(DataController.instance.nowSlot + 1);  //���Ӿ����� �̵�
    }
}
//���: C:/Users/user/AppData/LocalLow/DefaultCompany/New Unity ProjectVillageBoyA.json