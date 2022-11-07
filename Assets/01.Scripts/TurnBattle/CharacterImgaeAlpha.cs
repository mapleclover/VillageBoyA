using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CharacterImgaeAlpha : MonoBehaviour
{
    
    Color color;
    public bool DieCheck = false;
    void Start()
    {
        color = this.GetComponent<Image>().color;


    }

    // Update is called once per frame
    void Update()
    {

        if (TurnBattle.Inst.myState != TurnBattle.State.Choice || DieCheck)
        {
            Alpha05();
        }
        else
        {
            Alpha10();
        }
    }
    public void Alpha05()
    {
        color.a = 0.5f;
        this.gameObject.GetComponent<Image>().color = color;
    }
    void Alpha10()
    {
        color.a = 1.0f;
        this.gameObject.GetComponent<Image>().color = color;
    }
}
