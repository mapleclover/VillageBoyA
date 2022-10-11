using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TurnBattle : MonoBehaviour
{
    public GameObject Friendly;
    public GameObject Enemy;
    public GameObject Active;
    public GameObject Target;
    int Skill;
    Vector3 gos;
    //
    //public GameObject SelectedCharacter; 
    //
    public enum State
    {
        Create, Choice,Moving ,BackMoving,Battle, End
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
            case State.Moving:
                StartCoroutine(moving(Target.transform.position));
                break;
            case State.BackMoving:
                
                Active = Enemy;

                break;
            case State.Battle:
                StartCoroutine(Attack(Skill));
                
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
                    //SelectedCharacter.GetComponenet<Friendly>().Skill = 1;
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
            case State.Moving:
                break;
            case State.Battle:
                break;
            case State.End:
                break;
        }
    }
    private void Awake()
    {
        Active = Friendly;
        Target = Enemy;
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
            ChangeState(State.Moving);
        }
    }

    //
    /*public void CharacterSelect(int i)
    {
        SelectedCharacter = Friendly[i];

    }*/
    //

    IEnumerator Attack(int s)
    {
        if (Active == Friendly)
        {
            Active.GetComponent<Friendly>().ChoiceSkill(s);
            Skill = 0;
        }
        else if (Active==Enemy)
        {
            Active.GetComponent<Enemy>().RandomSkill();
        }
        yield return new WaitForSeconds(3.0f);
        ChangeState(State.BackMoving);
    }
    IEnumerator moving(Vector3 pos)
    {
        gos = Active.transform.position;
        Vector3 dir = pos - Active.transform.position;
        float dist = dir.magnitude;
        dir.Normalize();
        while (dist > 0.0f)
        {
            float delta = 5.0f * Time.deltaTime;
            if (delta > dist)
            {
                delta = dist;
            }
            dist -= delta;
            Active.transform.Translate(dir * delta, Space.World);
            yield return null;
        }
        if (dist == 0.0f)
        {
            ChangeState(State.Battle);
        }

    }
}
