//작성자 : 유은호
//설명 : 기본적인 구매 아이템 선택후 뜨는 창
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PurchaseBox : MonoBehaviour
{
    public ShopManager myShopManager;
    public Image itemImage;
    public int SelectedCost;
    public TextMeshProUGUI TotalCostText;
    public TextMeshProUGUI selectedItemName;

    public virtual void ShowValues()
    {
        selectedItemName.text = myShopManager.itemList[myShopManager.itemIndex].itemName;
        itemImage.sprite = myShopManager.itemList[myShopManager.itemIndex].itemImage;
        SelectedCost = myShopManager.itemCost;
        TotalCostText.text = $"가격 : {SelectedCost}";
    }

    public virtual void DecideBuy()
    {
        myShopManager.BuyItem(1);
        SoundTest.instance.PlaySE("SFX_Buy");
        this.gameObject.SetActive(false);
    }

    public virtual void CancelBuy()
    {
        this.gameObject.SetActive(false);
    }
}
