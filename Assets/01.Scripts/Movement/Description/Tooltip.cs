using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


//������
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

        //�ּ� �ɸ� �ڵ� ������ �����ּ��� ���߿� �ؽ�Ʈ������ �����Ϸ�����

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
