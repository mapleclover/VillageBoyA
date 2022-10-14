using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEngine.EventSystems;
public class SavingiinGame : MonoBehaviour
{
    public GameObject myImage;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            myImage.SetActive(true);
        }
    }
    public void YesClick()
    {
        DataController.instance.SaveGameData();
        myImage.SetActive(false);
    }
    public void NoClick()
    {
        myImage.SetActive(false);
    }
}
