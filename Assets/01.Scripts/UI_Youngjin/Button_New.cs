using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class Button_New : MonoBehaviour
{
    public static Button_New Instance = null;
    public static int cur = 0;
    public GameObject OnMouse;
    public GameObject[] myButtons;
    public GameObject mySaveLoad;
    public GameObject myStart;
    public GameObject myCreate;

    bool check = false;
    bool v = false;
    int min = 0, max = 2;
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
            Move();
            if (Input.GetKeyDown(KeyCode.Return))
            {
                switch (cur)
                {
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
        if (mySaveLoad.activeSelf)
        {
            OnMouse.transform.localScale = new Vector3(1.6f, 0.9975f, 0.9975f);
            OnMouse.transform.GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(30, 50);
            OnMouse.transform.GetChild(1).GetComponent<RectTransform>().sizeDelta = new Vector2(30, 50);
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (check && !myCreate.activeSelf)
                {
                    OnClickBack();
                    check = false;
                }
            }

        }
        else
        {
            OnMouse.transform.localScale = new Vector3(1.1f, 0.9975f, 0.9975f);
            OnMouse.transform.GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(50, 50);
            OnMouse.transform.GetChild(1).GetComponent<RectTransform>().sizeDelta = new Vector2(50, 50);
            //x scale:0.9975
            //width 50
            //-225
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
}
