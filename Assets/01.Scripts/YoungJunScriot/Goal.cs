using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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

    // Start is called before the first frame update
    void Start()
    {
        goalCounting = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (goalCounting == 2)
        {
            runKey.SetActive(true);
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
                //this.gameObject.transform.position = new Vector3(291.0f, 1.1f, 222.0f);
                Destroy(moveKey);
                //runKey.SetActive(true);
                //theTutorialText.text = "좋아요! Shift 를 누르면 뛸 수 있습니다!\n 이번에는 다음목표지점까지 뛰어가 볼까요?";
                theTutorialText.gameObject.SetActive(false);
                goalCounting++;
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (goalCounting == 1)
            {
                if (Input.GetKeyDown(KeyCode.E))
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
