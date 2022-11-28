//작성자 : 박영준
//설명 : 

using TMPro;
using UnityEngine;

public class BattleAreaTitle : MonoBehaviour
{
    [SerializeField] GameObject MapTitle;
    [SerializeField] TMP_Text text;

    public static bool isTowun = false;

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

                    SoundTest.instance.PlayBGM("BGM_Field");
                    break;
                case "CowArea":
                    MapTitle.SetActive(false);
                    MapTitle.SetActive(true);
                    text.text = "젖소 목장";

                    SoundTest.instance.PlayBGM("BGM_CowField");
                    break;
                case "Town":
                    MapTitle.SetActive(false);
                    MapTitle.SetActive(true);
                    text.text = "태초 마을";

                    SoundTest.instance.PlayBGM("BGM_Town");
                    break;
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.transform.tag == "Player")
        {
            switch (transform.name)
            {
                //case "Field":
                //    MapTitle.SetActive(false);
                //    MapTitle.SetActive(true);
                //    text.text = "푸른 초원";
                //    break;
                //case "CowArea":
                //    MapTitle.SetActive(false);
                //    MapTitle.SetActive(true);
                //    text.text = "젖소 목장";
                //    break;
                case "Town":
                    isTowun = true;
                    break;
            }
        }
    }
}