using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public GameObject optionsMenu;
    public GameObject settingMenu;
    public GameObject Waring;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            optionsMenu.gameObject.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            settingMenu.gameObject.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Waring.gameObject.SetActive(false);
        }
    }
}