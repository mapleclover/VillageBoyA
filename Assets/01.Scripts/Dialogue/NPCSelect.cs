using UnityEngine;

public class NPCSelect : MonoBehaviour
{
    public Item[] SellItemList;
    public ShopManager myShopManager;
    public EnhancementController myEnhancement;

    public void OnShopButtonPressed()
    {
        myShopManager.itemList = SellItemList;
        myShopManager.OpenUpShop();
    }

    public void OnEnchantButtonPressed()
    {
        myEnhancement.OpenUpEnhancement();
    }
}