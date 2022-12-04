//�ۼ��� : ����ȣ
//���� : ��� ��ũ��Ʈ TXT ���Ϻ��� �ҷ��� ������ �����ϰ� ���������� ��ȭ�Ŵ����� �Լ�ȣ���ϴ� ��ũ��Ʈ
using System.IO;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    DialogueManager dialogue;
    int index = 0;
    public string[]  s;

    private void Start()
    {
        dialogue = DialogueManager.instance;

        //s = File.ReadAllLines("Assets/10.Resources/TextScript/Text1.txt");
        TextAsset a = Resources.Load<TextAsset>("Text/Text1");
        s = a.text.Split("\n");

        Say(s[index]);
        index++;
    }

    

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.E))
        {
            if(dialogue.isSpeaking)
            {
                dialogue.skip = true;
            }
            else if (!dialogue.isSpeaking || dialogue.isWaitingForUserInput)
            {
                if (index >= s.Length)
                {
                    return;
                }
                Say(s[index]);
                index++;
            }
        }
    }

    public void LoadFirstSentence()
    {
        Say(s[index]);
        index++;
    }

    void Say(string s)
    {
        string[] parts = s.Split(':');
        if (parts[0].Contains("END"))
        {
            dialogue.CloseChatAnim();
            SceneLoad.Instance.ToBattleScene("Rock", false,
                "Rock", Random.Range(2, 4)
                , 0, 1);
            return;
        }
        string speech = parts[0];
        string speaker = "";
        int imageIndex = -1;
        
        if (parts.Length >= 2)
        {
            speaker = parts[1];
            if(parts[2] != null)
            {
                imageIndex = int.Parse(parts[2]);
            }
        }
        //string speaker = (parts.Length >= 2) ? parts[1] : "";

        dialogue.Say(speech, speaker, imageIndex);
    }

}