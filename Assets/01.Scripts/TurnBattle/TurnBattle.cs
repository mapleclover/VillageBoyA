///박진
///턴배틀
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using static UnityEditor.PlayerSettings;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class TurnBattle : MonoBehaviour
{
    public Transform PlayerParent;
    public List<GameObject> Player;
    public Transform EnemyParent;
    public List<GameObject> Enemy;
    public GameObject Active;
    public List<GameObject> PlayList;
    public List<GameObject> PlayerSpeedCheck;
    public GameObject SelectedCharacter;
    public GameObject SelectedCharacterTarget;
    public GameObject mySelectRing;
    public GameObject mySelectTargetRing;
    public Button[] CharacterButton;
    public Button AttackStartButton;
    public Button RunButton;
    public static TurnBattle Inst = null;

    public GameObject GameOverCanvas;
    public GameObject VictoryImage;
    public GameObject LoseImage;
    public Slider[] CharacterHpbar;
    public Slider EnemyHpbar;
    public List<Slider> EnHpbar;


    int runPercentage = 50;
    string PlayerCharacterName;
    string EnemyCharacterName;
    int x;
    public int HealingPotion;
    public int BattleTurn = 0;
    int Check = 0;
    bool VictoryCheck;
    bool FastSpeedCheck;    
    Vector3 pos;
    Vector3 pos2;
    public int Gold=0;
    public GameObject speedChanger;
    public enum State
    {
        Create, Choice, ActiveCheck, Battle, End, GameOver
    }
    public State myState = State.Create;
    void ChangeState(State s)
    {
        if (myState == s) return;
        myState = s;
        switch (myState)
        {
            case State.Create:
                for (int i = 0; i < CharacterButton.Length; ++i)
                {
                    CharacterButton[i].interactable = false;
                }
                break;
            case State.Choice:
                Check = 0;
                //선택단계에서 버튼활성화
                for (int i = 0; i < CharacterButton.Length; ++i)
                {
                    CharacterButton[i].interactable = true;
                }
                AttackStartButton.interactable = true;
                RunButton.interactable = true;                
                for (int i = 0; i < Enemy.Count; ++i)
                {
                    for (int j = 0; j < Player.Count; ++j)
                    {
                        if (Enemy[i].GetComponent<BattleCharacter>().myTarget = null)
                        {
                            Enemy[i].GetComponent<BattleCharacter>().myTarget = Player[j];
                        }
                    }
                }
                break;
            case State.ActiveCheck:
                Victory();
                Lose();
                break;
            case State.Battle:
                for (int i = 0; i < Enemy.Count; ++i)
                {
                    if (Active == Enemy[i])
                    {
                        Active.GetComponent<BattleCharacter>().myTarget = Player[Random.Range(0, Player.Count)];
                        Active.GetComponent<BattleCharacter>().myTarget = Player[Random.Range(0, Player.Count)];
                        if (Active.GetComponent<BattleCharacter>().myTarget.GetComponent<BattleCharacter>().State == STATE.Die)
                        {
                            EnemyTargetDie(Active);
                        }
                    }
                }
                if (!Active.GetComponent<BattleCharacter>().ActiveHeal)
                    StartCoroutine(Moving(Active.GetComponent<BattleCharacter>().myTarget.transform.position, Active.GetComponent<BattleCharacter>().longAttackCheck));
                else
                    StartCoroutine(HealingActive());
                break;
            case State.End:
                break;
            case State.GameOver:
                speedChanger.SetActive(false);
                Time.timeScale = 1.0f;
                if (VictoryCheck)
                {
                    foreach (GameObject act in Player)
                    {
                        act.GetComponent<Animator>().SetTrigger("Victory");
                    }
                    GameOverCanvas.SetActive(true);
                    VictoryImage.SetActive(true);

                }
                else if (!VictoryCheck)
                {
                    GameOverCanvas.SetActive(true);
                    LoseImage.SetActive(true);
                    
                }
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
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); //메인카메라의 위치값
                RaycastHit hit;
                if (SelectedCharacter != null)
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        if (Physics.Raycast(ray, out hit, 1000.0f, 1 << LayerMask.NameToLayer("Enemy")))
                        {
                            SelectedCharacterTarget = hit.collider.gameObject;
                            SelectedCharacter.GetComponent<BattleCharacter>().myTarget = SelectedCharacterTarget;
                        }
                    }
                }
                if (SelectedCharacter != null) //캐릭터가 선택되어있을경우
                {
                    mySelectRing.SetActive(true); //캐릭터가 선택되면 링온
                    mySelectRing.transform.position = SelectedCharacter.transform.position; //링을 선택캐릭터위치로
                    if (SelectedCharacter.GetComponent<BattleCharacter>().myTarget != null) //캐릭터가 타겟으로 잡고있는게 있는지 확인
                    {
                        mySelectTargetRing.SetActive(true); // 타겟링온 
                        mySelectTargetRing.transform.position = SelectedCharacter.GetComponent<BattleCharacter>().myTarget.transform.position; // 타겟링을 타겟위치로 
                    }
                }
                break;
            case State.ActiveCheck:
                
                PlayerTargetDie();
                Active = PlayList[0];
                while (!Active.GetComponent<BattleCharacter>().TurnActive || Active.GetComponent<BattleCharacter>().State != STATE.Live)
                {
                    ++Check;
                    if (Check == PlayList.Count) break;
                    Active = PlayList[Check];
                }
                if (Check == PlayList.Count)
                {
                    ++BattleTurn;
                    ChangeState(State.Choice);
                }
                else
                {
                    if (Active.GetComponent<BattleCharacter>().TurnActive) ChangeState(State.Battle);
                }
                break;
            case State.Battle:
                break;
            case State.End:
                break;
            case State.GameOver:                
                break;
        }
    }
    private void Awake()
    {
        Inst = this;
        Cursor.visible = true; // 커서안보이는거 트루로
        Cursor.lockState = CursorLockMode.None; //커서잠금모드 해제
        EnemyCharacterName = SceneLoad.Instance.MonsterType;
        InstantiateEnemy();
        InstantiatePlayerCharacter();

    }
    void Start()
    {
        if (DataController.instance.gameData.myItemCount.ContainsKey("포션"))
            HealingPotion = DataController.instance.gameData.myItemCount["포션"]; //포션
        else HealingPotion = 0;

        for (int i = 0; i < Player.Count; ++i) //플레이어갯수만큼 추가
        {
            PlayerSpeedCheck.Add(Player[i]);
        }
        for (int i = 0; i < Enemy.Count; ++i) // 에너미갯수만큼추가
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
                    if (PlayerSpeedCheck[i].GetComponent<BattleCharacter>().myStat.Speed < PlayerSpeedCheck[j].GetComponent<BattleCharacter>().myStat.Speed)
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
        ChangeState(State.Choice);

    }
    void Update()
    {

        mySelectRing.SetActive(false); //캐릭터가 선택되기전까지 링 오프
        mySelectTargetRing.SetActive(false); // 캐릭터 타겟 링 오프
        StateProcess();

        FollowEnemyHpbar();

    }
    public void VictoryOk()
    {
        if (VictoryCheck)
        {
            if (DataController.instance.gameData.questID == 30 && DataController.instance.gameData.questActionIndex == 1)
            {
                if (SceneLoad.Instance.MonsterType == "Fox")
                {
                    DataController.instance.gameData.questClear = true;
                    DataController.instance.gameData.questActionIndex += 1;
                    DataController.instance.gameData.victoryComplete[0] = true;
                    
                }
            }
            SceneLoad.Instance.ChangeScene("06.Field");
        }
    }
    void InstantiatePlayerCharacter()
    {
        for (int i = 0; i < DataController.instance.gameData.partyMember.Length; ++i)
        {
            CharacterButton[i].gameObject.SetActive(true);
            CharacterButton[i].GetComponent<CharacterButton>().MyChosenAttack.SetActive(true);
            switch (i)
            {
                case 0:
                    PlayerCharacterName = "KongForBattle";
                    x = -2;
                    break;
                case 1:
                    PlayerCharacterName = "JinForBattle";
                    x = 0;
                    break;
                case 2:
                    PlayerCharacterName = "EmberForBattle";
                    x = 2;
                    break;
            }
            GameObject obj = Instantiate(Resources.Load($"Prefabs/ForBattle/{PlayerCharacterName}"), PlayerParent) as GameObject;
            Vector3 pos = new Vector3(x, 0, 0);
            obj.transform.localPosition = pos;
            Player.Add(obj);
            CharacterButton[i].GetComponent<CharacterButton>().myCharacter = Player[i];
            Player[i].GetComponent<BattleCharacter>().myHpBar = CharacterHpbar[i];
            Player[i].GetComponent<BattleCharacter>().myTarget = Enemy[0];
            Player[i].GetComponent<BattleCharacter>().ValuemyHpmaxHP();
        }
    }
    void InstantiateEnemy()
    {
        for (int i = 0; i < SceneLoad.Instance.MonsterCount; ++i)
        {
            GameObject obj = Instantiate(Resources.Load($"Prefabs/ForBattle/{EnemyCharacterName}"), EnemyParent) as GameObject;
            Vector3 pos = new Vector3(-2 + (2 * i), 0, 0);
            obj.transform.localPosition = pos;
            Enemy.Add(obj);
            Enemy[i].GetComponent<BattleCharacter>().myStat.Speed = SceneLoad.Instance.MonsterSpeed;
            EnHpbar.Add(Instantiate(EnemyHpbar, Enemy[0].GetComponent<BattleCharacter>().Canvas.transform));
            Enemy[i].GetComponent<BattleCharacter>().myHpBar = EnHpbar[i];
            pos = Enemy[i].GetComponent<BattleCharacter>().transform.position;
            pos.y += 2.0f;
            pos2 = Camera.main.WorldToScreenPoint(pos);
            EnHpbar[i].transform.position = pos2;
            Enemy[i].GetComponent<BattleCharacter>().ValuemyHpmaxHP();
            Enemy[i].GetComponent<BattleCharacter>().Stunned = SceneLoad.Instance.BackAttack;
        }
    }


    void FollowEnemyHpbar() //몬스터hp바가 몬스터를 따라다니도록
    {
        for (int i = 0; i < Enemy.Count; ++i)
        {
            pos = Enemy[i].GetComponent<BattleCharacter>().transform.position;
            pos.y += 2.0f;
            pos2 = Camera.main.WorldToScreenPoint(pos);
            EnHpbar[i].transform.position = pos2;
        }
    }
    void PlayerTargetDie() //플레이어가 타겟으로삼는상대가죽으면
    {
        for (int i = 0; i < Player.Count; ++i)
        {
            if (Player[i].GetComponent<BattleCharacter>().myTarget.GetComponent<BattleCharacter>().State == STATE.Die)
            {
                foreach (GameObject act in Enemy)
                {
                    if (act.GetComponent<BattleCharacter>().State == STATE.Live) Player[i].GetComponent<BattleCharacter>().myTarget = act;
                }
            }
        }
    }
    void Victory() //승리시
    {
        foreach (GameObject act in Enemy) if (act.GetComponent<BattleCharacter>().State != STATE.Die) return;
        VictoryCheck = true;
        ChangeState(State.GameOver);
        Time.timeScale = 1.0f;
    }
    void Lose() //패배시
    {
        foreach (GameObject act in Player) if (act.GetComponent<BattleCharacter>().State != STATE.Die) return;
        VictoryCheck = false;
        ChangeState(State.GameOver);
        Time.timeScale = 1.0f;
    }
    void EnemyTargetDie(GameObject v) //적이 타겟으로 하고있는상대가죽으면 살아있는상대로변경
    {
        foreach (GameObject act in Player)
        {
            if (act.GetComponent<BattleCharacter>().State == STATE.Live)
            {
                v.GetComponent<BattleCharacter>().myTarget = act;
            }
        }
    }
    public void BattleRun() //도망버튼 클릭시 함수
    {
        if (runPercentage > Random.Range(0, 100))
        {
            SceneLoad.Instance.ChangeScene("06.Field");
        }
        else
        {
            for (int i = 0; i < Enemy.Count; ++i)
            {
                Enemy[i].GetComponent<BattleCharacter>().TurnActive = true; //적의 캐릭터 행동값을 트루로 만든다                  
            }
            if (!SelectedCharacterAttack.Inst.myAttack.activeSelf && !SelectedCharacterAttack.Inst.mySelectAttack.activeSelf)
            {
                ChangeState(State.ActiveCheck);
                AttackStartButton.interactable = false;
                RunButton.interactable = false;
                for (int i = 0; i < CharacterButton.Length; ++i)
                {
                    CharacterButton[i].interactable = false;  //버튼비활성화
                    CharacterButton[i].GetComponent<CharacterButton>().mySelectCharacter.SetActive(false);
                }
            }
            //클릭시 선택캐릭터 null값으로 변경 버튼들 비활성화

            SelectedCharacterAttack.Inst.myAttack.SetActive(false);
            SelectedCharacterAttack.Inst.mySelectAttack.SetActive(false);
            SelectedCharacterAttack.Inst.myActiveAttack.SetActive(true);
            SelectedCharacter = null;
            runPercentage += 25;
        }
    }
    public void BattleStart() //공격버튼 클릭시 함수
    {
        for (int i = 0; i < PlayList.Count; ++i)
        {
            PlayList[i].GetComponent<BattleCharacter>().TurnActive = true; //모든캐릭터 행동값을 트루로 만든다                  
        }
        if (!SelectedCharacterAttack.Inst.myAttack.activeSelf && !SelectedCharacterAttack.Inst.mySelectAttack.activeSelf)
        {
            ChangeState(State.ActiveCheck);
            AttackStartButton.interactable = false;
            RunButton.interactable = false;
            for (int i = 0; i < CharacterButton.Length; ++i)
            {
                CharacterButton[i].interactable = false;  //버튼비활성화
                CharacterButton[i].GetComponent<CharacterButton>().mySelectCharacter.SetActive(false);
            }
        }
        //클릭시 선택캐릭터 null값으로 변경 버튼들 비활성화

        SelectedCharacterAttack.Inst.myAttack.SetActive(false);
        SelectedCharacterAttack.Inst.mySelectAttack.SetActive(false);
        SelectedCharacterAttack.Inst.myActiveAttack.SetActive(true);
        SelectedCharacter = null;


    }
    IEnumerator HealingActive()
    {
        Active.GetComponent<BattleCharacter>().Healing();
        Active.GetComponent<BattleCharacter>().TurnActive = false;
        yield return new WaitForSeconds(1.0f);
        ChangeState(State.ActiveCheck);

    }

    IEnumerator Attack(Vector3 gos, Vector3 gos2, bool v = false) //공격
    {
        Active.GetComponent<BattleCharacter>().TurnActive = false;
        foreach (GameObject act in Player)
        {
            if (Active == act)
            {
                Active.GetComponent<BattleCharacter>().ChoiceSkill(Active.GetComponent<BattleCharacter>().Skill);

            }
        }
        foreach (GameObject act in Enemy)
        {
            if (Active == act)
            {
                Active.GetComponent<BattleCharacter>().RandomSkill();
            }
        }

        yield return new WaitForSeconds(0.5f);
        while (!Active.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Idle")) yield return null;
        if (!v) StartCoroutine(BackMoving(gos, gos2));
        else ChangeState(State.ActiveCheck);

    }
    IEnumerator Moving(Vector3 pos, bool v = false) //적한테
    {
        StartCoroutine(RotatingToPosition(pos, true));
        Vector3 gos = Active.transform.position;
        Vector3 dir = pos - Active.transform.position;
        Vector3 gos2 = dir;
        if (!v) // v값으로 원거리 공격인지 확인
        {
            Active.GetComponent<Animator>().SetBool("IsWalking", true);

            float dist = (dir.magnitude) - 1.5f; //캐릭터가 겹치면 안되니까 -
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
                StartCoroutine(Attack( gos, gos2));
            }
        }
        else
        {
            StartCoroutine(Attack(gos, gos2, v));
        }
    }
    IEnumerator BackMoving(Vector3 pos, Vector3 gos2) //원래자리로
    {
        StartCoroutine(RotatingToPosition(pos, true));
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

            Active.GetComponent<Animator>().SetBool("IsWalking", false);
            StartCoroutine(RotatingToPosition(gos2, false));
            yield return new WaitForSeconds(0.5f);
            ChangeState(State.ActiveCheck);
        }
    }
    IEnumerator RotatingToPosition(Vector3 pos, bool v) //회전
    {
        Vector3 dir;
        if (v) dir = (pos - Active.transform.position).normalized;
        else dir = pos.normalized;
        float Angle = Vector3.Angle(Active.transform.forward, dir);
        float rotDir = 1.0f;
        if (Vector3.Dot(Active.transform.right, dir) < 0.0f) rotDir = -rotDir;
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