using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


//전정우
//1025


public class Tooltip : MonoBehaviour
{
    //public TextMeshProUGUI nameTxt;
    //public TextMeshProUGUI descriptionTxt;

    private RectTransform rectTransform;
    Vector2 pos = new Vector2(10f, 150f);
    /*public void SetupTooltip(string name, string des, int atk)
    {
        //nameTxt.text = name;
        //descriptionTxt.text = des;

        //주석 걸린 코드 지우지 말아주세요 나중에 텍스트값으로 연결하려구요

    }*/

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        
    }
    private void Update()
    {

        //rectTransform.position = Input.mousePosition;
        rectTransform.transform.position = pos;

    }

}
