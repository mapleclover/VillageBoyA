///박진
///턴배틀
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using static UnityEditor.PlayerSettings;

public class TurnBattle : MonoBehaviour
{
    
    public GameObject[] Player=new GameObject[2];
    public GameObject[] Ene = new GameObject[2];
    public GameObject Active;
    public GameObject[] PlayerList;
    
    public GameObject SelectedCharacter;
    public GameObject SelectedCharacterTarget;
    public GameObject mySelectRing;
    public GameObject mySelectTargetRing;

    int Skill;
    Vector3 gos;
    Vector3 gos2;
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
                for (int i = 0; i < PlayerList.Length; ++i)
                {
                    PlayerList[i].GetComponent<Friendly>().Active5 = true; //초이스단계에서 모든캐릭터 행동값을 트루로 만든다
                }
                for (int i = 0; i < Ene.Length; ++i)
                {
                    for (int j = 0; j < Player.Length; ++j)
                    {
                        if (Ene[i].GetComponent<Friendly>().myTarget = null)
                        {
                            Ene[i].GetComponent<Friendly>().myTarget = Player[j];
                        }
                    }
                }
                break;
            case State.SpeedCheck:
                
                break;
            case State.ActiveCheck:
                
                break;
            case State.Moving:
                
                StartCoroutine(moving(Active.GetComponent<Friendly>().myTarget.transform.position));
                
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
                if (Input.GetMouseButtonDown(0))
                {
                    if (Physics.Raycast(ray, out hit, 1000.0f, 1 << LayerMask.NameToLayer("Friendly")))
                    {
                        SelectedCharacter = hit.collider.gameObject;

                    }
                }
                if (SelectedCharacter != null)
                {
                    if (Input.GetKeyDown(KeyCode.Q)) SelectedCharacter.GetComponent<Friendly>().Skill = 1;
                    if (Input.GetKeyDown(KeyCode.W)) SelectedCharacter.GetComponent<Friendly>().Skill = 2;
                    if (Input.GetKeyDown(KeyCode.E)) SelectedCharacter.GetComponent<Friendly>().Skill = 3;
                    if (Input.GetMouseButtonDown(0))
                    {
                        if (Physics.Raycast(ray, out hit, 1000.0f, 1 << LayerMask.NameToLayer("Enemy")))
                        {
                            SelectedCharacterTarget = hit.collider.gameObject;
                            SelectedCharacter.GetComponent<Friendly>().myTarget = SelectedCharacterTarget;
                        }
                    }

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
        
        PlayerList = new GameObject[Player.Length + Ene.Length];
        for (int i=0;i<Player.Length;++i)
        {
            PlayerList[i] = Player[i];
        }
        for(int i=0;i<Ene.Length;++i)
        {
            PlayerList[Player.Length + i] = Ene[i];
        }
        
    }

    void Start()
    {
        ChangeState(State.Choice);

    }


    void Update()
    {
        
        mySelectRing.SetActive(false); //캐릭터가 선택되기전까지 링 오프
        mySelectTargetRing.SetActive(false); // 캐릭터 타겟 링 오프
        StateProcess();
        if (Input.GetKeyDown(KeyCode.Return))        
        {
            SelectedCharacter = null;
            ChangeState(State.Moving);
        }
        if(SelectedCharacter!=null) //캐릭터가 선택되어있을경우
        {
            mySelectRing.SetActive(true); //캐릭터가 선택되면 링온
            mySelectRing.transform.position = SelectedCharacter.transform.position; //링을 선택캐릭터위치로
            if (SelectedCharacter.GetComponent<Friendly>().myTarget != null) //캐릭터가 타겟으로 잡고있는게 있는지 확인
            {
                mySelectTargetRing.SetActive(true); // 타겟링온 
                mySelectTargetRing.transform.position = SelectedCharacter.GetComponent<Friendly>().myTarget.transform.position; // 타겟링을 타겟위치로 
            }
        }

        float[] sort = { PlayerList[0].GetComponent<Friendly>().speed, PlayerList[1].GetComponent<Friendly>().speed,
            PlayerList[2].GetComponent<Friendly>().speed, PlayerList[3].GetComponent<Friendly>().speed,
            PlayerList[4].GetComponent<Friendly>().speed, PlayerList[5].GetComponent<Friendly>().speed };
        
        


    }

    //
    /*public void CharacterSelect(int i)
    {
        SelectedCharacter = Friendly[i];

    }*/
    //

    IEnumerator Attack(int s)
    {
        if (Active == Player[0] || Active == Player[1] || Active == Player[2]) Active.GetComponent<Friendly>().ChoiceSkill(Active.GetComponent<Friendly>().Skill);
        else if (Active == Ene[0] || Active == Ene[1] || Active == Ene[2]) Active.GetComponent<Friendly>().RandomSkill();
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
            ChangeState(State.ActiveCheck);            
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