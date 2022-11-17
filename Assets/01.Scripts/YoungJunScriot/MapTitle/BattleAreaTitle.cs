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
                    text.text = "푸른 초원";
                    break;
                case "CowArea":
                    MapTitle.SetActive(false);
                    MapTitle.SetActive(true);
                    text.text = "젖소 목장";
                    break;
                case "Town":
                    MapTitle.SetActive(false);
                    MapTitle.SetActive(true);
                    text.text = "태초 마을";
                    break;
            }
        
        }
    }
}
