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
    string check = "";

    GameObject obj;
    public void OnPointerEnter(PointerEventData eventData)
    {
        pos.x = this.transform.localPosition.x;
        pos.y=this.transform.localPosition.y+80;
        myArrow.transform.localPosition=pos;    
        myArrow.SetActive(true);

    }
    public void OnPointerExit(PointerEventData eventData)
    {
        myArrow.SetActive(false);
    }
    public void OnPointerClick(PointerEventData eventData)          //아이템을 누른 후 어떤 캐릭터에게 적용 시킬 것인지
    {
        Transform itemTransform = PointerInfo.instance.transform;
        if (itemTransform.gameObject.layer == 8)          //소모품을 캐릭터에게 사용
        {
            GameObject temp = PointerInfo.instance.transform.gameObject;
            int heal= temp.GetComponent<Pickup>().item.value;
            string name = temp.GetComponent<Pickup>().item.itemName;
            if (gameObject.CompareTag("Kong"))  //150
            {
                if (DataController.instance.gameData.Kong.HP <= 140)            //최대체력과 비교해 회복되는 양을 계산
                { 
                    DataController.instance.gameData.Kong.HP += heal;
                    myText.text = $"{this.transform.name} has gained +{heal}hp!";      //회복됐다는 알림
                    StartCoroutine(UsedPotion(1.0f));
                    CheckDestroy(name,temp);
                }
                else if (DataController.instance.gameData.Kong.HP < 150 && DataController.instance.gameData.Kong.HP > 140)
                {
                    heal = 150 - DataController.instance.gameData.Kong.HP;
                    DataController.instance.gameData.Kong.HP += heal;
                    myText.text = $"{this.transform.name} has gained +{heal}hp!";
                    StartCoroutine(UsedPotion(1.0f));
                    CheckDestroy(name, temp);
                }
                else
                {
                    myText.text = "이미 최대 체력입니다.";
                    StartCoroutine(UsedPotion(1.0f));           //Kong의 경우
                }
            }
            else if (gameObject.CompareTag("Jin"))      //100
            {
                if (DataController.instance.gameData.Jin.HP <= 90)
                {
                    DataController.instance.gameData.Jin.HP += heal;             //Jin의 경우
                    myText.text = $"{this.transform.name} has gained +{heal}hp!"; 
                    StartCoroutine(UsedPotion(1.0f));
                    CheckDestroy(name, temp);
                }
                else if (DataController.instance.gameData.Jin.HP < 100 && DataController.instance.gameData.Jin.HP > 90)
                {
                    heal = 100 - DataController.instance.gameData.Jin.HP;
                    DataController.instance.gameData.Jin.HP += heal;
                    myText.text = $"{this.transform.name} has gained +{heal}hp!";
                    StartCoroutine(UsedPotion(1.0f));
                    CheckDestroy(name, temp);
                }
                else
                {
                    myText.text = "이미 최대 체력입니다.";
                    StartCoroutine(UsedPotion(1.0f));
                }
            }
            else if (gameObject.CompareTag("Ember"))        //125
            {
                if (DataController.instance.gameData.Ember.HP <= 115)
                {
                    DataController.instance.gameData.Ember.HP += heal;
                    myText.text = $"{this.transform.name} has gained +{heal}hp!";       //Ember의 경우
                    StartCoroutine(UsedPotion(1.0f));
                    CheckDestroy(name, temp);
                }
                else if (DataController.instance.gameData.Ember.HP < 125 && DataController.instance.gameData.Ember.HP > 115)
                {
                    heal = 125 - DataController.instance.gameData.Ember.HP;
                    DataController.instance.gameData.Ember.HP += heal;
                    myText.text = $"{this.transform.name} has gained +{heal}hp!";
                    StartCoroutine(UsedPotion(1.0f));
                    CheckDestroy(name, temp);
                }
                else
                {
                    myText.text = "이미 최대 체력입니다.";
                    StartCoroutine(UsedPotion(1.0f));
                }
            }

         

        }

        else
        {
            if (itemTransform.gameObject.layer == 7)       //장비를 캐릭터에게 장착
            {
                GameObject thisEquipment = PointerInfo.instance.transform.gameObject ;
                string thisname = thisEquipment.GetComponent<Pickup>().item.itemName;
                int ap = thisEquipment.GetComponent<EnhanceableItems>().myData.AP;
                Item.ItemType myType= thisEquipment.GetComponent<Pickup>().item.itemType;

                if (thisEquipment.transform.childCount >= 2)      //만약 이 장비가 이전에 장착된 적이 있으면
                {
                    check=ThisCharEquipped(check, thisname, myType, thisEquipment);         //장착됐었던 캐릭터를 장착 해제
                }
               
                GameObject clickedEquip = itemTransform.gameObject;
                Item.ItemType thisType = clickedEquip.GetComponent<Pickup>().item.itemType;
              
                if (gameObject.CompareTag("Kong"))          //Kong에게 장착시키려 함
                {
                    if (!check.Equals("Kong"))      //전에 장착된게 Kong이 아니라면
                    {
                        if (DataController.instance.gameData.Kong.myUsedItems.Count.Equals(0))
                        {
                            DataController.instance.gameData.Kong.myUsedItems.Add(thisname);               //장비를 장착한 멤버에 따라 스탯에 적용됨
                            if (thisType.Equals(Item.ItemType.Accessory))
                            {
                                DataController.instance.gameData.Kong.strength += ap;   
                            }
                            else if (thisType.Equals(Item.ItemType.Armor))
                            {
                                DataController.instance.gameData.Kong.defPower += ap;       //Kong에게 아이템 값만큼 부여
                            }
                            ShowtoUI(itemTransform);        //장착된 캐릭터의 초상화 표시
                            myPanel.SetActive(false);
                        }
                        else
                        {
                            myText.text = "이미 장착된 장비가 있습니다.";       //장착된 장비가 있으면 알림만 뜨고 장착 안됨
                            StartCoroutine(UsedPotion(0.5f));
                        }
                    }
                    else
                    {
                        check = ThisCharEquipped(check, thisname, myType, thisEquipment);       //전에 장착된게 Kong이였다면 장착 해제
                        myPanel.SetActive(false);
                    }
                }
                else if (gameObject.CompareTag("Jin"))          //Jin의 경우
                {
                    if (!check.Equals("Jin"))
                    {
                        if (DataController.instance.gameData.Jin.myUsedItems.Count.Equals(0))
                        {
                            DataController.instance.gameData.Jin.myUsedItems.Add(thisname);
                            if (thisType.Equals(Item.ItemType.Accessory))
                            {
                                DataController.instance.gameData.Jin.strength += ap;
                            }
                            else if (thisType.Equals(Item.ItemType.Armor))
                            {
                                DataController.instance.gameData.Jin.defPower += ap;
                            }
                            ShowtoUI(itemTransform);
                            myPanel.SetActive(false);
                        }
                        else
                        {
                            myText.text = "이미 장착된 장비가 있습니다.";
                            StartCoroutine(UsedPotion(0.5f));
                        }
                    }
                    else
                    {
                        check=ThisCharEquipped(check, thisname, myType, thisEquipment);
                        myPanel.SetActive(false);
                    }
                }
                else if (gameObject.CompareTag("Ember"))            //Ember의 경우
                {
                    if (!check.Equals("Ember"))
                    {
                        if (DataController.instance.gameData.Ember.myUsedItems.Count.Equals(0))
                        {
                            DataController.instance.gameData.Ember.myUsedItems.Add(thisname);
                            if (thisType.Equals(Item.ItemType.Accessory))
                            {
                                DataController.instance.gameData.Ember.strength += ap;
                            }
                            else if (thisType.Equals(Item.ItemType.Armor))
                            {
                                DataController.instance.gameData.Ember.defPower += ap;
                            }
                            ShowtoUI(itemTransform);
                            myPanel.SetActive(false);
                        }
                        else
                        {
                            myText.text = "이미 장착된 장비가 있습니다.";
                            StartCoroutine(UsedPotion(0.5f));
                        }
                    }
                    else
                    {
                        check=ThisCharEquipped(check, thisname, myType, thisEquipment);
                        myPanel.SetActive(false);
                    }
                }
              //if(check!="")  check = ThisCharEquipped(check, thisname, myType, thisEquipment);
            }


        }
    }
    public string ThisCharEquipped(string check, string thisname, Item.ItemType myType,GameObject thisEquipment)
    {
        if (DataController.instance.gameData.Kong.myUsedItems.Contains(thisname))     // 콩이 장착했었으면 제거
        {
            check = "Kong";
            DataController.instance.gameData.Kong.myUsedItems.Remove(thisname);

            if (myType.Equals(Item.ItemType.Accessory))
            {
                DataController.instance.gameData.Kong.strength = 0;
            }
            else if (myType.Equals(Item.ItemType.Armor))               //더해진 스탯을 원래대로 되돌림
            {
                DataController.instance.gameData.Kong.defPower = 0;
            }

        }
        else if (DataController.instance.gameData.Jin.myUsedItems.Contains(thisname))        //진이 장착했었으면
        {
            check = "Jin";
            DataController.instance.gameData.Jin.myUsedItems.Remove(thisname);
            if (myType.Equals(Item.ItemType.Accessory))
            {
                DataController.instance.gameData.Jin.strength = 0;
            }
            else if (myType.Equals(Item.ItemType.Armor))
            {
                DataController.instance.gameData.Jin.defPower = 0;
            }
        }
        else if (DataController.instance.gameData.Ember.myUsedItems.Contains(thisname))        //앰버가 장착했었으면
        {
            check = "Ember";
            DataController.instance.gameData.Ember.myUsedItems.Remove(thisname);
            if (myType.Equals(Item.ItemType.Accessory))
            {
                DataController.instance.gameData.Ember.strength = 0;
            }
            else if (myType.Equals(Item.ItemType.Armor))
            {
                DataController.instance.gameData.Ember.defPower = 0;
            }
        }
        else check = "";
        if (thisEquipment.transform.childCount > 1)
        {
            Destroy(thisEquipment.transform.GetChild(1).gameObject);
        }
        else check = "";
        return check;
    }
    public void ShowtoUI(Transform itemTransform)
    {
        obj = Instantiate(this.gameObject);
        obj.GetComponent<RectTransform>().sizeDelta = new Vector2(25, 25);
        obj.transform.SetParent(PointerInfo.instance.transform);
        pos.x = itemTransform.localPosition.x + 20;
        pos.y = itemTransform.localPosition.y - 20;
        obj.transform.localPosition = pos;                          //UI로 누가 어떤 아이템 갖고 있는지 표시
        obj.GetComponent<RawImage>().raycastTarget = false;

    }
    public void CheckDestroy(string name, GameObject temp)
    {
        if (DataController.instance.gameData.myItemCount[name] > 1)
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
    IEnumerator UsedPotion(float sec)
    {
        myText.gameObject.SetActive(true);
        yield return new WaitForSeconds(sec);          //포션 사용됐다는 알림
        myText.gameObject.SetActive(false);
        myPanel.SetActive(false);
    }
  

}
