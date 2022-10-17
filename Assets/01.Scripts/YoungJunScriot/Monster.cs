using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// 박영준, 몬스터 상태기계
public class Monster : CharacterMoveMent
{
    Vector3 startPos = Vector3.zero;

    [SerializeField]
    private EnemyAI theEnemyAI;

    private Transform myTarget;

    public enum STATE
    {
        CREATE,
        IDLE,
        ROAMING,
        BATTLE,
        DEAD
    }

    public STATE myState = STATE.CREATE;

    public void ChangeState(STATE state)
    {
        if (myState == state) return;
        myState = state;
        switch (myState)
        {
            case STATE.CREATE:
                break;
            case STATE.IDLE:
                StartCoroutine(DelayRoaming(7.0f));
                break;
            case STATE.ROAMING:
                Vector3 pos = Vector3.zero;
                pos.x = Random.Range(-3.0f, 3.0f);
                pos.z = Random.Range(-3.0f, 3.0f);
                Debug.Log(startPos);
                pos = startPos + pos;
                MoveToPosition(pos, () => ChangeState(STATE.IDLE));
                break;
            case STATE.BATTLE:
                break;
            case STATE.DEAD:
                break;
        }
    }
    private void StateProcess()
    {
        switch (myState)
        {
            case STATE.CREATE:
                break;
            case STATE.IDLE:
                break;
            case STATE.ROAMING:
                break;
            case STATE.BATTLE:
                theEnemyAI.ChaseTarget(myTarget);
                break;
            case STATE.DEAD:
                break;
        }
    }

    IEnumerator DelayRoaming(float t)
    {
        yield return new WaitForSeconds(t);
        ChangeState(STATE.ROAMING);
    }

    // Start is called before the first frame update
    public void Start()
    {
        startPos = transform.position;
        ChangeState(STATE.IDLE);
    }

    // Update is called once per frame
    void Update()
    {
        StateProcess();
        Debug.Log(myState);
        theEnemyAI.View();
    }

    public void FindTarget(Transform target)
    {
        if (myState == STATE.DEAD) return;
        myTarget = target;
        StopAllCoroutines();
        ChangeState(STATE.BATTLE);
    }

    public void LostTarget()
    {
        if (myState == STATE.DEAD) return;
        myTarget = null;
        StopAllCoroutines();
        ChangeState(STATE.IDLE);
    }
}
