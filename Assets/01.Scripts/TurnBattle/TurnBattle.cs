//�ۼ��� : ����
//���� : �Ϲ�Ʋ

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static EnemySC;

public class TurnBattle : MonoBehaviour
{
    public static TurnBattle Inst = null;
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

    public TMP_Text RewardGold;

    public GameObject GameOverCanvas;
    public GameObject VictoryImage;
    public GameObject LoseImage;

    public Slider[] CharacterHpbar;
    public Slider EnemyHpbar;
    public List<Slider> EnHpbar;

    public Button[] CharacterButton;
    public Button AttackStartButton;
    public Button RunButton;

    public ActionCost myActionCost;
    int runPercentage = 50;
    string PlayerCharacterName;
    string EnemyCharacterName;
    float x;
    public int HealingPotion;
    public int BattleTurn = 0;
    int Check = 0;
    bool VictoryCheck;
    bool FastSpeedCheck;
    Vector3 pos;
    Vector3 pos2;
    public GameObject speedChanger;
    bool bossBattle = false;
    public GameObject[] victoryItemSlots;
    public GameObject[] myVictoryItems;
    private DataController myData;
    

    public enum State
    {
        Create,
        Choice,
        ActiveCheck,
        Battle,
        End,
        GameOver
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
                if(!bossBattle)RunButton.interactable = true;
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
                OnTotalCost();
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
                        if (Active.GetComponent<BattleCharacter>().myTarget.GetComponent<BattleCharacter>().State ==
                            STATE.Die)
                        {
                            EnemyTargetDie(Active);
                        }
                    }
                }
                if (!Active.GetComponent<BattleCharacter>().ActiveHeal)
                    StartCoroutine(Moving(Active.GetComponent<BattleCharacter>().myTarget.transform.position,
                        Active.GetComponent<BattleCharacter>().longAttackCheck));
                else
                    StartCoroutine(HealingActive());
                break;
            case State.End:
                break;
            case State.GameOver:
                speedChanger.SetActive(false);
                if (VictoryCheck)
                {
                    foreach (GameObject act in Player) act.GetComponent<Animator>().SetTrigger("Victory");
                    GameOverCanvas.SetActive(true);
                    switch (SceneLoad.Instance.MonsterType)
                    {
                        case "Fox":
                            if (myData.gameData.questID.Equals(30) &&
                           myData.gameData.questActionIndex.Equals(1))
                            {
                                ShowEarnedItem(0, 0);                              
                            }
                            RewardGold.text = $"{5 * Enemy.Count}";
                                                     
                            break;
                        case "BossFox":
                            if (myData.gameData.questID.Equals(30) &&
                          myData.gameData.questActionIndex.Equals(2))
                            {
                                ShowEarnedItem(1, 0);
                            }
                            RewardGold.text = $"{30 * Enemy.Count}";
                            break;
                        case "Rhydron":
                            if (myData.gameData.questID.Equals(40) &&
                    myData.gameData.questActionIndex.Equals(1))
                            {
                                ShowEarnedItem(2, 0);
                            }
                            RewardGold.text = $"{5 * Enemy.Count}";
                            break;
                        case "Milkcow":
                            if (myData.gameData.questID.Equals(40) &&
                   myData.gameData.questActionIndex.Equals(3))
                            {
                                ShowEarnedItem(3, 0);
                            }
                            RewardGold.text = $"{5 * Enemy.Count}";
                            break;
                        case "GenshinGolem":
                            if (myData.gameData.questID.Equals(40) &&
                   myData.gameData.questActionIndex.Equals(5))
                            {
                                ShowEarnedItem(4, 0);
                            }
                            RewardGold.text = $"{100 * Enemy.Count}";
                            break;
                    }
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

    void ShowEarnedItem(int itemIndex, int itemSlot)
    {
        GameObject obj = Instantiate(myVictoryItems[itemIndex]);
        RawImage img = obj.GetComponentInChildren<RawImage>();
        img.transform.SetParent(victoryItemSlots[itemSlot].transform);
        img.transform.GetComponent<RectTransform>().localScale = new Vector3(1.0f, 1.0f, 1.0f);
        img.GetComponent<RectTransform>().sizeDelta = new Vector2(80, 80);
        img.transform.localPosition = Vector2.zero;
        Destroy(obj);
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
                        mySelectTargetRing.transform.position = SelectedCharacter.GetComponent<BattleCharacter>()
                            .myTarget.transform.position; // Ÿ�ٸ��� Ÿ����ġ�� 
                    }
                }
                break;
            case State.ActiveCheck:
                PlayerTargetDie();
                Active = PlayList[0];
                while (!Active.GetComponent<BattleCharacter>().TurnActive ||
                       Active.GetComponent<BattleCharacter>().State != STATE.Live)
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
        myData = DataController.instance;
        InstantiateEnemy();
        InstantiatePlayerCharacter();
    }
 
    void Start()
    {      
        if (myData.gameData.itemList.Contains("����"))
        {
            int index = myData.gameData.itemList.IndexOf("����");
            HealingPotion = myData.gameData.itemCount[index]; //����
        }
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
                    if (PlayerSpeedCheck[i].GetComponent<BattleCharacter>().myStat.Speed <
                        PlayerSpeedCheck[j].GetComponent<BattleCharacter>().myStat.Speed)
                    {
                        FastSpeedCheck = false;
                    }
                }

                if (FastSpeedCheck) // ���Ϻ����ٸ� 
                {
                    PlayList.Add(PlayerSpeedCheck[i]); // �÷��̸���Ʈ�� �߰�
                    PlayerSpeedCheck.RemoveAt(i); // ���ǵ�üũ���� ���� 
                }
                else
                {
                    ++i; //�����ѰԾ������� i�� ����
                }
            }
        }
        OnTotalCost();
        ChangeState(State.Choice);
    }

    void Update()
    {
        mySelectRing.SetActive(false); //ĳ���Ͱ� ���õǱ������� �� ����
        mySelectTargetRing.SetActive(false); // ĳ���� Ÿ�� �� ����
        StateProcess();
    }

    public void OnTotalCost()
    {
        int a = 0;
        for (int i = 0; i < Player.Count; ++i)
        {
            if(Player[i].GetComponent<BattleCharacter>().State!=STATE.Die)
            a += Player[i].GetComponent<BattleCharacter>().myStat.orgData.SkillCost[Player[i].GetComponent<BattleCharacter>().Skill];
        }
        myActionCost.TotalCost(a);
    }
    void CheckVictoryAndType(int actionindex, int index)
    {
        myData.gameData.questClear = true;
        myData.gameData.questActionIndex += actionindex;
        myData.gameData.victoryComplete[index] = true;       
    }

    public void VictoryOk() //����â Ȯ�ι�ư�� Ŭ���� ������ �Լ�
    {
        if (VictoryCheck)
        {
            switch (SceneLoad.Instance.MonsterType)
            {
                case "Fox":
                    if (myData.gameData.questID == 30 &&
                        myData.gameData.questActionIndex == 1)
                    {
                        CheckVictoryAndType(1, 0); // �ߺ� �ڵ尡 ���� �Լ��� ��ü�߽��ϴ� -����                    
                    }
                    myData.gameData.gold += 5 * Enemy.Count;
                    break;

                case "BossFox":
                    if (myData.gameData.questID == 30 &&
                        myData.gameData.questActionIndex == 2)
                    {
                        CheckVictoryAndType(1, 1);// ��տ��� ���� ��¯ �� �̱� �¸�������                                              
                    }
                    myData.gameData.gold += 30 * Enemy.Count;
                    break;

                case "Rhydron":
                    if (myData.gameData.questID == 40 &&
                        myData.gameData.questActionIndex == 1)
                    {
                        CheckVictoryAndType(1, 2); // ���己���� ��¯ �� �̱� �¸�������
                    }
                    myData.gameData.gold += 5 * Enemy.Count;
                    break;
                case "Milkcow":
                    if (myData.gameData.questID == 40 &&
                        myData.gameData.questActionIndex == 3)
                    {
                        CheckVictoryAndType(1, 3); // ��ũī�� ���� ��¯ �� �̱� �¸�������                       
                    }
                    myData.gameData.gold += 5 * Enemy.Count;
                    break;

                case "GenshinGolem":
                    if (myData.gameData.questID == 40 &&
                        myData.gameData.questActionIndex == 5)
                    {
                        CheckVictoryAndType(1, 4); // ��ũī�� ���� ��¯ �� �̱� �¸�������
                    }
                    myData.gameData.gold += 100 * Enemy.Count;
                    break;
            }
            if (SceneLoad.Instance.MonsterType != "Rock")
            {
                SceneLoad.Instance.battleResult.victory = true;
            }
            for (int i = 0; i < Player.Count; ++i)
            {
                switch (i)
                {
                    case 0:
                        myData.gameData.Kong.HP =
                            (int)Player[0].GetComponent<BattleCharacter>().myStat.curHP;
                        break;
                    case 1:
                        myData.gameData.Jin.HP =
                            (int)Player[1].GetComponent<BattleCharacter>().myStat.curHP;
                        break;
                    case 2:
                        myData.gameData.Ember.HP =
                            (int)Player[2].GetComponent<BattleCharacter>().myStat.curHP;
                        break;
                }
            }
            SceneLoad.Instance.ChangeScene("06.Field");
        }
    }

    void InstantiatePlayerCharacter()
    {
        for (int i = 0; i < SceneLoad.Instance.PlayerCount; ++i)
        {
            CharacterButton[i].gameObject.SetActive(true);
            CharacterButton[i].GetComponent<CharacterButton>().MyChosenAttack.SetActive(true);
            switch (i)
            {
                case 0:
                    PlayerCharacterName = "KongForBattle";
                    x = -1.5f;
                    break;
                case 1:
                    PlayerCharacterName = "JinForBattle";
                    x = 0;
                    break;
                case 2:
                    PlayerCharacterName = "EmberForBattle";
                    x = 1.5f;
                    break;
            }

            GameObject obj = Instantiate(Resources.Load($"Prefabs/ForBattle/{PlayerCharacterName}"), PlayerParent) as GameObject;
            Vector3 pos = new Vector3(x, 0, 0);
            obj.transform.localPosition = pos;
            Player.Add(obj);
            Player[i].GetComponent<BattleCharacter>().myHpBar = CharacterHpbar[i];
            CharacterButton[i].GetComponent<CharacterButton>().myCharacter = Player[i];            
            Player[i].GetComponent<BattleCharacter>().myTarget = Enemy[0];            
        }
    }

    void InstantiateEnemy()
    {
        for (int i = 0; i < SceneLoad.Instance.MonsterCount; ++i)
        {
            
            GameObject obj = Instantiate(Resources.Load($"Prefabs/ForBattle/{EnemyCharacterName}"), EnemyParent) as GameObject;
            Vector3 pos = new Vector3(-2 + (2 * i), 0, 0);
            if (obj.GetComponent<BattleCharacter>().myStat.orgData.BattleType == PC.Type.Boss) pos = Vector3.zero;
            obj.transform.localPosition = pos;
            Enemy.Add(obj);
            BattleCharacter EnemyI = Enemy[i].GetComponent<BattleCharacter>();
            EnemyI.myStat.Speed = SceneLoad.Instance.MonsterSpeed;
            EnHpbar.Add(Instantiate(EnemyHpbar, EnemyI.Canvas.transform));
            EnemyI.myHpBar = EnHpbar[i];
            EnemyI.Stunned = SceneLoad.Instance.BackAttack;
            if (obj.GetComponent<BattleCharacter>().myStat.orgData.BattleType == PC.Type.Boss)
            {
                SoundTest.instance.PlayBGM("BGM_Boss");
                RunButton.interactable = false;
                bossBattle = true;
                break;
            }

        }
        if (Enemy[0].GetComponent<BattleCharacter>().myStat.orgData.BattleType != PC.Type.Boss)
        {
            SoundTest.instance.PlayBGM("BGM_Battle");
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
                    if (act.GetComponent<BattleCharacter>().State == STATE.Live)
                        Player[i].GetComponent<BattleCharacter>().myTarget = act;
                }
            }
        }
    }

    void Victory() //�¸���
    {
        foreach (GameObject act in Enemy)
            if (act.GetComponent<BattleCharacter>().State != STATE.Die)
                return;
        VictoryCheck = true;
        SoundTest.instance.PlayBGM("BGM_Victory");
        
        ChangeState(State.GameOver);
        Time.timeScale = 1.0f;
    }

    void Lose() //�й��
    {
        foreach (GameObject act in Player)
            if (act.GetComponent<BattleCharacter>().State != STATE.Die)
                return;
        VictoryCheck = false;
        SoundTest.instance.PlayBGM("BGM_Lose");
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
            if (!SelectedCharacterAttack.Inst.myAttack.activeSelf &&
                !SelectedCharacterAttack.Inst.mySelectAttack.activeSelf)
            {
                ChangeState(State.ActiveCheck);
                AttackStartButton.interactable = false;
                RunButton.interactable = false;
                for (int i = 0; i < CharacterButton.Length; ++i)
                {
                    CharacterButton[i].interactable = false; //��ư��Ȱ��ȭ
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

    Coroutine CostCheck = null;
    
    public void BattleStart() //���ݹ�ư Ŭ���� �Լ�
    {
        for (int i = 0; i < PlayList.Count; ++i)
        {
            PlayList[i].GetComponent<BattleCharacter>().TurnActive = true; //���ĳ���� �ൿ���� Ʈ��� �����                  
        }
        OnTotalCost();
        if (!SelectedCharacterAttack.Inst.myAttack.activeSelf &&
            !SelectedCharacterAttack.Inst.mySelectAttack.activeSelf)
        {            
            if (myActionCost.RemainingCost >= 0)
            {
                ChangeState(State.ActiveCheck);
                AttackStartButton.interactable = false;
                if (!bossBattle) RunButton.interactable = false;
                for (int i = 0; i < CharacterButton.Length; ++i)
                {
                    CharacterButton[i].interactable = false; //��ư��Ȱ��ȭ
                    CharacterButton[i].GetComponent<CharacterButton>().mySelectCharacter.SetActive(false);
                }
            }
            else
            {                
                if (CostCheck == null)
                {
                    CostCheck =  StartCoroutine(ActionCostNotEnough());
                }
            }
        }        
        //Ŭ���� ����ĳ���� null������ ���� ��ư�� ��Ȱ��ȭ
        SelectedCharacterAttack.Inst.mySelectCharacter.SetActive(false);
        SelectedCharacterAttack.Inst.myAttack.SetActive(false);
        SelectedCharacterAttack.Inst.mySelectAttack.SetActive(false);
        SelectedCharacterAttack.Inst.myActiveAttack.SetActive(true);
        SelectedCharacter = null;
    }
    IEnumerator ActionCostNotEnough()
    {
        float a = 3.0f;
        
        while(a>Mathf.Epsilon)
        {
            
            myActionCost.NotEnough.SetActive(false);
            yield return new WaitForSeconds(0.5f);
            myActionCost.NotEnough.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            a -= 1.0f;
        }
        CostCheck = null;
    }
    IEnumerator HealingActive()
    {
        Active.GetComponent<BattleCharacter>().Healing();
        SoundTest.instance.PlaySE("SFX_Heal");
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
                Active.GetComponent<BattleCharacter>().ChoiceSkill(Random.Range(0, 3));
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
                StartCoroutine(Attack(gos, gos2));
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