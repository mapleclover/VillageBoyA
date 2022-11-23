//작성자 : 전정우
//설명 :

using System;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using static HPBarInField;

[Serializable]
public struct SaveItemData
{ 
    public Item orgData;
    public int Level;

    public int AP
    {
        get => orgData.GetAP(Level);
    }
    public int EnchantCost
    {
        get => orgData.GetEnchantCost(Level);
    }
    public float Possibility
    {
        get => orgData.GetPossibility(Level);
    }
    public bool GetMaxLevel
    {// 현재 최대 레벨보다 낮다면 업그레이드가 가능하므로
        get => Level < orgData.GetMaxLevel(); // 작다면 트루
    }

    // 현재 문제점
    // 완료 1. 골드를 소모하지 않음 (재료)
    // 2. orgData를 인벤토리에서 끌어왔을 때 자동으로 바인딩 되도록 바꿔야 함.
    // 완료 3. Level 배열 최댓값이 아직 잡히지 않음 (집에가서 해)
    // 4. 강화 후 데이터가 출력되는 툴팁 필요 그리고 캐릭터 스텟에 적용@@@@@@
    // 5. 스크립터블에서 필요 재료 혹은 재화를 연동 해야함. >> 슬롯에 자동추가 >> 내 아이템 리스트에서
    // 완료 6. 강화 버튼 함수를 따로 만들어야 함
    // 완료 7. 강화 가능 유무 로직을 만들어야 함 (아에 강화 슬롯에 올라가지 않거나 올라가면 버튼 UI가 false)

    // 재료강화 >> 텍스트로



    // 일단 여기에 스트럭트 구조를 만들었지만 다 모였을 때 EnhanceableItems.cs 를
    // 스크립터블로 만들고 거기에서 아이템데이터 구조를 만들 수 있도록 해야할 것 같음
    // 왜냐면 아이템 전부가 이 스크립트 자식으로 만드는 것 보다 강화 가능 아이템에
    // EnhanceableItems.cs 를 추가하는게 저렴할 것 같음
}


public class EnhancementController : MonoBehaviour
{
    public GameObject myUI;
    public RectTransform myInventory;
    public GameObject setMyInventory;
    public bool onOff = false;

    public Button EnchantButton; // button UI

    public GameObject mySlot;
    public Item myItems;

    public GameObject myMaterial = null;



    [SerializeField] SaveItemData myData;
    

    public int AP
    {
        get => myData.AP;
    }
    public int EnchantCost
    {
        get => myData.EnchantCost;
    }
    public float Possibility
    {
        get => myData.Possibility;
    }
    public bool GetMaxLevel
    {
        get => myData.GetMaxLevel;
    }


    // Start is called before the first frame update
    void Start()
    {
        myInventory = myInventory.GetComponent<RectTransform>();
        myInventory.localPosition = new Vector2(0f, 0f);

        /*  public int ComposeItems()
          {
              if()
              return 1;
          }*/

    }

    // Update is called once per frame
    void Update()
    {
        if (myMaterial[0].transform.childCount > 0 && myMaterial[1].transform.childCount > 0)
        {

        }
        myMaterisdfww = myMaterial[0].transform.GetChild(0).gameObject;
        //GetComponent<Pickup>().item.value;
        myMaterial[1] = transform.GetChild(0).gameObject;


        EnchantLogic();

        if (Input.GetKeyDown(KeyCode.C))
        {

            myInventory.localPosition = new Vector2(200f, 0f);
            print("1");
            myUI.SetActive(true);
            setMyInventory.SetActive(true);
            onOff = true; // 나중에 사용할 것 같음
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            myInventory.localPosition = new Vector2(0f, 0f);
            print("2");
            myUI.SetActive(false);
            setMyInventory.SetActive(false);
            onOff = false;
        }
    }

    public void EnchantLogic()
    {
        if (GetMaxLevel)
        {
            if (myItems.enhanceableItem == Item.EnhanceableItem.Possible && EnchantButton)
            // 강화 가능 아이템인지
            {
                EnchantButton.onClick.RemoveAllListeners();
                EnchantButton.interactable = true;
                EnchantButton.onClick.AddListener(
                () =>
                {
                    DataController.instance.gameData.gold -= myItems.EnchantCost[myData.Level - 1];
                    //강화 시도 마다 골드 제거
                    InventoryController.Instance.ShowMyGold();
                    // 인벤토리에 바로 적용

                    if (myItems.CheckSuccess(myData.Level)) // 성공
                    {
                        myData.Level++;
                        Debug.Log("성공");

                        Debug.Log(DataController.instance.gameData.gold);
                        Debug.Log(myItems.EnchantCost[myData.Level - 1]);
                        Debug.Log(myItems.AP[myData.Level - 1]);
                        Debug.Log(myItems.Possibility[myData.Level - 1]);
                    }
                    else // 실패
                    {
                        Debug.Log("실패");
                        Debug.Log(DataController.instance.gameData.gold);
                        Debug.Log(myItems.EnchantCost[myData.Level - 1]);
                        Debug.Log(myItems.AP[myData.Level - 1]);
                        Debug.Log(myItems.Possibility[myData.Level - 1]);

                        // Destroy(슬롯안에.gameObject) 실패 시 아이템 파괴 로직 추가
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