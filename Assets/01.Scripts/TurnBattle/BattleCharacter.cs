//작성자 : 박진
//설명 : 캐릭터상태

using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.PlayerSettings;
using Random = UnityEngine.Random;

[Serializable]
public struct BattleCharacterStat
{
    public PC orgData;
    public float _curHP;

    public float curHP
    {
        get => _curHP;
        set
        {
            _curHP = value;
            if (_curHP < 0.0f) _curHP = 0.0f;
            if (_curHP > orgData.Health) _curHP = orgData.Health;
        }
    }
    public float Speed;
    public float[] AttackDmg;    
    public bool[] longAttack;
    public int AdditionalAttack;
    public int AdditionalDefence;
}

public enum STATE
{
    Live,
    Stunned,
    Die
}

public class BattleCharacter : CharacterProperty
{
    public BattleCharacterStat myStat;
    public STATE State = STATE.Live;
    float maxHp;
    public Slider myHpBar;
    public int Skill = 0;
    bool _longAttackCheck = false;

    public bool longAttackCheck
    {
        get => _longAttackCheck;
        set { _longAttackCheck = value; }
    }

    public GameObject myTarget;
    public bool TurnActive = false;
    public GameObject Canvas;
    public GameObject hudDmgText;
    public bool Stunned = false;
    int StunCheck;
    public bool ActiveHeal = false;
    public int StunTurn = 1;
    public GameObject Stun;


    void ChangeState(STATE s)
    {
        if (State == s) return;
        State = s;
        switch (State)
        {
            case STATE.Live:                
                break;
            case STATE.Stunned:
                if (Stun == null) Stun = Instantiate(Resources.Load<GameObject>("Prefabs/TurnBattle/Stun"));
                else if(Stun!=null) Stun.SetActive(true);
                Vector3 pos = transform.position; //타겟위치
                pos.y += 1.0f; // 위치에서 2만큼 y위로이동                
                Stun.transform.position = pos;
                Stun.transform.SetParent(transform);
                break;
            case STATE.Die:
                myAnim.SetBool("Death", true);
                if (gameObject.layer == LayerMask.NameToLayer("Enemy")) Invoke("SetActiveFalse", 2.0f);
                else
                {

                }
                break;
        }
    }

