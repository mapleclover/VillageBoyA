using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinimapIcon : MonoBehaviour
{
    public GameObject directionIndicator = null;
    public Sprite[] IconImages;

    public enum IconType
    {
        player, npc, quest, enemy
    }
    public IconType myIcon = IconType.player;    

    public void ChangeState(IconType s)
    {
        if (myIcon == s) return;
        myIcon = s;
        switch(s)
        {
            case IconType.player:
                break;
            case IconType.npc:
                break;
            case IconType.quest:
                break;
            case IconType.enemy:
                break;
        }
    }

    public void Initialize(Transform target)
    {
        switch(myIcon)
        {
            case IconType.player:
                this.GetComponent<Image>().sprite = IconImages[0];
                directionIndicator.SetActive(true);
                StartCoroutine(Following(target, Color.green));
                break;
            case IconType.npc:
                directionIndicator.SetActive(false);
                StartCoroutine(Following(target, Color.yellow));
                break;
            case IconType.quest:
                directionIndicator.SetActive(false);
                StartCoroutine(Following(target, Color.yellow));
                break;
            case IconType.enemy:
                directionIndicator.SetActive(false);
                StartCoroutine(Following(target, Color.red));
                break;
        }
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