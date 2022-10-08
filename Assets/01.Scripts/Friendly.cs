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
    public void ChoiceSkill(float s)
    {
        switch(s)
        {
            case 1:
                Jump();
                break;
            case 2:
                Zigzag();
                break;
            case 3:
                Straight();
                break;
        }
    }
    public void Jump()
    {
        myAnim.SetTrigger("Jump");
    }
    public void Zigzag()
    {
        myAnim.SetTrigger("Zigzag");
    }
    public void Straight()
    {
        myAnim.SetTrigger("Straight");
    }
}
