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

    public GameObject[] Player;
    public GameObject[] Enemy;
    public GameObject Active;
    public List<GameObject> PlayList;
    public List<GameObject> PlayerSpeedCheck;
    public GameObject SelectedCharacter;
    public GameObject SelectedCharacterTarget;
    public GameObject mySelectRing;
    public GameObject mySelectTargetRing;
    bool FastSpeedCheck;
    int Skill=0;
    Vector3 gos; //원래위치값
    Vector3 gos2; //원래바라보고있던위치값
    public enum State
    {
        Create, Choice,ActiveCheck,Moving, BackMoving, Battle, End
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
                for (int i = 0; i < PlayList.Count; ++i)
                {
                    PlayList[i].GetComponent<BattleCharacter>().Active5 = true; //초이스단계에서 모든캐릭터 행동값을 트루로 만든다
                }
                for (int i = 0; i < Enemy.Length; ++i)
                {
                    for (int j = 0; j < Player.Length; ++j)
                    {
                        if (Enemy[i].GetComponent<BattleCharacter>().myTarget = null)
                        {
                            Enemy[i].GetComponent<BattleCharacter>().myTarget = Player[j];
                        }
                    }
                }
                break;
            
            case State.ActiveCheck:
                
                break;
            case State.Moving:
                for (int i = 0; i < Enemy.Length; ++i)
                {
                    if (Active == Enemy[i])
                    {
                        Active.GetComponent<BattleCharacter>().myTarget = Player[Random.Range(0,Player.Length)];
                    }
                }
                StartCoroutine(Moving(Active.GetComponent<BattleCharacter>().myTarget.transform.position));
                break;            
            case State.Battle:
                StartCoroutine(Attack(Skill));
                break;
            case State.BackMoving:
                StartCoroutine(BackMoving(gos));
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
                    if (Input.GetKeyDown(KeyCode.Q)) SelectedCharacter.GetComponent<BattleCharacter>().Skill = 0;
                    if (Input.GetKeyDown(KeyCode.W)) SelectedCharacter.GetComponent<BattleCharacter>().Skill = 1;
                    if (Input.GetKeyDown(KeyCode.E)) SelectedCharacter.GetComponent<BattleCharacter>().Skill = 2;
                    if (Input.GetMouseButtonDown(0))
                    {
                        if (Physics.Raycast(ray, out hit, 1000.0f, 1 << LayerMask.NameToLayer("Enemy")))
                        {
                            SelectedCharacterTarget = hit.collider.gameObject;
                            SelectedCharacter.GetComponent<BattleCharacter>().myTarget = SelectedCharacterTarget;
                        }
                    }

                }
                
                break;
            
            case State.ActiveCheck:
                int Check = 0;
                Active = PlayList[0];
                while (!Active.GetComponent<BattleCharacter>().Active5)
                {

                    Active = PlayList[Check];
                    ++Check;
                    if(Check == PlayList.Count)
                    {
                        break;
                    }
                    
                }
                if(Check==PlayList.Count)
                {
                    ChangeState(State.End);
                }
                if (Active.GetComponent<BattleCharacter>().Active5)
                {
                    ChangeState(State.Moving);
                }

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
        
        for(int i = 0; i < Player.Length; ++i) //플레이어갯수만큼 추가
        {
            PlayerSpeedCheck.Add(Player[i]);
        }
        for(int i = 0; i < Enemy.Length; ++i) // 에너미갯수만큼추가
        {
            PlayerSpeedCheck.Add(Enemy[i]);
        }
        while (PlayerSpeedCheck.Count > 0)
        {
            for (int i = 0; i < PlayerSpeedCheck.Count;)
            {
                FastSpeedCheck = true; //제일빠른지 체크
                for (int j = 0; j < PlayerSpeedCheck.Count; ++j)
                {
                    if (i == j) continue; //같은값끼리 비교할필요가없어서 컨티뉴
                    if (PlayerSpeedCheck[i].GetComponent<BattleCharacter>().speed < PlayerSpeedCheck[j].GetComponent<BattleCharacter>().speed) //나보다빠른게있는지 체크
                    {
                        FastSpeedCheck = false; 
                    }

                }
                if (FastSpeedCheck) // 제일빠르다면 
                {
                    PlayList.Add(PlayerSpeedCheck[i]); // 플레이리스트에 추가
                    PlayerSpeedCheck.RemoveAt(i);  // 스피드체크에서 제거 
                }
                else
                {
                    ++i;  //제거한게없을때만 i값 증가
                }

            }
        }
        for (int i = 0; i < Player.Length; ++i)
        {
            Player[i].GetComponent<BattleCharacter>().myTarget = Enemy[0];
        }
        
        

        /*PlayerList.Add(Player[0]);
        PlayerList.Sort();*/

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
            ChangeState(State.ActiveCheck);
        }
        if(SelectedCharacter!=null) //캐릭터가 선택되어있을경우
        {
            mySelectRing.SetActive(true); //캐릭터가 선택되면 링온
            mySelectRing.transform.position = SelectedCharacter.transform.position; //링을 선택캐릭터위치로
            if (SelectedCharacter.GetComponent<BattleCharacter>().myTarget != null) //캐릭터가 타겟으로 잡고있는게 있는지 확인
            {
                mySelectTargetRing.SetActive(true); // 타겟링온 
                mySelectTargetRing.transform.position = SelectedCharacter.GetComponent<BattleCharacter>().myTarget.transform.position; // 타겟링을 타겟위치로 
            }
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
        if (Active == Player[0] || Active == Player[1] || Active == Player[2]) Active.GetComponent<BattleCharacter>().ChoiceSkill(Active.GetComponent<BattleCharacter>().Skill);
        else if (Active == Enemy[0] || Active == Enemy[1] || Active == Enemy[2]) Active.GetComponent<BattleCharacter>().RandomSkill();
        yield return new WaitForSeconds(3.0f);
        ChangeState(State.BackMoving);
    }
    IEnumerator Moving(Vector3 pos)
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
        if (Mathf.Approximately(dist, 0.0f))
        {
            Active.GetComponent<Animator>().SetBool("IsWalking", false);            
            ChangeState(State.Battle);
        }
        

    }
    IEnumerator BackMoving(Vector3 pos)
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
        if (Mathf.Approximately(dist, 0.0f))
        {
            Active.GetComponent<BattleCharacter>().Active5 = false;
            Active.GetComponent<Animator>().SetBool("IsWalking", false);
            StartCoroutine(RotatingToPosition(gos2));
            yield return new WaitForSeconds(0.5f);
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