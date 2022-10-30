///작성자 : 유은호
///대사 스크립트 TXT 파일부터 불러와 내용을 저장하고 누를때마다 대화매니져로 함수호출하는 스크립트

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    DialogueManager dialogue;
    int index = 0;
    public string[]  s;

    private void Start()
    {
        dialogue = DialogueManager.instance;

        s = System.IO.File.ReadAllLines("Assets/10.Resources/TextScript/Text1.txt");

        Say(s[index]);
        index++;
    }

    

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
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
            Debug.Log("이동화면으로 이동한다");
            dialogue.CloseChatAnim();
            SceneLoad.Instance.ChangeScene(int.Parse(parts[1]));
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