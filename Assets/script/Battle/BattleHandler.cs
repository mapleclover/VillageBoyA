using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleHandler : MonoBehaviour
{
    public CharacterBattle playerCharacterBattle;
    public CharacterBattle enemyCharacterBattle;
    public 
     enum STATE
    {
        WaitingforInput,Busy
    }
    private STATE state;
    private static BattleHandler instance;
    public static BattleHandler GetInstance()
    {
        return instance;
    }
    private void Awake()
    {
        instance = this;
    }
    private void Update()
    {
        if (state == STATE.WaitingforInput)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                state=STATE.Busy;
                playerCharacterBattle.Attack(enemyCharacterBattle, () => DoNothing());
                //캐릭터 코드 공격 실행
            }
        }
    }
    public void DoNothing()
    {

    }
}
