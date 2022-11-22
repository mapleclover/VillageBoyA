//작성자 : 박영준
//설명 : 퀘스트 매니저

using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public int questId;
    public bool questComplete;
    private bool tempCheck; // 퀘스트컴플리트 저장용 임시변수
    public int questActionIndex; // 퀘스트npc대화순서 변수
    public GameObject myInven;
    private Animator myAnim;
    public GameObject[] questObject;

    MinimapIcon my_Icon = null;


    Dictionary<int, QuestData> questList; // questId, questData(questName, npcID)

    // 필요한 컴퍼넌트
    public GameObject Klee_1000;
    public GameObject Hodu_2000;
    public GameObject Zhongli_3000;

    [SerializeField] private TMP_Text questPopupText;
    [SerializeField] private GameObject theComplete;
    [SerializeField] private InventoryController theInven;


    private void Awake()
    {
        questList = new Dictionary<int, QuestData>(); // 초기화

        GenerateData();
        myAnim = theComplete.GetComponent<Animator>();
    }


    private void GenerateData()
    {
        questList.Add(10, new QuestData("이동모션 듀토리얼", new int[] { 10000, 1000 }));
        questList.Add(20, new QuestData("사과 따오기", new int[] { 100, 100, 1000 }));
        questList.Add(30, new QuestData("여우 사냥", new int[] { 1000, 2000, 2000, 2000 }));
        questList.Add(40, new QuestData("골렘 사냥", new int[] { 1000, 3000, 3000, 1000, 1000, 3000, 3000, 10000 }));
    }

    public int GetQuestTalkIndex(int id)
    {
        return questId + questActionIndex;
    }

    // 대화가끝이났을때 인덱스증가함수.
    public string CheckQuest(int id, GameObject scanObject)
    {
        tempCheck = questComplete;
        //Next Talk Target(NPC)
        if (tempCheck)
        {
            if (id == questList[questId].npcId[questActionIndex]) // 현재진행중인 퀘스트id의 npc아이디체크.
            {
                questActionIndex++; //인덱스순서를주지않으면 A를거치고 B를가야하는데 A안거치고 B가도되기때문.  
            }
        }
        //Control Quest Object
        ControlPopup(scanObject);
        ControlObject(scanObject);

        //Talk Complete & Next Quest
        if (questActionIndex == questList[questId].npcId.Length) //npcId의배열? -> 퀘스트진행시 다뤄야할 대화
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

    // 퀘스트 받지않았을때 맨처음.상태.
    public string CheckQuest() //오버로딩. (매개변수에따른호출 , 맨첨이라 매개변수없으므로 매개변수는뺴줌)
    {
        //Quest Name Return

        return questList[questId].questName;
    }

    private void NextQuest()
    {
        questId += 10; // 다음questId로 증가.
        questActionIndex = 0; // 액션인덱스는 초기화.
        ControlPopup();
        ControlObject();
    }


    public void ControlObject(GameObject scanObject = null)
    {
        //questObject[0] = 사과
        //questObject[1] = ! 아이콘
        //questObject[2] = ? 아이콘
        //questObject[3] = 사과2
        //questObject[4] = 듀토리얼 goal
        //questObject[5] = 미니맵 ! 아이콘
        //questObject[6] = 미니맵 ? 아이콘 
        switch (questId)
        {
            case 10:
                if (questActionIndex == 1)
                {
                    questObject[1].SetActive(true); // ! 아이콘                  

                    //questObject[2].SetActive(true); // ? 아이콘
                    questObject[1].transform.position = Klee_1000.transform.position + Vector3.up * 2.0f;
                    questObject[2].transform.rotation = Quaternion.Euler(90.0f, 90.0f, 0.0f);
                    questObject[4].transform.position = new Vector3(291.0f, 1.1f, 222.0f); // goal지접이동
                }
                else if (questActionIndex == 2) // 10번 퀘스트의 npc대화순서. 2명과 2번대화하므로 "2"
                {
                    questObject[1].SetActive(false);
                    questObject[0].SetActive(true); // 사과 1 
                    questObject[3].SetActive(true); // 사과 2
                }

                break;
            case 20:
                if (questActionIndex == 0) // 10번 퀘스트의 npc대화순서. 2명과 2번대화하므로 "2"
                {
                    questObject[1].SetActive(false);
                    questObject[0].SetActive(true); // 사과 1 
                    questObject[3].SetActive(true); // 사과 2
                }
                else if (questActionIndex == 1)
                {
                    if (scanObject.GetComponent<ObjData>().id == 100)
                    {
                        Destroy(scanObject); // 사과
                    }
                }
                else if (questActionIndex == 2)
                {
                    questObject[2].SetActive(true); // ? 아이콘

                    questObject[2].transform.position = Klee_1000.transform.position + Vector3.up * 2.0f;
                    if (scanObject.GetComponent<ObjData>().id == 100)
                    {
                        Destroy(scanObject); // 사과
                    }
                }
                else if (questActionIndex == 3)
                {
                    questObject[2].SetActive(false); // ? 사라지게.                    
                    questObject[4].SetActive(true);
                    Goal.goalCounting++;
                }

                break;
            case 30:
                if (questActionIndex == 0)
                {
                    questObject[1].SetActive(true);
                    questObject[1].transform.position = Klee_1000.transform.position + Vector3.up * 2.0f;
                    // 미니맵 ! 아이콘
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

                if (questActionIndex == 1 && scanObject == Hodu_2000)
                {
                    questObject[1].SetActive(false);
                    Destroy(questObject[5]);
                }

                if (questActionIndex == 2 && questComplete)
                {
                    questObject[2].SetActive(true); // 호두 위에 ? 아이콘                   
                    questObject[2].transform.position = Hodu_2000.transform.position + Vector3.up * 2.3f;
                    questObject[2].transform.rotation = Quaternion.Euler(90, 0, 0);

                    questObject[6] = Instantiate(Resources.Load("Prefabs/Icons/QuestCompletIcon"), SceneData.Inst.Minimap) as GameObject;
                    questObject[6].GetComponent<MinimapIcon>().Initialize(Hodu_2000.transform, Color.yellow);
                }

                if (questActionIndex == 2 && scanObject == Hodu_2000)
                {
                    questObject[2].SetActive(false); // 여우꼬리 사라짐
                    RemovingItem("여우꼬리");
                    Destroy(questObject[6]);
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
                if(questActionIndex == 0)
                {
                    questObject[2].SetActive(false); // 대왕여우 머리 사라짐
                    RemovingItem("여우머리");
                    Destroy(questObject[6]);

                    questObject[1].SetActive(true);

                    questObject[5] = Instantiate(Resources.Load("Prefabs/Icons/QuestIcon"), SceneData.Inst.Minimap) as GameObject;
                    questObject[5].GetComponent<MinimapIcon>().Initialize(Klee_1000.transform, Color.yellow);

                    questObject[1].transform.position = Klee_1000.transform.position + Vector3.up * 2.0f;
                }
                if(questActionIndex == 1 && tempCheck)
                {
                    Destroy(questObject[5]);

                    questObject[1].transform.position = Zhongli_3000.transform.position + Vector3.up * 2.7f;

                    questObject[5] = Instantiate(Resources.Load("Prefabs/Icons/QuestIcon"), SceneData.Inst.Minimap) as GameObject;
                    questObject[5].GetComponent<MinimapIcon>().Initialize(Zhongli_3000.transform, Color.yellow);
                }
                if(questActionIndex == 1 && scanObject == Zhongli_3000)
                {
                    questObject[1].SetActive(false);

                    Destroy(questObject[5]);
                }
                if (questActionIndex == 2)
                {
                    questObject[2].SetActive(true);                  // 니드런 뿔 사라짐
                    questObject[2].transform.position = Zhongli_3000.transform.position + Vector3.up * 2.7f;
                    questObject[2].transform.rotation = Quaternion.Euler(90, 90, 0);

                    questObject[6] = Instantiate(Resources.Load("Prefabs/Icons/QuestCompletIcon"), SceneData.Inst.Minimap) as GameObject;
                    questObject[6].GetComponent<MinimapIcon>().Initialize(Zhongli_3000.transform, Color.yellow);
                }
                if(questActionIndex == 3 && tempCheck)
                {
                    questObject[2].SetActive(false);

                    Destroy(questObject[6]);
                    RemovingItem("뿔");
                    questObject[1].SetActive(true);                
                    questObject[1].transform.position = Klee_1000.transform.position + Vector3.up * 2.0f;

                    questObject[5] = Instantiate(Resources.Load("Prefabs/Icons/QuestIcon"), SceneData.Inst.Minimap) as GameObject;
                    questObject[5].GetComponent<MinimapIcon>().Initialize(Klee_1000.transform, Color.yellow);
                }
                if(questActionIndex == 3 && scanObject == Klee_1000)
                {
                    questObject[1].SetActive(false);

                    Destroy(questObject[5]);
                }
                if(questActionIndex == 4)
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
                    RemovingItem("우유");
                    Destroy(questObject[6]);

                    questObject[1].SetActive(true);
                    questObject[1].transform.position = Zhongli_3000.transform.position + Vector3.up * 2.7f;

                    questObject[5] = Instantiate(Resources.Load("Prefabs/Icons/QuestIcon"), SceneData.Inst.Minimap) as GameObject;
                    questObject[5].GetComponent<MinimapIcon>().Initialize(Zhongli_3000.transform, Color.yellow);
                }
                if (questActionIndex == 5 && scanObject == Zhongli_3000)
                {
                    questObject[1].SetActive(false);

                    Destroy(questObject[5]);
                }
                if (questActionIndex == 6)
                {
                    questObject[2].SetActive(true);    // 톱니바퀴 사라짐                   
                    questObject[2].transform.position = Zhongli_3000.transform.position + Vector3.up * 2.7f;
                    questObject[2].transform.rotation = Quaternion.Euler(90, 90, 0);

                    questObject[6] = Instantiate(Resources.Load("Prefabs/Icons/QuestCompletIcon"), SceneData.Inst.Minimap) as GameObject;
                    questObject[6].GetComponent<MinimapIcon>().Initialize(Zhongli_3000.transform, Color.yellow);
                }
                if (questActionIndex == 7)
                {
                    RemovingItem("기어부품");
                    questObject[2].SetActive(false);

                    Destroy(questObject[6]);
                }
                break;

        }
    }
    public void RemovingItem(string name)
    {
        for(int i = 0; i < 14; i++)
        {
            if (myInven.transform.GetChild(i).transform.GetChild(0) != null)
            {
                if (myInven.transform.GetChild(i).transform.GetChild(0).GetComponent<Pickup>().item.itemName.Equals(name))
                {
                    string thisItem = myInven.transform.GetChild(i).transform.GetChild(0).GetComponent<Pickup>().item.itemName;
                    DataController.instance.gameData.savedInventory.Remove(thisItem);
                    DataController.instance.gameData.myItemCount.Remove(thisItem);
                    Destroy(myInven.transform.GetChild(i).transform.GetChild(0).gameObject);
                    break;
                }
            }
            else continue;
        }
    }

    public void ControlPopup(GameObject scanObject = null)
    {
        // myAnim => 퀘스트완료깜빡이.
        switch (questId)
        {
            case 10:
                if (questActionIndex == 1)
                    questPopupText.text = "클레와 대화하기";
                if (questActionIndex == 2)
                {
                    questPopupText.text = "사과 구해오기 0/2";
                    myAnim.SetBool("isComplete", false);
                }

                break;
            case 20:
                if (questActionIndex == 1)
                {
                    questPopupText.text = "사과 구해오기 1/2";
                    theInven.GetItem(scanObject);
                }

                if (questActionIndex == 2)
                {
                    theInven.GetItem(scanObject);
                    questPopupText.text = "클레에게 \n사과 갖다주기";
                    myAnim.SetBool("isComplete", true);
                }

                if (questActionIndex == 3)
                {
                    questPopupText.text = "다음 목표지점으로\n이동하기";
                }

                break;
            case 30:
                if (questActionIndex == 0)
                {
                    questPopupText.text = "클레와 대화하기";
                }

                if (questActionIndex == 1 && tempCheck)
                {
                    questPopupText.text = "호두와 대화하기";

                    questComplete = false;
                }

                if (questActionIndex == 1 && scanObject == Hodu_2000)
                {
                    myAnim.SetBool("isComplete", false);
                    questPopupText.text = "여우 잡아오기";
                }

                if (questActionIndex == 2 && questComplete)
                {
                    myAnim.SetBool("isComplete", true);
                    questPopupText.text = "호두와 대화하기";

                    questComplete = false;
                }

                if (questActionIndex == 2 && scanObject == Hodu_2000)
                {
                    myAnim.SetBool("isComplete", false);
                    questPopupText.text = "대왕여우 잡아오기";
                }

                if (questActionIndex == 3)
                {
                    myAnim.SetBool("isComplete", true);
                    questPopupText.text = "호두와 대화하기";
                }

                break;
            case 40:
                if(questActionIndex == 0)
                {
                    myAnim.SetBool("isComplete", true);
                    questPopupText.text = "클레와 대화하기";
                }
                if(questActionIndex == 1 && tempCheck)
                {
                    questPopupText.text = "종려와 대화하기";

                    questComplete = false;
                }
                if (questActionIndex == 1 && scanObject == Zhongli_3000)
                {
                    myAnim.SetBool("isComplete", false);
                    questPopupText.text = "리드런 사냥하기";
                }
                if (questActionIndex == 2)
                {
                    myAnim.SetBool("isComplete", true);
                    questPopupText.text = "종려와 대화하기";
                }
                if(questActionIndex == 3 && tempCheck)
                {
                    questPopupText.text = "조사시간동안\n클레와 대화하기";

                    questComplete = false;
                }
                if (questActionIndex == 3 && scanObject == Klee_1000)
                {
                    myAnim.SetBool("isComplete", false);
                    questPopupText.text = "밀크카우 잡고\n클레에게\n우유전달하기";
                }
                if(questActionIndex == 4)
                {
                    myAnim.SetBool("isComplete", true);
                    questPopupText.text = "클레에게\n우유전달하기";
                }
                if (questActionIndex == 5 && tempCheck)
                {
                    questPopupText.text = "종려한테 대화걸기";

                    questComplete = false;
                }
                if (questActionIndex == 5 && scanObject == Zhongli_3000)
                {
                    myAnim.SetBool("isComplete", false);
                    questPopupText.text = "거대골렘을\n토벌하자";
                }
                if (questActionIndex == 6)
                {
                    myAnim.SetBool("isComplete", true);
                    questPopupText.text = "거대골렘을\n토벌했다 !\n종려에게로 가자";
                }
                if (questActionIndex == 7)
                {
                    myAnim.SetBool("isComplete", true);
                    questPopupText.text = "GameClear !";
                }
                break;

        }
    }
}