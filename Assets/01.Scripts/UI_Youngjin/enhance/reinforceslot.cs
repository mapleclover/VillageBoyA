using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class reinforceslot : MonoBehaviour, IDropHandler
{
    public GameObject[] myIngSlots;
    public GameObject[] myInven;
    public GameObject alert; // 재료가 없다면 무슨 재료가 필요한지 알림
    public TMPro.TMP_Text myMessage;
    public EnhanceableItems myItem;



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
        int level = 1;//나중에 아이템 레벨과 연동
        switch (thisitem.GetComponent<Pickup>().item.itemName)    //금반지: 다이아&철, 장갑: 철&별, 방패: 사과&별
        {                                                         //나중에 개수도 체크해야됨
            case "금반지":
                if (DataController.instance.gameData.savedInventory.ContainsKey("다이아몬드") && DataController.instance.gameData.savedInventory.ContainsKey("철"))
                {
                    CompareRequirements(thisitem,"다이아몬드","철",level);
                }
                else
                {
                    myMessage.text = $"다이아몬드 {level}개, 철 {level}개";
                    alert.SetActive(true);
                    FindMySlot(thisitem.gameObject);
                }
                break;  
            case "장갑":
                if (DataController.instance.gameData.savedInventory.ContainsKey("철") && DataController.instance.gameData.savedInventory.ContainsKey("별"))
                {
                    CompareRequirements(thisitem, "철", "별", level);
                }
                else
                {
                    myMessage.text = $"철  {level}개, 별  {level}개";
                    alert.SetActive(true);
                    FindMySlot(thisitem.gameObject);
                }
                break;
            case "방패":
                if (DataController.instance.gameData.savedInventory.ContainsKey("사과") && DataController.instance.gameData.savedInventory.ContainsKey("별"))
                {
                    CompareRequirements(thisitem, "사과", "별", level);
                }
                else
                {
                    myMessage.text = $"사과  {level}개, 별  {level}개";
                    alert.SetActive(true);
                    FindMySlot(thisitem.gameObject);
                }
                break;
        }
    }
    public void CompareRequirements(Transform thisitem,string itemname1,string itemname2, int level)
    {
        if(DataController.instance.gameData.myItemCount[itemname1] <level || DataController.instance.gameData.myItemCount[itemname2] <level)
        {
            myMessage.text = $"{itemname1} {level}개, {itemname2} {level}개";
            alert.SetActive(true);
            FindMySlot(thisitem.gameObject);
            return;
        }
        ShowIngredients(itemname1, 0);
        ShowIngredients(itemname2, 1);
      /*  if (DataController.instance.gameData.myItemCount[itemname1].Equals(level))
        {
            ShowIngredients(itemname1, 0);
        }
        else
        {
            ReduceIngredients(itemname1, 0,level);
        }
        if (DataController.instance.gameData.myItemCount[itemname2].Equals(level) )
        {
            ShowIngredients(itemname2, 1);
        }
        else
        {
            ReduceIngredients(itemname2, 1,level);
        }

        */
    }
    public void ReduceIngredients(string name, int index,int level)
    {
        for (int i = 0; i < 14; i++)
        {
            if (myInven[i].transform.childCount > 0)
            {
                GameObject thisIngred = myInven[i].transform.GetChild(0).gameObject;
                if (thisIngred != null && name != null)
                {
                    if (!thisIngred.GetComponent<Pickup>().item.itemName.Equals(name)) continue;
                    else
                    {
                        thisIngred.transform.SetParent(myIngSlots[index].transform);
                        thisIngred.transform.localPosition = Vector3.zero;
                        DataController.instance.gameData.savedInventory.Remove(name);
                        DataController.instance.gameData.myItemCount[name] -= level;        //아이템 레벨과 연동해 빼야됨
                        if (DataController.instance.gameData.myItemCount[name] <= 0)
                        {
                            DataController.instance.gameData.myItemCount.Remove(name);
                        }

                        break;
                    }
                }
            }
        }
    }
    public void ShowIngredients(string name,int index)
    {
        int level = 1;
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
                        DataController.instance.gameData.myItemCount[name] -= level;        //아이템 레벨과 연동해 빼야됨
                        if (DataController.instance.gameData.myItemCount[name] <= 0)
                        {
                            DataController.instance.gameData.myItemCount.Remove(name);
                        }

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
        Debug.Log(this.transform.GetChild(0).gameObject);
        EnchantLogic(this.transform.GetChild(0).gameObject);
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
        int level = 1;      //레벨과 연동 // 영진아 이거 레벨 뭐야?
        for (int i = 0; i < myInven.Length; i++)
        {
            if (myInven[i].transform.childCount == 0)
            {
                obj.transform.SetParent(myInven[i].transform);
                obj.transform.localPosition = Vector3.zero;
                DataController.instance.gameData.savedInventory[obj.GetComponent<Pickup>().item.itemName]=i;
              DataController.instance.gameData.myItemCount[obj.GetComponent<Pickup>().item.itemName]=level;
                break;
            }
        }
    }

    public void EnchantLogic(GameObject obj)
    {
        myItem = obj.GetComponent<EnhanceableItems>(); // 드래그 되자 마자 바인딩 되도록 바꿔야함
        if (myItem._GetMaxLevel)
        {

            if (myItem._EnhanceableItem)
            // 강화 가능 아이템인지
            {
                    myItem.ConsumeGold();
                    //강화 시도 마다 골드 제거
                    InventoryController.Instance.ShowMyGold();
                    // 인벤토리에 바로 적용

                    if (myItem.CheckSuccess()) // 성공
                    {
                        myItem.GetComponent<EnhanceableItems>().myData.Level++; 
                        DataController.instance.gameData.gloves.Level++;
                        print($"레벨{DataController.instance.gameData.gloves.Level}");
                        /*if (obj.GetComponent<Pickup>().item.itemName.Equals("장갑"))
                        {
                            
                            
                        }*/
                        Debug.Log("성공");

                        Debug.Log(DataController.instance.gameData.gold);
                        Debug.Log($"가격{myItem._EnchantCost}");
                        Debug.Log($"공격력{myItem._AP}");
                        Debug.Log($"확률{myItem._Possibility}");
                    }
                    else // 실패
                    {
                        Debug.Log("실패");
                        Debug.Log(DataController.instance.gameData.gold);
                        Debug.Log($"가격{myItem._EnchantCost}");
                        Debug.Log($"공격력{myItem._AP}");
                        Debug.Log($"확률{myItem._Possibility}");

                    }
            }
        }


    }

}

//취소 버튼 누르면 인벤토리 제자리로
//강화 버튼 누르면 재료는 인벤토리에서 사라짐

// 코루틴으로 쿨타임