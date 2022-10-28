///박진
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
    float _myhp = 100.0f;
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
    public bool longAttackCheck=false;

    public GameObject myTarget;
    public bool Active5=false;
    public GameObject Canvas;    
    public GameObject hudDmgText;
    public bool Stunned=false;
    bool Stunned2= false;
    int StunCheck;
    public int StunTurn = 1;
    void ChangeState(STATE s)
    {
        if (State == s) return;
        State = s;
        switch (State)
        {
            case STATE.Live:
                break;
            case STATE.Die:
                myAnim.SetBool("Death",true);
                break;
        }
    }
    void StateProcess()
    {
        switch (State)
        {
            case STATE.Live:
                if (myHp <= 0.0f)
                {
                    ChangeState(STATE.Die);
                }
                break;
            case STATE.Die:
                Active5 = false;
                break;
        }
    }
    private void Awake()
    {
        Canvas = GameObject.Find("Canvas");
    }
    void Start()
    {
        myHpBar.value = myHp / maxHp;
    }

    void Update()
    {
        StateProcess();        
        myHpBar.value = Mathf.Lerp(myHpBar.value, myHp / maxHp , 5.0f * Time.deltaTime);
        StunnedCheck(StunTurn);
    }
    void StunnedCheck(int v=1)
    {
        //스턴구상중
           
        if (StunCheck + v == TurnBattle.Inst.BattleTurn)
        {
            Stunned2 = false;
        }
        if (Stunned2)
        {
            Active5 = false;
        }
        if (Stunned)
        {
            Stunned2 = true;
            StunCheck = TurnBattle.Inst.BattleTurn;
            Stunned = false;
        }

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
    IEnumerator OnDmg(float dmg) //플로팅데미지
    {
        yield return new WaitForSeconds(1.0f); // 1.5초이후 생성된다
        myAnim.SetTrigger("Hit");
        GameObject hudText = Instantiate(hudDmgText, Canvas.transform); // 플로팅데미지 생성
        switch (Random.Range(0, 10)) // 크리, 미스 , 일반데미지 확률
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
        myHp -= dmg; // 크리미스 일반데미지 확인이후 체력에 -
        Vector3 pos = transform.position; //내위치
        pos.y += 2.0f; // 내위치에서 2만큼 y위로이동
        Vector3 pos2 = Camera.main.WorldToScreenPoint(pos); // pos2는 메인카메라에서 pos 위치값
        hudText.transform.position = pos2; // 플로팅데미지를 po2로이동
        if (dmg <= 0.0f) //데미지체크해서 0이하면 미스로뜨게한다
        {
            hudText.GetComponent<DmageText>().Dmg.text = "Miss";
        }
        else
        {
            hudText.GetComponent<DmageText>().dmg = dmg;
        }
    }
}

