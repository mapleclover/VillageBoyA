using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using static Cinemachine.DocumentationSortingAttribute;
using Unity.VisualScripting;
using static MirzaBeig.ParticleSystems.Demos.DemoManager;

public class reinforceslot : MonoBehaviour, IDropHandler        //마우스 우클릭하면 돈 100원씩 들어옵니다...-영진
{
    //public Transform myInven2;
    public GameObject[] myIngSlots;
    public GameObject[] myInven;
    public GameObject alert; // 재료가 없다면 무슨 재료가 필요한지 알림
    public TMPro.TMP_Text myMessage;
    public EnhanceableItems myItem;
    public TMPro.TMP_Text[] curAmount;
    public TMPro.TMP_Text[] required;
    public GameObject[] myNumbers;
    public GameObject myPanel;

    public bool EnchantDelay = false;
    public bool result = false;
    public Slider mySlider;
    public Animator mySliderAnim;
    public GameObject SuccessPanel;
    public GameObject FailPanel;



    public void UpdateNumberUI(string name, int level,int index)            
    {                                                                       
        curAmount[index].text = $"{DataController.instance.gameData.myItemCount[name]}";        //가지고 있는 개수와 몇개 필요한지 표시
        required[index].text = $"{level}";
    }


    public void OnDrop(PointerEventData eventData)
    {

        PointerInfo icon = transform.GetComponentInChildren<PointerInfo>();
        Transform movingObject = eventData.pointerDrag.transform;
        myItem = movingObject.GetComponent<EnhanceableItems>(); // 드래그 되자 마자 바인딩 되도록 바꿔야함
        if (myItem._GetMaxLevel)
        {
            if (movingObject.GetComponent<Pickup>().item.enhanceableItem.Equals(Item.EnhanceableItem.Possible))
            {
                if (icon != null && movingObject.parent.TryGetComponent<ItemSlot>(out ItemSlot slot))
                {
                    slot.SetChildren(icon.transform);
                    OnClickCancel();
                }
                else if (icon != null && movingObject.parent.TryGetComponent<reinforceslot>(out reinforceslot Slot))
                {
                    Slot.SetChild(icon.transform);
                    OnClickCancel();
                }
                movingObject.SetParent(transform);
                movingObject.localPosition = Vector3.zero;
                EnhancementController.inst.rectTransform.SetAsLastSibling();
                CheckIngredients(movingObject);                                  //강화 재료 확인
            }
            else return;
        }
        else
        {
            alert.SetActive(true);
            myMessage.text = $"강화 가능 레벨을 \n 초과 하였습니다.";
            
        }
            
        
        
    }

    public void SetChild(Transform child)
    {
        child.SetParent(transform);
        child.localPosition = Vector3.zero;
    }
    public void CheckIngredients(Transform thisitem)
    {
        int level = thisitem.GetComponent<EnhanceableItems>().myData.Level;             //이 아이템의 레벨
        int cost = thisitem.GetComponent<EnhanceableItems>().myData.EnchantCost;       //이 아이템의 레벨에 따른 가격
        switch (thisitem.GetComponent<Pickup>().item.itemName)    //금반지: 다이아&철, 장갑: 철&별, 방패: 사과&별
        {
            case "금반지":
                if (DataController.instance.gameData.savedInventory.ContainsKey("다이아몬드") && DataController.instance.gameData.savedInventory.ContainsKey("금")&&DataController.instance.gameData.gold>=cost)
                {
                    CompareRequirements(thisitem, "다이아몬드", "금", level);     //인벤토리에 있을 경우 개수 확인하는 함수 호출

                }
                else
                {
                    NotEnough(thisitem, "다이아몬드", "금", level, cost);         //인벤토리에 없을 경우 필요한 재료를 알리는 알림창
                }
                break;
            case "장갑":
                if (DataController.instance.gameData.savedInventory.ContainsKey("눈물") && DataController.instance.gameData.savedInventory.ContainsKey("스크롤") && DataController.instance.gameData.gold >= cost)
                {
                    CompareRequirements(thisitem, "눈물", "스크롤", level);
                }
                else
                {
                    NotEnough(thisitem, "눈물", "스크롤", level, cost);
                }
                break;
            case "목걸이":
                if (DataController.instance.gameData.savedInventory.ContainsKey("별") && DataController.instance.gameData.savedInventory.ContainsKey("철") && DataController.instance.gameData.gold >= cost)
                {
                    CompareRequirements(thisitem, "별", "철", level);
                }
                else
                {
                    NotEnough(thisitem, "별", "철", level, cost);
                }
                break;
        }

    }
    public void NotEnough(Transform thisitem, string st1, string st2, int level,int cost)       //필요한 재료를 알리는 경고창
    {
        myPanel.SetActive(false);
        myMessage.text = $"{st1} {level}개\n\n {st2} {level}개\n\n {cost}골드";
        alert.SetActive(true);
        FindMySlot(thisitem.gameObject);
    }
    public void CompareRequirements(Transform thisitem,string itemname1,string itemname2, int level)
    {
        if(DataController.instance.gameData.myItemCount[itemname1] <level || DataController.instance.gameData.myItemCount[itemname2] <level)
        {
            myMessage.text = $"{itemname1} {level}개\n\n {itemname2} {level}개\n\n";
            alert.SetActive(true);
            FindMySlot(thisitem.gameObject);            //만약 필요한 개수만큼 없을 경우 알려주는 경고창
            return;
        }
        ShowIngredients(itemname1, 0);      //있을 경우
        ShowIngredients(itemname2, 1);      //필요한 재료를 강화창 슬롯으로 가져옴
        UpdateNumberUI(itemname1,level,0);
        UpdateNumberUI(itemname2, level,1);     //현재 몇 개 있는지와 몇 개가 필요한지를 나타냄
        myNumbers[0].SetActive(true);
        myNumbers[1].SetActive(true);       
        myPanel.SetActive(true);
    }

