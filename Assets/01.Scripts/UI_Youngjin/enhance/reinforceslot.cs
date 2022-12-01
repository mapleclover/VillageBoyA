using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using static Cinemachine.DocumentationSortingAttribute;
using Unity.VisualScripting;
using static MirzaBeig.ParticleSystems.Demos.DemoManager;

public class reinforceslot : MonoBehaviour, IDropHandler        //���콺 ��Ŭ���ϸ� �� 100���� ���ɴϴ�...-����
{
    //public Transform myInven2;
    public GameObject[] myIngSlots;
    public GameObject[] myInven;
    public GameObject alert; // ��ᰡ ���ٸ� ���� ��ᰡ �ʿ����� �˸�
    public TMPro.TMP_Text myMessage;
    public EnhanceableItems myItem;
    public TMPro.TMP_Text[] curAmount;
    public TMPro.TMP_Text[] required;
    public GameObject[] myNumbers;
    public GameObject myPanel;

    public bool EnchantDelay = false;
    public bool result = false;
    public Slider mySlider;
    public Animator mySliderAnim;
    public GameObject SuccessPanel;
    public GameObject FailPanel;



    public void UpdateNumberUI(string name, int level,int index)            
    {                                                                       
        curAmount[index].text = $"{DataController.instance.gameData.myItemCount[name]}";        //������ �ִ� ������ � �ʿ����� ǥ��
        required[index].text = $"{level}";
    }


    public void OnDrop(PointerEventData eventData)
    {

        PointerInfo icon = transform.GetComponentInChildren<PointerInfo>();
        Transform movingObject = eventData.pointerDrag.transform;
        myItem = movingObject.GetComponent<EnhanceableItems>(); // �巡�� ���� ���� ���ε� �ǵ��� �ٲ����
        if (myItem._GetMaxLevel)
        {
            if (movingObject.GetComponent<Pickup>().item.enhanceableItem.Equals(Item.EnhanceableItem.Possible))
            {
                if (icon != null && movingObject.parent.TryGetComponent<ItemSlot>(out ItemSlot slot))
                {
                    slot.SetChildren(icon.transform);
                    OnClickCancel();
                }
                else if (icon != null && movingObject.parent.TryGetComponent<reinforceslot>(out reinforceslot Slot))
                {
                    Slot.SetChild(icon.transform);
                    OnClickCancel();
                }
                movingObject.SetParent(transform);
                movingObject.localPosition = Vector3.zero;
                EnhancementController.inst.rectTransform.SetAsLastSibling();
                CheckIngredients(movingObject);                                  //��ȭ ��� Ȯ��
            }
            else return;
        }
        else
        {
            alert.SetActive(true);
            myMessage.text = $"��ȭ ���� ������ \n �ʰ� �Ͽ����ϴ�.";
            
        }
            
        
        
    }

