using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IconManager : MonoBehaviour
{
    public IconManager myIconManager;
    public void Initialize(Transform target, Color color)
    {
        StartCoroutine(Following(target, color));
    }
    public IEnumerator Following(Transform target, Color color)
    {
        Vector2 size = transform.parent.GetComponent<RectTransform>().sizeDelta;
        RectTransform rt = GetComponent<RectTransform>();
        Debug.Log(Camera.allCameras[1].name);
        while (target != null)
        {
            
            Vector3 pos = Camera.allCameras[1].WorldToViewportPoint(target.position);
            rt.anchoredPosition = pos * size;
            yield return null;
        }
    }
}
