///����
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
        //���ϱ�����
           
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
    IEnumerator OnDmg(float dmg) //�÷��õ�����
    {
        yield return new WaitForSeconds(1.0f); // 1.5������ �����ȴ�
        myAnim.SetTrigger("Hit");
        GameObject hudText = Instantiate(hudDmgText, Canvas.transform); // �÷��õ����� ����
        switch (Random.Range(0, 10)) // ũ��, �̽� , �Ϲݵ����� Ȯ��
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
        myHp -= dmg; // ũ���̽� �Ϲݵ����� Ȯ������ ü�¿� -
        Vector3 pos = transform.position; //����ġ
        pos.y += 2.0f; // ����ġ���� 2��ŭ y�����̵�
        Vector3 pos2 = Camera.main.WorldToScreenPoint(pos); // pos2�� ����ī�޶󿡼� pos ��ġ��
        hudText.transform.position = pos2; // �÷��õ������� po2���̵�
        if (dmg <= 0.0f) //������üũ�ؼ� 0���ϸ� �̽��ζ߰��Ѵ�
        {
            hudText.GetComponent<DmageText>().Dmg.text = "Miss";
        }
        else
        {
            hudText.GetComponent<DmageText>().dmg = dmg;
        }
    }
}

