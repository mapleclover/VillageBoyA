using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//박여준 게임매니저 (토크매니저 및 퀘스트매니저처리)
public class GameManager : MonoBehaviour
{
    public TalkManager talkManager;
    public QuestManager questManager;
    public GameObject talkPanel; // 대화창배경
    public Image portraitImg; // 대화시 초상화.
    public TMPro.TMP_Text talkText; // 대화 대사
    public GameObject scanObject; // 대화하고있는 대상 
    public bool isAction; // 대화하고있는지아닌지
    public int talkIndex; // 대화순서



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
        //대화시작
        int questTalkIndex = questManager.GetQuestTalkIndex(id);
        string talkData = talkManager.GetTalk(id + questTalkIndex, talkIndex); // 대화가져옴.
        //대화종료
        if(talkData == null) // 더이상 대화할내용이없을경우 isaction을 false로바꿈.
        {
            isAction = false;
            talkIndex = 0; // 대화끝낫으면 인덱스 초기화.
            Debug.Log(questManager.CheckQuest(id)); // 퀘스트 대화인덱스 추가
            return; // 더어차피대화할것없으니 바로리턴.
        }
        //Npc일경우 토그 지속 및 초상화
        if (isNpc)
        {
            talkText.text = talkData.Split(':')[0]; // Split함수를 쓰면 내가설정한구분자':'기준으로 앞뒤로 인덱스가나뉨.

            portraitImg.sprite = talkManager.GetPortrait(id, int.Parse(talkData.Split(':')[1])); // 매개변수형식이 int형으로가져오는거기떄문에 Parse로 변환
            portraitImg.color = new Color(1, 1, 1, 1); // 맨뒤 1은 alpha값.
        }
        //Item
        else
        {
            talkText.text = talkData;

            portraitImg.color = new Color(1, 1, 1, 0); // 맨뒤 0은 alpha값. 아이템과 대화할때는 alpha값 최소.
        }

        isAction = true;
        talkIndex++; // 다음대화로 넘어감.
    }
}
