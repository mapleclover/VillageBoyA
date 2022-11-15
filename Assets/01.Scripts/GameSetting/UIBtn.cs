using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class UIBtn : MonoBehaviour
{
    public GameObject questBtn;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            questBtn.gameObject.SetActive(!questBtn.gameObject.activeSelf);
        }
    }
}
