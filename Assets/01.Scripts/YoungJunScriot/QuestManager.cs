using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//�ڿ��� ����Ʈ�Ŵ���
public class QuestManager : MonoBehaviour
{
    public int questId;
    public int questActionIndex; // ����Ʈnpc��ȭ���� ����
    private Animator myAnim;
    private ObjData theObjectData;
    public GameObject[] questObject;

   

    Dictionary<int, QuestData> questList; // questId, questData(questName, npcID)

    // �ʿ��� ���۳�Ʈ
    public GameObject testNPC_1000;
    public GameObject Kong_2000;
    [SerializeField]
    private TMPro.TMP_Text questPopupText;
    [SerializeField]
    private GameObject theComplete;
    [SerializeField]
    private InventoryController theInven;

    private void Awake()
    {
        questList = new Dictionary<int, QuestData>(); // �ʱ�ȭ
        GenerateData();
        myAnim = theComplete.GetComponent<Animator>();

    }

    private void GenerateData()
    {
        questList.Add(10, new QuestData("ù ���� �湮", new int[] { 1000, 2000 }));
        questList.Add(20, new QuestData("��� ������", new int[] { 100, 100, 2000 }));
        questList.Add(30, new QuestData("����Ʈ �� Ŭ����!", new int[] { 0 }));
    }

    public int GetQuestTalkIndex(int id)
    {
        return questId + questActionIndex;
    }
    // ��ȭ�����̳����� �ε��������Լ�.
    public string CheckQuest(int id, GameObject scanObject)
    {
        //Next Talk Target(NPC)
        if(id == questList[questId].npcId[questActionIndex]) // ������������ ����Ʈid�� npc���̵�üũ.
        questActionIndex++;  //�ε������������������� A����ġ�� B�������ϴµ� A�Ȱ�ġ�� B�����Ǳ⶧��.

        //Control Quest Object
        ControlObject(scanObject);
        ControlPopup(scanObject);

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

    private void ControlObject(GameObject scanObject)
    {
        //questObject[0] = ���
        //questObject[1] = ! ������
        //questObject[2] = ? �����ܤ�
        switch (questId)
        {
            case 10:
                if(questActionIndex == 1)
                {
                    questObject[1].SetActive(false); // ! ������
                    questObject[2].SetActive(true); // ? ������
                    questObject[2].transform.position = Kong_2000.transform.position + Vector3.up * 2.3f;
                    questObject[2].transform.rotation = Quaternion.Euler(90.0f, 90.0f, 0.0f);
                }
                if (questActionIndex == 2) // 10�� ����Ʈ�� npc��ȭ����. 2��� 2����ȭ�ϹǷ� "2"
                {
                    questObject[2].SetActive(false);
                }
                break;
            case 20:
                if (questActionIndex == 1)
                {
                    if (scanObject.GetComponent<ObjData>().id == 100)
                    {
                        
                        Destroy(scanObject); // ���
                    }
                }
                if(questActionIndex == 2)
                {
                    questObject[2].SetActive(true);
                    questObject[2].transform.position = Kong_2000.transform.position + Vector3.up * 2.3f;
                    if (scanObject.GetComponent<ObjData>().id == 100)
                    {
                        Destroy(scanObject); // ���
                    }
                }
                if(questActionIndex == 3)
                {
                    questObject[2].SetActive(false);
                }
                break;
        }
    }

    private void ControlPopup(GameObject scanObject)
    {
        // myAnim => ����Ʈ�Ϸ������.
        switch (questId)
        {
            case 10:
                if(questActionIndex == 1)
                    questPopupText.text = "Kong �� ��ȭ�ϱ�";
                if (questActionIndex == 2)
                {
                    questPopupText.text = "��� ���ؿ��� 0/2";
                    myAnim.SetBool("isComplete", false); 
                }
                break;
            case 20:
                if (questActionIndex == 1)
                {
                    questPopupText.text = "��� ���ؿ��� 1/2";
                    theInven.GetItem(scanObject);
                }
                if (questActionIndex == 2)
                {
                    questPopupText.text = "Kong���� \n��� �����ֱ�";
                    myAnim.SetBool("isComplete", true);
                }
                break;
            case 30:
                if (questActionIndex == 1)
                    questPopupText.text = "Empty";
                break;
        }
    }
}
