using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class Emphasis : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    private float yPos;
    public GameObject myEmphasis;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        yPos = this.gameObject.transform.localPosition.y;
        myEmphasis.SetActive(true);
        Vector2 newPostion = new Vector2(0.0f, yPos);
        myEmphasis.transform.localPosition = newPostion;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        myEmphasis.SetActive(false);
    }
}
