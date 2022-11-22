using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class reinforceslot : MonoBehaviour, IDropHandler
{
    public GameObject[] myIngSlots;
    public GameObject myInven;
    public void OnDrop(PointerEventData eventData)
    {
        PointerInfo icon = transform.GetComponentInChildren<PointerInfo>();
        Transform movingObject = eventData.pointerDrag.transform;
        if (movingObject.GetComponent<Pickup>().item.enhanceableItem.Equals(Item.EnhanceableItem.Possible))
        {
            if (icon != null)
            {
                reinforceslot slot = movingObject.parent.GetComponent<reinforceslot>();
                slot.SetChild(icon.transform);
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
                break;  
            case "�尩":
                if (DataController.instance.gameData.savedInventory.ContainsKey("ö") && DataController.instance.gameData.savedInventory.ContainsKey("��"))
                {
                    ShowIngredients("ö", 0);
                    ShowIngredients("��", 1);
                }
                break;
            case "����":
                if (DataController.instance.gameData.savedInventory.ContainsKey("���") && DataController.instance.gameData.savedInventory.ContainsKey("��"))
                {
                    ShowIngredients("���", 0);
                    ShowIngredients("��", 1);
                }
                break;
        }
    }
    public void ShowIngredients(string name,int index)
    {
        for(int i = 0; i < 14; i++)
        {
            if (myInven.transform.GetChild(i).transform.childCount > 0)
            {
                GameObject thisIngred = myInven.transform.GetChild(i).transform.GetChild(0).gameObject;
                if (thisIngred.GetComponent<Pickup>().item.itemName.Equals(name))
                {
                    thisIngred.transform.SetParent(myIngSlots[index].transform);
                    thisIngred.transform.localPosition = Vector3.zero;

                    break;
                }
            }
        }
    }
}

//��� ��ư ������ �κ��丮 ���ڸ���
//��ȭ ��ư ������ ���� �κ��丮���� �����