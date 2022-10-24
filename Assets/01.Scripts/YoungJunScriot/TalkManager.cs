using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//�ڿ��� ��ũ�Ŵ���
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
        talkData.Add(1000, new string[] { "�ȳ�?:0", "�̰��� ó�� �Ա���?:1" }); //��ȭ���Ǽ��ڴ� portratiIndex ":" ��������.
        talkData.Add(2000, new string[] { "�ȳ�?��" });
        talkData.Add(100, new string[] {"�������� �� ���� �����."  });

        //Quest Talk Data
        //ù���� �湮 �����������ȭ�ϱ�.
        talkData.Add(10 + 1000, new string[] {"� ��.:0",
                                            "�� ������ ���� ������ �ִٴµ�:0",
                                            "���ʿ� �ִ� ���� �˷��ٲ���:1"});
        talkData.Add(11 + 2000, new string[] {"���� ��:0",
                                            "�� ������ ������ ������ �°ž�?�� :0",
                                            "�� ������ �������ֱ� �� �׷��� ��:0",
                                            "����� �԰�; ����� �ϳ� �����ְھ�?��:0"});

        talkData.Add(20 + 2000, new string[] { "ã���� �� �� ������ ��:0" });
        talkData.Add(20 + 100, new string[] { "��ó���� ����� ã�Ҵ�." });
        talkData.Add(21 + 2000, new string[] { "���� ��:0" });


        //Portrait Data
        portraitData.Add(1000 + 0, portraitArr[0]); // id + portraitIndex
        portraitData.Add(1000 + 1, portraitArr[1]);

        portraitData.Add(2000 + 0, portraitArr[2]);
        
    }

    public string GetTalk(int id, int talkIndex)
    {
        //����ó��. ����Ʈ�ͻ������ ���� ��ȭ������.
        if(!talkData.ContainsKey(id))// ContainsKey => Dictionary �ȿ� �ش� Key���� �����ϴ��� üũ���ִ��Լ�. ������ true��ȯ
        {
            if (!talkData.ContainsKey(id - id % 10)) // ����Ʈ�⺻��縶�� ������(ex .������ �� �ǹ̾��� �繰)
                //if (talkIndex == talkData[id - id % 100].Length)
                //    return null;
                //else // ���� �⺻��縦 ������´�.
                //    return talkData[id - id % 100][talkIndex]; 
            {
                //Get First Talk
                return GetTalk(id - id % 100, talkIndex); // ���� �ּ����� ����ȭ (����Լ�)
            }
            else
            {
                ////�ش�����Ʈ ������� �� ��簡 ���� ��.
                //if (talkIndex == talkData[id - id % 10].Length)
                //    return null;
                //else//�ش� ����Ʈ ��ó�� ��縦 ������.
                //    return talkData[id - id % 10][talkIndex];

                //Get First Quest Talk
                return GetTalk(id - id % 10, talkIndex); //�� �ּ����� ����ȭ.(����Լ�)
            }
        }

        //����ó���ƴҶ� talkData ����
        if (talkIndex == talkData[id].Length) 
            return null; // �ش翣�ǽÿ��� ��ȭ(�ε���)�� �������� ����
        else
            return talkData[id][talkIndex];
    }

    public Sprite GetPortrait(int id, int portraitinIndex)
    {
        return portraitData[id + portraitinIndex]; // id���ٰ� �ʻ�ȭ�ε�������. ex 1001, 2001 
    }
}
