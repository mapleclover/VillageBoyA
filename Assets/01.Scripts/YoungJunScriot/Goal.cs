//�ۼ��� : �ڿ���
//���� : Ʃ�丮�� 

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
    public bool isTutorial; //  ���丮�� �˾� �����ִ��� �ƴ���.

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
                "���ƿ�! Shift �� ������ �� �� �ֽ��ϴ�!\n �̹����� ������ǥ�������� �پ �����?";
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

                //�ƾ��̵� ���θ��� �Ѿ������ƾ� !///////////////////////////////////////////////
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