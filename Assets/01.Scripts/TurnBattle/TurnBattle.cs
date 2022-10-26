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
    
    public GameObject[] Player;
    public GameObject[] Enemy;
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
    public TMPro.TMP_Text GameOverTxt = null;

    int Check = 0;
    bool VictoryCheck;
    bool FastSpeedCheck;
    int Skill=0;
    Vector3 gos; //������ġ��
    Vector3 gos2; //�����ٶ󺸰��ִ���ġ��
    
    public enum State
    {
        Create, Choice,ActiveCheck, Battle, End ,GameOver
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
                for (int i = 0; i < PlayList.Count; ++i)
                {
                    PlayList[i].GetComponent<BattleCharacter>().Active5 = true; //���̽��ܰ迡�� ���ĳ���� �ൿ���� Ʈ��� �����
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
            case State.Battle:
                for (int i = 0; i < Enemy.Length; ++i)
                {
                    if (Active == Enemy[i])
                    {
                        Active.GetComponent<BattleCharacter>().myTarget = Player[Random.Range(0, Player.Length)];
                        Active.GetComponent<BattleCharacter>().myTarget = Player[Random.Range(0, Player.Length)];
                        if (Active.GetComponent<BattleCharacter>().myTarget.GetComponent<BattleCharacter>().State == STATE.Die)
                        {
                            EnemyTargetDie(Active);
                        }
                    }
                }
                StartCoroutine(Moving(Active.GetComponent<BattleCharacter>().myTarget.transform.position));
                break;            
            case State.End:
                break;
            case State.GameOver:
                GameOverCanvas.SetActive(true);
                if (VictoryCheck)
                {
                    GameOverTxt.text = "�� ��";
                }
                else if (!VictoryCheck)
                {
                    GameOverTxt.text = "�� ��";
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
                Active = PlayList[0];
                while (!Active.GetComponent<BattleCharacter>().Active5 || Active.GetComponent<BattleCharacter>().State == STATE.Die)
                {
                    Active = PlayList[Check];
                    ++Check;
                    if (Check == PlayList.Count)
                    {
                        break;
                    }
                }
                if (Check == PlayList.Count)
                {
                    ChangeState(State.Choice);
                }
                else
                {
                    if (Active.GetComponent<BattleCharacter>().Active5)
                    {
                        ChangeState(State.Battle);
                    }
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
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        for (int i = 0; i < Player.Length; ++i) //�÷��̾����ŭ �߰�
        {
            PlayerSpeedCheck.Add(Player[i]);
        }
        for(int i = 0; i < Enemy.Length; ++i) // ���ʹ̰�����ŭ�߰�
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
                    if (PlayerSpeedCheck[i].GetComponent<BattleCharacter>().speed < PlayerSpeedCheck[j].GetComponent<BattleCharacter>().speed) //�����ٺ������ִ��� üũ
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
        for (int i = 0; i < Player.Length; ++i) // �÷��̾��� �⺻Ÿ�ټ���
        {
            Player[i].GetComponent<BattleCharacter>().myTarget = Enemy[0]; 
        }
    }
    void Start()
    {
        ChangeState(State.Choice);
        for (int i = 0; i < Player.Length; ++i) //ĳ���͹�ư�� ��ĳ���� �Ҵ�
        {
            CharacterButton[i].gameObject.GetComponent<CharacterButton>().myCharacter = Player[i];
        }
    }
    void Update()
    {
        
        mySelectRing.SetActive(false); //ĳ���Ͱ� ���õǱ������� �� ����
        mySelectTargetRing.SetActive(false); // ĳ���� Ÿ�� �� ����
        StateProcess();
        Victory();
        Lose();
        PlayerTargetDie();


    }
    public void PlayerTargetDie()
    {
        for (int i = 0; i < Player.Length; ++i)
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
    public void Victory()
    {
        foreach (GameObject act in Enemy) if (act.GetComponent<BattleCharacter>().State == STATE.Live) return;
        VictoryCheck = true;
        ChangeState(State.GameOver);
    }
    public void Lose()
    {
        foreach (GameObject act in Player) if (act.GetComponent<BattleCharacter>().State == STATE.Live) return;
        VictoryCheck = false;
        ChangeState(State.GameOver);
    }
    public void EnemyTargetDie(GameObject v)
    {
        foreach (GameObject act in Player) if (act.GetComponent<BattleCharacter>().State == STATE.Live) v.GetComponent<BattleCharacter>().myTarget = act;
    }
    public void BattleStart() //���ݹ�ư Ŭ���� �Լ�
    {
        ChangeState(State.ActiveCheck);
        //Ŭ���� ����ĳ���� null������ ���� ��ư�� ��Ȱ��ȭ
        SelectedCharacter = null;
        AttackStartButton.interactable = false;
        RunButton.interactable = false;
        for (int i = 0; i < CharacterButton.Length; ++i)
        {
            CharacterButton[i].interactable = false;
        }
    }
    

    IEnumerator Attack(int s)
    {
        foreach(GameObject act in Player)
        {
            if(Active==act)
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
        
        yield return new WaitForSeconds(3.0f);
        StartCoroutine(BackMoving(gos));
    }
    IEnumerator Moving(Vector3 pos)
    {
        StartCoroutine(RotatingToPosition(pos,true));
        Active.GetComponent<Animator>().SetBool("IsWalking", true);
        gos = Active.transform.position;
        Vector3 dir = pos - Active.transform.position;
        gos2 = dir; 
        float dist = (dir.magnitude)-0.8f; //ĳ���Ͱ� ��ġ�� �ȵǴϱ� �Ÿ����� -0.8 ��ŭ�ش�
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
            StartCoroutine(Attack(Skill));
        }
    }
    IEnumerator BackMoving(Vector3 pos)
    {
        StartCoroutine(RotatingToPosition(pos,true));
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
            StartCoroutine(RotatingToPosition(gos2,false));
            yield return new WaitForSeconds(0.5f);
            ChangeState(State.ActiveCheck);            
        }
    }
    IEnumerator RotatingToPosition(Vector3 pos,bool v)
    {
        Vector3 dir;
        if (v)  dir = (pos - Active.transform.position).normalized;
        else    dir=pos.normalized;        
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