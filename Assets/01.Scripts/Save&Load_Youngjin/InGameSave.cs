using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using TMPro;

public class InGameSave : MonoBehaviour
{
    public GameObject mySC;
    public GameObject myLoad;
    public GameObject mySave;
    public GameObject myESC;
    public static int mySn;
    bool v = true;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (myLoad.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                myLoad.SetActive(false);
                myESC.SetActive(true);
                v = true;
            }
        }
        else if (mySave.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                mySave.SetActive(false);
                myESC.SetActive(true);
                v = true;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                myESC.SetActive(v);
                if (v) v = false;
                else v = true;

            }
        }

    }
    public void OnClickSave()
    {
        mySave.SetActive(true);
        myESC.SetActive(false);
        //mySC.SetActive(true);

    }
    public void OnClickLoad()
    {
        myLoad.SetActive(true);
        myESC.SetActive(false);
    }
    public void OnClickOK()
    {
        mySC.SetActive(false);
        myESC.SetActive(true);
    }
    public void OnClickExit()
    {
        Application.Quit(); 
    }
    public void OnClickBack()
    {

        myLoad.SetActive(false);
        myESC.SetActive(true);

    }
    public void OnClickSaveButton()
    {
        
        mySC.SetActive(true);
    }
    public void OnClickSaveOK()
    {
        
        DataController.instance.SaveGameDataByESC(mySn);
        Select.instance.ShowUI();
        mySC.SetActive(false);
    }
    public void OnClickSaveBack()
    {
        mySave.SetActive(false);
        myESC.SetActive(true);
    }
    public void OnClickSaveNO()
    {
        mySC.SetActive(false);
    }

}
