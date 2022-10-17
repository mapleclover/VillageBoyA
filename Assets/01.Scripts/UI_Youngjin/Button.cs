using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;
using System.IO;
using System.Linq;

public class Button : MonoBehaviour
{ 
    public GameObject myStart;
    public GameObject mySaveLoad;
    public GameObject myKeyPressed;
    public GameObject OnMouse;
    public GameObject OnMouse2;
    public GameObject myCreate;

    int min = 0;
    int max = 2;
    public static int cur = 0;
    int originalCur=0;
    bool check = false;
    Vector2 pos = new Vector2(0, 153);
    Vector2 pos2 = new Vector2(0, 153);
    void Start()
    {


    }
    void Update()
    {
        if (check)
        {
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                if (cur < min)
                {
                    cur = min;
                }

                cur++;
                if (cur > max)
                {
                    cur = max;
                }
                pos.y -= 153;
                if (pos.y < -153) pos.y = -153;
                OnMouse2.transform.localPosition = pos;
            }
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                cur--;
                if (cur < min)
                {
                    cur = min;
                }
                pos.y += 153;
                if (pos.y > 153) pos.y = 153;
                OnMouse2.transform.localPosition = pos;
            }
           
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (mySaveLoad.activeSelf&&!myCreate.activeSelf)
                {
                    OnClickBack();
                    check = false;
                }
               
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                if (originalCur < min)
                {
                    originalCur = min;
                }

                originalCur++;
                if (originalCur > max)
                {
                    originalCur = max;
                }
                pos2.y -= 153;
                if (pos.y < -153) pos2.y = -153;
                OnMouse.transform.localPosition = pos2;
            }
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                originalCur--;
                if (originalCur < min)
                {
                    originalCur = min;
                }
                pos2.y += 153;
                if (pos2.y > 153) pos.y = 153;
                OnMouse.transform.localPosition = pos2;
            }
            if (Input.GetKeyDown(KeyCode.Return))
            {
                if (myKeyPressed.activeSelf && !mySaveLoad.activeSelf)
                {
                    switch (originalCur)
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
            }
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
    }
}
