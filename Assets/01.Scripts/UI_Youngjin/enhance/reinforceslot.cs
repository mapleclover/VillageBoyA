using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class reinforceslot : MonoBehaviour, IDropHandler
{
    public GameObject[] myIngSlots;
    public GameObject myInven;
    public void OnDrop(PointerEventData eventData)
    {
        PointerInfo icon = transform.GetComponentInChildren<PointerInfo>();
        Transform movingObject = eventData.pointerDrag.transform;
        if (movingObject.GetComponent<Pickup>().item.enhanceableItem.Equals(Item.EnhanceableItem.Possible))
        {
            if (icon != null)
            {
                reinforceslot slot = movingObject.parent.GetComponent<reinforceslot>();
                slot.SetChild(icon.transform);
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
                break;  
            case "장갑":
                if (DataController.instance.gameData.savedInventory.ContainsKey("철") && DataController.instance.gameData.savedInventory.ContainsKey("별"))
                {
                    ShowIngredients("철", 0);
                    ShowIngredients("별", 1);
                }
                break;
            case "방패":
                if (DataController.instance.gameData.savedInventory.ContainsKey("사과") && DataController.instance.gameData.savedInventory.ContainsKey("별"))
                {
                    ShowIngredients("사과", 0);
                    ShowIngredients("별", 1);
                }
                break;
        }
    }
    public void ShowIngredients(string name,int index)
    {
        for(int i = 0; i < 14; i++)
        {
            if (myInven.transform.GetChild(i).transform.childCount > 0)
            {
                GameObject thisIngred = myInven.transform.GetChild(i).transform.GetChild(0).gameObject;
                if (thisIngred.GetComponent<Pickup>().item.itemName.Equals(name))
                {
                    thisIngred.transform.SetParent(myIngSlots[index].transform);
                    thisIngred.transform.localPosition = Vector3.zero;

                    break;
                }
            }
        }
    }
}

//취소 버튼 누르면 인벤토리 제자리로
//강화 버튼 누르면 재료는 인벤토리에서 사라짐