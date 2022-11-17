using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuantityPurchaseBox : PurchaseBox
{
    public Slider QuantitySlider;
    public TMP_InputField QuantityText;
    private int totalCost;

    public override void ShowValues()
    {
        selectedItemName.text = myShopManager.itemList[myShopManager.itemIndex].itemName;
        SelectedCost = myShopManager.itemCost;

        itemImage.sprite = myShopManager.itemList[myShopManager.itemIndex].itemImage;
        QuantitySlider.value = 1;
        QuantityText.text = QuantitySlider.value.ToString();
        TotalCostText.text = $"총 가격 : {SelectedCost}";
        QuantitySlider.maxValue = myShopManager.GoldAmount / SelectedCost;
    }

    public void OnSliderValueChange()
    {
        QuantityText.text = QuantitySlider.value.ToString();
        CalculateTotalCost();
    }

    public void OnInputFieldValueChange()
    {
        if (int.TryParse(QuantityText.text, out int result))
        {
            if(result > QuantitySlider.maxValue)
            {
                QuantityText.text = QuantitySlider.maxValue.ToString();
                result = (int)QuantitySlider.maxValue;
            }
            QuantitySlider.value = result;
        }
        else
        {
            QuantityText.text = "1";
            QuantitySlider.value = 1;
        }
        CalculateTotalCost();
    }

    public void CalculateTotalCost()
    {
        totalCost = (int)QuantitySlider.value * SelectedCost;
        TotalCostText.text = $"총 가격 : {totalCost}";
    }

    public override void DecideBuy()
    {
        myShopManager.BuyItem((int)QuantitySlider.value);
        this.gameObject.SetActive(false);
    }

    public override void CancelBuy()
    {
        this.gameObject.SetActive(false);
    }
}
