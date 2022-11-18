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
        talkData.Add(1000, new string[] { "�ȳ�! ���� ���� Ŭ����:0", "�̰��� ó�� �Ա���?:1" }); //��ȭ���Ǽ��ڴ� portratiIndex ":" ��������.
        talkData.Add(2000, new string[] { "�ȳ� ~ �� ȣ�ξ� ~:0", "���� �� ������ ��� ��������~:1" });
        talkData.Add(3000, new string[] { "��.. ������ ���� ..:0" });
        talkData.Add(100, new string[] {"�������� �� ���� �����."  });

        //Quest Talk Data
        //ù���� �湮 �����������ȭ�ϱ�.
        talkData.Add(10 + 10000, new string[] {"���ƿ� !\n ��ȭ�� E ��ư�� ���� �Ѿ �� �־��.",
                                               "�̹����� �տ� �ִ� Ŭ������ E ��ư�� ���� ���� �ɾ���?" });

        talkData.Add(11 + 1000, new string[] {"�ȳ�! ����:0",
                                              "Npc�� �����۰� ��ȣ�ۿ� �ϰ� ������ �ٷ� �տ��� E ��ư������� !:0",
                                              "Ŭ���� �谡���µ� Ȥ�� �ǳ��� ��� �ΰ��� ������ �� ������?:1"});

        talkData.Add(20 + 100, new string[] { "��ó���� ����� ã�Ҵ�." });
        talkData.Add(21 + 100, new string[] { "��ó���� ����� ã�Ҵ�." });
        talkData.Add(22 + 1000, new string[] { "����! ����! �ȳ�:1" });

        talkData.Add(30 + 1000,new string[]{"�ȳ�! ����\n�Ʊ�� ������ !:1",
                                              "�Ʊ� ���̾� �������� ����µ�\n������ ��ȭ�� ���� �����ٸ�?:0",
                                              "������� ! ���� ������ ��ȭ�� ������ �� �� ����, �� ! �� !:0",
                                              "��ȭ�� �� ���� ���� ! ! \n�ݴ����� ȣ�� ��ϰ� ���� ��� ���ƺ��̴��� �ѹ������� ?:0"});
        talkData.Add(31 + 2000,new string[]{"���� . . . . . . . . . . . . .:0",
                                              "... ���� �Ѽ��� ���� ������ ���İ�,,?\n�Ƽ��� �����ٰžƴϸ� ������..:0",
                                              "��..?\n�ʰ��� �����̰� �� �����ְڴٰ�?:0",
                                              "Ǫ��..�׷��׷� ����:1",
                                              "��� �ֱٿ� �ٹ��� ������� �츮�ƺ� ���� �� ��Ƴ���\n�츮���� �ٽ��� ���� �ٹ��� ������� �� ������������ڴµ�..:0",
                                              "�ʹ������ϰ� ��������~\n�ٹ濩����� ��ü�� �����ٴϴϱ� ���� �������~:1"});
        talkData.Add(32 + 2000, new string[]{"���� ? ! ? !\n���첿�� ���׾ƴϾ� ? !:0",
                                              ". . . . . . �Ʊ�� �����ؼ� �̾��� . .\n��ģ���� ���� ?:0",
                                              "���� ! ��ʶ�� �ϱ� �׷��� \n���⳯�� ���̻��ѰŰ����� �ǳ��� NPC���� ���� !:1",
                                              "���Ⱝȭ�� �� �� �ְ� �ʿ��� �����۵� �Ǹ��ϰ� �ִ� \n . . . �׸��� Ȥ�� �Ѱ����� �� ���� �� �� ������ ?:0",
                                              "��� ������� ����ϴ� ���� �־�\n�� �༮�� �� ó������ �� �־� ?:0",
                                              "..�ʶ�� ���� �� ���� �� ����:1",
                                              "������� �Ϲݿ���� �Ȱ��� ����µ� ��û ū �༮���־�\n��⿡�� ��û���ϴٴϱ� �������⸦ ����°� ���� �� ����:1"});
        talkData.Add(33 + 2000, new string[]{"���� ����� ! ! ! ! !:0",
                                              "�츮������ ��ĩ�Ÿ� ���µ� !\n�̰� �����̾� !:0",
                                              "<color=yellow>"+"===========1 0 0 ��带 ȹ���Ͽ����ϴ�.=========="+"</color>" + "\n�� �츮������ �����̾� !:1",
                                              "Ŭ���� ã���� �ѹ� ���� ! :0"});
        talkData.Add(40 + 1000, new string[]{"���� ! �������̾� �����ϴ� ��տ��츦 ���񷶴ٸ鼭 ?:0",
                                              "�� ������ Ŭ������ ����� �����ٶ�����\n�ɻ�ġ ������ �����ٴϱ�?:1",
                                              "<size=20>"+"... ���߿� �������� ���������� ..�߾��߾�:0",
                                              "</size>"+"��..��? ����׷��İ� �ƹ��͵� �Ƴ� ! :1",
                                              "���� �����θ��� �ٸ��̾ƴ϶� ���� ����տ� ������� ������ ������ ?:0",
                                              "������ ���� Ȱ�༺�� ���� �ѹ� ���� �ʹٳ���\n�ð� �� �� �ѹ� ���� !:0"});
        talkData.Add(41 + 3000, new string[]{". . . . . . . .:0",
                                              ". . . . . . . . . .:0",
                                              ". . . . . . . . . . . . . .\n . . . . . . . . . . �� . . .:0",
                                              "��. . �մ��� �Ծ���. . . \n�ڳװ� �ֱٿ� ��տ��츦 ��� �系�ΰ� ?:0",
                                              "�ݰ��� . ���� ������� �ϳ�\n�� ���� ������ ��������:1",
                                              "�� . .? ����ġ��� ���ٰ�?\n�� . . . �����غ� ���� ���� �� �𸣰ڱ�.:0",
                                              "�� . . . . \n��� �Ѱ� ���ĵΰ�:0",
                                              "���� �ڳ׸� ���ڰ� �Ѱ� �ٸ��̾ƴϳ�\n�ڳ׵� ���� �����ϰ� ������ �԰�����:0",
                                              "�ֱ� �� ���� ��ó�� ���ͼ��ڴ� ������ �ƴ϶��:0",
                                              "Ȥ�� �ڳװ� �ʿ������� ���͸� �� ������ �� �� �ֳ�?:0",
                                              "������ �����̶� ����� �����̿��� �β������� . .\n ��Ź�ϳ�.:1",
                                              "���칫�� ���� "+"<color=red>"+"���己"+"</color>"+"�̶�� ���̴޸� ����ü�� �ִٳ�,\n����� ������ �������� . . .:0",
                                              "�� �༮�� ����ġ�� �� �༮�� ���� ������ �� �� �ְڳ�?:0",
                                              ". . . ����, �׷� ��ٸ��ڳ�\n�ε� �� �����ϰ�.:0"});



        //Portrait Data
        portraitData.Add(1000 + 0, portraitArr[0]); // id + portraitIndex
        portraitData.Add(1000 + 1, portraitArr[1]);

        portraitData.Add(2000 + 0, portraitArr[2]); // ȣ��
        portraitData.Add(2000 + 1, portraitArr[3]);

        portraitData.Add(3000 + 0, portraitArr[4]); // ����
        portraitData.Add(3000 + 1, portraitArr[5]);
        
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
