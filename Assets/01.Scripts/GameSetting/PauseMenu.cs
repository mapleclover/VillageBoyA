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
        paused = false;
        PauseUI.SetActive(false);
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseUI.gameObject.SetActive(!PauseUI.gameObject.activeSelf);

            if (PauseUI.gameObject.activeSelf)
            {
                paused = true;
            }
            else
            {
                paused = false;
            }

            //PauseUI.gameObject.SetActive(true);
            // PauseUI.SetActive(true);
            //Time.timeScale = 0f;

        }
        if(paused)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
        /*if (Time.timeScale == 0f && Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 1f;
            print("3");
        }*/


        /*if (paused)
        {
            PauseUI.SetActive(true);
            Time.timeScale = 0;
        }

        else
        {
            PauseUI.SetActive(false);
            Time.timeScale = 1;
        }*/
        /*  
          if (!paused)
          {
              PauseUI.SetActive(false);
              Time.timeScale = 1f;
          }*/
    }
}