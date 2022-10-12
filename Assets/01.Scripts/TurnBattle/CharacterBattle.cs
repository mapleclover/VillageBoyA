///박진
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
                transform.position += (MoveTargetPosition - GetPosition()) * Time.deltaTime;    //3. 매 프레임 마다 적 위치로 이동

                float reachedDistance = 1.0f; //적과 충분히 가까워졌음을 알리는 거리
                if (Vector3.Distance(GetPosition(), MoveTargetPosition) < reachedDistance)   //적과 충분히 가까워졌으면 다음 행동 실행
                {
                    OnMoveComplete();
                }
                break;
        }
    }

    public void Attack(CharacterBattle targetCharacterBattle, Action onAttackFinish)
    {
        Vector3 TargetPosition = targetCharacterBattle.GetPosition();   //1. 내 위치부터 적 위치까지 방향벡터 계산 
        Vector3 OriginalPosition = GetPosition();                       //때리고 다시 원래 위치로 복귀 벡터 저장
        /*this.GetComponent<Animator>().SetBool("IsMoving", true);*/
        MoveToPosition(TargetPosition, () =>                            //내 상태(State)를 이동중 상태로 만듬
        {
            /*this.GetComponent<Animator>().SetBool("IsMoving", false);*/
            state = State.Attack;   //4. 공격상태로 만들어줌
            StartCoroutine(CharacterAttack(() =>            //공격하는 코루틴 실행(코루틴을 쓰는 이유는 공격모션이 끝나기까지 yield return으로 시간지연을 주기위함)
            {
                /*this.GetComponent<Animator>().SetBool("IsMoving", true);*/
                MoveToPosition(OriginalPosition, () =>      //저장했던 원래자리로 이동
                {
                    /*this.GetComponent<Animator>().SetBool("IsMoving", false);*/
                    state = State.Idle;                     //그 후 다시 idle상태로 되돌리고
                    onAttackFinish.Invoke();                //턴 종료 후 BattleHandler에서 받아온 함수실행
                });
            }
            ));
        });
    }

    public void MoveToPosition(Vector3 TargetPosition, Action onMoveComplete)
    {
        this.MoveTargetPosition = TargetPosition;   //2. 내 캐릭터가 타켓으로 삼고있는 적 위치 저장
        if (onMoveComplete != null)
        {
            this.OnMoveComplete = onMoveComplete;   //내 캐릭터가 움직이고나서 다음으로 할 행동 저장
        }
        state = State.Moving;                       //update 함수에서 이동 할 수 있도록 만들어줌
    }
    IEnumerator CharacterAttack(Action done)        //공격! delegate로 다음 행동 받아옴
    {
        if(BattleHandler.GetComponent<BattleHandler>().check)
        {
           
        }
        else if(!BattleHandler.GetComponent<BattleHandler>().check)
        {
            
        }


        yield return new WaitForSeconds(3.0f);
        done();     //공격이 끝나면 다음 행동 실행(여기선 원래자리로 돌아가기)
    }
}
