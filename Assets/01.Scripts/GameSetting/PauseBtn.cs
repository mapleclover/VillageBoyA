using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PauseBtn : MonoBehaviour
{

    bool pauseActive = false;



    public void runButton()
    {

        Time.timeScale = 1;
        pauseActive = false;
    }
    /*
    public void pauseButton()
    {
        Time.timeScale = 0;
        pauseActive = true;
    }*/

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
