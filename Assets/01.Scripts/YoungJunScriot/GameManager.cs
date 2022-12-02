//�ۼ��� : �ڿ���
//���� : ���ӸŴ��� (��ũ�Ŵ��� �� ����Ʈ�Ŵ���ó��)

using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public TalkManager talkManager;
    public ActionController theActionController;
    public QuestManager questManager;
    static public GameManager inst;

    public GameObject talkPanel; // ��ȭâ���
    public Image portraitImg; // ��ȭ�� �ʻ�ȭ.
    public GameObject namePanel; // npc�̸� �ǳ�
    public TMP_Text nameText; // ��ȭ�� NPC �̸�.
    public TMP_Text talkText; // ��ȭ ���
    public GameObject scanObject; // ��ȭ�ϰ��ִ� ��� 
    public bool isAction; // ��ȭ�ϰ��ִ����ƴ���
    public int talkIndex; // ��ȭ����
    public float textSpeed;


    public bool isTalkAction = true;
    private bool canSkip = false;

    [SerializeField]
    private Goal theGoal;

    private void Awake()
    {
        inst = this;
    }

    public void Action(GameObject scanObj)
    {
        
        scanObject = scanObj;
        ObjData objData = scanObject.GetComponent<ObjData>();
        if (isTalkAction)
        {
            Talk(objData.id, objData.isNpc);
            NameText(scanObject, objData.isNpc);
        }

        theActionController.ItemInfoDisappear();
        theActionController.NpcInfoDisappear();
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
            if (isTalkAction)
            {
                StopAllCoroutines();
                StartCoroutine(SaySomething(talkData.Split(':')[0]));
                //alkText.text = talkData.Split(':')[0]; // Split�Լ��� ���� ���������ѱ�����':'�������� �յڷ� �ε���������.
                portraitImg.sprite = talkManager.GetPortrait(id, int.Parse(talkData.Split(':')[1])); // �Ű����������� int�����ΰ������°ű⋚���� Parse�� ��ȯ
                portraitImg.color = new Color(1, 1, 1, 1); // �ǵ� 1�� alpha��.
            }
        }
        //Item
        else
        {
            if (isTalkAction)
            {
                StopAllCoroutines();
                StartCoroutine(SaySomething(talkData));

                //talkText.text = talkData;

                portraitImg.color = new Color(1, 1, 1, 0); // �ǵ� 0�� alpha��. �����۰� ��ȭ�Ҷ��� alpha�� �ּ�.
            }
        }
        if (!ShopManager.isAction)
        {
            isAction = true;
        }
        talkIndex++; // ������ȭ�� �Ѿ.
    }

    IEnumerator SaySomething(string text)
    {
        textSpeed = PlayerPrefs.GetInt("TextSpeed");
        isTalkAction = false;
        canSkip = true;
        talkText.text = "";
        bool t_monster = false, t_white = false, t_yellow = false, t_size = false, t_orgsize = false;
        bool t_ignore = false; // Ư�����ڸ� �����ϱ����� bool��.
        if (textSpeed == 10)
        {
            for (int i = 0; i < text.Length; i++)
            {
                switch (text[i])
                {
                    case '��': t_monster = false; t_white = true; t_yellow = false; t_size = false; t_orgsize = false; t_ignore = true; break;
                    case '��': t_monster = true; t_white = false; t_yellow = false; t_size = false; t_orgsize = false; t_ignore = true; break;
                    case '��': t_monster = true; t_white = false; t_yellow = true; t_size = false; t_orgsize = false; t_ignore = true; break;
                    case '��': t_monster = false; t_white = false; t_yellow = false; t_size = true; t_orgsize = false; t_ignore = true; break;
                    case '��': t_monster = false; t_white = false; t_yellow = false; t_size = false; t_orgsize = true; t_ignore = true; break;
                }
                string t_letter = text[i].ToString();
                if (!t_ignore)
                {
                    if (t_white) { t_letter = "<color=#ffffff>" + t_letter + "</color>"; }
                    else if (t_monster) { t_letter = "<color=#42DEE3>" + t_letter + "</color>"; }
                    else if (t_yellow) { t_letter = "<color=#FFFF00>" + t_letter + "</color>"; }
                    else if (t_size) { t_letter = "<size=20>" + t_letter + "</size>"; }
                    else if (t_orgsize) { t_letter = "<size=36>" + t_letter + "</size>"; }
                    talkText.text += t_letter;
                }
                t_ignore = false;
            }
        }
        else
        {
            for (int i = 0; i < text.Length; i++)
            {
                switch (text[i])
                {
                    case '��': t_monster = false; t_white = true; t_yellow = false; t_size = false; t_orgsize = false; t_ignore = true; break;
                    case '��': t_monster = true; t_white = false; t_yellow = false; t_size = false; t_orgsize = false; t_ignore = true; break;
                    case '��': t_monster = true; t_white = false; t_yellow = true; t_size = false; t_orgsize = false; t_ignore = true; break;
                    case '��': t_monster = false; t_white = false; t_yellow = false; t_size = true; t_orgsize = false; t_ignore = true; break;
                    case '��': t_monster = false; t_white = false; t_yellow = false; t_size = false; t_orgsize = true; t_ignore = true; break;
                }
                string t_letter = text[i].ToString();
                if (!t_ignore)
                {
                    if (t_white) { t_letter = "<color=#ffffff>" + t_letter + "</color>"; }
                    else if (t_monster) { t_letter = "<color=#42DEE3>" + t_letter + "</color>"; }
                    else if (t_yellow) { t_letter = "<color=#FFFF00>" + t_letter + "</color>"; }
                    else if (t_size) { t_letter = "<size=20>" + t_letter + "</size>"; }
                    else if (t_orgsize) { t_letter = "<size=36>" + t_letter + "</size>"; }
                    talkText.text += t_letter;
                }
                t_ignore = false;
                if(canSkip)
                {
                    yield return new WaitForSeconds(0.2f / (textSpeed * 2));
                }
            }

        }
        canSkip = false;
        isTalkAction = true;
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

    private void Start()
    {
        SoundTest.instance.PlayBGM("BGM_Town");
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Space))
        {
            canSkip = false;
        }
    }
}
