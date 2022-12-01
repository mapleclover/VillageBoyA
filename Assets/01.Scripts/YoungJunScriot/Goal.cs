//작성자 : 박영준
//설명 : 튜토리얼 

using TMPro;
using UnityEngine;

public class Goal : MonoBehaviour
{
    public static int goalCounting;
    [SerializeField] private GameObject theTutorialText;
    [SerializeField] private GameObject moveKey;
    [SerializeField] private GameObject runKey;
    [SerializeField] private GameManager theManager;

    private int keyDownCount = 0;
    public bool isTutorial; //  듀토리얼 팝업 켜져있는지 아닌지.

    // Start is called before the first frame update
    void Start()
    {
        goalCounting = 0;
        isTutorial = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (goalCounting == 2)
        {
            runKey.SetActive(true);
            theTutorialText.gameObject.SetActive(true);
            isTutorial = true;
            theTutorialText.GetComponentInChildren<TextMeshProUGUI>().text =
                "좋아요! Shift 를 누르면 뛸 수 있습니다!\n 이번에는 다음목표지점까지 뛰어가 볼까요?";
            goalCounting++;
            //Destroy(this.gameObject);
            //Destroy(runKey);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (goalCounting == 0)
            {
                theManager.Action(this.gameObject);
                Destroy(moveKey);
                isTutorial = false;
                theTutorialText.gameObject.SetActive(false);
                goalCounting++;
            }

            if (goalCounting == 3)
            {
                Destroy(runKey);
                isTutorial = false;
                theTutorialText.gameObject.SetActive(false);

                //컷씬이동 돌부리에 넘어지는컷씬 !///////////////////////////////////////////////
                SceneLoad.Instance.ToBattleScene("Rock", false,
                    "Rock", Random.Range(2, 4)
                    , 0, 1);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (goalCounting == 1)
            {
                if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Space))
                {
                    if (theManager.isTalkAction)
                    {
                        theManager.Action(this.gameObject);
                        keyDownCount++;
                    }
                }

                if (keyDownCount == 2)
                {
                    QuestManager thequest = FindObjectOfType<QuestManager>();
                    
                    thequest.questActionIndex++;
                    thequest.ControlObject();
                    thequest.ControlPopup();
                    gameObject.SetActive(false);
                }
            }
        }
    }
}