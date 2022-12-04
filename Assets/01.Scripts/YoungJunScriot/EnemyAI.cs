//�ۼ��� : �ڿ���
//���� : �÷��̾� Ȯ�� üũ �� �i�ư���. ���������� ������ ��Ʋ����ȯ

using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private float viewAngle; // �þ߰�
    [SerializeField] private float viewDistance; // �þ߰Ÿ�
    [SerializeField] private LayerMask layerMask; // Ÿ�ٸ���ũ (�÷��̾�)
    [SerializeField] private float notChaseDist; // �����Ÿ��̻󵵸�ġ�� ���̻� ���Ͱ� �i�ƿ����ʰԲ�.

    [SerializeField] private Monster theMonster;
    [SerializeField] private NavMeshAgent theNav;
    [SerializeField] private ActionController theActionController;

    MinimapIcon myIcon = null;

    private bool findTarget = false;
    Color orgColor;

    // Start is called before the first frame update
    
    void Start()
    {
        if (this.GetComponent<Pickup>().enemy.enemyType == EnemySC.EnemyType.Boss)
        {
            RedOrBlack(Color.black);
            orgColor = Color.black;
        }
        else
        {
            RedOrBlack(Color.red);
            orgColor = Color.red;
        }    
    }
    private void OnEnable()
    {
        if(myIcon!=null) 
        {
            myIcon.gameObject.SetActive(true);
            myIcon.Initialize(transform, orgColor);
        }
    }

    void RedOrBlack(Color color)
    {
        GameObject obj = Instantiate(Resources.Load("Prefabs/Icons/MinimapIcon"), SceneData.Inst.Minimap) as GameObject;
        myIcon = obj.GetComponent<MinimapIcon>();
        myIcon.Initialize(transform,color);
    }
    


    private Vector3 BoundaryAngle(float _angle)
    {
        _angle += transform.eulerAngles.y; // rot�������ǵ��� _angle�� ������������
        return new Vector3(Mathf.Sin(_angle * Mathf.Deg2Rad), 0f, Mathf.Cos(_angle * Mathf.Deg2Rad));
    }

    public void View()
    {
        Vector3 _leftBoundary = BoundaryAngle(-viewAngle * 0.5f); // �����þ߰�
        Vector3 _rightBoundary = BoundaryAngle(viewAngle * 0.5f); // �����þ߰�

        Debug.DrawRay(transform.position + transform.up, _leftBoundary, Color.red);
        Debug.DrawRay(transform.position + transform.up, _rightBoundary, Color.red);

        Collider[] _target = Physics.OverlapSphere(transform.position, viewDistance, layerMask);

        if (!findTarget) //false�϶��� ����.
        {
            for (int i = 0; i < _target.Length; i++)
            {
                if (_target.Length > 0) //�迭�� �÷��̾�������������.
                {
                    Transform Target = _target[i].transform;
                    if (Target.tag == "Player") // Ÿ�� �̸��� �÷��̾���
                    {
                        Vector3 _direction = Target.position - transform.position; // �ڽ� -> �÷��̾�� ���� ���⺤��
                        float _angle = Vector3.Angle(_direction, transform.forward); // ����� �÷��̾������ ����

                        if (_angle < viewAngle * 0.5f)
                        {
                            RaycastHit _hitinfo;
                            if (Physics.Raycast(transform.position + transform.up, _direction, out _hitinfo,
                                    viewDistance, layerMask))
                            {
                                if (_hitinfo.transform.tag == "Player")
                                {
                                    findTarget = true; //�÷��̾�ã������ true�� �Ұ�����.
                                    Debug.Log("�÷��̾ �þ߳��� �ֽ��ϴ�.");
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
        if (theActionController.isBattle) // �����Ÿ������� ������ ��Ʋ�����γѾư���.
        {
            theNav.SetDestination(transform.position);
            theNav.ResetPath();

            DataController.instance.SaveData();
            SceneLoad.Instance.ToBattleScene(transform.name, theActionController.isBackAttack,
                this.transform.GetComponent<Pickup>().enemy.enemyName, Random.Range(2, 4)
                , this.transform.GetComponent<Pickup>().enemy.Speed);
            //EnemyBackAttackInfoDisappear();
            // ================��Ʋ�����γѾ. ===================
        }
        else // �÷��̾ �i�ư��Բ�.
        {
            if ((target.position - transform.position).magnitude < notChaseDist) // �÷��̾���ǰŸ��� �����Ÿ����̶�� ��Ӧi�ƿ�.
            {
                if (theNav.destination != target.position)
                {
                    theNav.SetDestination(target.position); // �÷��̾���ġ���� ���󰡰�
                }
            }
            else // �÷��̾�Ÿ��� �����Ÿ������γ����� �ٽ� Roaming - Idle �ݺ�.
            {
                findTarget = false;
                theNav.ResetPath();
                theMonster.LostTarget();
            }
        }
    }
}