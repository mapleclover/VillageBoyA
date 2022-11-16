using UnityEngine;
using TMPro;

public class ShopManager : MonoBehaviour
{
    //저장 데이터에서 소유 골드 불러올 때 수정해야함
    public TextMeshProUGUI GoldText;
    public int GoldAmount = 1000;
    //UI패널 
    public GameObject PurchasePanel;
    public GameObject MainPanel;
    //상점아이템판매리스트
    public Item[] itemList;
    //아이템선택되었을때 무슨아이템이 선택되었는지, 가격은 얼마인지 담을 변수
    public int itemCost;
    public int itemIndex;

    private void OpenUpShop()
    {
        GoldAmount = DataController.instance.gameData.gold;
        GoldText.text = $"소유 골드 : {GoldAmount}";
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (PurchasePanel.activeSelf)
            {
                PurchasePanel.SetActive(false);
            }
            else if (MainPanel.activeSelf)
            {
                MainPanel.SetActive(false);
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!MainPanel.activeSelf)
            {
                MainPanel.SetActive(true);
                OpenUpShop();
            }
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
        PurchasePanel.SetActive(true);
        PurchasePanel.GetComponent<PurchaseBox>().ShowValues();
    }

    public void BuyItem(int cost)
    {
        GoldAmount -= cost;
        GoldText.text = "소유 골드 : " + GoldAmount.ToString();
        //여기에 인벤토리에 산 아이템 추가하는 기능 추가
        //Add(itemList[itemIndex]);
    }
}