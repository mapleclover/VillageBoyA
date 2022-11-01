using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 박영준 상호작용 스크립트

public class ActionController : MonoBehaviour
{
    [SerializeField] private float range; // raycast 범위지정
    [SerializeField] private float backAttackRange; // 뒤치기 범위
    [SerializeField] private float viewAngle; // 플레이어 시야각 (130도예정)
    [SerializeField] private float viewDistance; // 플레이어 시야거리
    [SerializeField] private float _backAttackAngle; // 뒤치기 기습범위 (30도 + 30도 = 60도 예정.)
    

    //대상 감지시 활성화.
    private bool pickNpcActivated = false;
    private bool pickItemActivated = false;
    private bool isBackAttack = false;

    public bool isBattle;

    private RaycastHit hitInfo;
    private GameObject scanObject;
    

    [SerializeField]
    private LayerMask layerMask; // 해당레이어에만 반응하게끔.
    [SerializeField]
    private LayerMask enemyMask; // 적 레이어

    //필요한 컴포넌트
    [SerializeField]
    private TMPro.TextMeshProUGUI CheckText;
    [SerializeField]
    private Image npcTextBackground;
    [SerializeField]
    private Image itemTextBackground;
    [SerializeField]
    private Image enemyextBackground;
    [SerializeField]
    private GameManager theManager;
    [SerializeField]
    private Goal theGoal;
    





    // Start is called before the first frame update
    void Start()
    {
        isBattle = false;   
    }

    // Update is called once per frame
    void Update()
    {
        CheckObject();
        if (hitInfo.collider != null)
        {
            scanObject = hitInfo.collider.gameObject; // 레이저로 맞춘놈의 gameobject 저장.
        }
        else
        {
            scanObject = null;
        }
        TryPickupAction();
    }

    private Vector3 BoudaryAngle(float _angle)
    {
        _angle += transform.eulerAngles.y; // 플레이어의 로테이션값에따라 angle값 변화주기위함.
        return new Vector3(Mathf.Sin(_angle * Mathf.Deg2Rad), 0f, Mathf.Cos(_angle * Mathf.Deg2Rad)); 
        //deg2rad -> 값을 라디안값으로 변환.
    }

