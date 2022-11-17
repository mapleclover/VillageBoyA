using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleAreaTitle : MonoBehaviour
{
    [SerializeField] GameObject MapTitle;
    [SerializeField] TMPro.TMP_Text text;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("�ȳ�");
        if (other.gameObject.transform.tag == "Player")
        {
            MapTitle.SetActive(true);
            text.text = "���� ��� ����";
        }
    }
    private void OnTriggerExit(Collider other)
    {
        Debug.Log("�߰�");
        if (other.gameObject.transform.tag == "Player")
        {
            MapTitle.SetActive(true);
            text.text = "���ʸ���";
        }
    }
}
