using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;
public class UsingItemtoParty : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler,IPointerClickHandler
{
    Vector2 pos = Vector2.zero;
    public GameObject myArrow;
    public GameObject myPanel;
    public Transform myInventory;
    public TMPro.TMP_Text myText;


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
        if (PointerInfo.instance.transform.gameObject.layer == 8)          //소모품을 캐릭터에게 사용
        {
          
            GameObject temp = PointerInfo.instance.transform.gameObject;
            switch (this.name)
            {
                case "MainCharacter":
                   DataController.instance.gameData.Kong.HP += temp.GetComponent<Pickup>().item.value;
                    break;
                case "Jin":
                    DataController.instance.gameData.Jin.HP += temp.GetComponent<Pickup>().item.value;
                    break;
                case "Ember":
                    DataController.instance.gameData.Ember.HP += temp.GetComponent<Pickup>().item.value;          //누가 사용하는지에 따라 회복됨
                    break;
            }
            myText.text = $"{this.transform.name} has gained +{temp.GetComponent<Pickup>().item.value}hp!";      //회복됐다는 알림
            StartCoroutine(UsedPotion());
            if ( DataController.instance.gameData.myItemCount[temp.GetComponent<Pickup>().item.itemName]>1)
            {
                   //포션이 2개 이상이면 destroy 대신 -1
                DataController.instance.gameData.myItemCount[temp.GetComponent<Pickup>().item.itemName]--;
                InventoryController.Instance.ShowNumbertoUI();
            }
            else
            {
                DataController.instance.gameData.savedInventory.Remove(temp.GetComponent<Pickup>().item.itemName);
                Destroy(temp); //1개일 경우 destroy
            }
           

        }

        else
        {
            if (PointerInfo.instance.transform.gameObject.layer == 7)       //장비를 캐릭터에게 장착
            {
                obj = Instantiate(this.gameObject);
                obj.GetComponent<RectTransform>().sizeDelta = new Vector2(25, 25);
                pos.x = PointerInfo.instance.transform.localPosition.x + 20;
                pos.y = PointerInfo.instance.transform.localPosition.y - 20;

                obj.transform.SetParent(PointerInfo.instance.transform);
                GameObject thisEquipment = PointerInfo.instance.transform.gameObject ;
                string thisname = thisEquipment.GetComponent<Pickup>().item.itemName;
                if (PointerInfo.instance.transform.childCount > 2)      //만약 이 장비가 이전에 장착된 적이 있으면
                {
                    if (DataController.instance.gameData.Kong.myUsedItems.Contains(thisname))     // 콩이 장착했었으면 제거
                    {
                        DataController.instance.gameData.Kong.myUsedItems.Remove(thisname);

                        if (thisEquipment.GetComponent<Pickup>().item.itemType == Item.NpcType.Weapon)
                        {
                            DataController.instance.gameData.Kong.strength -= thisEquipment.GetComponent<Pickup>().item.value;
                        }
                        else if (thisEquipment.GetComponent<Pickup>().item.itemType == Item.NpcType.Armor)                  //더해진 스탯을 원래대로 되돌림
                        {
                            DataController.instance.gameData.Kong.defPower -= thisEquipment.GetComponent<Pickup>().item.value;
                        }                                                                                                                               

                    }
                    else if (DataController.instance.gameData.Jin.myUsedItems.Contains(thisname))        //진이 장착했으면
                    {
                        DataController.instance.gameData.Jin.myUsedItems.Remove(thisname);
                        if (thisEquipment.GetComponent<Pickup>().item.itemType == Item.NpcType.Weapon)
                        {
                            DataController.instance.gameData.Jin.strength -= thisEquipment.GetComponent<Pickup>().item.value;
                        }
                        else if (thisEquipment.GetComponent<Pickup>().item.itemType == Item.NpcType.Armor)
                        {
                            DataController.instance.gameData.Jin.defPower -= thisEquipment.GetComponent<Pickup>().item.value;
                        }
                    }
                    else if (DataController.instance.gameData.Ember.myUsedItems.Contains(thisname))        //앰버가 장착했으면
                    {
                        DataController.instance.gameData.Ember.myUsedItems.Remove(thisname);
                        if (thisEquipment.GetComponent<Pickup>().item.itemType == Item.NpcType.Weapon)
                        {
                            DataController.instance.gameData.Ember.strength -= thisEquipment.GetComponent<Pickup>().item.value;
                        }
                        else if (thisEquipment.GetComponent<Pickup>().item.itemType == Item.NpcType.Armor)
                        {
                            DataController.instance.gameData.Ember.defPower -= thisEquipment.GetComponent<Pickup>().item.value;
                        }
                    }
                    Destroy(PointerInfo.instance.transform.GetChild(1).gameObject);             //전에 장비 위에 표시됐던 초상화 UI를 제거
                }
                obj.transform.localPosition = pos;                          //UI로 누가 어떤 아이템 갖고 있는지 표시
                obj.GetComponent<RawImage>().raycastTarget = false;
                //게임데이터에 적용할 때 여기서
                GameObject clickedEquip = PointerInfo.instance.transform.gameObject;
                switch (this.name)
                {
                    case "MainCharacter":
                        DataController.instance.gameData.Kong.myUsedItems.Add(thisname);               //장비를 장착한 멤버에 따라 스탯에 적용됨
                        if (clickedEquip.GetComponent<Pickup>().item.itemType ==Item.NpcType.Weapon)
                        {
                            DataController.instance.gameData.Kong.strength += clickedEquip.GetComponent<Pickup>().item.value;
                        }
                        else if (clickedEquip.GetComponent<Pickup>().item.itemType == Item.NpcType.Armor)
                        {
                            DataController.instance.gameData.Kong.defPower += clickedEquip.GetComponent<Pickup>().item.value;
                        }
                        Debug.Log($"{this.name}has{clickedEquip}");
                        break;
                    case "Jin":
                        DataController.instance.gameData.Jin.myUsedItems.Add(thisname);
                        if (clickedEquip.GetComponent<Pickup>().item.itemType == Item.NpcType.Weapon)
                        {
                            DataController.instance.gameData.Jin.strength += clickedEquip.GetComponent<Pickup>().item.value;
                        }
                        else if (clickedEquip.GetComponent<Pickup>().item.itemType == Item.NpcType.Armor)
                        {
                            DataController.instance.gameData.Jin.defPower += clickedEquip.GetComponent<Pickup>().item.value;
                        }
                        break;
                    case "Ember":
                        DataController.instance.gameData.Ember.myUsedItems.Add(thisname);
                        if (clickedEquip.GetComponent<Pickup>().item.itemType == Item.NpcType.Weapon)
                        {
                            DataController.instance.gameData.Ember.strength += clickedEquip.GetComponent<Pickup>().item.value;
                        }
                        else if (clickedEquip.GetComponent<Pickup>().item.itemType == Item.NpcType.Armor)
                        {
                            DataController.instance.gameData.Ember.defPower += clickedEquip.GetComponent<Pickup>().item.value;
                        }
                        break;
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
