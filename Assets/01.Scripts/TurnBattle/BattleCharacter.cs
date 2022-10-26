///¹ÚÁø
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public enum STATE
{
    Live, Die
}
public class BattleCharacter : CharacterProperty
{
    public STATE State = STATE.Live;
    public float _myhp = 100.0f;
    float maxHp = 100.0f;
    float minHp = 0.0f;
    public Slider myHpBar;
    public float myHp 
    {
        get => _myhp;
        set
        {
            _myhp = value;
            if(_myhp > maxHp) _myhp=maxHp;
            if(_myhp < minHp) _myhp=minHp;
        }
        
    }
    public float speed = 2.0f;
    public int Skill = 0;
    public GameObject myTarget;
    public bool Active5=false;
    public GameObject Canvas;    
    public GameObject hudDmgText;

    private void Awake()
    {
        Canvas = GameObject.Find("Canvas");
    }
    void Start()
    {
              
    }

    void Update()
    {
        if (myHp <= 0.0f)
        {
            State = STATE.Die;
        }
        else
        {
            State = STATE.Live;
        }
        if (State == STATE.Die)
        {
            Active5 = false;
        }
        myHpBar.value = Mathf.Lerp(myHpBar.value, myHp / maxHp , 5.0f * Time.deltaTime);
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
        yield return new WaitForSeconds(1.5f);
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
        if (dmg <= 0.0f)
        {
            hudText.GetComponent<DmageText>().Dmg.text = "Miss";
        }
        else
        {
            hudText.GetComponent<DmageText>().dmg = dmg;
        }
    }
}

