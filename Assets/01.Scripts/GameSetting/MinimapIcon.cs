using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinimapIcon : MonoBehaviour
{
  //  public GameObject directionIndicator = null;
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
        
        switch (myIcon)
        {
            case IconType.player:
                this.GetComponent<Image>().sprite = IconImages[0];
                //directionIndicator.SetActive(true);
                GetComponent<Image>().color = Color.green;
                //my_Icon.GetComponent<Image>().color = Color.green;
                //StartCoroutine(IconManager.Following(transform, Color.green));
                break;
            case IconType.npc:
                this.GetComponent<Image>().sprite = IconImages[1];
               // directionIndicator.SetActive(false);
                GetComponent<Image>().color = Color.yellow;
                //my_Icon.GetComponent<Image>().color = Color.yellow;
                //StartCoroutine(IconManager.Following(transform, Color.yellow));
                break;
            case IconType.quest:
                this.GetComponent<Image>().sprite = IconImages[2];
               // directionIndicator.SetActive(false);
                GetComponent<Image>().color = Color.yellow;
                //my_Icon.GetComponent<Image>().color = Color.yellow;
                //StartCoroutine(IconManager.Following(transform, Color.yellow));
                break;
            case IconType.enemy:
                this.GetComponent<Image>().sprite = IconImages[0];
               // directionIndicator.SetActive(false);
                GetComponent<Image>().color = Color.red;
                //my_Icon.GetComponent<Image>().color = Color.yellow;
                //StartCoroutine(IconManager.Following(transform, Color.yellow));
                break;
        }
    }

    public void Initialize()
    {
        switch (myIcon)
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
    
    

    void Start()
    {
        ChangeState(IconType.player);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            ChangeState(IconType.npc);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            ChangeState(IconType.quest);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            ChangeState(IconType.player);
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            ChangeState(IconType.enemy);
        }

        
    }
    
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