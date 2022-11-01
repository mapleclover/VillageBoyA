///박진
///캐릭터 상태
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public enum STATE
{
    Live, Stunned, Die
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
    public float speed;
    public int Skill = 0;
    public bool longAttackCheck=false;
    public bool[] longAttack = new bool[3];
    public GameObject myTarget;
    public bool TurnActive=false;
    public GameObject Canvas;    
    public GameObject hudDmgText;
    public bool Stunned=false;    
    int StunCheck;
    public bool ActiveHeal=false;
    public int StunTurn = 1;
    public float[] SkillDmg=new float[3] {10.0f,20.0f,30.0f };
    void ChangeState(STATE s)
    {
        if (State == s) return;
        State = s;
        switch (State)
        {
            case STATE.Live:
                break;
            case STATE.Stunned:
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
                if (Stunned)
                {
                    ChangeState(STATE.Stunned);
                    StunCheck = TurnBattle.Inst.BattleTurn;
                    Stunned = false;
                }
                break;
            case STATE.Stunned:
                if (StunCheck + StunTurn == TurnBattle.Inst.BattleTurn)
                {
                    ChangeState(STATE.Live);
                }
                break;
            case STATE.Die:
                TurnActive = false;
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
        
    }    
    
    public void ChoiceSkill(int s)
    {
        switch(s)
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
        }
    }
    public void RandomSkill()
    {
        int rnd = Random.Range(0, 3);
        switch (rnd)
        {
            case 0:
                myAnim.SetTrigger("Attack");
                //OnTargetDamage(10.0f);
                break;
            case 1:
                myAnim.SetTrigger("Attack2");
                //OnTargetDamage(20.0f);
                break;
            case 2:
                myAnim.SetTrigger("Attack3");
                //OnTargetDamage(30.0f);
                break;
            default:
                break;
        }
    }
    public void Healing()
    {
        TurnBattle.Inst.HealingPotion -= 1;
        myHp += 30.0f;
        ActiveHeal = false;
    }

    public void OnTargetDamage(int a)
    {
        switch (a)
        {
            case 0:
                StartCoroutine(OnDmg(SkillDmg[0]));
                break;                
            case 1:
                StartCoroutine(OnDmg(SkillDmg[1]));
                break;
            case 2:
                StartCoroutine(OnDmg(SkillDmg[2]));
                break;
        }
    }
    IEnumerator OnDmg(float dmg) //플로팅데미지
    {        
        
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
        if(dmg > 0) myTarget.GetComponent<BattleCharacter>().GetComponent<Animator>().SetTrigger("Hit"); //미스시 피격모션x
        myTarget.GetComponent<BattleCharacter>().myHp -= dmg; // 크리미스 일반데미지 확인이후 체력에 -
        Vector3 pos = myTarget.transform.position; //타겟위치
        pos.y += 2.0f; // 위치에서 2만큼 y위로이동
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
        yield return null;
    }
}

