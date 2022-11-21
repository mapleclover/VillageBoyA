//�ۼ��� : �̿���
//���� : 
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
        if (itemTransform.gameObject.layer == 8)          //�Ҹ�ǰ�� ĳ���Ϳ��� ���
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
                DataController.instance.gameData.Jin.HP += heal;             //���� ����ϴ����� ���� ȸ����
            }
            else if (gameObject.CompareTag("Ember"))
            {
                DataController.instance.gameData.Ember.HP += heal;
            }

            myText.text = $"{this.transform.name} has gained +{heal}hp!";      //ȸ���ƴٴ� �˸�
            StartCoroutine(UsedPotion());
            if ( DataController.instance.gameData.myItemCount[name] >1)
            {
                   //������ 2�� �̻��̸� destroy ��� -1
                DataController.instance.gameData.myItemCount[name]--;
                InventoryController.Instance.ShowNumbertoUI();
            }
            else
            {
                DataController.instance.gameData.savedInventory.Remove(name);
                Destroy(temp); //1���� ��� destroy
            }
           

        }

        else
        {
            if (itemTransform.gameObject.layer == 7)       //��� ĳ���Ϳ��� ����
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

                if (PointerInfo.instance.transform.childCount > 2)      //���� �� ��� ������ ������ ���� ������
                {
                    if (DataController.instance.gameData.Kong.myUsedItems.Contains(thisname))     // ���� �����߾����� ����
                    {
                        DataController.instance.gameData.Kong.myUsedItems.Remove(thisname);

                        if (myType == Item.ItemType.Weapon)
                        {
                            DataController.instance.gameData.Kong.strength -= val;
                        }
                        else if (myType == Item.ItemType.Armor)                  //������ ������ ������� �ǵ���
                        {
                            DataController.instance.gameData.Kong.defPower -= val;
                        }                                                                                                                               

                    }
                    else if (DataController.instance.gameData.Jin.myUsedItems.Contains(thisname))        //���� ����������
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
                    else if (DataController.instance.gameData.Ember.myUsedItems.Contains(thisname))        //�ڹ��� ����������
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
                    Destroy(itemTransform.GetChild(1).gameObject);             //���� ��� ���� ǥ�õƴ� �ʻ�ȭ UI�� ����
                }
                obj.transform.localPosition = pos;                          //UI�� ���� � ������ ���� �ִ��� ǥ��
                obj.GetComponent<RawImage>().raycastTarget = false;
                //���ӵ����Ϳ� ������ �� ���⼭
                GameObject clickedEquip = itemTransform.gameObject;
                int Val = clickedEquip.GetComponent<Pickup>().item.value;
                Item.ItemType thisType = clickedEquip.GetComponent<Pickup>().item.itemType;
              
                if (gameObject.CompareTag("Kong"))
                {
                    DataController.instance.gameData.Kong.myUsedItems.Add(thisname);               //��� ������ ����� ���� ���ȿ� �����
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
        yield return new WaitForSeconds(1.0f);          //���� ���ƴٴ� �˸�
        myText.gameObject.SetActive(false);
        myPanel.SetActive(false);
    }
  

}
