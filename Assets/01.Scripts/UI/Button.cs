using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public GameObject myStart;
    public GameObject mySaveLoad;
   public void OnClickStart()
    {
        myStart.SetActive(false);
        mySaveLoad.SetActive(true);
    }
    public void OnClickSettings()
    {

    }
    public void OnClickExit()
    {

    }
    public void OnClickBack()
    {
        Debug.Log("뒤로가기");
        mySaveLoad.SetActive(false);
        myStart.SetActive(true);
    }
}