    public void SetChild(Transform child)
    {
        child.SetParent(transform);
        child.localPosition = Vector3.zero;
    }
    public void CheckIngredients(Transform thisitem)
    {
        int level = thisitem.GetComponent<EnhanceableItems>().myData.Level;             //�� �������� ����
        int cost = thisitem.GetComponent<EnhanceableItems>().myData.EnchantCost;       //�� �������� ������ ���� ����
        switch (thisitem.GetComponent<Pickup>().item.itemName)    //�ݹ���: ���̾�&ö, �尩: ö&��, ����: ���&��
        {
            case "�ݹ���":
                if (DataController.instance.gameData.savedInventory.ContainsKey("���̾Ƹ��") && DataController.instance.gameData.savedInventory.ContainsKey("��")&&DataController.instance.gameData.gold>=cost)
                {
                    CompareRequirements(thisitem, "���̾Ƹ��", "��", level);     //�κ��丮�� ���� ��� ���� Ȯ���ϴ� �Լ� ȣ��

                }
                else
                {
                    NotEnough(thisitem, "���̾Ƹ��", "��", level, cost);         //�κ��丮�� ���� ��� �ʿ��� ��Ḧ �˸��� �˸�â
                }
                break;
            case "�尩":
                if (DataController.instance.gameData.savedInventory.ContainsKey("����") && DataController.instance.gameData.savedInventory.ContainsKey("��ũ��") && DataController.instance.gameData.gold >= cost)
                {
                    CompareRequirements(thisitem, "����", "��ũ��", level);
                }
                else
                {
                    NotEnough(thisitem, "����", "��ũ��", level, cost);
                }
                break;
            case "�����":
                if (DataController.instance.gameData.savedInventory.ContainsKey("��") && DataController.instance.gameData.savedInventory.ContainsKey("ö") && DataController.instance.gameData.gold >= cost)
                {
                    CompareRequirements(thisitem, "��", "ö", level);
                }
                else
                {
                    NotEnough(thisitem, "��", "ö", level, cost);
                }
                break;
        }

    }
    public void NotEnough(Transform thisitem, string st1, string st2, int level,int cost)       //�ʿ��� ��Ḧ �˸��� ���â
    {
        myPanel.SetActive(false);
        myMessage.text = $"{st1} {level}��\n\n {st2} {level}��\n\n {cost}���";
        alert.SetActive(true);
        FindMySlot(thisitem.gameObject);
    }
    public void CompareRequirements(Transform thisitem,string itemname1,string itemname2, int level)
    {
        if(DataController.instance.gameData.myItemCount[itemname1] <level || DataController.instance.gameData.myItemCount[itemname2] <level)
        {
            myMessage.text = $"{itemname1} {level}��\n\n {itemname2} {level}��\n\n";
            alert.SetActive(true);
            FindMySlot(thisitem.gameObject);            //���� �ʿ��� ������ŭ ���� ��� �˷��ִ� ���â
            return;
        }
        ShowIngredients(itemname1, 0);      //���� ���
        ShowIngredients(itemname2, 1);      //�ʿ��� ��Ḧ ��ȭâ �������� ������
        UpdateNumberUI(itemname1,level,0);
        UpdateNumberUI(itemname2, level,1);     //���� �� �� �ִ����� �� ���� �ʿ������� ��Ÿ��
        myNumbers[0].SetActive(true);
        myNumbers[1].SetActive(true);       
        myPanel.SetActive(true);
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
                        

                        break;
                    }
                }
            }
        }
    }
    public void OnClickCancel()     //��Ҹ� ������ ���
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
            FindMySlot(ing2);                                                   //�κ��丮�� �ٽ� �̵�
        }
        myPanel.SetActive(false);
        myNumbers[0].SetActive(false);
        myNumbers[1].SetActive(false);

    }
    public void OnClickEnhance()               //��ȭ�� ������ ���
    {
        if(!EnchantDelay)
        {
            if (this.transform.childCount > 0)
            {
                GameObject enchantingObj = this.transform.GetChild(0).gameObject;
                if (myIngSlots[0].transform.childCount > 0)
                {
                    GameObject obj = myIngSlots[0].transform.GetChild(0).gameObject;
                    CheckDestroy(obj, enchantingObj);       //������ ���� destroy�ؾ��ϴ��� �κ��丮�� ���ƾ� �ϴ��� ����
                    myNumbers[0].SetActive(false);
                }
                if (myIngSlots[1].transform.childCount > 0)
                {
                    GameObject obj2 = myIngSlots[1].transform.GetChild(0).gameObject;
                    CheckDestroy(obj2, enchantingObj);      
                    myNumbers[1].SetActive(false);
                }
                DataController.instance.gameData.gold -= enchantingObj.GetComponent<EnhanceableItems>().myData.EnchantCost;
                InventoryController.Instance.ShowMyGold();          //��忡 �ݿ�
                EnchantLogic(enchantingObj);            
                StartCoroutine(Delay(3f, enchantingObj, 1.5f));               
                myPanel.SetActive(false);                
                //FindMySlot()�Լ� Delay �ڷ�ƾ �ȿ� ����
            }
        }
        else
        {
            myPanel.SetActive(false);
        }
        
    }
    public void CheckDestroy(GameObject obj, GameObject enchantingObj)
    {
        int level = enchantingObj.GetComponent<EnhanceableItems>().myData.Level;
        if (DataController.instance.gameData.myItemCount[obj.GetComponent<Pickup>().item.itemName]>level)
        {
            DataController.instance.gameData.myItemCount[obj.GetComponent<Pickup>().item.itemName] -= level;
            FindMySlot(obj);        
            InventoryController.Instance.ShowNumbertoUI();          //���� ���� �ִ� ������ �ʿ� �������� ���� ��� �Ҹ� �� �κ����� �̵�
        }
        else
        {
            DataController.instance.gameData.savedInventory.Remove(obj.GetComponent<Pickup>().item.itemName);
            DataController.instance.gameData.myItemCount.Remove(obj.GetComponent<Pickup>().item.itemName);
            Destroy(obj);       //���� ��� destroy
        }
    }
    public void FindMySlot(GameObject obj)      //�κ��丮���� ����ִ� ������ ã�ư�
    {
        if(!EnchantDelay)
        {
            //������ ���� // ������ �̰� ���� ����? //�̰� �����۸��� �������ִ� �����ε� // �׷�����
            for (int i = 0; i < myInven.Length; i++)
            {
                if (myInven[i].transform.childCount == 0)
                {
                    obj.transform.SetParent(myInven[i].transform);      //
                    obj.transform.localPosition = Vector3.zero;
                    DataController.instance.gameData.savedInventory[obj.GetComponent<Pickup>().item.itemName] = i;
                    break;
                }
            }
        }
    }

    public void EnchantLogic(GameObject obj)
    {
        myItem = obj.GetComponent<EnhanceableItems>(); // �巡�� ���� ���� ���ε� �ǵ��� �ٲ����
        if (myItem._GetMaxLevel)
        {

            if (myItem._EnhanceableItem)
            // ��ȭ ���� ����������
            {
                
                //myItem.ConsumeGold();
                //��ȭ �õ� ���� ��� ���� (�Ⱦ�)
                InventoryController.Instance.ShowMyGold();
                // �κ��丮�� �ٷ� ����

                if (myItem.CheckSuccess()) // ����
                {
                    result = true;
                    myItem.GetComponent<EnhanceableItems>().myData.Level++;
                    if (myItem.GetComponent<Pickup>().item.itemName.Equals("�尩"))
                    {
                        DataController.instance.gameData.gloves.Level++;
                    }
                    else if (myItem.GetComponent<Pickup>().item.itemName.Equals("�����"))
                    {
                        DataController.instance.gameData.necklace.Level++;
                    }
                   else  if (myItem.GetComponent<Pickup>().item.itemName.Equals("�ݹ���"))
                    {
                        DataController.instance.gameData.goldring.Level++;
                    }
                    Debug.Log("����");
                    Debug.Log($"����{myItem._EnchantCost}");
                    Debug.Log($"���ݷ�{myItem._AP}");
                    Debug.Log($"Ȯ��{myItem._Possibility}");
                    

                    CheckEquipped(obj);                    
                }
                else // ����
                {
                    result = false;
                    Debug.Log("����");
                    Debug.Log($"����{myItem._EnchantCost}");
                    Debug.Log($"���ݷ�{myItem._AP}");
                    Debug.Log($"Ȯ��{myItem._Possibility}");                   
                }
            }
        }
    }
    public void CheckEquipped(GameObject obj)       //���� ������ ���¿��� ��ȭ�� ���� ��� ĳ���� ���ȿ� �ٷ� ����
    {
        string name = obj.GetComponent<Pickup>().item.itemName;
        if (DataController.instance.gameData.Kong.myUsedItems.Contains(name))       //Kong�� ���
        {
            if (obj.GetComponent<Pickup>().item.itemType.Equals(Item.ItemType.Accessory))
            {
                DataController.instance.gameData.Kong.strength = obj.GetComponent<EnhanceableItems>().myData.AP;
                Debug.Log(DataController.instance.gameData.Kong.strength);
            }
            else if (obj.GetComponent<Pickup>().item.itemType.Equals(Item.ItemType.Armor))
            {
                DataController.instance.gameData.Kong.defPower = obj.GetComponent<EnhanceableItems>().myData.AP;
            }
        }
       else if (DataController.instance.gameData.Jin.myUsedItems.Contains(name))        //Jin�� ���
        {
            if (obj.GetComponent<Pickup>().item.itemType.Equals(Item.ItemType.Accessory))
            {
                DataController.instance.gameData.Jin.strength = obj.GetComponent<EnhanceableItems>().myData.AP;
            }
            else if (obj.GetComponent<Pickup>().item.itemType.Equals(Item.ItemType.Armor))
            {
                DataController.instance.gameData.Jin.defPower = obj.GetComponent<EnhanceableItems>().myData.AP;
            }
        }
       else if (DataController.instance.gameData.Ember.myUsedItems.Contains(name))      //Ember�� ���
        {
            if (obj.GetComponent<Pickup>().item.itemType.Equals(Item.ItemType.Accessory))
            {
                DataController.instance.gameData.Ember.strength = obj.GetComponent<EnhanceableItems>().myData.AP;
            }
            else if (obj.GetComponent<Pickup>().item.itemType.Equals(Item.ItemType.Armor))
            {
                DataController.instance.gameData.Ember.defPower = obj.GetComponent<EnhanceableItems>().myData.AP;
            }
        }
    }
    IEnumerator Delay(float cool, GameObject enchantingObj, float showTime)
    {
        float coolTime = cool;
        SoundTest.instance.PlaySE("SFX_Upgrade");
        while (cool > 0.0f)
        {
            EnchantDelay = true; //Ʈ�縦 �ְ�
            cool -= Time.deltaTime;
            mySlider.value = 1f - (cool / coolTime);
            mySliderAnim.SetBool("IsWorking", true);          
            yield return null;

        }
        mySliderAnim.SetBool("IsWorking", false);
        EnchantDelay = false; //�ð��� ������
        FindMySlot(enchantingObj);

        if (result)
        {
            result = true;
            SoundTest.instance.PlaySE("SFX_Complete");
        }
        else
        {
            result = false;
            SoundTest.instance.PlaySE("SFX_Fail");
        }

        while (showTime > 0f)
        {        
            if (result)
            {
                SuccessPanel.SetActive(true);
            }
            else
            {
                FailPanel.SetActive(true);
            }
            showTime -= Time.deltaTime;
            yield return null;
        }
        SuccessPanel.SetActive(false);
        FailPanel.SetActive(false);
    }
}

//��� ��ư ������ �κ��丮 ���ڸ���
//��ȭ ��ư ������ ���� �κ��丮���� �����

// �ڷ�ƾ���� ��Ÿ��