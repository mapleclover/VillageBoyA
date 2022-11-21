//작성자 : 이영진
//설명 : 
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UsingItemtoParty : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler,IPointerClickHandler
{
    Vector2 pos = Vector2.zero;
    public GameObject myArrow;
    public GameObject myPanel;
    public TMP_Text myText;


    GameObject obj;
    public void OnPointerEnter(PointerEventData eventData)
    {
        pos.x = this.transform.localPosition.x;
        pos.y=this.transform.localPosition.y+100;
        myArrow.transform.localPosition=pos;    
        myArrow.SetActive(true);

    }
    public void OnPointerExit(PointerEventData eventData)
    {
        myArrow.SetActive(false);
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        Transform itemTransform = PointerInfo.instance.transform;
        if (itemTransform.gameObject.layer == 8)          //소모품을 캐릭터에게 사용
        {
            GameObject temp = PointerInfo.instance.transform.gameObject;
            int heal= temp.GetComponent<Pickup>().item.value;
            string name = temp.GetComponent<Pickup>().item.itemName;
            if (gameObject.CompareTag("Kong"))
            {
                DataController.instance.gameData.Kong.HP += heal;
            }
            else if (gameObject.CompareTag("Jin"))
            {
                DataController.instance.gameData.Jin.HP += heal;             //누가 사용하는지에 따라 회복됨
            }
            else if (gameObject.CompareTag("Ember"))
            {
                DataController.instance.gameData.Ember.HP += heal;
            }

            myText.text = $"{this.transform.name} has gained +{heal}hp!";      //회복됐다는 알림
            StartCoroutine(UsedPotion());
            if ( DataController.instance.gameData.myItemCount[name] >1)
            {
                   //포션이 2개 이상이면 destroy 대신 -1
                DataController.instance.gameData.myItemCount[name]--;
                InventoryController.Instance.ShowNumbertoUI();
            }
            else
            {
                DataController.instance.gameData.savedInventory.Remove(name);
                Destroy(temp); //1개일 경우 destroy
            }
           

        }

        else
        {
            if (itemTransform.gameObject.layer == 7)       //장비를 캐릭터에게 장착
            {
                obj = Instantiate(this.gameObject);
                obj.GetComponent<RectTransform>().sizeDelta = new Vector2(25, 25);
                pos.x = itemTransform.localPosition.x + 20;
                pos.y = itemTransform.localPosition.y - 20;

                obj.transform.SetParent(PointerInfo.instance.transform);
                GameObject thisEquipment = PointerInfo.instance.transform.gameObject ;
                string thisname = thisEquipment.GetComponent<Pickup>().item.itemName;
                int val = thisEquipment.GetComponent<Pickup>().item.value;
                Item.ItemType myType= thisEquipment.GetComponent<Pickup>().item.itemType;

                if (PointerInfo.instance.transform.childCount > 2)      //만약 이 장비가 이전에 장착된 적이 있으면
                {
                    if (DataController.instance.gameData.Kong.myUsedItems.Contains(thisname))     // 콩이 장착했었으면 제거
                    {
                        DataController.instance.gameData.Kong.myUsedItems.Remove(thisname);

                        if (myType == Item.ItemType.Weapon)
                        {
                            DataController.instance.gameData.Kong.strength -= val;
                        }
                        else if (myType == Item.ItemType.Armor)                  //더해진 스탯을 원래대로 되돌림
                        {
                            DataController.instance.gameData.Kong.defPower -= val;
                        }                                                                                                                               

                    }
                    else if (DataController.instance.gameData.Jin.myUsedItems.Contains(thisname))        //진이 장착했으면
                    {
                        DataController.instance.gameData.Jin.myUsedItems.Remove(thisname);
                        if (myType == Item.ItemType.Weapon)
                        {
                            DataController.instance.gameData.Jin.strength -= val;
                        }
                        else if (myType == Item.ItemType.Armor)
                        {
                            DataController.instance.gameData.Jin.defPower -= val;
                        }
                    }
                    else if (DataController.instance.gameData.Ember.myUsedItems.Contains(thisname))        //앰버가 장착했으면
                    {
                        DataController.instance.gameData.Ember.myUsedItems.Remove(thisname);
                        if (myType == Item.ItemType.Weapon)
                        {
                            DataController.instance.gameData.Ember.strength -= val;
                        }
                        else if (myType == Item.ItemType.Armor)
                        {
                            DataController.instance.gameData.Ember.defPower -= val;
                        }
                    }
                    Destroy(itemTransform.GetChild(1).gameObject);             //전에 장비 위에 표시됐던 초상화 UI를 제거
                }
                obj.transform.localPosition = pos;                          //UI로 누가 어떤 아이템 갖고 있는지 표시
                obj.GetComponent<RawImage>().raycastTarget = false;
                //게임데이터에 적용할 때 여기서
                GameObject clickedEquip = itemTransform.gameObject;
                int Val = clickedEquip.GetComponent<Pickup>().item.value;
                Item.ItemType thisType = clickedEquip.GetComponent<Pickup>().item.itemType;
              
                if (gameObject.CompareTag("Kong"))
                {
                    DataController.instance.gameData.Kong.myUsedItems.Add(thisname);               //장비를 장착한 멤버에 따라 스탯에 적용됨
                    if (thisType == Item.ItemType.Weapon)
                    {
                        DataController.instance.gameData.Kong.strength += Val;
                    }
                    else if (thisType == Item.ItemType.Armor)
                    {
                        DataController.instance.gameData.Kong.defPower += Val;
                    }
                }
                else if (gameObject.CompareTag("Jin"))
                {
                    DataController.instance.gameData.Jin.myUsedItems.Add(thisname);
                    if (thisType == Item.ItemType.Weapon)
                    {
                        DataController.instance.gameData.Jin.strength += Val;
                    }
                    else if (thisType == Item.ItemType.Armor)
                    {
                        DataController.instance.gameData.Jin.defPower += Val;
                    }
                }
                else if (gameObject.CompareTag("Ember"))
                {
                    DataController.instance.gameData.Ember.myUsedItems.Add(thisname);
                    if (thisType == Item.ItemType.Weapon)
                    {
                        DataController.instance.gameData.Ember.strength += Val;
                    }
                    else if (thisType == Item.ItemType.Armor)
                    {
                        DataController.instance.gameData.Ember.defPower += Val;
                    }
                }
            }

            myPanel.SetActive(false);
        }
    }
    IEnumerator UsedPotion()
    {
        myText.gameObject.SetActive(true);
        yield return new WaitForSeconds(1.0f);          //포션 사용됐다는 알림
        myText.gameObject.SetActive(false);
        myPanel.SetActive(false);
    }
  

}
