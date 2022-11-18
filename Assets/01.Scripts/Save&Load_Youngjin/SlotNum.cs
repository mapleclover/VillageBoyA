//작성자 : 이영진
//설명 :
using UnityEngine;
using UnityEngine.EventSystems;

public class SlotNum : MonoBehaviour, IPointerClickHandler
{
    public int mySlotnum;
    public static SlotNum instance = null;

    public void OnPointerClick(PointerEventData evnetData)
    {
        InGameSave.mySn = mySlotnum;
    }
}