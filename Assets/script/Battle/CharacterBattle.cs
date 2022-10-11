using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBattle : MonoBehaviour
{
    private Vector3 MoveTargetPosition;
    private Action OnMoveComplete;
    private enum STATE
    {
        Idle,Moving,Attack
    }
    private STATE state;
    private void Awake()
    {
        state = STATE.Idle;
      //  Attack(Character, FinishAttack);
    }
    private void Update()
    {
        switch (state)
        {
            case STATE.Idle:
                break;
            case STATE.Moving:
                transform.position+=(MoveTargetPosition-GetPosition())*Time.deltaTime;
                float reachedDistance = 1.0f;
                if (Vector3.Distance(GetPosition(),MoveTargetPosition)<reachedDistance)
                {
                    OnMoveComplete();
                }
                break;
            case STATE Attack:
                break;
        }
      }
    public Vector3 GetPosition()
    {
        return transform.position;
    }
    public void Attack(CharacterBattle targetCharacterBattle,Action onAttackFinish)
    {
        Vector3 TargetPosition = targetCharacterBattle.GetPosition();
        MoveToPosition(TargetPosition, () => { state = STATE.Attack; });

        state=STATE.Attack;
        this.GetComponent<Animator>().SetTrigger("Attack");
        onAttackFinish?.Invoke();
    }
    public void FinishAttack()
    {

    }
    public void MoveToPosition(Vector3 TargetPosition,Action onMoveComplete)
    {
        this.MoveTargetPosition = TargetPosition;
        this.OnMoveComplete = onMoveComplete;
        state = STATE.Moving;
    }

}