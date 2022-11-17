using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopButton : MonoBehaviour
{
    public Item myItem;
    public ShopManager shopManager;
    public Image myItemImage;
    public TextMeshProUGUI myPriceText;
    public TextMeshProUGUI myItemNameText;
    
    // Update is called once per frame
    public void showItem()//버튼이 생성되면서 연동된 Item 정보로 버튼 UI생성
    {
        shopManager = GameObject.Find("ShopManager").GetComponent<ShopManager>();
        myPriceText.text = myItem.itemPrice.ToString();
        myItemNameText.text = myItem.itemName;
        myItemImage.sprite = myItem.itemImage;
    }

    void Update()//소유금액이 아이템 1개의 가격보다 적으면 버튼 비활성화
    {
        if (myItem.itemPrice > shopManager.GoldAmount)
        {
            this.GetComponent<Button>().interactable = false;
        }
        else
        {
            this.GetComponent<Button>().interactable = true;
        }
        if (DataController.instance.gameData.savedInventory.ContainsKey(myItem.itemName))
        {
            if(myItem.itemType!=Item.NpcType.Ingredient)
            this.GetComponent<Button>().interactable = false;
        }
    }

    void OnClick()
    {
        shopManager.DecideAmountToBuy(myItem);
    }
    private void OnEnable()
    {
        GetComponent<Button>().onClick.AddListener(OnClick);
    }

    private void OnDisable()
    {
        Destroy(this.gameObject);
    }
}
