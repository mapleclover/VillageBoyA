using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnhancingSlot : MonoBehaviour
{
    public EnhanceableItems myItem;
    public Button EnchantButton; // button UI

    void Start()
    {
        
    }

    void Update()
    {
        if(myItem != null) { EnchantLogic(); }
        else { EnchantButton.interactable = false; }
        
    }
    public void EnchantLogic()
    {
        if (myItem._GetMaxLevel)
        {
            if (myItem._EnhanceableItem)
            // ��ȭ ���� ����������
            {
                EnchantButton.onClick.RemoveAllListeners();
                EnchantButton.interactable = true;
                EnchantButton.onClick.AddListener(
                () =>
                {
                    myItem.ConsumeGold();
                    //��ȭ �õ� ���� ��� ����
                    InventoryController.Instance.ShowMyGold();
                    // �κ��丮�� �ٷ� ����

                    if (myItem.CheckSuccess()) // ����
                    {
                        myItem.LevelUp();
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
                });
            }
        }
        else
        {
            EnchantButton.interactable = false;
        }


    }
}
