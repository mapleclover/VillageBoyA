using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public GameObject myStart;
    public GameObject mySaveLoad;
    public GameObject[] OnMouse;

    int min = 0;
    int max = 2;
    int cur = -1;
    public static Button instance;
    int countEnter = 0;
    void Start()
    {
        OnMouse[0].SetActive(true);
       
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (cur < min)
            {
                cur = min;
            }
            OnMouse[cur].SetActive(false);
            cur++;
            if (cur > max)
            {
                cur = max;
            }
            OnMouse[cur].SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            OnMouse[cur].SetActive(false);
            cur--;
            if (cur < min)
            {
                cur = min;
            }
            OnMouse[cur].SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            countEnter++;
            if (countEnter > 1)
            {
                switch (cur)
                {
                    case -1:
                        OnClickStart();
                        break;
                    case 0:
                        OnClickStart();
                        break;
                    case 1:
                        OnClickSettings();
                        break;
                    case 2:
                        OnClickExit();
                        break;
                }
            }
        }
        if (Input.GetKey(KeyCode.Escape))
        {
            OnClickBack();
        }
    }
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
        //Debug.Log("뒤로가기");
        mySaveLoad.SetActive(false);
        myStart.SetActive(true);
    }
}
