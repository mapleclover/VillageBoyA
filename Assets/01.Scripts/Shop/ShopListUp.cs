using UnityEngine;

public class ShopListUp : MonoBehaviour
{
    public ShopManager myShopManager;
    public GameObject ButtonPrefabs;
    public Transform ContentPanel;

    private void OnEnable()
    {
        for (int i = 0; i < myShopManager.itemList.Length; i++)
        {
            GameObject obj = Instantiate(ButtonPrefabs, ContentPanel);
            obj.GetComponent<ShopButton>().myItem = myShopManager.itemList[i];
            obj.GetComponent<ShopButton>().showItem();
        }
    }
}
