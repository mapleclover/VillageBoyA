///박진
///캐릭터 상태
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.InputSystem.HID;
using static UnityEditor.Experimental.GraphView.GraphView;

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
    bool _longAttackCheck = false;
    public bool longAttackCheck
    {
        get => _longAttackCheck;
        set
        {
            _longAttackCheck = value;
        }
    }
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
    GameObject Stun;
    void ChangeState(STATE s)
    {
        if (State == s) return;
        State = s;
        switch (State)
        {
            case STATE.Live:
                break;
            case STATE.Stunned:
                if (Stun == null)
                {
                    Stun = Instantiate(Resources.Load<GameObject>("Prefabs/TurnBattle/Stun"));
                }
                else
                {
                    Stun.SetActive(true);
                }
                Vector3 pos = transform.position; //타겟위치
                pos.y += 1.0f; // 위치에서 2만큼 y위로이동                
                Stun.transform.position = pos;
                Stun.transform.SetParent(transform);
                break;
            case STATE.Die:
                myAnim.SetBool("Death",true);
                if (gameObject.layer == LayerMask.NameToLayer("Enemy"))
                {
                    transform.Translate(Vector3.down * 1.0f * Time.deltaTime);
                    Invoke("SetActiveFalse", 2.0f);
                }
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
                    if (Stun != null) Stun.SetActive(false);
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
        longAttackCheck = longAttack[0];
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
        GameObject obj = Instantiate(Resources.Load<GameObject>("Prefabs/TurnBattle/Heal"));
        Vector3 pos = transform.position;
        pos.y += 1.0f;
        obj.transform.position = pos;
        Destroy(obj, 2.0f);        
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
    public void BowAttack1()
    {
        GameObject obj= Instantiate(Resources.Load<GameObject>("Prefabs/TurnBattle/BowAttack1"));
        Vector3 pos;        
        Vector3 myPos=transform.position;
        myPos.y += 1.0f;
        Vector3 myTargetPos= myTarget.transform.position;
        myTargetPos.y += 1.0f;
        Ray ray = new Ray(myPos, (myTargetPos - myPos).normalized);
        RaycastHit hitData;

        if (Physics.Raycast(ray, out hitData, 100f, 1 << LayerMask.NameToLayer("Enemy")))
        {
            //Debug.DrawLine(transform.position, hitData.point, Color.red);

            pos=hitData.point;
            obj.transform.position = pos;
        }
        Destroy(obj,2.0f);
    }
    public void BowAttack2()
    {
        GameObject obj = Instantiate(Resources.Load<GameObject>("Prefabs/TurnBattle/BowAttack2"));
        Vector3 pos;
        Vector3 myPos = transform.position;
        myPos.y += 1.0f;
        Vector3 myTargetPos = myTarget.transform.position;
        myTargetPos.y += 1.0f;
        Ray ray = new Ray(myPos, (myTargetPos - myPos).normalized);
        RaycastHit hitData;

        if (Physics.Raycast(ray, out hitData, 100f, 1 << LayerMask.NameToLayer("Enemy")))
        {
            //Debug.DrawLine(transform.position, hitData.point, Color.red);

            pos = hitData.point;
            obj.transform.position = pos;
        }
        Destroy(obj, 1.0f);
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
    void SetActiveFalse()
    {
        gameObject.SetActive(false);
        myHpBar.gameObject.SetActive(false);
    }
}

