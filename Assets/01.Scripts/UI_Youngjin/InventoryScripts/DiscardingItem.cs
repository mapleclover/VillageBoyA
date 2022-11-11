using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class DiscardingItem : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler,IDropHandler
{
    public GameObject myText;
    DiscardingItem trashcan;
    public GameObject myPanel;
    Vector2 originalPosition;
    PointerInfo icon;
    Transform originalParent;
    public GameObject isQuestItem;
    public void OnDrop(PointerEventData eventData)
    {
        icon = transform.GetComponentInChildren<PointerInfo>();

        if (icon != null)
            {        
                originalParent = eventData.pointerDrag.transform.parent;
                trashcan = eventData.pointerDrag.transform.parent.GetComponent<DiscardingItem>();
                trashcan.SetChildren(icon.transform);            
            }
            originalParent = eventData.pointerDrag.transform.parent;
            eventData.pointerDrag.transform.SetParent(transform);
            eventData.pointerDrag.transform.localPosition = Vector3.zero;

        if (eventData.pointerDrag.transform.gameObject.layer == 9)
        {
            isQuestItem.SetActive(true);

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
    public void OnClickYesDiscard()
    {
        GameObject selectedItem = this.transform.GetChild(0).gameObject;

        if (DataController.instance.gameData.Kong.myUsedItems.Contains(selectedItem))
        {
            DataController.instance.gameData.Kong.myUsedItems.Remove(selectedItem);
        }
        else if (DataController.instance.gameData.Jin.myUsedItems.Contains(selectedItem))
        {
            DataController.instance.gameData.Jin.myUsedItems.Remove(selectedItem);
        }
        else if (DataController.instance.gameData.Ember.myUsedItems.Contains(selectedItem))
        {
            DataController.instance.gameData.Ember.myUsedItems.Remove(selectedItem);
        }

        DataController.instance.gameData.savedInventory.Remove(selectedItem);
        Destroy(selectedItem);
        
        myPanel.SetActive(false);
    }
    public void OnClickNoDiscard()
    {
       Transform temp = this.transform.GetChild(0);
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