    public void ShowIngredients(string name,int index)
    {
        for(int i = 0; i < 14; i++)
        {
            if (myInven[i].transform.childCount > 0)
            {
                GameObject thisIngred = myInven[i].transform.GetChild(0).gameObject;
                if (thisIngred != null&&name!=null)
                {
                    if (!thisIngred.GetComponent<Pickup>().item.itemName.Equals(name)) continue;
                    else
                    {
                        thisIngred.transform.SetParent(myIngSlots[index].transform);
                        thisIngred.transform.localPosition = Vector3.zero;
                        

                        break;
                    }
                }
            }
        }
    }
    public void OnClickCancel()     //취소를 눌렀을 경우
    {
        if (transform.childCount > 0)
        {
            GameObject obj = transform.GetChild(0).gameObject;
            FindMySlot(obj);
        }
        if (myIngSlots[0].transform.childCount > 0) 
        {
            GameObject ing1 = myIngSlots[0].transform.GetChild(0).gameObject;
            FindMySlot(ing1);
        }
        if (myIngSlots[1].transform.childCount > 0)
        {
            GameObject ing2 = myIngSlots[1].transform.GetChild(0).gameObject;
            FindMySlot(ing2);                                                   //인벤토리로 다시 이동
        }
        myPanel.SetActive(false);
        myNumbers[0].SetActive(false);
        myNumbers[1].SetActive(false);

    }
    public void OnClickEnhance()               //강화를 눌렀을 경우
    {
        if(!EnchantDelay)
        {
            if (this.transform.childCount > 0)
            {
                GameObject enchantingObj = this.transform.GetChild(0).gameObject;
                if (myIngSlots[0].transform.childCount > 0)
                {
                    GameObject obj = myIngSlots[0].transform.GetChild(0).gameObject;
                    CheckDestroy(obj, enchantingObj);       //개수를 비교해 destroy해야하는지 인벤토리에 남아야 하는지 정함
                    myNumbers[0].SetActive(false);
                }
                if (myIngSlots[1].transform.childCount > 0)
                {
                    GameObject obj2 = myIngSlots[1].transform.GetChild(0).gameObject;
                    CheckDestroy(obj2, enchantingObj);      
                    myNumbers[1].SetActive(false);
                }
                DataController.instance.gameData.gold -= enchantingObj.GetComponent<EnhanceableItems>().myData.EnchantCost;
                InventoryController.Instance.ShowMyGold();          //골드에 반영
                EnchantLogic(enchantingObj);            
                StartCoroutine(Delay(3f, enchantingObj, 1.5f));               
                myPanel.SetActive(false);                
                //FindMySlot()함수 Delay 코루틴 안에 넣음
            }
        }
        else
        {
            myPanel.SetActive(false);
        }
        
    }
    public void CheckDestroy(GameObject obj, GameObject enchantingObj)
    {
        int level = enchantingObj.GetComponent<EnhanceableItems>().myData.Level;
        if (DataController.instance.gameData.myItemCount[obj.GetComponent<Pickup>().item.itemName]>level)
        {
            DataController.instance.gameData.myItemCount[obj.GetComponent<Pickup>().item.itemName] -= level;
            FindMySlot(obj);        
            InventoryController.Instance.ShowNumbertoUI();          //내가 갖고 있는 개수가 필요 개수보다 많을 경우 소모 후 인벤으로 이동
        }
        else
        {
            DataController.instance.gameData.savedInventory.Remove(obj.GetComponent<Pickup>().item.itemName);
            DataController.instance.gameData.myItemCount.Remove(obj.GetComponent<Pickup>().item.itemName);
            Destroy(obj);       //같을 경우 destroy
        }
    }
    public void FindMySlot(GameObject obj)      //인벤토리에서 비어있는 슬롯을 찾아감
    {
        if(!EnchantDelay)
        {
            //레벨과 연동 // 영진아 이거 레벨 뭐야? //이거 아이템마다 가지고있는 레벨인뎅 // 그렇구만
            for (int i = 0; i < myInven.Length; i++)
            {
                if (myInven[i].transform.childCount == 0)
                {
                    obj.transform.SetParent(myInven[i].transform);      //
                    obj.transform.localPosition = Vector3.zero;
                    DataController.instance.gameData.savedInventory[obj.GetComponent<Pickup>().item.itemName] = i;
                    break;
                }
            }
        }
    }

