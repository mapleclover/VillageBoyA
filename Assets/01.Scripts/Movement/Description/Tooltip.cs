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

    public void SetupTooltip(string name, string des, int atk)
    {
        //nameTxt.text = name;
        //descriptionTxt.text = des;

        //�ּ� �ɸ� �ڵ� ������ �����ּ��� ���߿� �ؽ�Ʈ������ �����Ϸ�����

    }

    private void Start()
    {
        
    }
    private void Update()
    {
        transform.position = Input.mousePosition;
    }

}
