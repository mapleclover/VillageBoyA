using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameSave : MonoBehaviour
{
    public GameObject mySC;
    public GameObject mySaveLoad;
    public GameObject myESC;
    bool v = true;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (mySaveLoad.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                mySaveLoad.SetActive(false);
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
        DataController.instance.SaveGameData();
        mySC.SetActive(true);
        
    }
    public void OnClickLoad()
    {
        mySaveLoad.SetActive(true);
        myESC.SetActive(false);
    }
    public void OnClickOK()
    {
        mySC.SetActive(false);
        myESC.SetActive(true);
    }
    public void OnClickExit()
    {

    }
    public void OnClickBack()
    {

        mySaveLoad.SetActive(false);
        myESC.SetActive(true);
      
    }
}