    // 어떤종류의 tag인지 tag 확인
    private void CheckObject()
    {
        Vector3 _leftBoundary = BoudaryAngle(-viewAngle * 0.5f); // 시야각 좌측경계선 눈으로확인용 만듬.
        Vector3 _rightBoundary = BoudaryAngle(viewAngle * 0.5f); // 시야각 우측경계선 눈으로 확인용 만듬.

        Debug.DrawRay(transform.position + transform.up, _leftBoundary, Color.red);
        Debug.DrawRay(transform.position + transform.up, _rightBoundary, Color.red);

        Collider[] _target = Physics.OverlapSphere(transform.position, viewDistance, layerMask); //주변반경 콜라이더수집

        if (_target.Length > 0)
        {
            for (int i = 0; i < _target.Length; i++)
            {
                Debug.Log(_target[i].transform.name);
                Transform Target = _target[i].transform;

                if (Target.tag == "Npc" || Target.tag == "Item") // 해당태그만 if문실행.
                {
                    Vector3 _direction = (Target.position - transform.position).normalized; // 본인->대상으로가는 방향벡터
                    float _angle = Vector3.Angle(_direction, transform.forward); // 정면 ~ 대상 사이의 각도 
                    if (_angle < viewAngle * 0.5f)
                    {
                        // 플레이어위치에서 ovelapSphere에 감지된 대상에게로 레이저를쏨.
                        if (Physics.Raycast(transform.position, _direction, out hitInfo, range, layerMask))
                        {
                            if (hitInfo.transform.tag == "Npc" && !theManager.isAction && !theGoal.isTutorial)
                            {
                                NpcInfoAppear();
                            }
                            else if (hitInfo.transform.tag == "Item" && !theManager.isAction && !theGoal.isTutorial)
                            {
                                ItemInfoAppear();
                            }
                            else
                            {
                                NpcInfoDisappear();
                                ItemInfoDisappear();
                            }
                        }
                        else //거리안맞을떄지움.
                        {
                            NpcInfoDisappear();
                            ItemInfoDisappear();
                        }
                    }
                    else //각도 안맞을대 지움.
                    {
                        NpcInfoDisappear();
                        ItemInfoDisappear();
                    }
                }
                else if(Target.CompareTag("Enemy")) // 적발견시
                {
                    Vector3 _direction = (Target.position - transform.position).normalized;

                    //float _frontEnemy = Vector3.Dot(transform.forward, _direction); // 앞인지 뒤인지구별 양수면 적이 내앞
                    _backAttackAngle = Vector3.Dot(transform.forward, Target.forward); // 각도확인.
                    //Debug.Log("앵글" + _backAttackAngle);
                    //Debug.Log("내앞에있나요?" + _frontEnemy);
                    float _angle = Vector3.Angle(_direction, transform.forward);
                    if (_angle < viewAngle * 0.5f)
                    { 
                        if (Physics.Raycast(transform.position, _direction, out hitInfo, range, enemyMask))
                        {
                            if (hitInfo.transform.tag == "Enemy")//한번더체크
                            {

                                if (_backAttackAngle > 0.866f) // 30도 + 30도 = 60도
                                {
                                    isBackAttack = true;
                                    //EnemyBackAttackInfoAppear();
                                }
                                else if (_backAttackAngle <= 0.866f)// _zvalue값이 0이상일때 (뒤치기아닐때)
                                {
                                    isBackAttack = false;
                                    //EnemyBackAttackInfoDisappear();
                                }
                            }
                        }
                        else // 거리안맞을대
                        {
                            isBackAttack = false;
                            //EnemyBackAttackInfoDisappear();
                        }
                    }
                    else // 각도안맞을때
                    {
                        isBackAttack = false;
                        //EnemyBackAttackInfoDisappear();
                    }
                }
            }
        }
        else // _target 배열안에 아무것도없을때 지움.
        {
            NpcInfoDisappear();
            ItemInfoDisappear();
            isBackAttack = false;
            //EnemyBackAttackInfoDisappear();
        }
    }
    // 아이템체크 후 pickup 함수활성화
    private void TryPickupAction()
    {
        if (scanObject != null)
        {
            if (scanObject.transform.tag == "Npc" || scanObject.transform.tag == "Item")
            {
                if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Space))
                {
                    if (scanObject != null)
                    {
                        CheckObject();
                        CanPickUp();
                    }
                }
            }
        }
        if (isBattle)
        {
            CheckObject();
            CanPickUp();
        }
    }
    // 아이템획득가능으로전환.
    private void CanPickUp()
    {
        if(pickItemActivated) // 아이템 횔득활성시에만 기능
        {
            if(hitInfo.transform != null) // 한번더 체크 및 아이템획득
            {
                Debug.Log(scanObject);
                ItemInfoDisappear();
                theManager.Action(scanObject);
                // 인벤토리창으로 아이템들어감 ///////////////////**************
            }
        }
        else if (pickNpcActivated)
        {
            if(hitInfo.transform != null) // 한번 더 확인 및 // NPC와 대화.
            {
                Debug.Log(scanObject);
                theManager.Action(scanObject);
            }
        }
        else if (isBackAttack)
        {
            if(hitInfo.transform != null && isBattle)
            {
                //기습- 배틀씬으로넘어감.///////////////////////////////**************
                Destroy(hitInfo.transform.gameObject);
                isBackAttack = false;
                isBattle = false;
                //EnemyBackAttackInfoDisappear();
                //기습 할때 배틀신 넘어감 //********************************************************************************
                SceneLoad.Instance.ChangeScene(4);
            }
        }
        else if (theManager.isAction)
        {
            Debug.Log(scanObject);
            theManager.Action(scanObject);
        }
    }

    // npc 정보창 오픈
    private void NpcInfoAppear()
    {
        if (!pickNpcActivated && !theManager.isAction && !theGoal.isTutorial) // false일때말실행
        {
            pickNpcActivated = true;
            npcTextBackground.gameObject.SetActive(true);
            CheckText.gameObject.SetActive(true); // 텍스트창 활성화
            CheckText.alignment = TMPro.TextAlignmentOptions.Right;
            CheckText.text = "<color=blue>" + hitInfo.transform.GetComponent<Pickup>().npc.npcName + "</color>" + "와 대화하시겠습니까?" + "<color=yellow>" + " (E) " + "</color>";
        }
    }
    // item 정보창 오픈
    private void ItemInfoAppear()
    {
        if (!pickItemActivated && !theManager.isAction && !theGoal.isTutorial)
        {
            pickItemActivated = true;
            itemTextBackground.gameObject.SetActive(true);
            CheckText.gameObject.SetActive(true);
            CheckText.alignment = TMPro.TextAlignmentOptions.Center;
            CheckText.text = "<color=red>" + hitInfo.transform.GetComponent<Pickup>().item.itemName + "</color>" + "획득" + "<color=yellow>" + " (E) " + "</color>";
        }
    }

    // npc 정보창 클로즈
    private void NpcInfoDisappear()
    {
        if (pickNpcActivated) // true일때만 발동,
        {
            pickNpcActivated = false;
            npcTextBackground.gameObject.SetActive(false);
            CheckText.gameObject.SetActive(false);
        }
    }
    // 아이템 정보창 클로즈
    private void ItemInfoDisappear()
    {
        if (pickItemActivated) //trye 일때만발동.
        {
            pickItemActivated = false;
            itemTextBackground.gameObject.SetActive(false);
            CheckText.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "Enemy")
        {
            isBattle = true;
        }
    }
    //private void EnemyBackAttackInfoAppear()
    //{
    //    if(isBackAttack) // true 일때만 실행.
    //    {
    //        enemyextBackground.gameObject.SetActive(true);
    //        CheckText.gameObject.SetActive(true);
    //        CheckText.alignment = TMPro.TextAlignmentOptions.Center;
    //        CheckText.text = "<color=red>" + hitInfo.transform.GetComponent<Pickup>().enemy.enemyName + "</color>" + " 기습 하시겠습니까?" + "<color=yellow>" + " (E) " + "</color>";
    //    }
    //}
    //private void EnemyBackAttackInfoDisappear()
    //{
    //    if(!isBackAttack) // false 일때만 실행
    //    {
    //        enemyextBackground.gameObject.SetActive(false);
    //        CheckText.gameObject.SetActive(false);
    //    }
    //}
}
