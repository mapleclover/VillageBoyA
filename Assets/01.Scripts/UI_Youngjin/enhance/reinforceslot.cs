using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class reinforceslot : MonoBehaviour, IDropHandler
{
    public GameObject[] myIngSlots;
    public GameObject[] myInven;
    public GameObject alert;
    public TMPro.TMP_Text myMessage;

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
        switch (thisitem.GetComponent<Pickup>().item.itemName)    //�ݹ���: ���̾�&ö, �尩: ö&��, ����: ���&��
        {                                                         //���߿� ������ üũ�ؾߵ�
            case "�ݹ���":
                if (DataController.instance.gameData.savedInventory.ContainsKey("���̾Ƹ��") && DataController.instance.gameData.savedInventory.ContainsKey("ö"))
                {
                    ShowIngredients("���̾Ƹ��", 0);
                    ShowIngredients("ö", 1);
                }
                else
                {
                    myMessage.text = $"���̾Ƹ��, ö";
                    alert.SetActive(true);
                    FindMySlot(thisitem.gameObject);
                }
                break;  
            case "�尩":
                if (DataController.instance.gameData.savedInventory.ContainsKey("ö") && DataController.instance.gameData.savedInventory.ContainsKey("��"))
                {
                    ShowIngredients("ö", 0);
                    ShowIngredients("��", 1);
                }
                else
                {
                    myMessage.text = $"ö, ��";
                    alert.SetActive(true);
                    FindMySlot(thisitem.gameObject);
                }
                break;
            case "����":
                if (DataController.instance.gameData.savedInventory.ContainsKey("���") && DataController.instance.gameData.savedInventory.ContainsKey("��"))
                {
                    ShowIngredients("���", 0);
                    ShowIngredients("��", 1);
                }
                else
                {
                    myMessage.text = $"���, ��";
                    alert.SetActive(true);
                    FindMySlot(thisitem.gameObject);
                }
                break;
        }
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
                        DataController.instance.gameData.savedInventory.Remove(name);
                        //  DataController.instance.gameData.myItemCount.Remove(name);    ��ȭ ������ŭ ���ߵ�
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
    public void OnClickEnhance()                //������ �����ؾߵ�, ����Ʈ �߰�?
    {
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
        for (int i = 0; i < 14; i++)
        {
            if (myInven[i].transform.childCount == 0)
            {
                obj.transform.SetParent(myInven[i].transform);
                obj.transform.localPosition = Vector3.zero;
                DataController.instance.gameData.savedInventory[obj.GetComponent<Pickup>().item.itemName]=i;
              //  DataController.instance.gameData.myItemCount[];
                break;
            }
        }
    }

}

//��� ��ư ������ �κ��丮 ���ڸ���
//��ȭ ��ư ������ ���� �κ��丮���� �����