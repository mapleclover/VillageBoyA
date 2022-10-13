///박진
///턴배틀
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using static UnityEditor.PlayerSettings;

public class TurnBattle : MonoBehaviour
{
    public GameObject Friendly;
    public GameObject Enemy;
    public GameObject[] Player=new GameObject[2];
    public GameObject[] Ene = new GameObject[2];
    public GameObject Active;
    public GameObject Target;
    int Skill;
    public GameObject[] PlayerList=new GameObject[6];
    Vector3 gos;
    Vector3 gos2;
    public GameObject SelectedCharacter; 
    
    public enum State
    {
        Create, Choice,SpeedCheck ,ActiveCheck,Moving, BackMoving, Battle, End
    }
    public State myState = State.Create;
    void ChangeState(State s)
    {
        if (myState == s) return;
        myState = s;
        switch (myState)
        {
            case State.Create:
                
                break;
            case State.Choice:
                break;
            case State.SpeedCheck:
                
                break;
            case State.ActiveCheck:
                break;
            case State.Moving:
                
                StartCoroutine(moving(Target.transform.position));
                
                break;            
            case State.Battle:
                StartCoroutine(Attack(Skill));
                break;
            case State.BackMoving:
                StartCoroutine(backmoving(gos));
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
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 1000.0f, 1 << LayerMask.NameToLayer("Friendly")))
                {
                    SelectedCharacter=hit.collider.gameObject;
                }
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    SelectedCharacter.GetComponent<Friendly>().Skill = 1;

                    //Active.GetComponent<Friendly>().Skill = 1;
                }
                if (Input.GetKeyDown(KeyCode.W))
                {
                    SelectedCharacter.GetComponent<Friendly>().Skill = 2;
                    //Active.GetComponent<Friendly>().Skill = 2;
                }
                if (Input.GetKeyDown(KeyCode.E))
                {
                    SelectedCharacter.GetComponent<Friendly>().Skill = 3;
                    //Active.GetComponent<Friendly>().Skill = 3;
                }
                break;
            case State.SpeedCheck:
                break;
            case State.ActiveCheck:
                break;
            case State.Moving:
                break;
            case State.Battle:
                break;
            case State.BackMoving:
                break;
            case State.End:
                break;
        }
    }
    private void Awake()
    {
        Active = Friendly;
        Target = Enemy;
        PlayerList[0] = Player[0];
        PlayerList[1] = Player[1];
        PlayerList[2] = Player[2];
        PlayerList[3] = Ene[0];
        PlayerList[4] = Ene[1];
        PlayerList[5] = Ene[2];
        
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
            Active.GetComponent<Friendly>().ChoiceSkill(Active.GetComponent<Friendly>().Skill);



        }
        else if (Active == Enemy)
        {
            Active.GetComponent<Friendly>().RandomSkill();


        }
        yield return new WaitForSeconds(3.0f);
        ChangeState(State.BackMoving);
    }
    IEnumerator moving(Vector3 pos)
    {
        StartCoroutine(RotatingToPosition(pos));
        Active.GetComponent<Animator>().SetBool("IsWalking", true);
        gos = Active.transform.position;
        Vector3 dir = pos - Active.transform.position;
        gos2 = dir;
        float dist = (dir.magnitude)-0.8f;
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
            Active.GetComponent<Animator>().SetBool("IsWalking", false);
            ChangeState(State.Battle);
        }
        
    }
    IEnumerator backmoving(Vector3 pos)
    {
        StartCoroutine(RotatingToPosition(pos));
        Active.GetComponent<Animator>().SetBool("IsWalking", true);
        
        Vector3 dir = pos - Active.transform.position;
        float dist = (dir.magnitude);
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
            
            Active.GetComponent<Animator>().SetBool("IsWalking", false);
            StartCoroutine(RotatingToPosition(gos2));
            /*if(Active == Enemy)
            {
                Active = Friendly;
                Target = Enemy;
            }
            else if(Active==Friendly)
            {
                Active = Enemy;
                Target=Friendly;
            }
            ChangeState(State.Moving);*/
        }
        

    }
    IEnumerator RotatingToPosition(Vector3 pos)
    {
        Vector3 dir = (pos - Active.transform.position).normalized;
        float Angle = Vector3.Angle(Active.transform.forward, dir);
        float rotDir = 1.0f;
        if (Vector3.Dot(Active.transform.right, dir) < 0.0f)
        {
            rotDir = -rotDir;
        }

        while (Angle > 0.0f)
        {

            float delta = 360.0f * Time.deltaTime;
            if (delta > Angle)
            {
                delta = Angle;
            }
            Angle -= delta;
            Active.transform.Rotate(Vector3.up * rotDir * delta, Space.World);

            yield return null;
        }
    }
}