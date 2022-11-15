using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopManager : MonoBehaviour
{
    public TextMeshProUGUI GoldText;
    public int GoldAmount = 0;
    public GameObject PurchasePanel;
    public GameObject MainPanel;
    public int PricePerOne;

    private void Start()
    {
        GoldText.text = "소유 골드 : " + GoldAmount.ToString();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (PurchasePanel.activeSelf)
            {
                PurchasePanel.SetActive(false);
            }
            else if(MainPanel.activeSelf)
            {
                MainPanel.SetActive(false);
            }
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!MainPanel.activeSelf)
            {
                MainPanel.SetActive(true);
            }
        }
    }

    public void DecideAmountToBuy(int price)
    {
        PricePerOne = price;
        PurchasePanel.SetActive(true);
    }

    public void BuyItem(int cost)
    {
        GoldAmount -= cost;
        GoldText.text = "소유 골드 : " + GoldAmount.ToString();
    }
}
