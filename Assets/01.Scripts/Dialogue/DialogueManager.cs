///�ۼ��� : ����ȣ
///���� : �ҷ��� ��縦 ��ȭâ�� �׸��� ȭ���� �̹����� ����ִ� ��ũ��Ʈ
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;

    private void Awake()
    {
        instance = this;
    }

    public GameObject DialogueBox;
    public TextMeshProUGUI Name;
    public TextMeshProUGUI Text;

    public Image CharacterImage;
    public Sprite[] CharacterImages;

    public void Say(string speech, string speaker = "", int imageIndex = -1)
    {
        StopSpeaking();
        speaking = StartCoroutine(Speaking(speech, speaker, imageIndex));
    }

    public void StopSpeaking()
    {
        if (isSpeaking)
        {
            StopCoroutine(speaking);
        }
        speaking = null;
    }
    public bool isSpeaking { get { return speaking != null; } }
    public bool isWaitingForUserInput = false;
    Coroutine speaking = null;
    IEnumerator Speaking(string targetSpeech, string speaker = "", int imageIndex = -1)
    {
        DialogueBox.SetActive(true);
        Text.text = "";
        Name.text = DetermineSpeaker(speaker);
        if(imageIndex != -1)
        {
            CharacterImage.sprite = CharacterImages[imageIndex];
        }        
        isWaitingForUserInput = false;

        while(Text.text != targetSpeech)
        {
            Text.text += targetSpeech[Text.text.Length];
            yield return new WaitForEndOfFrame();
        }
        isWaitingForUserInput = true;
        while(isWaitingForUserInput)
            yield return new WaitForEndOfFrame();

        StopSpeaking();
    }

    string DetermineSpeaker(string speaker)
    {
        string retVal = Name.text;
        if(speaker != Name.text && speaker != "")
        {
            retVal = (speaker.ToLower().Contains("narrator")) ? "" : speaker;
        }
        return retVal;
    }
}
