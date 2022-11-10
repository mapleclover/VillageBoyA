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
                   DataController.instance.gameData.partyHP[0] += temp.GetComponent<Pickup>().item.value;
                    break;
                case "Jin":
                    DataController.instance.gameData.partyHP[1] += temp.GetComponent<Pickup>().item.value;
                    break;
                case "Ember":
                    DataController.instance.gameData.partyHP[2] += temp.GetComponent<Pickup>().item.value;          //누가 사용하는지에 따라 회복됨
                    break;
            }
            myText.text = $"{this.transform.name} has gained +{temp.GetComponent<Pickup>().item.value}hp!";      //회복됐다는 알림
            StartCoroutine(UsedPotion());
            if (DataController.instance.gameData.myItem[temp.GetComponent<Pickup>().item.itemName] > 1)
            {
                DataController.instance.gameData.myItem[temp.GetComponent<Pickup>().item.itemName]--;       //포션이 2개 이상이면 destroy 대신 -1
                InventoryController.Instance.ShowNumbertoUI();
            }
            else
            {
                Destroy(PointerInfo.instance.transform.gameObject); //1개일 경우 destroy
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

                if (PointerInfo.instance.transform.childCount > 2)      //만약 이 장비가 이전에 장착된 적이 있으면
                {
                    if (DataController.instance.gameData.partyItems[0].Contains(thisEquipment))     // 콩이 장착했었으면 제거
                    {
                        DataController.instance.gameData.partyItems[0].Remove(thisEquipment);

                        if (thisEquipment.GetComponent<Pickup>().item.itemType == Item.NpcType.Weapon)
                        {
                            DataController.instance.gameData.partyStats[0, 0] -= thisEquipment.GetComponent<Pickup>().item.value;
                        }
                        else if (thisEquipment.GetComponent<Pickup>().item.itemType == Item.NpcType.Armor)                  //더해진 스탯을 원래대로 되돌림
                        {
                            DataController.instance.gameData.partyStats[0, 1] -= thisEquipment.GetComponent<Pickup>().item.value;
                        }                                                                                                                               

                    }
                    else if (DataController.instance.gameData.partyItems[1].Contains(thisEquipment))        //진이 장착했으면
                    {
                        DataController.instance.gameData.partyItems[1].Remove(thisEquipment);
                        if (thisEquipment.GetComponent<Pickup>().item.itemType == Item.NpcType.Weapon)
                        {
                            DataController.instance.gameData.partyStats[1, 0] -= thisEquipment.GetComponent<Pickup>().item.value;
                        }
                        else if (thisEquipment.GetComponent<Pickup>().item.itemType == Item.NpcType.Armor)
                        {
                            DataController.instance.gameData.partyStats[1, 1] -= thisEquipment.GetComponent<Pickup>().item.value;
                        }
                    }
                    else if (DataController.instance.gameData.partyItems[2].Contains(thisEquipment))        //앰버가 장착했으면
                    {
                        DataController.instance.gameData.partyItems[2].Remove(thisEquipment);
                        if (thisEquipment.GetComponent<Pickup>().item.itemType == Item.NpcType.Weapon)
                        {
                            DataController.instance.gameData.partyStats[2, 0] -= thisEquipment.GetComponent<Pickup>().item.value;
                        }
                        else if (thisEquipment.GetComponent<Pickup>().item.itemType == Item.NpcType.Armor)
                        {
                            DataController.instance.gameData.partyStats[2, 1] -= thisEquipment.GetComponent<Pickup>().item.value;
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
                        DataController.instance.gameData.partyItems[0].Add(clickedEquip);               //포션을 쓴 멤버에 따라 스탯에 적용됨
                        if (clickedEquip.GetComponent<Pickup>().item.itemType ==Item.NpcType.Weapon)
                        {
                            DataController.instance.gameData.partyStats[0, 0] += clickedEquip.GetComponent<Pickup>().item.value;
                        }
                        else if (clickedEquip.GetComponent<Pickup>().item.itemType == Item.NpcType.Armor)
                        {
                            DataController.instance.gameData.partyStats[0,1] += clickedEquip.GetComponent<Pickup>().item.value;
                        }
                        Debug.Log($"{this.name}has{clickedEquip}");
                        break;
                    case "Jin":
                        DataController.instance.gameData.partyItems[1].Add(clickedEquip);
                        if (clickedEquip.GetComponent<Pickup>().item.itemType == Item.NpcType.Weapon)
                        {
                            DataController.instance.gameData.partyStats[1, 0] += clickedEquip.GetComponent<Pickup>().item.value;
                        }
                        else if (clickedEquip.GetComponent<Pickup>().item.itemType == Item.NpcType.Armor)
                        {
                            DataController.instance.gameData.partyStats[1, 1] += clickedEquip.GetComponent<Pickup>().item.value;
                        }
                        break;
                    case "Ember":
                        DataController.instance.gameData.partyItems[2].Add(clickedEquip);
                        if (clickedEquip.GetComponent<Pickup>().item.itemType == Item.NpcType.Weapon)
                        {
                            DataController.instance.gameData.partyStats[2, 0] += clickedEquip.GetComponent<Pickup>().item.value;
                        }
                        else if (clickedEquip.GetComponent<Pickup>().item.itemType == Item.NpcType.Armor)
                        {
                            DataController.instance.gameData.partyStats[2, 1] += clickedEquip.GetComponent<Pickup>().item.value;
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
