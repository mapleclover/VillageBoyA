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
                theTutorialText.text = "���ƿ�! Shift �� ������ �� �� �ֽ��ϴ�!\n �̹����� ������ǥ�������� �پ �����?";
                goalCounting++;
            }
            else
            {
                Destroy(this.gameObject);
                Destroy(runKey);
                //���Ѿ��.
            }
        }
    }
}
