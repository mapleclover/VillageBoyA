using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextSpeedControl : MonoBehaviour
{
    public string exampleText;
    public int textSpeed;
    public Coroutine isSaying;
    public TextMeshProUGUI Text;
    public Slider textSpeedSlider;

    private void Awake()
    {
        if (!PlayerPrefs.HasKey("TextSpeed"))
        {
            PlayerPrefs.SetInt("TextSpeed", 7);
        }
    }

    public void SettingOpen()
    {
        textSpeedSlider.value = PlayerPrefs.GetInt("TextSpeed");
        isSaying = StartCoroutine(ExampleSay());
    }

    public void OnValueChange()
    {
        PlayerPrefs.SetInt("TextSpeed", (int)textSpeedSlider.value);
        if (isSaying != null)
        {
            StopAllCoroutines();
            isSaying = null;
        }
        Text.text = "";
        isSaying = StartCoroutine(ExampleSay());
    }

    IEnumerator ExampleSay()
    {
        textSpeed = PlayerPrefs.GetInt("TextSpeed");
        if (textSpeed == 10)
        {
            while (true)
            {
                Text.text = exampleText;
                yield return new WaitForSeconds(.9f);
                Text.text = "";
                yield return new WaitForSeconds(.1f);
            }
        }
        else
        {
            while (Text.text != exampleText)
            {
                Text.text += exampleText[Text.text.Length];
                if(Text.text == exampleText)
                {
                    yield return new WaitForSeconds(1.0f);
                    Text.text = ""; 
                }
                yield return new WaitForSeconds(0.3f / (textSpeed-5));
            }
        }
    }
}
