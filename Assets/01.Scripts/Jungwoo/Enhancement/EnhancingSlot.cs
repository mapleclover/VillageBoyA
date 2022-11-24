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
            // 강화 가능 아이템인지
            {
                EnchantButton.onClick.RemoveAllListeners();
                EnchantButton.interactable = true;
                EnchantButton.onClick.AddListener(
                () =>
                {
                    myItem.ConsumeGold();
                    //강화 시도 마다 골드 제거
                    InventoryController.Instance.ShowMyGold();
                    // 인벤토리에 바로 적용

                    if (myItem.CheckSuccess()) // 성공
                    {
                        myItem.LevelUp();
                        Debug.Log("성공");

                        Debug.Log(DataController.instance.gameData.gold);
                        Debug.Log($"가격{myItem._EnchantCost}");
                        Debug.Log($"공격력{myItem._AP}");
                        Debug.Log($"확률{myItem._Possibility}");
                    }
                    else // 실패
                    {
                        Debug.Log("실패");
                        Debug.Log(DataController.instance.gameData.gold);
                        Debug.Log($"가격{myItem._EnchantCost}");
                        Debug.Log($"공격력{myItem._AP}");
                        Debug.Log($"확률{myItem._Possibility}");

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
