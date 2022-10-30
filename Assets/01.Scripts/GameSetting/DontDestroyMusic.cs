using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextSpeedControl : MonoBehaviour
{
    public Slider speedController;

    private void Awake()
    {
        if (PlayerPrefs.HasKey("TextSpeed"))
        {
            speedController.value = PlayerPrefs.GetInt("TextSpeed");
        }
        else
        {
            PlayerPrefs.SetInt("TextSpeed", 7);
            speedController.value = PlayerPrefs.GetInt("TextSpeed");
        }
    }
    public void changeTextSpeed()
    {
        PlayerPrefs.SetInt("TextSpeed", (int)speedController.value);
    }
}
