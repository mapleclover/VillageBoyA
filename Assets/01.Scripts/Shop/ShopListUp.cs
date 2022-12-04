//작성자 : 유은호
//설명 : 샵매니저에 있는 아이템만큼 버튼 생성하고 버튼에 item을 달아주는 스크립트
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
