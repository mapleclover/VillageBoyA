//작성자 : 전정우
//설명 :

using System;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using static HPBarInField;



public class EnhancementController : MonoBehaviour
{
    public GameObject myUI;
    public RectTransform myInventory;
    public GameObject setMyInventory;

    //public GameObject myMaterial = null;

    // Start is called before the first frame update
    void Start()
    {
        myInventory = myInventory.GetComponent<RectTransform>();
        myInventory.localPosition = new Vector2(0f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        /*if (myMaterial[0].transform.childCount > 0 && myMaterial[1].transform.childCount > 0)
        {

        }
        myMateris = myMaterial[0].transform.GetChild(0).gameObject;
        //GetComponent<Pickup>().item.value;
        myMaterial[1] = transform.GetChild(0).gameObject;*/

       /* if (myItems != null)
        {
            EnchantLogic();
            EnchantButton.interactable = true;
        }
        else
        {
            myItems = null;
            EnchantButton.interactable = false;
        }*/


        if (Input.GetKeyDown(KeyCode.C))
        {
            myInventory.localPosition = new Vector2(200f, 0f);
            print("1");
            myUI.SetActive(true);
            setMyInventory.SetActive(true);
            
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            myInventory.localPosition = new Vector2(0f, 0f);
            print("2");
            myUI.SetActive(false);
            setMyInventory.SetActive(false);
            
        }
    }


}