using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Friendly : CharacterProperty
{
    public float myHp = 100.0f;
    public float speed = 2.0f;
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    public void ChoiceSkill(int s)
    {
        switch (s)
        {
            case 1:
                myAnim.SetTrigger("Jump");
                break;
            case 2:
                myAnim.SetTrigger("Zigzag");
                break;
            case 3:
                myAnim.SetTrigger("Straight");
                break;
        }
    }
    public void RandomSkill()
    {
        int rnd = Random.Range(0, 3);
        switch (rnd)
        {
            case 0:
                myAnim.SetTrigger("Jump");
                break;
            case 1:
                myAnim.SetTrigger("Zigzag");
                break;
            case 2:
                myAnim.SetTrigger("Straight");
                break;
            default:
                break;
        }
    }


}
