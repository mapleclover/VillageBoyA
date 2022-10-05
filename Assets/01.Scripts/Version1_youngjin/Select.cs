using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class Select : MonoBehaviour
{
    public GameObject create;
    public Text[] slotText;// ���� ��ư �Ʒ� �ؽ�Ʈ
    public Text newPlayerName;//���� �ԷµǴ� �г���
    bool[] savefile = new bool[3];//���̺����� ���� ����
    void Start()
    {
        for(int i = 0; i < 3; i++)
        {
            if (File.Exists(DataController.Instance.filePath + $"{i}")) //������ ����
            {
                savefile[i] = true;
                DataController.Instance.nowSlot = i;
                DataController.Instance.LoadGameData();
                slotText[i].text = DataController.Instance.gameData.name;   //�г��� ǥ��
            }
            else
            {
                slotText[i].text = "�������";
            }
        }
        DataController.Instance.DataClear();//�ҷ��� ������ �ʱ�ȭ(�г��� ǥ�⸸ ��)
    }
    public void Slot(int num)
    {
        DataController.Instance.nowSlot = num;
        if (savefile[num])
        {
            DataController.Instance.nowSlot = num;
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
    }
    public void Game()      
    {
        if (!savefile[DataController.Instance.nowSlot])     //���� ���Կ� ������ ������ 
        {
            DataController.Instance.gameData.name = newPlayerName.text;
            DataController.Instance.SaveGameData(); //�Է��� �̸� ���� �� ���� ���� ����
        }
        SceneManager.LoadScene(1);  //���Ӿ����� �̵�
    }


}
