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

   

    Dictionary<int, QuestData> questList; // questId, questData(questName, npcID)

    // 필요한 컴퍼넌트
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
        questList = new Dictionary<int, QuestData>(); // 초기화
        GenerateData();
        myAnim = theComplete.GetComponent<Animator>();

    }

    private void GenerateData()
    {
        questList.Add(10, new QuestData("첫 마을 방문", new int[] { 1000, 2000 }));
        questList.Add(20, new QuestData("사과 따오기", new int[] { 100, 100, 2000 }));
        questList.Add(30, new QuestData("퀘스트 올 클리어!", new int[] { 0 }));
    }

    public int GetQuestTalkIndex(int id)
    {
        return questId + questActionIndex;
    }
    // 대화가끝이났을때 인덱스증가함수.
    public string CheckQuest(int id, GameObject scanObject)
    {
        //Next Talk Target(NPC)
        if(id == questList[questId].npcId[questActionIndex]) // 현재진행중인 퀘스트id의 npc아이디체크.
        questActionIndex++;  //인덱스순서를주지않으면 A를거치고 B를가야하는데 A안거치고 B가도되기때문.

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
        //questObject[2] = ? 아이콘ㅋ
        switch (questId)
        {
            case 10:
                if(questActionIndex == 1)
                {
                    questObject[1].SetActive(false); // ! 아이콘
                    questObject[2].SetActive(true); // ? 아이콘
                    questObject[2].transform.position = Kong_2000.transform.position + Vector3.up * 2.3f;
                    questObject[2].transform.rotation = Quaternion.Euler(90.0f, 90.0f, 0.0f);
                }
                if (questActionIndex == 2) // 10번 퀘스트의 npc대화순서. 2명과 2번대화하므로 "2"
                {
                    questObject[2].SetActive(false);
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
                    questObject[2].SetActive(true);
                    questObject[2].transform.position = Kong_2000.transform.position + Vector3.up * 2.3f;
                    if (scanObject.GetComponent<ObjData>().id == 100)
                    {
                        Destroy(scanObject); // 사과
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
        // myAnim => 퀘스트완료깜빡이.
        switch (questId)
        {
            case 10:
                if(questActionIndex == 1)
                    questPopupText.text = "Kong 과 대화하기";
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
                    questPopupText.text = "Kong에게 \n사과 갖다주기";
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
