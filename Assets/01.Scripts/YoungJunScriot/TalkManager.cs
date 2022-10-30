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
        talkData.Add(1000, new string[] { "안녕?:0", "이곳에 처음 왔구나?:1" }); //대화뒤의숫자는 portratiIndex ":" 은구분자.
        talkData.Add(2000, new string[] { "안녕?ㅋ" });
        talkData.Add(100, new string[] {"새빨갛게 잘 익은 사과다."  });

        //Quest Talk Data
        //첫마을 방문 마을사람과대화하기.
        talkData.Add(10 + 1000, new string[] {"어서 와.:0",
                                            "이 마을에 놀라운 전설이 있다는데:0",
                                            "저쪽에 있는 공이 알려줄꺼야:1"});
        talkData.Add(11 + 2000, new string[] {"여어 ㅋ:0",
                                            "이 마을의 전설을 들으러 온거야?ㅋ :0",
                                            "맨 입으로 가르쳐주긴 좀 그렇고 ㅋ:0",
                                            "사과가 먹고싶어서 사과좀 하나 따와주겠어?ㅋ:0"});

        talkData.Add(20 + 2000, new string[] { "찾으면 꼭 좀 가져다 줘:0" });
        talkData.Add(20 + 100, new string[] { "근처에서 사과를 찾았다." });
        talkData.Add(21 + 2000, new string[] { "고마워 ㅋ:0" });


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
