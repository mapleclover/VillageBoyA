using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class textblink : MonoBehaviour
{
    public TMPro.TMP_Text gameTitle;
   public TMPro.TMP_Text flashingText;
    public GameObject secondUI;
    void Start()
    {
       // flashingText = GetComponent<TMPro.TMP_Text>();
        StartCoroutine(BlinkText());
    }
    void Update()
    {
        if (Input.anyKeyDown)
        {
            StopAllCoroutines();
            gameTitle.text = "";
            flashingText.text = "";
            secondUI.SetActive(true);
        }
    }
    IEnumerator BlinkText()
    {
        while (true)
        {
            flashingText.text = "";
            yield return new WaitForSeconds(1f);
            flashingText.text = "Press Any Key";
            yield return new WaitForSeconds(1f);
        }
    }
}
