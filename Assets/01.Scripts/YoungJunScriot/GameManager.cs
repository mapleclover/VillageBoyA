//작성자 : 박영준
//설명 : 게임매니저 (토크매니저 및 퀘스트매니저처리)

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

    public GameObject talkPanel; // 대화창배경
    public Image portraitImg; // 대화시 초상화.
    public GameObject namePanel; // npc이름 판넬
    public TMP_Text nameText; // 대화시 NPC 이름.
    public TMP_Text talkText; // 대화 대사
    public GameObject scanObject; // 대화하고있는 대상 
    public bool isAction; // 대화하고있는지아닌지
    public int talkIndex; // 대화순서
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
        //대화시작
        int questTalkIndex = questManager.GetQuestTalkIndex(id);
        string talkData = talkManager.GetTalk(id + questTalkIndex, talkIndex); // 대화가져옴.
        //대화종료
        if(talkData == null) // 더이상 대화할내용이없을경우 isaction을 false로바꿈.
        {
            isAction = false;
            talkIndex = 0; // 대화끝낫으면 인덱스 초기화.
            Debug.Log(questManager.CheckQuest(id, scanObject)); // 퀘스트 대화인덱스 추가
            return; // 더어차피대화할것없으니 바로리턴.
        }
        //Npc일경우 토그 지속 및 초상화
        if (isNpc)
        {
            if (isTalkAction)
            {
                StopAllCoroutines();
                StartCoroutine(SaySomething(talkData.Split(':')[0]));
                //alkText.text = talkData.Split(':')[0]; // Split함수를 쓰면 내가설정한구분자':'기준으로 앞뒤로 인덱스가나뉨.
                portraitImg.sprite = talkManager.GetPortrait(id, int.Parse(talkData.Split(':')[1])); // 매개변수형식이 int형으로가져오는거기떄문에 Parse로 변환
                portraitImg.color = new Color(1, 1, 1, 1); // 맨뒤 1은 alpha값.
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

                portraitImg.color = new Color(1, 1, 1, 0); // 맨뒤 0은 alpha값. 아이템과 대화할때는 alpha값 최소.
            }
        }
        if (!ShopManager.isAction)
        {
            isAction = true;
        }
        talkIndex++; // 다음대화로 넘어감.
    }

    IEnumerator SaySomething(string text)
    {
        textSpeed = PlayerPrefs.GetInt("TextSpeed");
        isTalkAction = false;
        canSkip = true;
        talkText.text = "";
        bool t_monster = false, t_white = false, t_yellow = false, t_size = false, t_orgsize = false;
        bool t_ignore = false; // 특수문자를 생략하기위한 bool값.
        if (textSpeed == 10)
        {
            for (int i = 0; i < text.Length; i++)
            {
                switch (text[i])
                {
                    case 'ⓦ': t_monster = false; t_white = true; t_yellow = false; t_size = false; t_orgsize = false; t_ignore = true; break;
                    case 'ⓜ': t_monster = true; t_white = false; t_yellow = false; t_size = false; t_orgsize = false; t_ignore = true; break;
                    case 'ⓨ': t_monster = true; t_white = false; t_yellow = true; t_size = false; t_orgsize = false; t_ignore = true; break;
                    case 'ⓢ': t_monster = false; t_white = false; t_yellow = false; t_size = true; t_orgsize = false; t_ignore = true; break;
                    case 'ⓞ': t_monster = false; t_white = false; t_yellow = false; t_size = false; t_orgsize = true; t_ignore = true; break;
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
                    case 'ⓦ': t_monster = false; t_white = true; t_yellow = false; t_size = false; t_orgsize = false; t_ignore = true; break;
                    case 'ⓜ': t_monster = true; t_white = false; t_yellow = false; t_size = false; t_orgsize = false; t_ignore = true; break;
                    case 'ⓨ': t_monster = true; t_white = false; t_yellow = true; t_size = false; t_orgsize = false; t_ignore = true; break;
                    case 'ⓢ': t_monster = false; t_white = false; t_yellow = false; t_size = true; t_orgsize = false; t_ignore = true; break;
                    case 'ⓞ': t_monster = false; t_white = false; t_yellow = false; t_size = false; t_orgsize = true; t_ignore = true; break;
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
