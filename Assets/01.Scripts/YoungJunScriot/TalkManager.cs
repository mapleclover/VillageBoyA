using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//박영준 토크매니저
public class TalkManager : MonoBehaviour
{
    Dictionary<int, string[]> talkData;
    Dictionary<int, Sprite> portraitData;

    public Sprite[] portraitArr;


    // Start is called before the first frame update
    void Awake()
    {
        talkData = new Dictionary<int, string[]>();
        portraitData = new Dictionary<int, Sprite>();
        GenerateData();
    }

    private void GenerateData()
    {
        //Talk Data
        //NPC A:1000
        //Apple:100
        talkData.Add(1000, new string[] { "안녕! 오빠 나는 클레야:0", "이곳에 처음 왔구나?:1" }); //대화뒤의숫자는 portratiIndex ":" 은구분자.
        talkData.Add(2000, new string[] { "안녕?ㅋ:0" });
        talkData.Add(100, new string[] {"새빨갛게 잘 익은 사과다."  });

        //Quest Talk Data
        //첫마을 방문 마을사람과대화하기.
        talkData.Add(10 + 10000, new string[] {"좋아요 !\n 대화는 E 버튼을 눌러 넘어갈 수 있어요.",
                                               "이번에는 앞에 있는 클레에게 E 버튼을 눌러 말을 걸어볼까요?"
                                              });

        talkData.Add(11 + 1000, new string[] {"안녕! 오빠:0",
                                              "Npc나 아이템과 상호작용 하고 싶으면 바로 앞에서 E 버튼누르면돼 !:0",
                                              "클레가 배가고픈데 혹시 건너편 사과 두개만 구해줄 수 있을까?:1"});

        talkData.Add(20 + 100, new string[] { "근처에서 사과를 찾았다." });
        talkData.Add(21 + 100, new string[] { "근처에서 사과를 찾았다." });
        talkData.Add(22 + 1000, new string[] { "오빠! 고마워! 냠냠:0" });


        //Portrait Data
        portraitData.Add(1000 + 0, portraitArr[0]); // id + portraitIndex
        portraitData.Add(1000 + 1, portraitArr[1]);

        portraitData.Add(2000 + 0, portraitArr[2]);
        
    }

    public string GetTalk(int id, int talkIndex)
    {
        //예외처리. 퀘스트와상관없는 대상과 대화했을때.
        if(!talkData.ContainsKey(id))// ContainsKey => Dictionary 안에 해당 Key값이 존재하는지 체크해주는함수. 있으면 true반환
        {
            if (!talkData.ContainsKey(id - id % 10)) // 퀘스트기본대사마저 없을때(ex .아이템 및 의미없는 사물)
                //if (talkIndex == talkData[id - id % 100].Length)
                //    return null;
                //else // 가장 기본대사를 가지고온다.
                //    return talkData[id - id % 100][talkIndex]; 
            {
                //Get First Talk
                return GetTalk(id - id % 100, talkIndex); // 위의 주석내용 간소화 (재귀함수)
            }
            else
            {
                ////해당퀘스트 진행순서 중 대사가 없을 때.
                //if (talkIndex == talkData[id - id % 10].Length)
                //    return null;
                //else//해당 퀘스트 맨처음 대사를 가져옴.
                //    return talkData[id - id % 10][talkIndex];

                //Get First Quest Talk
                return GetTalk(id - id % 10, talkIndex); //위 주석내용 간소화.(재귀함수)
            }
        }

        //예외처리아닐때 talkData 리턴
        if (talkIndex == talkData[id].Length) 
            return null; // 해당엔피시와의 대화(인덱스)가 끝났을때 리턴
        else
            return talkData[id][talkIndex];
    }

    public Sprite GetPortrait(int id, int portraitinIndex)
    {
        return portraitData[id + portraitinIndex]; // id에다가 초상화인덱스더함. ex 1001, 2001 
    }
}
