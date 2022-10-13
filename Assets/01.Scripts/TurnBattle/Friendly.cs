///����
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Friendly : CharacterProperty
{
    public float myHp = 100.0f;
    public float speed = 2.0f;
    public int Skill = 0;
    public GameObject Target;
    public bool Active5=true;
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    public void ChoiceSkill(int s)
    {
        switch(s)
        {
            case 1:
                myAnim.SetTrigger("Attack");
                break;
            case 2:
                myAnim.SetTrigger("Attack2");
                break;
            case 3:
                myAnim.SetTrigger("Attack3");
                break;
        }
    }
    public void RandomSkill()
    {
        int rnd = Random.Range(0, 3);
        switch (rnd)
        {
            case 0:
                myAnim.SetTrigger("Attack");
                break;
            case 1:
                myAnim.SetTrigger("Attack2");
                break;
            case 2:
                myAnim.SetTrigger("Attack3");
                break;
            default:
                break;
        }
    }

}
