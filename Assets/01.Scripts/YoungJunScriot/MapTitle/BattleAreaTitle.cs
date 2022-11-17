using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleAreaTitle : MonoBehaviour
{
    [SerializeField] GameObject MapTitle;
    [SerializeField] TMPro.TMP_Text text;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("안녕");
        if (other.gameObject.transform.tag == "Player")
        {
            MapTitle.SetActive(true);
            text.text = "몬스터 출몰 지역";
        }
    }
    private void OnTriggerExit(Collider other)
    {
        Debug.Log("잘가");
        if (other.gameObject.transform.tag == "Player")
        {
            MapTitle.SetActive(true);
            text.text = "태초마을";
        }
    }
}
