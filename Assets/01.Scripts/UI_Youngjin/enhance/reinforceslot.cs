using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using static Cinemachine.DocumentationSortingAttribute;

public class reinforceslot : MonoBehaviour, IDropHandler
{
    public GameObject[] myIngSlots;
    public GameObject[] myInven;
    public GameObject alert; // ��ᰡ ���ٸ� ���� ��ᰡ �ʿ����� �˸�
    public TMPro.TMP_Text myMessage;
    public EnhanceableItems myItem;
    public TMPro.TMP_Text[] curAmount;
    public TMPro.TMP_Text[] required;
    public GameObject[] myNumbers;
    public GameObject myPanel;

    public void UpdateNumberUI(string name, int level,int index)            
    {                                                                       
        curAmount[index].text = $"{DataController.instance.gameData.myItemCount[name]}";
        required[index].text = $"{level}";
    }


    public void OnDrop(PointerEventData eventData)
    {
        PointerInfo icon = transform.GetComponentInChildren<PointerInfo>();
        Transform movingObject = eventData.pointerDrag.transform;
        if (movingObject.GetComponent<Pickup>().item.enhanceableItem.Equals(Item.EnhanceableItem.Possible))
        {
            if (icon != null&&movingObject.parent.TryGetComponent<ItemSlot>(out ItemSlot slot))
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
        int level = thisitem.GetComponent<EnhanceableItems>().myData.Level;       //���� �̷��� ���ܿ��°� ����? - ����
        switch (thisitem.GetComponent<Pickup>().item.itemName)    //�ݹ���: ���̾�&ö, �尩: ö&��, ����: ���&��
        {                                                        
            case "�ݹ���":
                if (DataController.instance.gameData.savedInventory.ContainsKey("���̾Ƹ��") && DataController.instance.gameData.savedInventory.ContainsKey("ö"))
                {
                    CompareRequirements(thisitem,"���̾Ƹ��","ö",level);

                }
                else
                {
                    NotEnough(thisitem, "���̾Ƹ��", "ö", level);                  
                }
                break;  
            case "�尩":
                if (DataController.instance.gameData.savedInventory.ContainsKey("ö") && DataController.instance.gameData.savedInventory.ContainsKey("��"))
                {
                    CompareRequirements(thisitem, "ö", "��", level);
                }
                else
                {
                    NotEnough(thisitem, "ö", "��", level);
                }
                break;
            case "����":
                if (DataController.instance.gameData.savedInventory.ContainsKey("���") && DataController.instance.gameData.savedInventory.ContainsKey("��"))
                {
                    CompareRequirements(thisitem, "���", "��", level);
                }
                else
                {
                    NotEnough(thisitem, "���", "��", level);
                }
                break;
        }
    }
    public void NotEnough(Transform thisitem, string st1, string st2, int level)
    {
        myPanel.SetActive(false);
        myMessage.text = $"{st1} {level}��, {st2} {level}��";
        alert.SetActive(true);
        FindMySlot(thisitem.gameObject);
    }
    public void CompareRequirements(Transform thisitem,string itemname1,string itemname2, int level)
    {
        if(DataController.instance.gameData.myItemCount[itemname1] <level || DataController.instance.gameData.myItemCount[itemname2] <level)
        {
            myMessage.text = $"{itemname1} {level}��, {itemname2} {level}��";
            alert.SetActive(true);
            FindMySlot(thisitem.gameObject);
            return;
        }
        ShowIngredients(itemname1, 0);
        ShowIngredients(itemname2, 1);
        UpdateNumberUI(itemname1,level,0);
        UpdateNumberUI(itemname2, level,1);
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
        myPanel.SetActive(false);

    }
    public void OnClickEnhance()                //������ �����ؾߵ�, ����Ʈ �߰�?
    {
        if (this.transform.childCount > 0)
        {
            GameObject enchantingObj = this.transform.GetChild(0).gameObject;
            EnchantLogic(enchantingObj);
            if (myIngSlots[0].transform.childCount > 0)
            {
                GameObject obj = myIngSlots[0].transform.GetChild(0).gameObject;
                CheckDestroy(obj, enchantingObj);
                myNumbers[0].SetActive(false);
            }
            if (myIngSlots[1].transform.childCount > 0)
            {
                GameObject obj = myIngSlots[1].transform.GetChild(0).gameObject;
                CheckDestroy(obj, enchantingObj);
                myNumbers[1].SetActive(false);
            }
            FindMySlot(enchantingObj);
        }
        myPanel.SetActive(false);
    }
    public void CheckDestroy(GameObject obj,GameObject enchantingObj)
    {
        int level = enchantingObj.GetComponent<EnhanceableItems>().myData.Level;
        if (DataController.instance.gameData.myItemCount[obj.GetComponent<Pickup>().item.itemName]>level)
        {
            DataController.instance.gameData.myItemCount[obj.GetComponent<Pickup>().item.itemName] -= level;
            FindMySlot(obj);
            InventoryController.Instance.ShowNumbertoUI();
        }
        else
        {
            DataController.instance.gameData.savedInventory.Remove(obj.GetComponent<Pickup>().item.itemName);
            DataController.instance.gameData.myItemCount.Remove(obj.GetComponent<Pickup>().item.itemName);
            Destroy(obj);
        }
    }
    public void FindMySlot(GameObject obj)
    {
             //������ ���� // ������ �̰� ���� ����?   �̰� �����۸��� �������ִ� �����ε�
        for (int i = 0; i < myInven.Length; i++)
        {
            if (myInven[i].transform.childCount == 0)
            {
                obj.transform.SetParent(myInven[i].transform);
                obj.transform.localPosition = Vector3.zero;
                DataController.instance.gameData.savedInventory[obj.GetComponent<Pickup>().item.itemName]=i;
                break;
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
                    myItem.ConsumeGold();
                    //��ȭ �õ� ���� ��� ����
                    InventoryController.Instance.ShowMyGold();
                    // �κ��丮�� �ٷ� ����

                    if (myItem.CheckSuccess()) // ����
                    {
                        myItem.GetComponent<EnhanceableItems>().myData.Level++; 
                        DataController.instance.gameData.gloves.Level++;
                        print($"����{DataController.instance.gameData.gloves.Level}");
                    /*if (obj.GetComponent<Pickup>().item.itemName.Equals("�尩"))
                    {
                         DataController.instance.gameData.gloves.Level++;

                    }*/
                    Debug.Log("����");

                        Debug.Log(DataController.instance.gameData.gold);
                        Debug.Log($"����{myItem._EnchantCost}");
                        Debug.Log($"���ݷ�{myItem._AP}");
                        Debug.Log($"Ȯ��{myItem._Possibility}");
                    }
                    else // ����
                    {
                        Debug.Log("����");
                        Debug.Log(DataController.instance.gameData.gold);
                        Debug.Log($"����{myItem._EnchantCost}");
                        Debug.Log($"���ݷ�{myItem._AP}");
                        Debug.Log($"Ȯ��{myItem._Possibility}");

                    }
            }
        }


    }

}

//��� ��ư ������ �κ��丮 ���ڸ���
//��ȭ ��ư ������ ���� �κ��丮���� �����

// �ڷ�ƾ���� ��Ÿ��