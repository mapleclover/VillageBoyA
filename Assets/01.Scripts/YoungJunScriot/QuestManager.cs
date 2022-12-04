//�ۼ��� : �ڿ���
//���� : ����Ʈ �Ŵ���

using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public int questId;
    public bool questComplete = true;
    public bool tempCheck = true; // ����Ʈ���ø�Ʈ ����� �ӽú���
    public int questActionIndex; // ����Ʈnpc��ȭ���� ����
    public GameObject[] mySlots;
    private Animator myAnim;
    public GameObject[] questObject;
    public GameObject credit;


    Dictionary<int, QuestData> questList; // questId, questData(questName, npcID)

    // �ʿ��� ���۳�Ʈ
    public GameObject Klee_1000;
    public GameObject Hodu_2000;
    public GameObject Zhongli_3000;
    public GameObject Hangchu_4000;

    [SerializeField] private TMP_Text questPopupText;
    [SerializeField] private GameObject theComplete;
    [SerializeField] private InventoryController theInven;
    [SerializeField] private TMP_Text theQuestPopupText;


    private void Awake()
    {
        questList = new Dictionary<int, QuestData>(); // �ʱ�ȭ

        GenerateData();
        myAnim = theComplete.GetComponent<Animator>();
    }


    private void GenerateData()
    {
        questList.Add(10, new QuestData("�̵���� ���丮��", new int[] { 10000, 1000 }));
        questList.Add(20, new QuestData("��� ������", new int[] { 100, 100, 1000, 10000 }));
        questList.Add(30, new QuestData("���� ���", new int[] { 1000, 2000, 2000, 2000 }));
        questList.Add(40, new QuestData("�� ���", new int[] { 1000, 3000, 3000, 1000, 1000, 3000, 3000, 10000 }));
    }

    public int GetQuestTalkIndex(int id)
    {
        return questId + questActionIndex;
    }

    // ��ȭ�����̳����� �ε��������Լ�.
    public string CheckQuest(int id, GameObject scanObject)
    {
        tempCheck = questComplete;
        //Next Talk Target(NPC)
        if (tempCheck)
        {
            if (id == questList[questId].npcId[questActionIndex]) // ������������ ����Ʈid�� npc���̵�üũ.
            {
                questActionIndex++; //�ε������������������� A����ġ�� B�������ϴµ� A�Ȱ�ġ�� B�����Ǳ⶧��.  
            }
        }
        //Control Quest Object
        ControlPopup(scanObject);
        ControlObject(scanObject);

        //Talk Complete & Next Quest
        if (questActionIndex == questList[questId].npcId.Length) //npcId�ǹ迭? -> ����Ʈ����� �ٷ���� ��ȭ
        {
            if (questId == 30)
            {
                DataController.instance.gameData.gold += 100;
            }

            NextQuest();
        }

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
        ControlPopup();
        ControlObject();
    }


    public void ControlObject(GameObject scanObject = null)
    {
        //questObject[0] = ���
        //questObject[1] = ! ������
        //questObject[2] = ? ������
        //questObject[3] = ���2
        //questObject[4] = ���丮�� goal
        //questObject[5] = �̴ϸ� ! ������
        //questObject[6] = �̴ϸ� ? ������ 
        switch (questId)
        {
            case 10:
                if (questActionIndex == 1)
                {
                    questObject[1].SetActive(true); // ! ������                  
                    questComplete = true;
                    questObject[1].transform.position = Klee_1000.transform.position + Vector3.up * 2.0f;
                    questObject[2].transform.rotation = Quaternion.Euler(90.0f, 90.0f, 0.0f);
                    questObject[4].transform.position = new Vector3(291.0f, 1.1f, 222.0f); // goal�����̵�
                }

                break;
            case 20:
                if (questActionIndex == 0) // 10�� ����Ʈ�� npc��ȭ����. 2��� 2����ȭ�ϹǷ� "2"
                {
                    questObject[1].SetActive(false);
                    questObject[0].SetActive(true); // ��� 1 
                    questObject[3].SetActive(true); // ��� 2
                }
                else if (questActionIndex == 1)
                {
                    if (scanObject.GetComponent<ObjData>().id == 100)
                    {
                        Destroy(scanObject); // ���
                    }
                }
                else if (questActionIndex == 2)
                {
                    questObject[2].SetActive(true); // ? ������

                    questObject[2].transform.position = Klee_1000.transform.position + Vector3.up * 2.0f;
                    if (scanObject.GetComponent<ObjData>().id == 100)
                    {
                        Destroy(scanObject); // ���
                    }
                }
                else if (questActionIndex == 3)
                {
                    questObject[2].SetActive(false); // ? �������.                    
                    questObject[4].SetActive(true);
                    Goal.goalCounting++;
                }

                break;
            case 30:
                if (questActionIndex == 0)
                {
                    questObject[1].SetActive(true);
                    questObject[1].transform.position = Klee_1000.transform.position + Vector3.up * 2.0f;
                    // �̴ϸ� ! ������
                    questObject[5] = Instantiate(Resources.Load("Prefabs/Icons/QuestIcon"), SceneData.Inst.Minimap) as GameObject;
                    questObject[5].GetComponent<MinimapIcon>().Initialize(Klee_1000.transform, Color.yellow);
                }

                if (questActionIndex == 1 && tempCheck)
                {
                    Destroy(questObject[5]);

                    questObject[1].transform.position = Hodu_2000.transform.position + Vector3.up * 2.3f;

                    questObject[5] = Instantiate(Resources.Load("Prefabs/Icons/QuestIcon"), SceneData.Inst.Minimap) as GameObject;
                    questObject[5].GetComponent<MinimapIcon>().Initialize(Hodu_2000.transform, Color.yellow);
                }
                else if (questActionIndex == 1 && scanObject == null)
                {
                    questObject[1].SetActive(false);
                }

                if (questActionIndex == 1 && scanObject == Hodu_2000)
                {
                    questObject[1].SetActive(false);
                    Destroy(questObject[5]);
                }
                

                if (questActionIndex == 2 && tempCheck)
                {
                    questObject[2].SetActive(true); // ȣ�� ���� ? ������                   
                    questObject[2].transform.position = Hodu_2000.transform.position + Vector3.up * 2.3f;
                    questObject[2].transform.rotation = Quaternion.Euler(90, 0, 0);

                    questObject[6] = Instantiate(Resources.Load("Prefabs/Icons/QuestCompletIcon"), SceneData.Inst.Minimap) as GameObject;
                    questObject[6].GetComponent<MinimapIcon>().Initialize(Hodu_2000.transform, Color.yellow);
                }
                else if (questActionIndex == 2 && scanObject == null)
                {
                    questObject[2].SetActive(false); // ���첿�� �����
                }

                if (questActionIndex == 2 && scanObject == Hodu_2000)
                {
                    questObject[2].SetActive(false);
                    Destroy(questObject[6]);
                    RemovingItem("���첿��");
                }

                if (questActionIndex == 3)
                {
                    questObject[2].SetActive(true);
                    questObject[2].transform.position = Hodu_2000.transform.position + Vector3.up * 2.3f;
                    questObject[2].transform.rotation = Quaternion.Euler(90, 0, 0);

                    questObject[6] = Instantiate(Resources.Load("Prefabs/Icons/QuestCompletIcon"), SceneData.Inst.Minimap) as GameObject;
                    questObject[6].GetComponent<MinimapIcon>().Initialize(Hodu_2000.transform, Color.yellow);
                }

                break;
            case 40:
                if (questActionIndex == 0)
                {
                    questObject[2].SetActive(false); // ��տ��� �Ӹ� �����
                    RemovingItem("����Ӹ�");
                    Destroy(questObject[6]);

                    questObject[1].SetActive(true);

                    questObject[5] = Instantiate(Resources.Load("Prefabs/Icons/QuestIcon"), SceneData.Inst.Minimap) as GameObject;
                    questObject[5].GetComponent<MinimapIcon>().Initialize(Klee_1000.transform, Color.yellow);

                    questObject[1].transform.position = Klee_1000.transform.position + Vector3.up * 2.0f;
                }
                if (questActionIndex == 1 && tempCheck)
                {
                    Destroy(questObject[5]);

                    questObject[1].transform.position = Zhongli_3000.transform.position + Vector3.up * 2.7f;

                    questObject[5] = Instantiate(Resources.Load("Prefabs/Icons/QuestIcon"), SceneData.Inst.Minimap) as GameObject;
                    questObject[5].GetComponent<MinimapIcon>().Initialize(Zhongli_3000.transform, Color.yellow);
                }
                else if (questActionIndex == 1 && scanObject == null)
                {
                    questObject[1].SetActive(false);
                }
                if (questActionIndex == 1 && scanObject == Zhongli_3000)
                {
                    questObject[1].SetActive(false);

                    Destroy(questObject[5]);
                }
                if (questActionIndex == 2)
                {
                    questObject[2].SetActive(true);                  // �ϵ己 �� �����
                    questObject[2].transform.position = Zhongli_3000.transform.position + Vector3.up * 2.7f;
                    questObject[2].transform.rotation = Quaternion.Euler(90, 90, 0);

                    questObject[6] = Instantiate(Resources.Load("Prefabs/Icons/QuestCompletIcon"), SceneData.Inst.Minimap) as GameObject;
                    questObject[6].GetComponent<MinimapIcon>().Initialize(Zhongli_3000.transform, Color.yellow);
                }
                if (questActionIndex == 3 && tempCheck)
                {
                    questObject[2].SetActive(false);

                    Destroy(questObject[6]);
                    RemovingItem("��");
                    questObject[1].SetActive(true);
                    questObject[1].transform.position = Klee_1000.transform.position + Vector3.up * 2.0f;

                    questObject[5] = Instantiate(Resources.Load("Prefabs/Icons/QuestIcon"), SceneData.Inst.Minimap) as GameObject;
                    questObject[5].GetComponent<MinimapIcon>().Initialize(Klee_1000.transform, Color.yellow);
                }
                else if (questActionIndex == 3 && scanObject == null)
                {
                    questObject[1].SetActive(false);
                }
                if (questActionIndex == 3 && scanObject == Klee_1000)
                {
                    questObject[1].SetActive(false);

                    Destroy(questObject[5]);
                }
                if (questActionIndex == 4)
                {
                    questObject[2].SetActive(true);
                    questObject[2].transform.position = Klee_1000.transform.position + Vector3.up * 2.0f;
                    questObject[2].transform.rotation = Quaternion.Euler(90, 180, 0);

                    questObject[6] = Instantiate(Resources.Load("Prefabs/Icons/QuestCompletIcon"), SceneData.Inst.Minimap) as GameObject;
                    questObject[6].GetComponent<MinimapIcon>().Initialize(Klee_1000.transform, Color.yellow);
                }
                if (questActionIndex == 5 && tempCheck)
                {
                    questObject[2].SetActive(false);
                    RemovingItem("����");
                    Destroy(questObject[6]);

                    questObject[1].SetActive(true);
                    questObject[1].transform.position = Zhongli_3000.transform.position + Vector3.up * 2.7f;

                    questObject[5] = Instantiate(Resources.Load("Prefabs/Icons/QuestIcon"), SceneData.Inst.Minimap) as GameObject;
                    questObject[5].GetComponent<MinimapIcon>().Initialize(Zhongli_3000.transform, Color.yellow);
                }
                else if (questActionIndex == 5 && scanObject == null)
                {
                    questObject[1].SetActive(false);
                }
                if (questActionIndex == 5 && scanObject == Zhongli_3000)
                {
                    questObject[1].SetActive(false);

                    Destroy(questObject[5]);
                }
                if (questActionIndex == 6)
                {
                    questObject[2].SetActive(true);    // ��Ϲ��� �����                   
                    questObject[2].transform.position = Zhongli_3000.transform.position + Vector3.up * 2.7f;
                    questObject[2].transform.rotation = Quaternion.Euler(90, 90, 0);

                    questObject[6] = Instantiate(Resources.Load("Prefabs/Icons/QuestCompletIcon"), SceneData.Inst.Minimap) as GameObject;
                    questObject[6].GetComponent<MinimapIcon>().Initialize(Zhongli_3000.transform, Color.yellow);
                }
                if (questActionIndex == 7)
                {
                    RemovingItem("����ǰ");
                    questObject[2].SetActive(false);

                    Destroy(questObject[6]);
                }
                break;

        }
    }
    public void RemovingItem(string name)
    {
        for (int i = 0; i <mySlots.Length; i++)
        {
            if (mySlots[i].transform.childCount>0&&DataController.instance.gameData.itemList.Contains(name))
            {
                if (mySlots[i].transform.GetChild(0).GetComponent<Pickup>().item.itemName.Equals(name))
                {
                    string thisItem =mySlots[i].transform.GetChild(0).GetComponent<Pickup>().item.itemName;
                    int index = DataController.instance.gameData.itemList.IndexOf(thisItem);
                    DataController.instance.gameData.itemList.Remove(thisItem);
                    DataController.instance.gameData.itemCount.RemoveAt(index);
                    DataController.instance.gameData.slotNum.Remove(i);
                    Destroy(mySlots[i].transform.GetChild(0).gameObject);
                    break;
                }
            }
            else continue;
        }
    }

    public void ControlPopup(GameObject scanObject = null)
    {

        // myAnim => ����Ʈ�Ϸ������.
        switch (questId)
        {
            case 10:
                if (questActionIndex.Equals(1))
                {
                    questPopupText.text = "Ŭ���� ��ȭ�ϱ�";
                    theQuestPopupText.text = "Ŭ���� ��ȭ�� �ϼ���.\n��ȭ�� ���� �ٰ����� 'E'��ư ������˴ϴ�.";
                }
                if (questActionIndex.Equals(2))
                {
                    questPopupText.text = "��� ���ؿ��� 0/2";
                    theQuestPopupText.text = "Ŭ���� ����� ���ش޶�� ��û�մϴ�.\n������� �ٰ����� 'E'��ư ��������\n��� ���ؿ��� 0 / 2";
                    myAnim.SetBool("isComplete", false);
                }

                break;
            case 20:
                if (questActionIndex.Equals(1))
                {
                    questPopupText.text = "��� ���ؿ��� 1/2";
                    theQuestPopupText.text = "Ŭ���� ����� ���ش޶�� ��û�մϴ�.\n������� �ٰ����� 'E'��ư ��������\n��� ���ؿ��� 1 / 2";
                    theInven.GetItem(scanObject);
                }

                if (questActionIndex.Equals(2))
                {
                    theInven.GetItem(scanObject);
                    questPopupText.text = "Ŭ������ \n��� �����ֱ�";
                    theQuestPopupText.text = "����� ���� ���߽��ϴ�\nŬ������ ��� ������ �ּ���.";
                    myAnim.SetBool("isComplete", true);
                }

                if (questActionIndex.Equals(3))
                {
                    questPopupText.text = "���� ��ǥ��������\n�̵��ϱ�";
                    theQuestPopupText.text = "���� ��ǥ��������\n�̵��ϼ���.";
                }

                break;
            case 30:
                if (questActionIndex.Equals(0))
                {
                    questPopupText.text = "Ŭ���� ��ȭ�ϱ�";
                    theQuestPopupText.text = "������ ó�� �湮�߽��ϴ�.\nŬ���� ��ȭ�ϼ���.";
                }

                if (questActionIndex.Equals(1) && questComplete)
                {
                    questPopupText.text = "ȣ�ο� ��ȭ�ϱ�";
                    theQuestPopupText.text = "ȣ�ΰ� ����� �־�Դϴ�.\nȣ�ο� ��ȭ�ϼ���.";
                    questComplete = false;
                }
                else if (questActionIndex.Equals(1) && scanObject == null)
                {
                    myAnim.SetBool("isComplete", false);
                    questPopupText.text = "���� ��ƿ���";
                    theQuestPopupText.text = "������ ��ĩ�Ÿ�\n���츦 óġ�ϼ���.\n���Ϳ� �ε����� ��Ʋ�� �����մϴ�.";
                }

                if (questActionIndex.Equals(1) && scanObject == Hodu_2000)
                {
                    myAnim.SetBool("isComplete", false);
                    questPopupText.text = "���� ��ƿ���";
                    theQuestPopupText.text = "������ ��ĩ�Ÿ�\n���츦 óġ�ϼ���.\n���Ϳ� �ε����� ��Ʋ�� �����մϴ�.";
                }

                if (questActionIndex.Equals(2) && questComplete)
                {
                    myAnim.SetBool("isComplete", true);
                    questPopupText.text = "ȣ�ο� ��ȭ�ϱ�";
                    theQuestPopupText.text = "���츦 óġ�߽��ϴ�!\nȣ�θ� ã�ư�����.";
                    questComplete = false;
                }
                else if (questActionIndex.Equals(2) && scanObject == null)
                {
                    myAnim.SetBool("isComplete", false);
                    questPopupText.text = "��տ��� ��ƿ���";
                    theQuestPopupText.text = "������� ���� óġ�ϼ���.\n�ٸ�����麸�� ��� ū ���� ������ �ֽ��ϴ�.";
                }

                if (questActionIndex.Equals(2) && scanObject == Hodu_2000)
                {
                    myAnim.SetBool("isComplete", false);
                    questPopupText.text = "��տ��� ��ƿ���";
                    theQuestPopupText.text = "������� ���� óġ�ϼ���.\n�ٸ�����麸�� ��� ū ���� ������ �ֽ��ϴ�.";
                }

                if (questActionIndex.Equals(3))
                {
                    myAnim.SetBool("isComplete", true);
                    questPopupText.text = "ȣ�ο� ��ȭ�ϱ�";
                    theQuestPopupText.text = "��� ���츦 �����ƽ��ϴ�.\nȣ�θ� ã�ư�����.";
                }

                break;
            case 40:
                if (questActionIndex.Equals(0))
                {
                    myAnim.SetBool("isComplete", true);
                    questPopupText.text = "Ŭ���� ��ȭ�ϱ�";
                    theQuestPopupText.text = "Ŭ���� ��ȭ�ϼ���.";
                }
                if (questActionIndex.Equals(1) && questComplete)
                {
                    questPopupText.text = "������ ��ȭ�ϱ�";
                    theQuestPopupText.text = "������ �� ��Ⱑ �־�Դϴ�.\n������ ã�ư� ������.";

                    questComplete = false;
                }
                else if (questActionIndex.Equals(1) && scanObject == null)
                {
                    myAnim.SetBool("isComplete", false);
                    questPopupText.text = "���己 ����ϱ�";
                    theQuestPopupText.text = "������ ���͵��� ���縦 ����\n���己 ����� ��û�߽��ϴ�. ���己�� ����ġ����.";
                }

                if (questActionIndex.Equals(1) && scanObject == Zhongli_3000)
                {
                    myAnim.SetBool("isComplete", false);
                    questPopupText.text = "���己 ����ϱ�";
                    theQuestPopupText.text = "������ ���͵��� ���縦 ����\n���己 ����� ��û�߽��ϴ�. ���己�� ����ġ����.";
                }
                if (questActionIndex.Equals(2))
                {
                    myAnim.SetBool("isComplete", true);
                    questPopupText.text = "������ ��ȭ�ϱ�";
                    theQuestPopupText.text = "���己�� �����ƽ��ϴ�.\n������ ã�ư�������.";
                }
                if (questActionIndex.Equals(3) && questComplete)
                {
                    questPopupText.text = "����ð�����\nŬ���� ��ȭ�ϱ�";
                    theQuestPopupText.text = "������ ���己�� ���� �̿��� ���縦 �����ϰڴٰ� �մϴ�.\n����ð����� Ŭ���� ã�ư�������.";
                    questComplete = false;
                }
                else if (questActionIndex.Equals(3) && scanObject == null)
                {
                    myAnim.SetBool("isComplete", false);
                    questPopupText.text = "��ũī�� ���\nŬ������\n���������ϱ�";
                    theQuestPopupText.text = "Ŭ���� ������ ���ش޶���մϴ�. ��ũī�츦 ������ ������ ȹ���ϼ���.";
                }

                if (questActionIndex.Equals(3) && scanObject == Klee_1000)
                {
                    myAnim.SetBool("isComplete", false);
                    questPopupText.text = "��ũī�� ���\nŬ������\n���������ϱ�";
                    theQuestPopupText.text = "Ŭ���� ������ ���ش޶���մϴ�. ��ũī�츦 ������ ������ ȹ���ϼ���.";
                }
                if (questActionIndex.Equals(4))
                {
                    myAnim.SetBool("isComplete", true);
                    questPopupText.text = "Ŭ������\n���������ϱ�";
                    theQuestPopupText.text = "������ ���߽��ϴ�.\nŬ���� ã�ư�����.";
                }
                if (questActionIndex.Equals(5) && questComplete)
                {
                    questPopupText.text = "�������� ��ȭ�ɱ�";
                    theQuestPopupText.text = "������ ���縦 ������Ű����ϴ�. ������ ã�ư�������.";

                    questComplete = false;
                }
                else if (questActionIndex.Equals(5) && scanObject == null)
                {
                    myAnim.SetBool("isComplete", false);
                    questPopupText.text = "�Ŵ����\n�������";
                    theQuestPopupText.text = "���������� ���� �Ŵ���� �������ּ���.";
                }

                if (questActionIndex.Equals(5) && scanObject == Zhongli_3000)
                {
                    myAnim.SetBool("isComplete", false);
                    questPopupText.text = "�Ŵ����\n�������";
                    theQuestPopupText.text = "���������� ���� �Ŵ���� �������ּ���.";
                }
                if (questActionIndex.Equals(6))
                {
                    myAnim.SetBool("isComplete", true);
                    questPopupText.text = "�Ŵ����\n����ߴ� !\n�������Է� ����";
                    theQuestPopupText.text = "�Ŵ���� ����ߴ�.\n������ ã�ư�����.";
                }
                if (questActionIndex.Equals(7))
                {
                    myAnim.SetBool("isComplete", true);
                    questPopupText.text = "GameClear !";
                    theQuestPopupText.text = "GameClear !";
                    credit.SetActive(true);
                }
                break;
        }
    }
}