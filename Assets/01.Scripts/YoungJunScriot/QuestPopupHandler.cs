using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
// 박영준 ,, 퀘스트팝업창 타이틀부분 드래그시 이동.
public class QuestPopupHandler : MonoBehaviour, IBeginDragHandler, IDragHandler
{
    private RectTransform _parentRect;

    private Vector2 _parentStartPos; // 드래그시작시 panel 중심점 위치
    private Vector2 _startPos; // 드래시작시 마우스포인터 위치
    private Vector2 _moveOffset; 

    private void Awake()
    {
        _parentRect = transform.parent.GetComponent<RectTransform>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _parentStartPos = _parentRect.position;
        _startPos = eventData.position;
        Debug.Log(_startPos);
    }


    public void OnDrag(PointerEventData eventData)
    {
        _moveOffset = eventData.position - _startPos;
        _parentRect.position = _parentStartPos + _moveOffset;
    }

}
