using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;

//박영준 플레이어 확인 체크 및 쫒아가기. 일정범위안 도착시 배틀씬전환,
public class EnemyAI : MonoBehaviour
{
    [SerializeField] private float viewAngle; // 시야각
    [SerializeField] private float viewDistance; // 시야거리
    [SerializeField] private LayerMask layerMask; // 타겟마스크 (플레이어)
    [SerializeField] private float notChaseDist; // 일정거리이상도망치면 더이상 몬스터가 쫒아오지않게끔.

    [SerializeField]
    private Monster theMonster;
    [SerializeField]
    private NavMeshAgent theNav;
    [SerializeField]
    private ActionController theActionController;

    private bool findTarget = false;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private Vector3 BoundaryAngle(float _angle)
    {
        _angle += transform.eulerAngles.y; // rot값변경의따른 _angle값 유동성을위해
        return new Vector3(Mathf.Sin(_angle * Mathf.Deg2Rad), 0f, Mathf.Cos(_angle * Mathf.Deg2Rad));
    }
    public void View()
    {
        Vector3 _leftBoundary = BoundaryAngle(-viewAngle * 0.5f); // 좌측시야각
        Vector3 _rightBoundary = BoundaryAngle(viewAngle * 0.5f); // 우측시야각

        Debug.DrawRay(transform.position + transform.up, _leftBoundary, Color.red);
        Debug.DrawRay(transform.position + transform.up, _rightBoundary, Color.red);

        Collider[] _target = Physics.OverlapSphere(transform.position, viewDistance, layerMask);

        if (!findTarget) //false일때만 실행.
        {
            for (int i = 0; i < _target.Length; i++)
            {
                if (_target.Length > 0) //배열에 플레이어있을때만실행.
                {
                    Transform Target = _target[i].transform;
                    if (Target.tag == "Player") // 타겟 이름이 플레이어라면
                    {
                        Vector3 _direction = Target.position - transform.position; // 자신 -> 플레이어로 가는 방향벡터
                        float _angle = Vector3.Angle(_direction, transform.forward); // 정면과 플레이어사이의 각도

                        if (_angle < viewAngle * 0.5f)
                        {
                            RaycastHit _hitinfo;
                            if (Physics.Raycast(transform.position + transform.up, _direction, out _hitinfo, viewDistance, layerMask))
                            {
                                if (_hitinfo.transform.tag == "Player")
                                {
                                    findTarget = true; //플레이어찾앗으면 true로 불값변경.
                                    Debug.Log("플레이어가 시야내에 있습니다.");
                                    theMonster.FindTarget(Target);
                                }
                            }
                        }
                    }
                }
            }
        }
    }
    public void ChaseTarget(Transform target)
    {
        if (theActionController.isBattle) // 일정거리안으로 들어오면 배틀씬으로넘아가게.
        {
           theNav.SetDestination(transform.position);
           theNav.ResetPath();

            DataController.instance.SaveData();
          //  DataController.instance.SavingSlots();
            SceneLoad.Instance.ToBattleScene(theActionController.isBackAttack, this.transform.GetComponent<Pickup>().enemy.enemyName, Random.Range(2, 4)
                                                              , this.transform.GetComponent<Pickup>().enemy.Speed);
            //EnemyBackAttackInfoDisappear();
            // ================배틀씬으로넘어감. ===================
        }
        else // 플레이어를 쫒아가게끔.
        {
            if ((target.position - transform.position).magnitude < notChaseDist) // 플레이어와의거리가 일정거리안이라면 계속쫒아옴.
            {
                if (theNav.destination != target.position)
                {
                    theNav.SetDestination(target.position); // 플레이어위치까지 따라가고
                }
            }
            else // 플레이어거리가 일정거리밖으로나가면 다시 Roaming - Idle 반복.
            {
               findTarget = false;
               theNav.ResetPath();
               theMonster.LostTarget();
            }
        }
    }
}
