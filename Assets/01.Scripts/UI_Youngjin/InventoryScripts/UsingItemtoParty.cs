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
    GameObject konghp;
    GameObject jinhp;
    GameObject emberhp;
    string check = "";
    public void Start()
    {
        konghp = GameObject.FindWithTag("KongHP");
        jinhp = GameObject.FindWithTag("JinHP");
        emberhp = GameObject.FindWithTag("EmberHP");
    }
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
    public void OnPointerClick(PointerEventData eventData)          //�������� ���� �� � ĳ���Ϳ��� ���� ��ų ������
    {
        Transform itemTransform = PointerInfo.instance.transform;
        if (itemTransform.gameObject.layer.Equals(8))          //�Ҹ�ǰ�� ĳ���Ϳ��� ���
        {

            GameObject temp = PointerInfo.instance.transform.gameObject;
            int heal= temp.GetComponent<Pickup>().item.value;
            string name = temp.GetComponent<Pickup>().item.itemName;
            int index = DataController.instance.gameData.itemList.IndexOf(name);
            if (gameObject.CompareTag("Kong"))  //150
            {
                if (DataController.instance.gameData.Kong.HP <= 140)            //�ִ�ü�°� ���� ȸ���Ǵ� ���� ���
                { 
                    DataController.instance.gameData.Kong.HP += heal;
                    myText.text = $"{this.transform.name} has gained +{heal}hp!";      //ȸ���ƴٴ� �˸�
                    StartCoroutine(UsedPotion(1.0f));
                    CheckDestroy(index,temp);
                    konghp.GetComponent<Slider>().value += heal / 150f;     //�ʵ忡 �ִ� �����̴��� �ݿ�
                }
                else if (DataController.instance.gameData.Kong.HP < 150 && DataController.instance.gameData.Kong.HP > 140)
                {
                    heal = 150 - DataController.instance.gameData.Kong.HP;
                    DataController.instance.gameData.Kong.HP += heal;
                    myText.text = $"{this.transform.name} has gained +{heal}hp!";
                    StartCoroutine(UsedPotion(1.0f));
                    CheckDestroy(index, temp);
                    konghp.GetComponent<Slider>().value += heal / 150f;
                }
                else
                {
                    myText.text = "�̹� �ִ� ü���Դϴ�.";
                    StartCoroutine(UsedPotion(1.0f));           //Kong�� ���
                }
            }
            else if (gameObject.CompareTag("Jin"))      //100
            {
                if (DataController.instance.gameData.Jin.HP <= 90)
                {
                    DataController.instance.gameData.Jin.HP += heal;             //Jin�� ���
                    myText.text = $"{this.transform.name} has gained +{heal}hp!"; 
                    StartCoroutine(UsedPotion(1.0f));
                    CheckDestroy(index, temp);
                    jinhp.GetComponent<Slider>().value += heal * 0.01f;
                }
                else if (DataController.instance.gameData.Jin.HP < 100 && DataController.instance.gameData.Jin.HP > 90)
                {
                    heal = 100 - DataController.instance.gameData.Jin.HP;
                    DataController.instance.gameData.Jin.HP += heal;
                    myText.text = $"{this.transform.name} has gained +{heal}hp!";
                    StartCoroutine(UsedPotion(1.0f));
                    CheckDestroy(index, temp);
                    jinhp.GetComponent<Slider>().value += heal * 0.01f;
                }
                else
                {
                    myText.text = "�̹� �ִ� ü���Դϴ�.";
                    StartCoroutine(UsedPotion(1.0f));
                }
            }
            else if (gameObject.CompareTag("Ember"))        //125
            {
                if (DataController.instance.gameData.Ember.HP <= 115)
                {
                    DataController.instance.gameData.Ember.HP += heal;
                    myText.text = $"{this.transform.name} has gained +{heal}hp!";       //Ember�� ���
                    StartCoroutine(UsedPotion(1.0f));
                    CheckDestroy(index, temp);
                    emberhp.GetComponent<Slider>().value += heal * 0.008f;
                }
                else if (DataController.instance.gameData.Ember.HP < 125 && DataController.instance.gameData.Ember.HP > 115)
                {
                    heal = 125 - DataController.instance.gameData.Ember.HP;
                    DataController.instance.gameData.Ember.HP += heal;
                    myText.text = $"{this.transform.name} has gained +{heal}hp!";
                    StartCoroutine(UsedPotion(1.0f));
                    CheckDestroy(index, temp);
                    emberhp.GetComponent<Slider>().value += heal * 0.008f;
                }
                else
                {
                    myText.text = "�̹� �ִ� ü���Դϴ�.";
                    StartCoroutine(UsedPotion(1.0f));
                }
            }

         

        }

        else
        {
            if (itemTransform.gameObject.layer == 7)       //��� ĳ���Ϳ��� ����
            {
                GameObject thisEquipment = PointerInfo.instance.transform.gameObject ;
                string thisname = thisEquipment.GetComponent<Pickup>().item.itemName;
                int ap = thisEquipment.GetComponent<EnhanceableItems>().myData.AP;
                int dp = thisEquipment.GetComponent<EnhanceableItems>().myData.DP;
                Item.ItemType myType= thisEquipment.GetComponent<Pickup>().item.itemType;
                

                if (thisEquipment.transform.childCount >= 2)      //���� �� ��� ������ ������ ���� ������
                {
                    check=ThisCharEquipped(check, thisname, myType, thisEquipment);         //�����߾��� ĳ���͸� ���� ����
                }
               
                GameObject clickedEquip = itemTransform.gameObject;
                Item.ItemType thisType = clickedEquip.GetComponent<Pickup>().item.itemType;
              
                if (gameObject.CompareTag("Kong"))          //Kong���� ������Ű�� ��
                {
                    if (!check.Equals("Kong"))      //���� �����Ȱ� Kong�� �ƴ϶��
                    {
                        if (DataController.instance.gameData.Kong.myUsedItems.Count.Equals(0))
                        {
                            SoundTest.instance.PlaySE("SFX_Equip");
                            DataController.instance.gameData.Kong.myUsedItems.Add(thisname);               //��� ������ ����� ���� ���ȿ� �����
                            if (thisType.Equals(Item.ItemType.Accessory))
                            {
                                DataController.instance.gameData.Kong.strength += ap;   
                            }
                            else if (thisType.Equals(Item.ItemType.Armor))
                            {
                                DataController.instance.gameData.Kong.defPower += dp;       //Kong���� ������ ����ŭ �ο�
                            }
                            ShowtoUI(itemTransform);        //������ ĳ������ �ʻ�ȭ ǥ��
                            myPanel.SetActive(false);
                        }
                        else
                        {
                            myText.text = "�̹� ������ ��� �ֽ��ϴ�.";       //������ ��� ������ �˸��� �߰� ���� �ȵ�
                            StartCoroutine(UsedPotion(0.5f));
                        }
                    }
                    else
                    {
                        check = ThisCharEquipped(check, thisname, myType, thisEquipment);       //���� �����Ȱ� Kong�̿��ٸ� ���� ����
                        myPanel.SetActive(false);
                    }
                }
                else if (gameObject.CompareTag("Jin"))          //Jin�� ���
                {
                    if (!check.Equals("Jin"))
                    {
                        if (DataController.instance.gameData.Jin.myUsedItems.Count.Equals(0))
                        {
                            SoundTest.instance.PlaySE("SFX_Equip");
                            DataController.instance.gameData.Jin.myUsedItems.Add(thisname);
                            if (thisType.Equals(Item.ItemType.Accessory))
                            {
                                DataController.instance.gameData.Jin.strength += ap;
                            }
                            else if (thisType.Equals(Item.ItemType.Armor))
                            {
                                DataController.instance.gameData.Jin.defPower += dp;
                            }
                            ShowtoUI(itemTransform);
                            myPanel.SetActive(false);
                        }
                        else
                        {
                            myText.text = "�̹� ������ ��� �ֽ��ϴ�.";
                            StartCoroutine(UsedPotion(0.5f));
                        }
                    }
                    else
                    {
                        check=ThisCharEquipped(check, thisname, myType, thisEquipment);
                        myPanel.SetActive(false);
                    }
                }
                else if (gameObject.CompareTag("Ember"))            //Ember�� ���
                {
                    if (!check.Equals("Ember"))
                    {
                        if (DataController.instance.gameData.Ember.myUsedItems.Count.Equals(0))
                        {
                            SoundTest.instance.PlaySE("SFX_Equip");
                            DataController.instance.gameData.Ember.myUsedItems.Add(thisname);
                            if (thisType.Equals(Item.ItemType.Accessory))
                            {
                                DataController.instance.gameData.Ember.strength += ap;
                            }
                            else if (thisType.Equals(Item.ItemType.Armor))
                            {
                                DataController.instance.gameData.Ember.defPower += dp;
                            }
                            ShowtoUI(itemTransform);
                            myPanel.SetActive(false);
                        }
                        else
                        {
                            myText.text = "�̹� ������ ��� �ֽ��ϴ�.";
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
        if (DataController.instance.gameData.Kong.myUsedItems.Contains(thisname))     // ���� �����߾����� ����
        {
            check = "Kong";
            DataController.instance.gameData.Kong.myUsedItems.Remove(thisname);

            if (myType.Equals(Item.ItemType.Accessory))
            {
                DataController.instance.gameData.Kong.strength = 0;
            }
            else if (myType.Equals(Item.ItemType.Armor))               //������ ������ ������� �ǵ���
            {
                DataController.instance.gameData.Kong.defPower = 0;
            }

        }
        else if (DataController.instance.gameData.Jin.myUsedItems.Contains(thisname))        //���� �����߾�����
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
        else if (DataController.instance.gameData.Ember.myUsedItems.Contains(thisname))        //�ڹ��� �����߾�����
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
        obj.GetComponent<RectTransform>().sizeDelta = new Vector2(20, 20);
        obj.transform.SetParent(PointerInfo.instance.transform);
        pos.x = itemTransform.localPosition.x + 20;
        pos.y = itemTransform.localPosition.y - 20;
        obj.transform.localPosition = pos;                          //UI�� ���� � ������ ���� �ִ��� ǥ��
        obj.GetComponent<RawImage>().raycastTarget = false;

    }
    public void CheckDestroy(int index, GameObject temp)
    {
        if (DataController.instance.gameData.itemCount[index] > 1)
        {
            //������ 2�� �̻��̸� destroy ��� -1
            DataController.instance.gameData.itemCount[index]--;
            InventoryController.Instance.ShowNumbertoUI();
        }
        else
        {
            DataController.instance.gameData.itemList.RemoveAt(index);
            DataController.instance.gameData.itemCount.RemoveAt(index);
            DataController.instance.gameData.slotNum.RemoveAt(index);
            Destroy(temp); //1���� ��� destroy
        }

    }
    IEnumerator UsedPotion(float sec)
    {
        myText.gameObject.SetActive(true);
        yield return new WaitForSeconds(sec);          //���� ���ƴٴ� �˸�
        myText.gameObject.SetActive(false);
        myPanel.SetActive(false);

    }
  

}
