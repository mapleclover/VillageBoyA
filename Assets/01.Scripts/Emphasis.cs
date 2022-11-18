//작성자 : 이영진
//설명 : 
using UnityEngine;
using UnityEngine.EventSystems;

public class Emphasis : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private float yPos;

    public GameObject myEmphasis;

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