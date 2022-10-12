///¹ÚÁø
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
<<<<<<< Updated upstream:Assets/01.Scripts/TurnBattle/Enemy.cs
       
=======

    
>>>>>>> Stashed changes:Assets/01.Scripts/Enemy.cs
}
