//�ۼ��� : �ڿ���
//���� : ���ӸŴ��� (��ũ�Ŵ��� �� ����Ʈ�Ŵ���ó��)

using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public TalkManager talkManager;
    public QuestManager questManager;
    public GameObject talkPanel; // ��ȭâ���
    public Image portraitImg; // ��ȭ�� �ʻ�ȭ.
    public GameObject namePanel; // npc�̸� �ǳ�
    public TMP_Text nameText; // ��ȭ�� NPC �̸�.
    public TMP_Text talkText; // ��ȭ ���
    public GameObject scanObject; // ��ȭ�ϰ��ִ� ��� 
    public bool isAction; // ��ȭ�ϰ��ִ����ƴ���
    public int talkIndex; // ��ȭ����
    public float textSpeed;

    [SerializeField]
    private Goal theGoal;
    
    public void Action(GameObject scanObj)
    {
        scanObject = scanObj;
        ObjData objData = scanObject.GetComponent<ObjData>();
        Talk(objData.id, objData.isNpc);
        NameText(scanObject, objData.isNpc);


        talkPanel.SetActive(isAction);
    }

    private void Talk(int id, bool isNpc)
    {
        //��ȭ����
        int questTalkIndex = questManager.GetQuestTalkIndex(id);
        string talkData = talkManager.GetTalk(id + questTalkIndex, talkIndex); // ��ȭ������.
        //��ȭ����
        if(talkData == null) // ���̻� ��ȭ�ҳ����̾������ isaction�� false�ιٲ�.
        {
            isAction = false;
            talkIndex = 0; // ��ȭ�������� �ε��� �ʱ�ȭ.
            Debug.Log(questManager.CheckQuest(id, scanObject)); // ����Ʈ ��ȭ�ε��� �߰�
            return; // �������Ǵ�ȭ�Ұ;����� �ٷθ���.
        }
        //Npc�ϰ�� ��� ���� �� �ʻ�ȭ
        if (isNpc)
        {
            StopAllCoroutines();
            StartCoroutine(SaySomething(talkData.Split(':')[0]));
            //talkText.text = talkData.Split(':')[0]; // Split�Լ��� ���� ���������ѱ�����':'�������� �յڷ� �ε���������.

            portraitImg.sprite = talkManager.GetPortrait(id, int.Parse(talkData.Split(':')[1])); // �Ű����������� int�����ΰ������°ű⋚���� Parse�� ��ȯ
            portraitImg.color = new Color(1, 1, 1, 1); // �ǵ� 1�� alpha��.
        }
        //Item
        else
        {
            StopAllCoroutines();
            StartCoroutine(SaySomething(talkData));
            //talkText.text = talkData;

            portraitImg.color = new Color(1, 1, 1, 0); // �ǵ� 0�� alpha��. �����۰� ��ȭ�Ҷ��� alpha�� �ּ�.
        }

        isAction = true;
        talkIndex++; // ������ȭ�� �Ѿ.
    }

    IEnumerator SaySomething(string text)
    {
        talkText.text = "";
        //float textSpeed = (float)PlayerPrefs.GetInt("TextSpeed");
        //if(textSpeed == 10.0f)
        //{
        //    talkText.text = i;
        //}
        //else
        //{
        //    while (talkText.text != i)
        //    {
        //        talkText.text += i[talkText.text.Length];
        //        yield return new WaitForSeconds(0.2f / textSpeed);
        //    }
        //}
        for (int i = 0; i < text.Length; i++)
        {
            talkText.text += text[i];
            yield return new WaitForSeconds(textSpeed);
        }
    }
    private void NameText(GameObject gameObject, bool isNpc)
    {
        if (isNpc)
        {
            namePanel.SetActive(true);
            nameText.text = gameObject.transform.GetComponent<Pickup>().npc.npcName;
        }
        else
        {
            namePanel.SetActive(false);
        }
    }
}
