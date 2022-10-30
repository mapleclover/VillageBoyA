using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.UI;


public class Button_New : MonoBehaviour
{
    public static Button_New Instance = null;
    public static int cur = 0;
    public GameObject OnMouse;
    public GameObject[] myButtons;
    public GameObject[] myButtons2;
    public GameObject mySaveLoad;
    public GameObject myStart;
    public GameObject GameSetting;

    bool check = false;
    bool v = false;
    int min = 0, max = 3;
    Vector2 pos = Vector2.zero;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (v)
        {
            if (mySaveLoad.activeSelf)
            {
                SecondMove();
                OnMouse.transform.localScale = new Vector3(2.2f, 0.9975f, 0.9975f);
                OnMouse.transform.GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(30, 50);
                OnMouse.transform.GetChild(1).GetComponent<RectTransform>().sizeDelta = new Vector2(30, 50);
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    if (check)
                    {
                        OnClickBack();
                        check = false;
                    }
                }
                if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    cur++;
                    if (cur > max) cur = max;
                }
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    cur--;
                    if (cur < min) cur = min;
                }

            }
            else
            {
                OnMouse.transform.localScale = new Vector3(0.8975f, 0.9975f, 0.9975f);
                OnMouse.transform.GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(50, 50);
                OnMouse.transform.GetChild(1).GetComponent<RectTransform>().sizeDelta = new Vector2(50, 50);
                Move();
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    switch (cur)
                    {
                        case 0:
                            Select.instance.Game();
                            break;
                        case 1:
                            OnClickStart();
                            break;
                        case 2:
                            OnClickSettings();
                            break;
                        case 3:
                            OnClickExit();
                            break;
                    }
                }
                if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    cur++;
                    if (cur > max) cur = max;
                }
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    cur--;
                    if (cur < min) cur = min;
                }
            }


        }
        if (Input.anyKey)
        {
            if (!v)
            {
                v = true;
            }
            OnMouse.SetActive(true);
        }
    }
    public void OnClickStart()
    {
        myStart.SetActive(false);
        mySaveLoad.SetActive(true);
        check = true;
    }
    public void OnClickSettings()
    {
        myStart.SetActive(false);
        OnMouse.SetActive(false);
        GameSetting.SetActive(true);
    }
    public void OnClickExit()
    {

    }
    public void OnClickBack()
    {

        mySaveLoad.SetActive(false);
        myStart.SetActive(true);
        check = false;
    }
    public void Move()
    {
        pos.y = myButtons[cur].transform.localPosition.y;
        OnMouse.transform.localPosition = pos;
    }
    public void SecondMove()
    {
        pos.y = myButtons2[cur].transform.localPosition.y;
        OnMouse.transform.localPosition = pos;
    }
}
