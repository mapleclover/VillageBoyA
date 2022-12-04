//�ۼ��� : �ڿ���
//���� : 

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
                    text.text = "Ǫ�� �ʿ�";

                    SoundTest.instance.PlayBGM("BGM_Field");
                    break;
                case "CowArea":
                    MapTitle.SetActive(false);
                    MapTitle.SetActive(true);
                    text.text = "���� ����";

                    SoundTest.instance.PlayBGM("BGM_CowField");
                    break;
                case "Town":
                    MapTitle.SetActive(false);
                    MapTitle.SetActive(true);
                    text.text = "���� ����";

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
                //    text.text = "Ǫ�� �ʿ�";
                //    break;
                //case "CowArea":
                //    MapTitle.SetActive(false);
                //    MapTitle.SetActive(true);
                //    text.text = "���� ����";
                //    break;
                case "Town":
                    isTowun = true;
                    break;
            }
        }
    }
}