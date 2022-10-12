using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using TMPro;
public class Title : MonoBehaviour
{
    public GameObject myCanvas;
    public GameObject myMainScreen;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            myCanvas.SetActive(false);
            myMainScreen.SetActive(true);
        }
    }
}
