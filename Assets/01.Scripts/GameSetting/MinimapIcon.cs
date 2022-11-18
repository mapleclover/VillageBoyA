//작성자 : 이현호
//설명 : 

using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MinimapIcon : MonoBehaviour
{
    public void Initialize(Transform target, Color color)
    {
        StartCoroutine(Following(target, color));
    }

    IEnumerator Following(Transform target, Color color)
    {
        GetComponent<Image>().color = color;
        Vector2 size = transform.parent.GetComponent<RectTransform>().sizeDelta;
        RectTransform rt = GetComponent<RectTransform>();
        while (target != null)
        {
            Vector3 pos = Camera.allCameras[1].WorldToViewportPoint(target.position);
            rt.anchoredPosition = pos * size;
            yield return null;
        }
    }
}