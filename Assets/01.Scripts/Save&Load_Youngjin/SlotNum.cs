using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class SlotNum : MonoBehaviour,IPointerClickHandler
{
    public int mySlotnum;
    public static SlotNum instance = null;
    public void OnPointerClick(PointerEventData evnetData)
    {
        InGameSave.mySn = mySlotnum;
    }
}
