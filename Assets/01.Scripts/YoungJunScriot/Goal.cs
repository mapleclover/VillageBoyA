using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
// 박영준 , 듀토리얼 
public class Goal : MonoBehaviour
{
    public static int goalCounting;
    [SerializeField]
    private GameObject theTutorialText;
    [SerializeField]
    private GameObject moveKey;
    [SerializeField]
    private GameObject runKey;
    [SerializeField]
    private GameManager theManager;

    private int keyDowunCount = 0;
    public bool isTutorial; //  듀토리얼 팝업 켜져있는지 아닌지.

    // Start is called before the first frame update
    void Start()
    {
        goalCounting = 0;
        isTutorial = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (goalCounting == 2)
        {
            runKey.SetActive(true);
            theTutorialText.gameObject.SetActive(true);
            isTutorial = true;
            theTutorialText.GetComponentInChildren<TextMeshProUGUI>().text = "좋아요! Shift 를 누르면 뛸 수 있습니다!\n 이번에는 다음목표지점까지 뛰어가 볼까요?";
            goalCounting++;
            //Destroy(this.gameObject);
            //Destroy(runKey);

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            if(goalCounting == 0)
            {
                theManager.Action(this.gameObject);
                Destroy(moveKey);
                isTutorial = false;
                theTutorialText.gameObject.SetActive(false);
                goalCounting++;
            }
            if(goalCounting == 3)
            {
                Destroy(runKey);
                isTutorial = false;
                theTutorialText.gameObject.SetActive(false);

                //컷씬이동 돌부리에 넘어지는컷씬 !///////////////////////////////////////////////
                SceneLoad.Instance.ChangeScene(4);
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
                    theManager.Action(this.gameObject);
                    keyDowunCount++;
                }
                if (keyDowunCount == 2)
                {
                    gameObject.SetActive(false);
                }
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        
    }

}
