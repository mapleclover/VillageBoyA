///����
///�Ϲ�Ʋ
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
                //���ôܰ迡�� ��ưȰ��ȭ
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
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); //����ī�޶��� ��ġ��
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
                if (SelectedCharacter != null) //ĳ���Ͱ� ���õǾ��������
                {
                    mySelectRing.SetActive(true); //ĳ���Ͱ� ���õǸ� ����
                    mySelectRing.transform.position = SelectedCharacter.transform.position; //���� ����ĳ������ġ��
                    if (SelectedCharacter.GetComponent<BattleCharacter>().myTarget != null) //ĳ���Ͱ� Ÿ������ ����ִ°� �ִ��� Ȯ��
                    {
                        mySelectTargetRing.SetActive(true); // Ÿ�ٸ��� 
                        mySelectTargetRing.transform.position = SelectedCharacter.GetComponent<BattleCharacter>().myTarget.transform.position; // Ÿ�ٸ��� Ÿ����ġ�� 
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
        Cursor.visible = true; // Ŀ���Ⱥ��̴°� Ʈ���
        Cursor.lockState = CursorLockMode.None; //Ŀ����ݸ�� ����
        EnemyCharacterName = SceneLoad.Instance.MonsterType;
        InstantiateEnemy();
        InstantiatePlayerCharacter();

    }
    void Start()
    {
        if (DataController.instance.gameData.myItemCount.ContainsKey("����"))
            HealingPotion = DataController.instance.gameData.myItemCount["����"]; //����
        else HealingPotion = 0;

        for (int i = 0; i < Player.Count; ++i) //�÷��̾����ŭ �߰�
        {
            PlayerSpeedCheck.Add(Player[i]);
        }
        for (int i = 0; i < Enemy.Count; ++i) // ���ʹ̰�����ŭ�߰�
        {
            PlayerSpeedCheck.Add(Enemy[i]);
        }
        while (PlayerSpeedCheck.Count > 0)
        {
            for (int i = 0; i < PlayerSpeedCheck.Count;)
            {
                FastSpeedCheck = true; //���Ϻ����� üũ
                for (int j = 0; j < PlayerSpeedCheck.Count; ++j)
                {
                    if (i == j) continue; //���������� �����ʿ䰡��� ��Ƽ��
                    if (PlayerSpeedCheck[i].GetComponent<BattleCharacter>().myStat.Speed < PlayerSpeedCheck[j].GetComponent<BattleCharacter>().myStat.Speed)
                    {
                        FastSpeedCheck = false;
                    }
                }
                if (FastSpeedCheck) // ���Ϻ����ٸ� 
                {
                    PlayList.Add(PlayerSpeedCheck[i]); // �÷��̸���Ʈ�� �߰�
                    PlayerSpeedCheck.RemoveAt(i);  // ���ǵ�üũ���� ���� 
                }
                else
                {
                    ++i;  //�����ѰԾ������� i�� ����
                }
            }
        }
        ChangeState(State.Choice);

    }
    void Update()
    {

        mySelectRing.SetActive(false); //ĳ���Ͱ� ���õǱ������� �� ����
        mySelectTargetRing.SetActive(false); // ĳ���� Ÿ�� �� ����
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


    void FollowEnemyHpbar() //����hp�ٰ� ���͸� ����ٴϵ���
    {
        for (int i = 0; i < Enemy.Count; ++i)
        {
            pos = Enemy[i].GetComponent<BattleCharacter>().transform.position;
            pos.y += 2.0f;
            pos2 = Camera.main.WorldToScreenPoint(pos);
            EnHpbar[i].transform.position = pos2;
        }
    }
    void PlayerTargetDie() //�÷��̾ Ÿ�����λ�»�밡������
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
    void Victory() //�¸���
    {
        foreach (GameObject act in Enemy) if (act.GetComponent<BattleCharacter>().State != STATE.Die) return;
        VictoryCheck = true;
        ChangeState(State.GameOver);
        Time.timeScale = 1.0f;
    }
    void Lose() //�й��
    {
        foreach (GameObject act in Player) if (act.GetComponent<BattleCharacter>().State != STATE.Die) return;
        VictoryCheck = false;
        ChangeState(State.GameOver);
        Time.timeScale = 1.0f;
    }
    void EnemyTargetDie(GameObject v) //���� Ÿ������ �ϰ��ִ»�밡������ ����ִ»��κ���
    {
        foreach (GameObject act in Player)
        {
            if (act.GetComponent<BattleCharacter>().State == STATE.Live)
            {
                v.GetComponent<BattleCharacter>().myTarget = act;
            }
        }
    }
    public void BattleRun() //������ư Ŭ���� �Լ�
    {
        if (runPercentage > Random.Range(0, 100))
        {
            SceneLoad.Instance.ChangeScene("06.Field");
        }
        else
        {
            for (int i = 0; i < Enemy.Count; ++i)
            {
                Enemy[i].GetComponent<BattleCharacter>().TurnActive = true; //���� ĳ���� �ൿ���� Ʈ��� �����                  
            }
            if (!SelectedCharacterAttack.Inst.myAttack.activeSelf && !SelectedCharacterAttack.Inst.mySelectAttack.activeSelf)
            {
                ChangeState(State.ActiveCheck);
                AttackStartButton.interactable = false;
                RunButton.interactable = false;
                for (int i = 0; i < CharacterButton.Length; ++i)
                {
                    CharacterButton[i].interactable = false;  //��ư��Ȱ��ȭ
                    CharacterButton[i].GetComponent<CharacterButton>().mySelectCharacter.SetActive(false);
                }
            }
            //Ŭ���� ����ĳ���� null������ ���� ��ư�� ��Ȱ��ȭ

            SelectedCharacterAttack.Inst.myAttack.SetActive(false);
            SelectedCharacterAttack.Inst.mySelectAttack.SetActive(false);
            SelectedCharacterAttack.Inst.myActiveAttack.SetActive(true);
            SelectedCharacter = null;
            runPercentage += 25;
        }
    }
    public void BattleStart() //���ݹ�ư Ŭ���� �Լ�
    {
        for (int i = 0; i < PlayList.Count; ++i)
        {
            PlayList[i].GetComponent<BattleCharacter>().TurnActive = true; //���ĳ���� �ൿ���� Ʈ��� �����                  
        }
        if (!SelectedCharacterAttack.Inst.myAttack.activeSelf && !SelectedCharacterAttack.Inst.mySelectAttack.activeSelf)
        {
            ChangeState(State.ActiveCheck);
            AttackStartButton.interactable = false;
            RunButton.interactable = false;
            for (int i = 0; i < CharacterButton.Length; ++i)
            {
                CharacterButton[i].interactable = false;  //��ư��Ȱ��ȭ
                CharacterButton[i].GetComponent<CharacterButton>().mySelectCharacter.SetActive(false);
            }
        }
        //Ŭ���� ����ĳ���� null������ ���� ��ư�� ��Ȱ��ȭ

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

    IEnumerator Attack(Vector3 gos, Vector3 gos2, bool v = false) //����
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
    IEnumerator Moving(Vector3 pos, bool v = false) //������
    {
        StartCoroutine(RotatingToPosition(pos, true));
        Vector3 gos = Active.transform.position;
        Vector3 dir = pos - Active.transform.position;
        Vector3 gos2 = dir;
        if (!v) // v������ ���Ÿ� �������� Ȯ��
        {
            Active.GetComponent<Animator>().SetBool("IsWalking", true);

            float dist = (dir.magnitude) - 1.5f; //ĳ���Ͱ� ��ġ�� �ȵǴϱ� -
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
    IEnumerator BackMoving(Vector3 pos, Vector3 gos2) //�����ڸ���
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
    IEnumerator RotatingToPosition(Vector3 pos, bool v) //ȸ��
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