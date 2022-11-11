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
        if (PointerInfo.instance.transform.gameObject.layer == 8)          //�Ҹ�ǰ�� ĳ���Ϳ��� ���
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
                    DataController.instance.gameData.Ember.HP += temp.GetComponent<Pickup>().item.value;          //���� ����ϴ����� ���� ȸ����
                    break;
            }
            myText.text = $"{this.transform.name} has gained +{temp.GetComponent<Pickup>().item.value}hp!";      //ȸ���ƴٴ� �˸�
            StartCoroutine(UsedPotion());
            if (temp.GetComponent<Pickup>().item.count>1)
            {
               temp.GetComponent<Pickup>().item.count--;       //������ 2�� �̻��̸� destroy ��� -1
                InventoryController.Instance.ShowNumbertoUI();
            }
            else
            {
                Destroy(PointerInfo.instance.transform.gameObject); //1���� ��� destroy
            }
           

        }

        else
        {
            if (PointerInfo.instance.transform.gameObject.layer == 7)       //��� ĳ���Ϳ��� ����
            {
                obj = Instantiate(this.gameObject);
                obj.GetComponent<RectTransform>().sizeDelta = new Vector2(25, 25);
                pos.x = PointerInfo.instance.transform.localPosition.x + 20;
                pos.y = PointerInfo.instance.transform.localPosition.y - 20;

                obj.transform.SetParent(PointerInfo.instance.transform);
                GameObject thisEquipment = PointerInfo.instance.transform.gameObject ;

                if (PointerInfo.instance.transform.childCount > 2)      //���� �� ��� ������ ������ ���� ������
                {
                    if (DataController.instance.gameData.Kong.myUsedItems.Contains(thisEquipment))     // ���� �����߾����� ����
                    {
                        DataController.instance.gameData.Kong.myUsedItems.Remove(thisEquipment);

                        if (thisEquipment.GetComponent<Pickup>().item.itemType == Item.NpcType.Weapon)
                        {
                            DataController.instance.gameData.Kong.strength -= thisEquipment.GetComponent<Pickup>().item.value;
                        }
                        else if (thisEquipment.GetComponent<Pickup>().item.itemType == Item.NpcType.Armor)                  //������ ������ ������� �ǵ���
                        {
                            DataController.instance.gameData.Kong.defPower -= thisEquipment.GetComponent<Pickup>().item.value;
                        }                                                                                                                               

                    }
                    else if (DataController.instance.gameData.Jin.myUsedItems.Contains(thisEquipment))        //���� ����������
                    {
                        DataController.instance.gameData.Jin.myUsedItems.Remove(thisEquipment);
                        if (thisEquipment.GetComponent<Pickup>().item.itemType == Item.NpcType.Weapon)
                        {
                            DataController.instance.gameData.Jin.strength -= thisEquipment.GetComponent<Pickup>().item.value;
                        }
                        else if (thisEquipment.GetComponent<Pickup>().item.itemType == Item.NpcType.Armor)
                        {
                            DataController.instance.gameData.Jin.defPower -= thisEquipment.GetComponent<Pickup>().item.value;
                        }
                    }
                    else if (DataController.instance.gameData.Ember.myUsedItems.Contains(thisEquipment))        //�ڹ��� ����������
                    {
                        DataController.instance.gameData.Ember.myUsedItems.Remove(thisEquipment);
                        if (thisEquipment.GetComponent<Pickup>().item.itemType == Item.NpcType.Weapon)
                        {
                            DataController.instance.gameData.Ember.strength -= thisEquipment.GetComponent<Pickup>().item.value;
                        }
                        else if (thisEquipment.GetComponent<Pickup>().item.itemType == Item.NpcType.Armor)
                        {
                            DataController.instance.gameData.Ember.defPower -= thisEquipment.GetComponent<Pickup>().item.value;
                        }
                    }
                    Destroy(PointerInfo.instance.transform.GetChild(1).gameObject);             //���� ��� ���� ǥ�õƴ� �ʻ�ȭ UI�� ����
                }
                obj.transform.localPosition = pos;                          //UI�� ���� � ������ ���� �ִ��� ǥ��
                obj.GetComponent<RawImage>().raycastTarget = false;
                //���ӵ����Ϳ� ������ �� ���⼭
                GameObject clickedEquip = PointerInfo.instance.transform.gameObject;
                switch (this.name)
                {
                    case "MainCharacter":
                        DataController.instance.gameData.Kong.myUsedItems.Add(clickedEquip);               //��� ������ ����� ���� ���ȿ� �����
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
                        DataController.instance.gameData.Jin.myUsedItems.Add(clickedEquip);
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
                        DataController.instance.gameData.Ember.myUsedItems.Add(clickedEquip);
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
        yield return new WaitForSeconds(1.0f);          //���� ���ƴٴ� �˸�
        myText.gameObject.SetActive(false);
        myPanel.SetActive(false);
    }
  

}
