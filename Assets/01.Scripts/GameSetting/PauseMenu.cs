using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 작성자 : 이현호
public class PauseMenu : MonoBehaviour
{
    public GameObject PauseUI;


    private bool paused = false;

    void Start()
    {
        PauseUI.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseUI.SetActive(true);
            Time.timeScale = 0;
            //paused = !paused;
        }
        /*
            if (paused)
            {
                PauseUI.SetActive(true);
                Time.timeScale = 0;
            }
            else
            {
                PauseUI.SetActive(false);
                Time.timeScale = 1;
            }

            if (!paused)
            {
                PauseUI.SetActive(false);
                Time.timeScale = 1f;
            }*/
    }
}