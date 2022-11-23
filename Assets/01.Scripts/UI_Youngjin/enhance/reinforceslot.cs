using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class reinforceslot : MonoBehaviour, IDropHandler
{
    public GameObject[] myIngSlots;
    public GameObject[] myInven;
    public GameObject alert;
    public TMPro.TMP_Text myMessage;

    public void OnDrop(PointerEventData eventData)
    {
        PointerInfo icon = transform.GetComponentInChildren<PointerInfo>();
        Transform movingObject = eventData.pointerDrag.transform;
        if (movingObject.GetComponent<Pickup>().item.enhanceableItem.Equals(Item.EnhanceableItem.Possible))
        {
            if (icon != null)
            {
                ItemSlot slot = movingObject.parent.GetComponent<ItemSlot>();
                slot.SetChildren(icon.transform);
                OnClickCancel();
            }
            movingObject.SetParent(transform);
            movingObject.localPosition = Vector3.zero;

            CheckIngredients(movingObject);

        }
        else return;
    }

    public void SetChild(Transform child)
    {
        child.SetParent(transform);
        child.localPosition = Vector3.zero;
    }
    public void CheckIngredients(Transform thisitem)
    {
        switch (thisitem.GetComponent<Pickup>().item.itemName)    //금반지: 다이아&철, 장갑: 철&별, 방패: 사과&별
        {                                                         //나중에 개수도 체크해야됨
            case "금반지":
                if (DataController.instance.gameData.savedInventory.ContainsKey("다이아몬드") && DataController.instance.gameData.savedInventory.ContainsKey("철"))
                {
                    ShowIngredients("다이아몬드", 0);
                    ShowIngredients("철", 1);
                }
                else
                {
                    myMessage.text = $"다이아몬드, 철";
                    alert.SetActive(true);
                    FindMySlot(thisitem.gameObject);
                }
                break;  
            case "장갑":
                if (DataController.instance.gameData.savedInventory.ContainsKey("철") && DataController.instance.gameData.savedInventory.ContainsKey("별"))
                {
                    ShowIngredients("철", 0);
                    ShowIngredients("별", 1);
                }
                else
                {
                    myMessage.text = $"철, 별";
                    alert.SetActive(true);
                    FindMySlot(thisitem.gameObject);
                }
                break;
            case "방패":
                if (DataController.instance.gameData.savedInventory.ContainsKey("사과") && DataController.instance.gameData.savedInventory.ContainsKey("별"))
                {
                    ShowIngredients("사과", 0);
                    ShowIngredients("별", 1);
                }
                else
                {
                    myMessage.text = $"사과, 별";
                    alert.SetActive(true);
                    FindMySlot(thisitem.gameObject);
                }
                break;
        }
    }
    public void ShowIngredients(string name,int index)
    {
        for(int i = 0; i < 14; i++)
        {
            if (myInven[i].transform.childCount > 0)
            {
                GameObject thisIngred = myInven[i].transform.GetChild(0).gameObject;
                if (thisIngred != null&&name!=null)
                {
                    if (!thisIngred.GetComponent<Pickup>().item.itemName.Equals(name)) continue;
                    else
                    {
                        thisIngred.transform.SetParent(myIngSlots[index].transform);
                        thisIngred.transform.localPosition = Vector3.zero;
                        DataController.instance.gameData.savedInventory.Remove(name);
                        //  DataController.instance.gameData.myItemCount.Remove(name);    강화 개수만큼 빼야됨
                        break;
                    }
                }
            }
        }
    }
    public void OnClickCancel()
    {
        if (transform.childCount > 0)
        {
            GameObject obj = transform.GetChild(0).gameObject;
            FindMySlot(obj);
        }
        if (myIngSlots[0].transform.childCount > 0) 
        {
            GameObject ing1 = myIngSlots[0].transform.GetChild(0).gameObject;
            FindMySlot(ing1);
        }
        if (myIngSlots[1].transform.childCount > 0)
        {
            GameObject ing2 = myIngSlots[1].transform.GetChild(0).gameObject;
            FindMySlot(ing2);
        }

    }
    public void OnClickEnhance()                //데이터 연결해야됨, 이펙트 추가?
    {
        if (myIngSlots[0].transform.childCount > 0)
        {
           Destroy(myIngSlots[0].transform.GetChild(0).gameObject);
        }
        if (myIngSlots[1].transform.childCount > 0)
        {
            Destroy(myIngSlots[1].transform.GetChild(0).gameObject);
        }
    }
    public void FindMySlot(GameObject obj)
    {
        for (int i = 0; i < 14; i++)
        {
            if (myInven[i].transform.childCount == 0)
            {
                obj.transform.SetParent(myInven[i].transform);
                obj.transform.localPosition = Vector3.zero;
                DataController.instance.gameData.savedInventory[obj.GetComponent<Pickup>().item.itemName]=i;
              //  DataController.instance.gameData.myItemCount[];
                break;
            }
        }
    }

}

//취소 버튼 누르면 인벤토리 제자리로
//강화 버튼 누르면 재료는 인벤토리에서 사라짐