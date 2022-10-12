///����
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor.Experimental.GraphView;

public class CharacterBattle : MonoBehaviour
{
    public GameObject BattleHandler;
    private State state;
    private Vector3 MoveTargetPosition;
    private Action OnMoveComplete;
    

    private enum State
    {
        Idle, Moving, Attack
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    private void Awake()
    {
        state = State.Idle;
    }

    private void Update()
    {
        switch (state)
        {
            case State.Idle:
                break;
            case State.Attack:
                break;
            case State.Moving:
                transform.position += (MoveTargetPosition - GetPosition()) * Time.deltaTime;    //3. �� ������ ���� �� ��ġ�� �̵�

                float reachedDistance = 1.0f; //���� ����� ����������� �˸��� �Ÿ�
                if (Vector3.Distance(GetPosition(), MoveTargetPosition) < reachedDistance)   //���� ����� ����������� ���� �ൿ ����
                {
                    OnMoveComplete();
                }
                break;
        }
    }

    public void Attack(CharacterBattle targetCharacterBattle, Action onAttackFinish)
    {
        Vector3 TargetPosition = targetCharacterBattle.GetPosition();   //1. �� ��ġ���� �� ��ġ���� ���⺤�� ��� 
        Vector3 OriginalPosition = GetPosition();                       //������ �ٽ� ���� ��ġ�� ���� ���� ����
        /*this.GetComponent<Animator>().SetBool("IsMoving", true);*/
        MoveToPosition(TargetPosition, () =>                            //�� ����(State)�� �̵��� ���·� ����
        {
            /*this.GetComponent<Animator>().SetBool("IsMoving", false);*/
            state = State.Attack;   //4. ���ݻ��·� �������
            StartCoroutine(CharacterAttack(() =>            //�����ϴ� �ڷ�ƾ ����(�ڷ�ƾ�� ���� ������ ���ݸ���� ��������� yield return���� �ð������� �ֱ�����)
            {
                /*this.GetComponent<Animator>().SetBool("IsMoving", true);*/
                MoveToPosition(OriginalPosition, () =>      //�����ߴ� �����ڸ��� �̵�
                {
                    /*this.GetComponent<Animator>().SetBool("IsMoving", false);*/
                    state = State.Idle;                     //�� �� �ٽ� idle���·� �ǵ�����
                    onAttackFinish.Invoke();                //�� ���� �� BattleHandler���� �޾ƿ� �Լ�����
                });
            }
            ));
        });
    }

    public void MoveToPosition(Vector3 TargetPosition, Action onMoveComplete)
    {
        this.MoveTargetPosition = TargetPosition;   //2. �� ĳ���Ͱ� Ÿ������ ����ִ� �� ��ġ ����
        if (onMoveComplete != null)
        {
            this.OnMoveComplete = onMoveComplete;   //�� ĳ���Ͱ� �����̰��� �������� �� �ൿ ����
        }
        state = State.Moving;                       //update �Լ����� �̵� �� �� �ֵ��� �������
    }
    IEnumerator CharacterAttack(Action done)        //����! delegate�� ���� �ൿ �޾ƿ�
    {
        if(BattleHandler.GetComponent<BattleHandler>().check)
        {
           
        }
        else if(!BattleHandler.GetComponent<BattleHandler>().check)
        {
            
        }


        yield return new WaitForSeconds(3.0f);
        done();     //������ ������ ���� �ൿ ����(���⼱ �����ڸ��� ���ư���)
    }
}
