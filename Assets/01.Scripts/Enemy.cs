using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : CharacterProperty
{
    public float myHp = 100.0f;
    public float speed = 2.0f;
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    public void RandomSkill()
    {
        int rnd = Random.Range(0, 3);
        switch(rnd)
        {
            case 0:
                Jump();
                break;
            case 1:
                Zigzag();
                break;
            case 2:
                Straight();
                break;
            default:
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
