using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//박영준 퀘스트매니저
public class QuestManager : MonoBehaviour
{
    public int questId;
    public int questActionIndex; // 퀘스트npc대화순서 변수
    public GameObject[] questObject;

    Dictionary<int, QuestData> questList; // questId, questData(questName, npcID)

    private void Awake()
    {
        questList = new Dictionary<int, QuestData>(); // 초기화
        GenerateData();
    }

    private void GenerateData()
    {
        questList.Add(10, new QuestData("첫 마을 방문", new int[] { 1000, 2000 }));
        questList.Add(20, new QuestData("사과 따오기", new int[] { 100, 2000 }));
        questList.Add(30, new QuestData("퀘스트 올 클리어!", new int[] { 0 }));
    }

    public int GetQuestTalkIndex(int id)
    {
        return questId + questActionIndex;
    }
    // 대화가끝이났을때 인덱스증가함수.
    public string CheckQuest(int id)
    {
        //Next Talk Target(NPC)
        if(id == questList[questId].npcId[questActionIndex]) // 현재진행중인 퀘스트id의 npc아이디체크.
        questActionIndex++;  //인덱스순서를주지않으면 A를거치고 B를가야하는데 A안거치고 B가도되기때문.

        //Control Quest Object
        ControlObject();

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

    private void ControlObject()
    {
        switch (questId)
        {
            case 10:
                if (questActionIndex == 2) // 10번 퀘스트의 npc대화순서. 2명과 2번대화하므로 "2"
                    questObject[0].SetActive(true); // 사과.
                break;
            case 20:
                if (questActionIndex == 1)
                    Destroy(questObject[0]); // 사과
                break;
        }
    }
}
