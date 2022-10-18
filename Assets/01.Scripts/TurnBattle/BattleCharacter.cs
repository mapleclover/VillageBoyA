///¹ÚÁø
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleCharacter : CharacterProperty
{
    public float myHp = 100.0f;
    public float speed = 2.0f;
    public int Skill = 0;
    public GameObject myTarget;
    public bool Active5=false;
    public GameObject Canvas;
    
    public GameObject hudDmgText;
        
    void Start()
    {
        Canvas = GameObject.Find("Canvas");
    }

    void Update()
    {
        
    }
    public void ChoiceSkill(int s)
    {
        switch(s)
        {
            case 0:
                myAnim.SetTrigger("Attack");
                myTarget.GetComponent<BattleCharacter>().OnDamage(10.0f);
                break;
            case 1:
                myAnim.SetTrigger("Attack2");
                myTarget.GetComponent<BattleCharacter>().OnDamage(20.0f);
                break;
            case 2:
                myAnim.SetTrigger("Attack3");
                myTarget.GetComponent<BattleCharacter>().OnDamage(30.0f);
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
                myTarget.GetComponent<BattleCharacter>().OnDamage(10.0f);
                break;
            case 1:
                myAnim.SetTrigger("Attack2");
                myTarget.GetComponent<BattleCharacter>().OnDamage(20.0f);
                break;
            case 2:
                myAnim.SetTrigger("Attack3");
                myTarget.GetComponent<BattleCharacter>().OnDamage(30.0f);
                break;
            default:
                break;
        }
    }
    public void OnDamage(float dmg)
    {
        StartCoroutine(OnDmg(dmg));
    }
    IEnumerator OnDmg(float dmg)
    {
        yield return new WaitForSeconds(1.0f);
        GameObject hudText = Instantiate(hudDmgText, Canvas.transform);
        switch (Random.Range(0, 10))
        {
            case 0:
                dmg *= 2;
                hudText.GetComponent<DmageText>().color = Color.red;
                break;
            case 1:
                dmg *= 0;
                hudText.GetComponent<DmageText>().color = Color.gray;
                break;
            default:
                hudText.GetComponent<DmageText>().color = Color.white;
                break;
        }
        myHp -= dmg;
        Vector3 pos = transform.position;
        pos.y += 2.0f;
        Vector3 pos2 = Camera.main.WorldToScreenPoint(pos);
        hudText.transform.position = pos2;
        hudText.GetComponent<DmageText>().dmg = dmg;
    }
}

