//작성자 : 유은호
//설명 : 스타스 텍스트 블링크
using System.Collections;
using UnityEngine;

public class text : MonoBehaviour
{
    public GameObject myText;

    void Start()
    {
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