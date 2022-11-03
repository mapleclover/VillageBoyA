using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestIcon : MonoBehaviour
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