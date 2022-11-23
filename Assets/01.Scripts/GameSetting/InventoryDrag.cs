using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryDrag : MonoBehaviour, IDragHandler, IBeginDragHandler
{
    public Transform myInventory;
    Vector2 dragOffset = Vector2.zero;
    Vector2 halfSize = Vector2.zero;

    public void OnBeginDrag(PointerEventData eventData)
    {
        dragOffset = (Vector2)myInventory.position - eventData.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 pos = dragOffset + eventData.position;
        pos.x = Mathf.Clamp(pos.x, halfSize.x, Screen.width - halfSize.x);
        pos.y = Mathf.Clamp(pos.y, halfSize.y, Screen.height - halfSize.y);
        myInventory.position = pos;
    }
    // Start is called before the first frame update
    void Start()
    {
        halfSize = myInventory.GetComponent<RectTransform>().sizeDelta * 0.5f * SceneData.Inst.canVas.scaleFactor;
    }

    // Update is called once per frame
    void Update()
    {
      
    }
}
