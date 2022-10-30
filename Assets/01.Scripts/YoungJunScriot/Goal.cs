using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Goal : MonoBehaviour
{
    private int goalCounting;
    [SerializeField]
    private TMPro.TMP_Text theTutorialText;
    [SerializeField]
    private GameObject moveKey;
    [SerializeField]
    private GameObject runKey;
    

    // Start is called before the first frame update
    void Start()
    {
        goalCounting = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            if(goalCounting == 0)
            {
                this.gameObject.transform.position = new Vector3(291.0f, 1.1f, 222.0f);
                Destroy(moveKey);
                runKey.SetActive(true);
                theTutorialText.text = "좋아요! Shift 를 누르면 뛸 수 있습니다!\n 이번에는 다음목표지점까지 뛰어가 볼까요?";
                goalCounting++;
            }
            else
            {
                Destroy(this.gameObject);
                Destroy(runKey);
                //씬넘어가기.
            }
        }
    }
}