    public void EnchantLogic(GameObject obj)
    {
        myItem = obj.GetComponent<EnhanceableItems>(); // 드래그 되자 마자 바인딩 되도록 바꿔야함
        if (myItem._GetMaxLevel)
        {

            if (myItem._EnhanceableItem)
            // 강화 가능 아이템인지
            {
                
                //myItem.ConsumeGold();
                //강화 시도 마다 골드 제거 (안씀)
                InventoryController.Instance.ShowMyGold();
                // 인벤토리에 바로 적용

                if (myItem.CheckSuccess()) // 성공
                {
                    result = true;
                    myItem.GetComponent<EnhanceableItems>().myData.Level++;
                    if (myItem.GetComponent<Pickup>().item.itemName.Equals("장갑"))
                    {
                        DataController.instance.gameData.gloves.Level++;
                    }
                    else if (myItem.GetComponent<Pickup>().item.itemName.Equals("목걸이"))
                    {
                        DataController.instance.gameData.necklace.Level++;
                    }
                   else  if (myItem.GetComponent<Pickup>().item.itemName.Equals("금반지"))
                    {
                        DataController.instance.gameData.goldring.Level++;
                    }
                    Debug.Log("성공");
                    Debug.Log($"가격{myItem._EnchantCost}");
                    Debug.Log($"공격력{myItem._AP}");
                    Debug.Log($"확률{myItem._Possibility}");
                    

                    CheckEquipped(obj);                    
                }
                else // 실패
                {
                    result = false;
                    Debug.Log("실패");
                    Debug.Log($"가격{myItem._EnchantCost}");
                    Debug.Log($"공격력{myItem._AP}");
                    Debug.Log($"확률{myItem._Possibility}");                   
                }
            }
        }
    }
    public void CheckEquipped(GameObject obj)       //만약 장착된 상태에서 강화를 했을 경우 캐릭터 스탯에 바로 적용
    {
        string name = obj.GetComponent<Pickup>().item.itemName;
        if (DataController.instance.gameData.Kong.myUsedItems.Contains(name))       //Kong일 경우
        {
            if (obj.GetComponent<Pickup>().item.itemType.Equals(Item.ItemType.Accessory))
            {
                DataController.instance.gameData.Kong.strength = obj.GetComponent<EnhanceableItems>().myData.AP;
                Debug.Log(DataController.instance.gameData.Kong.strength);
            }
            else if (obj.GetComponent<Pickup>().item.itemType.Equals(Item.ItemType.Armor))
            {
                DataController.instance.gameData.Kong.defPower = obj.GetComponent<EnhanceableItems>().myData.AP;
            }
        }
       else if (DataController.instance.gameData.Jin.myUsedItems.Contains(name))        //Jin일 경우
        {
            if (obj.GetComponent<Pickup>().item.itemType.Equals(Item.ItemType.Accessory))
            {
                DataController.instance.gameData.Jin.strength = obj.GetComponent<EnhanceableItems>().myData.AP;
            }
            else if (obj.GetComponent<Pickup>().item.itemType.Equals(Item.ItemType.Armor))
            {
                DataController.instance.gameData.Jin.defPower = obj.GetComponent<EnhanceableItems>().myData.AP;
            }
        }
       else if (DataController.instance.gameData.Ember.myUsedItems.Contains(name))      //Ember일 경우
        {
            if (obj.GetComponent<Pickup>().item.itemType.Equals(Item.ItemType.Accessory))
            {
                DataController.instance.gameData.Ember.strength = obj.GetComponent<EnhanceableItems>().myData.AP;
            }
            else if (obj.GetComponent<Pickup>().item.itemType.Equals(Item.ItemType.Armor))
            {
                DataController.instance.gameData.Ember.defPower = obj.GetComponent<EnhanceableItems>().myData.AP;
            }
        }
    }
    IEnumerator Delay(float cool, GameObject enchantingObj, float showTime)
    {
        float coolTime = cool;
        SoundTest.instance.PlaySE("SFX_Upgrade");
        while (cool > 0.0f)
        {
            EnchantDelay = true; //트루를 주고
            cool -= Time.deltaTime;
            mySlider.value = 1f - (cool / coolTime);
            mySliderAnim.SetBool("IsWorking", true);          
            yield return null;

        }
        mySliderAnim.SetBool("IsWorking", false);
        EnchantDelay = false; //시간이 끝나면
        FindMySlot(enchantingObj);

        if (result)
        {
            result = true;
            SoundTest.instance.PlaySE("SFX_Complete");
        }
        else
        {
            result = false;
            SoundTest.instance.PlaySE("SFX_Fail");
        }

        while (showTime > 0f)
        {        
            if (result)
            {
                SuccessPanel.SetActive(true);
            }
            else
            {
                FailPanel.SetActive(true);
            }
            showTime -= Time.deltaTime;
            yield return null;
        }
        SuccessPanel.SetActive(false);
        FailPanel.SetActive(false);
    }
}

//취소 버튼 누르면 인벤토리 제자리로
//강화 버튼 누르면 재료는 인벤토리에서 사라짐

// 코루틴으로 쿨타임