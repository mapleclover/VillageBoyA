using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class text : MonoBehaviour
{
    public GameObject myText;
    void Start()
    {
       // myText = GetComponent<TMPro.TMP_Text>();
        StartCoroutine(BlinkText());

    }

    public IEnumerator BlinkText()
    {
        while (true)
        {
            myText.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            myText.SetActive(false);
            yield return new WaitForSeconds(0.5f);
        }
    }
}
