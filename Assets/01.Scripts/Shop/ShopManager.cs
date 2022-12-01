//작성자 : 유은호
//설명 : 게임데이터의 인벤토리와 재화를 연동하여 표시하고 구매시 인벤토리에 아이템을 넣으며 소유골드는 사용만큼 차감

using TMPro;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    //저장 데이터에서 소유 골드 불러올 때 수정해야함
    public TextMeshProUGUI GoldText;
    public int GoldAmount = 0;
    public static bool isAction = false;

    //UI패널 
    public GameObject PurchasePanel;
    public GameObject EquipPurchasePanel;
    public GameObject MainPanel;

    public GameObject myInventory;

    //상점아이템판매리스트
    public Item[] itemList;

    //아이템선택되었을때 무슨아이템이 선택되었는지, 가격은 얼마인지 담을 변수
    public int itemCost;
    public int itemIndex;

    public void OpenUpShop()
    {
        GoldAmount = DataController.instance.gameData.gold;
        GoldText.text = $"소유 골드 : {GoldAmount}";
    }

    private void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (PurchasePanel.activeSelf)
            {
                PurchasePanel.SetActive(false);
            }
            else if (EquipPurchasePanel.activeSelf)
            {
                EquipPurchasePanel.SetActive(false);
            }
            else if (MainPanel.activeSelf)
            {
                MainPanel.SetActive(false);
                isAction = false;
            }
            
        }

        if (isAction)
        {
            MainPanel.SetActive(true);
            myInventory.SetActive(false);
            OpenUpShop();
        }
    }

    public void DecideAmountToBuy(Item selectedItem)
    {
        for (int i = 0; i < itemList.Length; i++)
        {
            if (itemList[i] == selectedItem)
            {
                itemIndex = i;
                break;
            }
        }

        itemCost = itemList[itemIndex].itemPrice;

        if (itemList[itemIndex].itemType != Item.ItemType.Ingredient)
        {
            EquipPurchasePanel.SetActive(true);
            EquipPurchasePanel.GetComponent<PurchaseBox>().ShowValues();
        }
        else
        {
            PurchasePanel.SetActive(true);
            PurchasePanel.GetComponent<PurchaseBox>().ShowValues();
        }
    }

    public void BuyItem(int count)
    {
        GoldText.text = "소유 골드 : " + GoldAmount.ToString();
        GoldAmount -= count * itemList[itemIndex].itemPrice;
        GoldText.text = $"소유 골드 : {GoldAmount}";
        //여기에 인벤토리에 산 아이템만큼 추가하는 기능 추가
        //Add(itemList[itemIndex] * count);
        DataController.instance.gameData.gold = GoldAmount;
        InventoryController.Instance.GetItem(itemList[itemIndex].itemPrefab);
        int index = DataController.instance.gameData.itemList.IndexOf(itemList[itemIndex].itemName);
        DataController.instance.gameData.itemCount[index] += count - 1;
    }
}