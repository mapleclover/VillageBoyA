using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnhancementController : PointerInfo
{
    public GameObject myUI;

    public RectTransform myInventory;
    public GameObject setMyInventory;
    public bool onOff = false;
    // Start is called before the first frame update
    void Start()
    {
        myInventory = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.C))
        {
            print("1");

            myUI.SetActive(true);
            setMyInventory.SetActive(true);
            onOff = true;
        }

            myInventory.GetComponent<RectTransform>().right = myUI.transform.right;
        



        if (Input.GetKeyDown(KeyCode.Escape))
        {
            print("2");
            myUI.SetActive(false);
            setMyInventory.SetActive(false);
            onOff = false;
        }
    }
}
