///����
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
            if (Input.GetKeyDown(KeyCode.Space)) //�����̽��ٸ� ������ ����
            {
                state = State.Busy;     //���� ���� ������
                playerCharacterBattle.Attack(enemyCharacterBattle, () => NextCharacterToAction()); //�� ��� ��Ʋ������ �ߵ� �� ���� ������ ĳ���� ����
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
