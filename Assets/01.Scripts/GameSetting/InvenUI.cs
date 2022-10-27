using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class InvenUI : MonoBehaviour
{
    public GameObject invenBtn;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            invenBtn.gameObject.SetActive(!invenBtn.gameObject.activeSelf);
        }
    }
}
