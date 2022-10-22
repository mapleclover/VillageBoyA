using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//�ڿ��� ����Ʈ�Ŵ���
public class QuestManager : MonoBehaviour
{
    public int questId;
    public int questActionIndex; // ����Ʈnpc��ȭ���� ����
    public GameObject[] questObject;

    Dictionary<int, QuestData> questList; // questId, questData(questName, npcID)

    private void Awake()
    {
        questList = new Dictionary<int, QuestData>(); // �ʱ�ȭ
        GenerateData();
    }

    private void GenerateData()
    {
        questList.Add(10, new QuestData("ù ���� �湮", new int[] { 1000, 2000 }));
        questList.Add(20, new QuestData("��� ������", new int[] { 100, 2000 }));
        questList.Add(30, new QuestData("����Ʈ �� Ŭ����!", new int[] { 0 }));
    }

    public int GetQuestTalkIndex(int id)
    {
        return questId + questActionIndex;
    }
    // ��ȭ�����̳����� �ε��������Լ�.
    public string CheckQuest(int id)
    {
        //Next Talk Target(NPC)
        if(id == questList[questId].npcId[questActionIndex]) // ������������ ����Ʈid�� npc���̵�üũ.
        questActionIndex++;  //�ε������������������� A����ġ�� B�������ϴµ� A�Ȱ�ġ�� B�����Ǳ⶧��.

        //Control Quest Object
        ControlObject();

        //Talk Complete & Next Quest
        if (questActionIndex == questList[questId].npcId.Length)//npcId�ǹ迭? -> ����Ʈ����� �ٷ���� ��ȭ
            NextQuest();

        //Quest Name Return
        return questList[questId].questName;
    }
    // ����Ʈ �����ʾ����� ��ó��.����.
    public string CheckQuest() //�����ε�. (�Ű�����������ȣ�� , ��÷�̶� �Ű����������Ƿ� �Ű���������)
    {
        //Quest Name Return
        return questList[questId].questName;
    }
    private void NextQuest()
    {
        questId += 10; // ����questId�� ����.
        questActionIndex = 0; // �׼��ε����� �ʱ�ȭ.
    }

    private void ControlObject()
    {
        switch (questId)
        {
            case 10:
                if (questActionIndex == 2) // 10�� ����Ʈ�� npc��ȭ����. 2��� 2����ȭ�ϹǷ� "2"
                    questObject[0].SetActive(true); // ���.
                break;
            case 20:
                if (questActionIndex == 1)
                    Destroy(questObject[0]); // ���
                break;
        }
    }
}
