using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// �ڿ��� ��ȣ�ۿ� ��ũ��Ʈ

public class ActionController : MonoBehaviour
{
    [SerializeField] private float range; // raycast ��������
    [SerializeField] private float backAttackRange; // ��ġ�� ����
    [SerializeField] private float viewAngle; // �÷��̾� �þ߰� (130������)
    [SerializeField] private float viewDistance; // �÷��̾� �þ߰Ÿ�
    [SerializeField] private float _backAttackAngle; // ��ġ�� ������� (30�� + 30�� = 60�� ����.)
    

    //��� ������ Ȱ��ȭ.
    private bool pickNpcActivated = false;
    private bool pickItemActivated = false;
    private bool isBackAttack = false;

    private RaycastHit hitInfo;
    

    [SerializeField]
    private LayerMask layerMask; // �ش緹�̾�� �����ϰԲ�.
    [SerializeField]
    private LayerMask enemyMask; // �� ���̾�

    //�ʿ��� ������Ʈ
    [SerializeField]
    private TMPro.TextMeshProUGUI CheckText;
    [SerializeField]
    private Image npcTextBackground;
    [SerializeField]
    private Image itemTextBackground;
    [SerializeField]
    private Image enemyextBackground;
    [SerializeField]
    private Camera theCamera;





    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckObject();
        TryPickupAction();
    }

    private Vector3 BoudaryAngle(float _angle)
    {
        _angle += transform.eulerAngles.y; // �÷��̾��� �����̼ǰ������� angle�� ��ȭ�ֱ�����.
        return new Vector3(Mathf.Sin(_angle * Mathf.Deg2Rad), 0f, Mathf.Cos(_angle * Mathf.Deg2Rad)); 
        //deg2rad -> ���� ���Ȱ����� ��ȯ.
    }

    // ������� tag���� tag Ȯ��
    private void CheckObject()
    {
        Vector3 _leftBoundary = BoudaryAngle(-viewAngle * 0.5f); // �þ߰� ������輱 ������Ȯ�ο� ����.
        Vector3 _rightBoundary = BoudaryAngle(viewAngle * 0.5f); // �þ߰� ������輱 ������ Ȯ�ο� ����.

        Debug.DrawRay(transform.position + transform.up, _leftBoundary, Color.red);
        Debug.DrawRay(transform.position + transform.up, _rightBoundary, Color.red);

        Collider[] _target = Physics.OverlapSphere(transform.position, viewDistance, layerMask); //�ֺ��ݰ� �ݶ��̴�����

        if (_target.Length > 0)
        {
            for (int i = 0; i < _target.Length; i++)
            {
                Transform Target = _target[i].transform;
                if (Target.tag == "Npc" || Target.tag == "Item") // �ش��±׸� if������.
                {
                    Vector3 _direction = (Target.position - transform.position).normalized; // ����->������ΰ��� ���⺤��
                    float _angle = Vector3.Angle(_direction, transform.forward); // ���� ~ ��� ������ ���� 
                    if (_angle < viewAngle * 0.5f)
                    {
                        // �÷��̾���ġ���� ovelapSphere�� ������ ��󿡰Է� ����������.
                        if (Physics.Raycast(transform.position, _direction, out hitInfo, range, layerMask))
                        {
                            if (hitInfo.transform.tag == "Npc")
                            {
                                NpcInfoAppear();
                            }
                            else if (hitInfo.transform.tag == "Item")
                            {
                                ItemInfoAppear();
                            }
                        }
                        else //�Ÿ��ȸ���������.
                        {
                            NpcInfoDisappear();
                            ItemInfoDisappear();
                        }
                    }
                    else //���� �ȸ����� ����.
                    {
                        NpcInfoDisappear();
                        ItemInfoDisappear();
                    }
                }
                else if(Target.tag == "Enemy") // ���߽߰�
                {
                    Vector3 _direction = (Target.position - transform.position).normalized;

                    //float _frontEnemy = Vector3.Dot(transform.forward, _direction); // ������ ���������� ����� ���� ����
                    _backAttackAngle = Vector3.Dot(transform.forward, Target.forward); // ����Ȯ��.
                    //Debug.Log("�ޱ�" + _backAttackAngle);
                    //Debug.Log("���տ��ֳ���?" + _frontEnemy);
                    float _angle = Vector3.Angle(_direction, transform.forward);
                    if (_angle < viewAngle * 0.5f)
                    { 
                        if (Physics.Raycast(transform.position, _direction, out hitInfo, range, enemyMask))
                        {
                            if (hitInfo.transform.tag == "Enemy")//�ѹ���üũ
                            {

                                if (_backAttackAngle > 0.866f) // 30�� + 30�� = 60��
                                {
                                    isBackAttack = true;
                                    EnemyBackAttackInfoAppear();
                                }
                                else if (_backAttackAngle <= 0.866f)// _zvalue���� 0�̻��϶� (��ġ��ƴҶ�)
                                {
                                    isBackAttack = false;
                                    EnemyBackAttackInfoDisappear();
                                }
                            }
                        }
                        else // �Ÿ��ȸ�����
                        {
                            isBackAttack = false;
                            EnemyBackAttackInfoDisappear();
                        }
                    }
                    else // �����ȸ�����
                    {
                        isBackAttack = false;
                        EnemyBackAttackInfoDisappear();
                    }
                }
            }
        }
        else // _target �迭�ȿ� �ƹ��͵������� ����.
        {
            NpcInfoDisappear();
            ItemInfoDisappear();
            isBackAttack = false;
            EnemyBackAttackInfoDisappear();
        }
    }
    // ������üũ �� pickup �Լ�Ȱ��ȭ
    private void TryPickupAction()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            CheckObject();
            CanPickUp();
        }
    }
    // ������ȹ�氡��������ȯ.
    private void CanPickUp()
    {
        if(pickItemActivated) // ������ Ȼ��Ȱ���ÿ��� ���
        {
            if(hitInfo.transform != null) // �ѹ��� üũ �� ������ȹ��
            {
                Destroy(hitInfo.transform.gameObject);
                ItemInfoDisappear();
                // �κ��丮â���� �����۵� ///////////////////**************
            }
        }
        else if (pickNpcActivated)
        {
            if(hitInfo.transform != null) // �ѹ� �� Ȯ�� �� // NPC�� ��ȭ.
            {
                //��ȭ. ////////////////////////*************
            }
        }
        else if (isBackAttack)
        {
            if(hitInfo.transform != null)
            {
                //���- ��Ʋ�����γѾ.///////////////////////////////**************
                Destroy(hitInfo.transform.gameObject);
                isBackAttack = false;
                EnemyBackAttackInfoDisappear();
            }
        }
    }

    // npc ����â ����
    private void NpcInfoAppear()
    {
        if (!pickNpcActivated) // false�϶�������
        {
            pickNpcActivated = true;
            npcTextBackground.gameObject.SetActive(true);
            CheckText.gameObject.SetActive(true); // �ؽ�Ʈâ Ȱ��ȭ
            CheckText.alignment = TMPro.TextAlignmentOptions.Right;
            CheckText.text = "<color=blue>" + hitInfo.transform.GetComponent<Pickup>().npc.npcName + "</color>" + "�� ��ȭ�Ͻðڽ��ϱ�?" + "<color=yellow>" + " (Y) " + "</color>";
        }
    }
    // item ����â ����
    private void ItemInfoAppear()
    {
        if (!pickItemActivated)
        {
            pickItemActivated = true;
            itemTextBackground.gameObject.SetActive(true);
            CheckText.gameObject.SetActive(true);
            CheckText.alignment = TMPro.TextAlignmentOptions.Center;
            CheckText.text = "<color=red>" + hitInfo.transform.GetComponent<Pickup>().item.itemName + "</color>" + "ȹ��" + "<color=yellow>" + " (E) " + "</color>";
        }
    }

    // npc ����â Ŭ����
    private void NpcInfoDisappear()
    {
        if (pickNpcActivated) // true�϶��� �ߵ�,
        {
            pickNpcActivated = false;
            npcTextBackground.gameObject.SetActive(false);
            CheckText.gameObject.SetActive(false);
        }
    }
    // ������ ����â Ŭ����
    private void ItemInfoDisappear()
    {
        if (pickItemActivated) //trye �϶����ߵ�.
        {
            pickItemActivated = false;
            itemTextBackground.gameObject.SetActive(false);
            CheckText.gameObject.SetActive(false);
        }
    }
    private void EnemyBackAttackInfoAppear()
    {
        if(isBackAttack) // true �϶��� ����.
        {
            enemyextBackground.gameObject.SetActive(true);
            CheckText.gameObject.SetActive(true);
            CheckText.alignment = TMPro.TextAlignmentOptions.Center;
            CheckText.text = "<color=red>" + hitInfo.transform.GetComponent<Pickup>().enemy.enemyName + "</color>" + " ��� �Ͻðڽ��ϱ�?" + "<color=yellow>" + " (E) " + "</color>";
        }
    }
    private void EnemyBackAttackInfoDisappear()
    {
        if(!isBackAttack) // false �϶��� ����
        {
            enemyextBackground.gameObject.SetActive(false);
            CheckText.gameObject.SetActive(false);
        }
    }
}
