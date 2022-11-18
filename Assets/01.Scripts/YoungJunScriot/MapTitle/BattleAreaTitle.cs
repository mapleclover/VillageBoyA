using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleAreaTitle : MonoBehaviour
{
    [SerializeField] GameObject MapTitle;
    [SerializeField] TMPro.TMP_Text text;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.transform.tag == "Player")
        {
            switch (transform.name)
            {
                case "Field":
                    MapTitle.SetActive(false);
                    MapTitle.SetActive(true);
                    text.text = "Ǫ�� �ʿ�";
                    break;
                case "CowArea":
                    MapTitle.SetActive(false);
                    MapTitle.SetActive(true);
                    text.text = "���� ����";
                    break;
                case "Town":
                    MapTitle.SetActive(false);
                    MapTitle.SetActive(true);
                    text.text = "���� ����";
                    break;
            }
        
        }
    }
}
