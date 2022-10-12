///¹ÚÁø
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnBattle : MonoBehaviour
{
    public GameObject Friendly;
    public GameObject Enemy;
    
    int Skill;
    public enum State
    {
        Create, Choice, Battle, End
    }
    public State myState = State.Create;
    void ChangeState(State s)
    {
        if (myState == s) return;
        myState = s;
        switch(myState)
        {
            case State.Create:
                break;
            case State.Choice:
                break;
            case State.Battle:
                if(Enemy.GetComponent<Enemy>().speed < Friendly.GetComponent<Friendly>().speed)
                {
                    Friendly.GetComponent<Friendly>().ChoiceSkill(Skill);
                    Enemy.GetComponent<Enemy>().RandomSkill();
                }
                else if (Enemy.GetComponent<Enemy>().speed > Friendly.GetComponent<Friendly>().speed)
                {
                    Enemy.GetComponent<Enemy>().RandomSkill();
                    Friendly.GetComponent<Friendly>().ChoiceSkill(Skill);
                }
                else
                {
                    int x = Random.Range(0, 1);
                    switch(x)
                    {
                        case 0:
                            Friendly.GetComponent<Friendly>().ChoiceSkill(Skill);
                            Enemy.GetComponent<Enemy>().RandomSkill();
                            break;
                        case 1:
                            Enemy.GetComponent<Enemy>().RandomSkill();
                            Friendly.GetComponent<Friendly>().ChoiceSkill(Skill);
                            break;
                    }
                }
                Skill = 0;
                ChangeState(State.Choice);
                break;
            case State.End:
                break;
        }

    }
    void StateProcess()
    {
        switch (myState)
        {
            case State.Create:
                break;
            case State.Choice:
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    Skill = 1;
                }
                if (Input.GetKeyDown(KeyCode.W))
                {
                    Skill = 2;
                }
                if (Input.GetKeyDown(KeyCode.E))
                {
                    Skill = 3;
                }
                break;
            case State.Battle:
                break;
            case State.End:
                break;
        }
    }

    void Start()
    {
        ChangeState(State.Choice);
               
    }

    
    void Update()
    {
        
        StateProcess();
        if (Input.GetKeyDown(KeyCode.Return))
        {
            ChangeState(State.Battle);

        }
    }
}
