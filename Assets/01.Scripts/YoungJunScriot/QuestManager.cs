using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//박영준 퀘스트매니저
public class QuestManager : MonoBehaviour
{
    public int questId;
    public int questActionIndex; // 퀘스트npc대화순서 변수
    private Animator myAnim;
    private ObjData theObjectData;
    public GameObject[] questObject;

    public bool _questComplete;
    public bool questComplete
    {
        get => _questComplete;
        set
        {
            _questComplete = value;
        }
    }


    Dictionary<int, QuestData> questList; // questId, questData(questName, npcID)

    // 필요한 컴퍼넌트
    public GameObject Klee_1000;
    public GameObject Hodu_2000;

    [SerializeField]
    private TMPro.TMP_Text questPopupText;
    [SerializeField]
    private GameObject theComplete;
    [SerializeField]
    private InventoryController theInven;


    
    private void Awake()
    {
        questList = new Dictionary<int, QuestData>(); // 초기화
        GenerateData();
        myAnim = theComplete.GetComponent<Animator>();
        questComplete = DataController.instance.gameData.questClear;
    }

    private void GenerateData()
    {
        questList.Add(10, new QuestData("이동모션 듀토리얼", new int[] { 10000, 1000 }));
        questList.Add(20, new QuestData("사과 따오기", new int[] { 100, 100, 1000 }));
        questList.Add(30, new QuestData("마을 첫 방문 및 여우잡기", new int[] { 1000, 2000, 2000 }));
    }

    public int GetQuestTalkIndex(int id)
    {
        return questId + questActionIndex;
    }
    // 대화가끝이났을때 인덱스증가함수.
    public string CheckQuest(int id, GameObject scanObject)
    {
        //Next Talk Target(NPC)
        if (questComplete)
        {
            if (id == questList[questId].npcId[questActionIndex]) // 현재진행중인 퀘스트id의 npc아이디체크.
                questActionIndex++;  //인덱스순서를주지않으면 A를거치고 B를가야하는데 A안거치고 B가도되기때문.
        }
        //Control Quest Object
        ControlObject(scanObject);
        ControlPopup(scanObject);

        //Talk Complete & Next Quest
        if (questActionIndex == questList[questId].npcId.Length)//npcId의배열? -> 퀘스트진행시 다뤄야할 대화
            NextQuest();

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
    }

   
    private void ControlObject(GameObject scanObject)
    {
        //questObject[0] = 사과
        //questObject[1] = ! 아이콘
        //questObject[2] = ? 아이콘
        //questObject[3] = 사과2
        //questObject[4] = 듀토리얼 goal
        switch (questId)
        {
            case 10:
                if(questActionIndex == 1)
                {
                    questObject[1].SetActive(true); // ! 아이콘                  
                    //questObject[2].SetActive(true); // ? 아이콘
                    questObject[1].transform.position = Klee_1000.transform.position + Vector3.up * 2.0f;
                    questObject[2].transform.rotation = Quaternion.Euler(90.0f, 90.0f, 0.0f);
                    questObject[4].transform.position = new Vector3(291.0f, 1.1f, 222.0f); // goal지접이동
                }
                if (questActionIndex == 2) // 10번 퀘스트의 npc대화순서. 2명과 2번대화하므로 "2"
                {                   
                    questObject[1].SetActive(false);
                    questObject[0].SetActive(true); // 사과 1 
                    questObject[3].SetActive(true); // 사과 2
                }
                break;
            case 20:
                if (questActionIndex == 1)
                {                    
                    if (scanObject.GetComponent<ObjData>().id == 100)
                    {
                        Destroy(scanObject); // 사과
                    }
                }
                if(questActionIndex == 2)
                {                  
                    questObject[2].SetActive(true); // ? 아이콘
                    questObject[2].transform.position = Klee_1000.transform.position + Vector3.up * 2.0f;
                    if (scanObject.GetComponent<ObjData>().id == 100)
                    {
                        Destroy(scanObject); // 사과
                    }
                }
                if(questActionIndex == 3)
                {                   
                    questObject[2].SetActive(false); // ? 사라지게.                    
                    questObject[4].SetActive(true);                 
                    Goal.goalCounting++; 
                }
                break;
            case 30:                          
                if(questActionIndex == 1)
                {
                    questObject[1].transform.position = Hodu_2000.transform.position + Vector3.up * 2.3f;
                }
                if(questActionIndex == 1 && !questComplete)
                    questObject[1].SetActive(false);
                if (questActionIndex == 2 && questComplete)
                {
                    questObject[2].SetActive(true);
                    questObject[2].transform.position = Hodu_2000.transform.position + Vector3.up * 2.3f;
                }
                break;
        }
    }

    private void ControlPopup(GameObject scanObject)
    {
        // myAnim => 퀘스트완료깜빡이.
        switch (questId)
        {
            case 10:
                if(questActionIndex == 1)
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
                if (questActionIndex == 1)
                {
                    questComplete = false;
                    questPopupText.text = "호두와 대화하기";
                    myAnim.SetBool("isComplete", false);
                }
                if(questActionIndex == 1 && !questComplete)
                    questPopupText.text = "여우 잡아오기";
                if (questActionIndex == 2)
                    questPopupText.text = "호두에게 가기";
                if (questActionIndex == 3)
                    questPopupText.text = "호두에게 가기";
                break;
        }
    }
}
