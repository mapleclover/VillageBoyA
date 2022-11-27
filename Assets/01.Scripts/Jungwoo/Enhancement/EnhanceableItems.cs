//작성자 : 전정우
//설명 :
using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Random = UnityEngine.Random;





public class EnhanceableItems : MonoBehaviour, IDragHandler,IBeginDragHandler, IEndDragHandler
{
    public SaveItemData myData;
    //public RawImage[] myIng;
    // public SaveItemData myData;

    public int _AP
    {
        get => myData.AP;
    }
    public int _EnchantCost
    {
        get => myData.EnchantCost;
    }
    public float _Possibility
    {
        get => myData.Possibility;
    }
    public bool _GetMaxLevel
    {
        get => myData.GetMaxLevel;
    }
    public bool _EnhanceableItem
    {
        get => myData.GetEnhanceableItem;
    }

 
    public void ConsumeGold()
    {
        DataController.instance.gameData.gold -= myData.EnchantCost;
    }

    public bool CheckSuccess()
    {
        float rnd = Random.Range(0, 100);
        if (rnd <= _Possibility)
        {
            Debug.Log($"랜덤숫자{rnd}");
            return true;
        }
        Debug.Log($"랜덤숫자{rnd}");

        return false;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        //myIng[0].gameObject.SetActive(true);
        //myIng[1].gameObject.SetActive(true);
    }

    public void OnDrag(PointerEventData eventData)
    {

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //myIng[0].gameObject.SetActive(false);
        //myIng[1].gameObject.SetActive(false);
    }
}