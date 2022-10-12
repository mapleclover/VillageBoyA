///박진
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor.Experimental.GraphView;

public class BattleHandler : MonoBehaviour
{
    public CharacterBattle playerCharacterBattle;
    public CharacterBattle enemyCharacterBattle;
    public CharacterBattle activeCharacterBattle;

    private State state;
    public bool check;
    
    private enum State
    {
        WaitingforInput,
        Busy
    }

    private static BattleHandler instance;
    public static BattleHandler GetInstance()
    {
        return instance;
    }

    private void Awake()
    {
        instance = this;
        activeCharacterBattle = playerCharacterBattle;
    }

    private void Update()
    {
        
        if (state == State.WaitingforInput)
        {
            if (Input.GetKeyDown(KeyCode.Space)) //스페이스바를 누르면 공격
            {
                state = State.Busy;     //나는 지금 공격중
                playerCharacterBattle.Attack(enemyCharacterBattle, () => NextCharacterToAction()); //내 모든 배틀스퀀스 발동 후 다음 공격할 캐릭터 선정
            }
        }
        if (activeCharacterBattle == playerCharacterBattle)
        {
            check = true;
        }
        else if(activeCharacterBattle == enemyCharacterBattle)
        {
            check = false;
        }
    }

    public void NextCharacterToAction()
    {
        
        if (activeCharacterBattle == playerCharacterBattle)
        {
            state = State.Busy;
            activeCharacterBattle = enemyCharacterBattle;
            enemyCharacterBattle.Attack(playerCharacterBattle, () => NextCharacterToAction());
        }
        else
        {
            activeCharacterBattle = playerCharacterBattle;
            state = State.WaitingforInput;
        }
    }
}
