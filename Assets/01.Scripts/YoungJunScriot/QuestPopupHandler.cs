//작성자 : 박영준
//설명 : 퀘스트팝업창 타이틀부분 드래그시 이동

using UnityEngine;
using UnityEngine.EventSystems;

public class QuestPopupHandler : MonoBehaviour, IBeginDragHandler, IDragHandler
{
    private RectTransform _parentRect; // 팝업창 전체의 rect값

    private Vector2 _moveOffset; // 드래그시작직전 마우스포인터와 rect사이거리를 넣을변수.

    private Vector2 halfSize = Vector2.zero; //화면밖으로나가게하지않게하기위한변수.

    private void Awake()
    {
        _parentRect = transform.parent.GetComponent<RectTransform>();
        halfSize = _parentRect.sizeDelta * 0.5f;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _moveOffset = (Vector2)_parentRect.position - eventData.position;
    }
    
    public void OnDrag(PointerEventData eventData)
    {
        Vector2 pos = eventData.position + _moveOffset;
        pos.x = Mathf.Clamp(pos.x, halfSize.x, Screen.width - halfSize.x);
        pos.y = Mathf.Clamp(pos.y, halfSize.y, Screen.height - halfSize.y);
        _parentRect.position = pos;
    }
}