//작성자 : 이영진
//설명 : 

using UnityEngine;
using UnityEngine.EventSystems;

public class DiscardingItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IDropHandler
{
    public GameObject myText;
    DiscardingItem trashcan;
    public GameObject myPanel;
    Vector2 originalPosition;
    PointerInfo icon;
    Transform originalParent;
    public GameObject isQuestItem;

    public void OnDrop(PointerEventData eventData)              //쓰레기통에 드랍
    {
        icon = transform.GetComponentInChildren<PointerInfo>();
        Transform movingItem = eventData.pointerDrag.transform;
        if (icon != null)
        {
            originalParent = movingItem.parent;
            trashcan = movingItem.parent.GetComponent<DiscardingItem>();
            trashcan.SetChildren(icon.transform);
        }
        originalParent = movingItem.parent;
        movingItem.SetParent(transform);
        movingItem.localPosition = Vector3.zero;

        if (movingItem.gameObject.layer.Equals(9))
        {
            isQuestItem.SetActive(true);        //퀘스트 아이템은 못버림
        }
        else
        {
            myPanel.SetActive(true);    
        }
    }

    public void SetChildren(Transform child)
    {
        child.SetParent(transform);
        child.localPosition = Vector3.zero;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        myText.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        myText.SetActive(false);
    }

    public void OnClickYesDiscard()                 //확인창에서 확인을 눌렀을 경우
    {
        GameObject selectedItem = this.transform.GetChild(0).gameObject;
        string thisname = selectedItem.GetComponent<Pickup>().item.itemName;
        if (DataController.instance.gameData.Kong.myUsedItems.Contains(thisname))       //그 아이템을 장착하고 있던 캐릭터가 있을 경우
        {
            DataController.instance.gameData.Kong.myUsedItems.Remove(thisname);
            DataController.instance.gameData.Kong.strength = 0;
            DataController.instance.gameData.Kong.defPower = 0;
        }
        else if (DataController.instance.gameData.Jin.myUsedItems.Contains(thisname))
        {
            DataController.instance.gameData.Jin.myUsedItems.Remove(thisname);
            DataController.instance.gameData.Jin.strength = 0;
            DataController.instance.gameData.Jin.defPower = 0;
        }
        else if (DataController.instance.gameData.Ember.myUsedItems.Contains(thisname))
        {
            DataController.instance.gameData.Ember.myUsedItems.Remove(thisname);
            DataController.instance.gameData.Ember.strength = 0;
            DataController.instance.gameData.Ember.defPower = 0;
        }

        DataController.instance.gameData.savedInventory.Remove(thisname);

        DataController.instance.gameData.myItemCount.Remove(thisname);  
        Destroy(selectedItem);

        myPanel.SetActive(false);
    }

    public void OnClickNoDiscard()
    {
        Transform temp = this.transform.GetChild(0);            //취소를 눌렀을 경우 원래 슬롯으로 돌아감
        temp.SetParent(originalParent);
        temp.localPosition = Vector3.zero;
        myPanel.SetActive(false);
    }

    public void OnClickQuestItemOK()
    {
        OnClickNoDiscard();
        isQuestItem.SetActive(false);
    }
}