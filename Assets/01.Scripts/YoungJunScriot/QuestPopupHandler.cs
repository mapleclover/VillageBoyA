using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
// �ڿ��� ,, ����Ʈ�˾�â Ÿ��Ʋ�κ� �巡�׽� �̵�.
public class QuestPopupHandler : MonoBehaviour, IBeginDragHandler, IDragHandler
{
    private RectTransform _parentRect;

    private Vector2 _parentStartPos; // �巡�׽��۽� panel �߽��� ��ġ
    private Vector2 _startPos; // �巡���۽� ���콺������ ��ġ
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