    void StateProcess()
    {
        switch (State)
        {
            case STATE.Live:
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
                    Stun.SetActive(false);
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
        callData();
        Canvas = GameObject.Find("Canvas");
        longAttackCheck = myStat.longAttack[0];
        maxHp = myStat.orgData.Health;
    }

    void Update()
    {
        StateProcess();
        FollowEnemyHpbar();
        myHpBar.value = Mathf.Lerp(myHpBar.value, myStat.curHP / maxHp, 5.0f * Time.deltaTime);
        if (myStat.curHP <= Mathf.Epsilon) ChangeState(STATE.Die);        
    }

    void callData()
    {
        if (gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            myStat.curHP = myStat.orgData.Health;
            myStat.AdditionalAttack = 0;
            if (myStat.orgData.BattleType == PC.Type.Boss)
            {
                myStat.AdditionalDefence = 10;
            }
            else
            {
                myStat.AdditionalDefence = 0;
            }
            
        }
        else if (gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if (this.gameObject.name == "KongForBattle(Clone)")
            {
                myStat.curHP = DataController.instance.gameData.Kong.HP;
                myStat.AdditionalAttack = DataController.instance.gameData.Kong.strength;
                myStat.AdditionalDefence = DataController.instance.gameData.Kong.defPower;
            }
            if (this.gameObject.name == "JinForBattle(Clone)")
            {
                myStat.curHP = DataController.instance.gameData.Jin.HP;
                myStat.AdditionalAttack = DataController.instance.gameData.Jin.strength;
                myStat.AdditionalDefence = DataController.instance.gameData.Jin.defPower;
            }
            if (this.gameObject.name == "EmberForBattle(Clone)")
            {
                myStat.curHP = DataController.instance.gameData.Ember.HP;
                myStat.AdditionalAttack = DataController.instance.gameData.Ember.strength;
                myStat.AdditionalDefence = DataController.instance.gameData.Ember.defPower;
            }
        }
        myStat.Speed = myStat.orgData.Speed;        
        myStat.longAttack = myStat.orgData.IsRangeAttack;
    }

    

    public void ChoiceSkill(int s)
    {
        switch (s)
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

    public void Healing()
    {
        GameObject obj = Instantiate(Resources.Load<GameObject>("Prefabs/TurnBattle/Heal"));
        Vector3 pos = transform.position;
        pos.y += 1.0f;
        obj.transform.position = pos;
        Destroy(obj, 2.0f);
        myStat.curHP += 30.0f;
        ActiveHeal = false;
    }
    

    public void OnTargetDamage(int a)
    {
        if (!myStat.orgData.IsAOE[a])
        {
            StartCoroutine(OnDmg(myStat.orgData.GetDamage(a) * (myStat.orgData.BaseAttackDamage + myStat.AdditionalAttack - myTarget.GetComponent<BattleCharacter>().myStat.AdditionalDefence), a, myTarget));
        }
        else if (myStat.orgData.IsAOE[a])
        {
            for (int i = 0; i < TurnBattle.Inst.Enemy.Count; ++i)
            {
                StartCoroutine(OnDmg(myStat.orgData.GetDamage(a) * (myStat.orgData.BaseAttackDamage + myStat.AdditionalAttack - TurnBattle.Inst.Enemy[i].GetComponent<BattleCharacter>().myStat.AdditionalDefence), a, TurnBattle.Inst.Enemy[i]));
            }
        }
    }
    public void KongAttack3()
    {
        GameObject obj = Instantiate(Resources.Load<GameObject>("Prefabs/TurnBattle/KongAttack3"));
             
        SoundTest.instance.PlaySE("SFX_KongAttack3");            
        Vector3 pos = myTarget.transform.position;
        obj.transform.position = pos;
        Destroy(obj, 2.0f);
    }
    public void BowAttack1()
    {
        GameObject obj = Instantiate(Resources.Load<GameObject>("Prefabs/TurnBattle/BowAttack1"));
        SoundTest.instance.PlaySE("SFX_Arrow");
        Vector3 pos;
        Vector3 myPos = transform.position;
        myPos.y += 1.0f;
        Vector3 myTargetPos = myTarget.transform.position;
        myTargetPos.y += 1.0f;
        Ray ray = new Ray(myPos, (myTargetPos - myPos).normalized);
        RaycastHit hitData;
        if (Physics.Raycast(ray, out hitData, 100f, 1 << LayerMask.NameToLayer("Enemy")))
        {
            pos = hitData.point;
            obj.transform.position = pos;
        }
        Destroy(obj, 2.0f);
    }
    public void BombAttack()
    {
        GameObject obj=Instantiate(Resources.Load<GameObject>("Prefabs/TurnBattle/Bomb"));
        SoundTest.instance.PlaySE("SFX_JinAttack2");
        Vector3 pos= TurnBattle.Inst.EnemyParent.position;        
        obj.transform.position = pos;
        Destroy(obj, 2.0f);
    }

    public void BowAttack2()
    {
        GameObject obj = Instantiate(Resources.Load<GameObject>("Prefabs/TurnBattle/BowAttack2"));
        SoundTest.instance.PlaySE("SFX_Arrow");
        Vector3 pos;
        Vector3 myPos = transform.position;
        myPos.y += 1.0f;
        Vector3 myTargetPos = myTarget.transform.position;
        myTargetPos.y += 1.0f;
        Ray ray = new Ray(myPos, (myTargetPos - myPos).normalized);
        RaycastHit hitData;
        if (Physics.Raycast(ray, out hitData, 100f, 1 << LayerMask.NameToLayer("Enemy")))
        {            
            pos = hitData.point;
            obj.transform.position = pos;
        }
        Destroy(obj, 1.0f);
    }
    void FollowEnemyHpbar()
    {
        if(gameObject.layer == LayerMask.NameToLayer("Player")) return;
        Vector3 pos = transform.position;
        pos.y += 2.0f;
        pos = Camera.main.WorldToScreenPoint(pos);
        myHpBar.transform.position = pos;
    }

    IEnumerator OnDmg(float dmg,int a,GameObject myTarget) //플로팅데미지
    {
        if (myTarget.GetComponent<BattleCharacter>().State == STATE.Die) yield break;
        GameObject hudText = Instantiate(hudDmgText, Canvas.transform); // 플로팅데미지 생성
        hudText.GetComponent<DmageText>().color = Color.white;
        if (myStat.orgData.IsCritical(a))
        {
            hudText.GetComponent<DmageText>().color = Color.red;
            dmg *= myStat.orgData.CriticalRatio[a];
        }
        dmg = (int)dmg;
        myTarget.GetComponent<BattleCharacter>().myStat.curHP -= dmg; // 크리미스 일반데미지 확인이후 체력에 -
        myTarget.GetComponent<Animator>().SetTrigger("Hit");
        Vector3 pos = myTarget.transform.position; //타겟위치
        pos.y += 2.0f; // 위치에서 2만큼 y위로이동
        Vector3 pos2 = Camera.main.WorldToScreenPoint(pos); // pos2는 메인카메라에서 pos 위치값
        hudText.transform.position = pos2; // 플로팅데미지를 po2로이동
        hudText.GetComponent<DmageText>().dmg = dmg;
        yield return null;
    }

    void SetActiveFalse()
    {
        gameObject.SetActive(false);
        myHpBar.gameObject.SetActive(false);
    }
}