using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//�ڿ��� ���ӸŴ��� (��ũ�Ŵ��� �� ����Ʈ�Ŵ���ó��)
public class GameManager : MonoBehaviour
{
    public TalkManager talkManager;
    public QuestManager questManager;
    public GameObject talkPanel; // ��ȭâ���
    public Image portraitImg; // ��ȭ�� �ʻ�ȭ.
    public TMPro.TMP_Text talkText; // ��ȭ ���
    public GameObject scanObject; // ��ȭ�ϰ��ִ� ��� 
    public bool isAction; // ��ȭ�ϰ��ִ����ƴ���
    public int talkIndex; // ��ȭ����



    private void Start()
    {
        Debug.Log(questManager.CheckQuest());
    }
    public void Action(GameObject scanObj)
    {
        scanObject = scanObj;
        ObjData objData = scanObject.GetComponent<ObjData>();
        Talk(objData.id, objData.isNpc);

        talkPanel.SetActive(isAction);
    }

    private void Talk(int id, bool isNpc)
    {
        //��ȭ����
        int questTalkIndex = questManager.GetQuestTalkIndex(id);
        string talkData = talkManager.GetTalk(id + questTalkIndex, talkIndex); // ��ȭ������.
        //��ȭ����
        if(talkData == null) // ���̻� ��ȭ�ҳ����̾������ isaction�� false�ιٲ�.
        {
            isAction = false;
            talkIndex = 0; // ��ȭ�������� �ε��� �ʱ�ȭ.
            Debug.Log(questManager.CheckQuest(id)); // ����Ʈ ��ȭ�ε��� �߰�
            return; // �������Ǵ�ȭ�Ұ;����� �ٷθ���.
        }
        //Npc�ϰ�� ��� ���� �� �ʻ�ȭ
        if (isNpc)
        {
            talkText.text = talkData.Split(':')[0]; // Split�Լ��� ���� ���������ѱ�����':'�������� �յڷ� �ε���������.

            portraitImg.sprite = talkManager.GetPortrait(id, int.Parse(talkData.Split(':')[1])); // �Ű����������� int�����ΰ������°ű⋚���� Parse�� ��ȯ
            portraitImg.color = new Color(1, 1, 1, 1); // �ǵ� 1�� alpha��.
        }
        //Item
        else
        {
            talkText.text = talkData;

            portraitImg.color = new Color(1, 1, 1, 0); // �ǵ� 0�� alpha��. �����۰� ��ȭ�Ҷ��� alpha�� �ּ�.
        }

        isAction = true;
        talkIndex++; // ������ȭ�� �Ѿ.
    }
}
