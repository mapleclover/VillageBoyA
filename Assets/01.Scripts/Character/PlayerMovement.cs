using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;


//������ ���� 10�� 12��
//������

public class PlayerMovement : CharacterProperty // ĳ����������Ƽ ��������־ �����Խ��ϴ�
{
    //������ٵ� Ȱ���Ͽ� �������� ����
    // public Rigidbody rigidbody;
    [SerializeField] private float speed = 3f;
    [SerializeField] private float jumpHeight = 4f; //���� ����
    [SerializeField] private float dash = 6f; // ��� - �ϴ� �޸��� �ӵ� ������ ���� �� �ּ���
    [SerializeField] private float rotSpeed = 10f; // deltatime �� �����ָ� ������ ������ rotSpeed�� ȸ�� �ӵ��� ���� �� ����

    private Vector3 dir = Vector3.zero;

    private bool ground = false; // ������������
    [SerializeField] private LayerMask layer; // ������������

    public bool run; // �޸���
    //public float finalSpeed; // �⺻�ӵ��� �޸��� �ӵ��� ����

    // Start is called before the first frame update
    void Start()
    {
        // CharacterProperty���� myRigid ������ ���µ� ���߿� ���� ������ �𸣴� �켱 �ѰԿ�
        // rigidbody = this.GetComponent<Rigidbody>(); // ������ٵ� �� ��ü�� ����
        // ����Ƽ���� ���ε� �� �� �ʿ� ����
    }

    // Update is called once per frame
    void Update()
    {
        dir.x = Input.GetAxisRaw("Horizontal"); // Raw�� ������ ���� ���ǰ� �ʿ��� �� ���ƿ�
        // A �� D Ű�� ������ �� �̵�����
        dir.z = Input.GetAxisRaw("Vertical");
        // W �� S �� ������ �� �� �� �̵����� �Է¹���
        float totalDist = dir.magnitude;
        dir.Normalize(); // ���� �׻� 1�� �����ϰ� ó���ϰ� �밢������ �̵��ϴ��� �ӵ��� �������� ���� ����

        CheckGround(); // �������� ����

        if (totalDist > 0.0f)
        {
            myAnim.SetBool("IsWalking", true);
        }
        if (totalDist <= 0.0f)
        {
            myAnim.SetBool("IsWalking", false);

        }


        //����
        // ����Ƽ �⺻���� Jump Ű�� �ҷ��ͼ� �����̽��ٷ� ����
        if (Input.GetButtonDown("Jump") && ground) // �������� ���� = && ground �׶��尡 ���� �� 
        {
            Vector3 jumpPower = Vector3.up * jumpHeight;
            myRigid.AddForce(jumpPower, ForceMode.VelocityChange);
            //������ ���� �� ���� �� �� �ֵ���

        }

        // �޸���
        if (Input.GetKey(KeyCode.LeftShift) && totalDist > 0.0f)
        {
            run = true;
            myAnim.SetBool("IsRunning", true);

        }
        else // �̵��Ÿ����� 0���� ���� �� shift�� �޸��� �ߵ� ���� �� �ֵ���
        {
            run = false;
            myAnim.SetBool("IsRunning", false);
        }


        /*// ��� ���� - ��� ���� �� ���Ƽ� �ּ�ó�� �� ����
        if(Input.GetButtonDown("Dash"))
        {
            Vector3 dashPower = this.transform.forward * -Mathf.Log(1/rigidbody.drag) * dash;
            // drag �������װ��� ������ ����� �α׷� �ٲٰ� - �� �־��༭ ���� ���� �� �츮�� ���� ��þ��� �����ش� < �ڿ������� ��ø� ����(���� �Ҹ����� �𸣰ڴ�.)
            rigidbody.AddForce(dashPower, ForceMode.VelocityChange);
        }
        // ��� Ű�� �⺻���� ��� ����Ƽ ������Ʈ ���ÿ��� �߰�
        // ����Ƽ���� ������ٵ� Drag�� 10 ���� ���� �� �ָ� Ȯ�� �� �� ����
        // ������ �������װ��� �־��ָ� ������ �� ������ �������� ������ ������ �ϴ� �ּ�ó�� �ϰ� �޸��⸦ ������ ����
        // �������� �������� ������ ������ٵ� �߷°��� ���� ���̰� õõ�� �������� ������ �巡�� �������װ� �����̹Ƿ� �ΰ��� �����ؼ� ���� ������ �ذ��϶�*/





    }
    //ĳ������ �ε巯�� ȸ���� ����
    private void FixedUpdate() // �������� �̵��̳� ȸ���� �� �� ���� ����
    {

        //ȸ��
        if (dir != Vector3.zero) //������ ���ΰ� �ƴ϶�� Ű �Է��� ��
        {
            // ������ ���ư� �� + �������� ���ư��µ� �ݴ�������� �������� Ű�� ������ �� -�������� ȸ���ϸ鼭 ����� ������ �����ϱ����� (��ȣ�� ���� �ݴ��� ��츦 üũ�ؼ� ��¦�� �̸� �����ִ� �ڵ�) ��Ƴ׿�... 
            // ���� �ٶ󺸴� ������ ��ȣ != ���ư� ���� ��ȣ
            if (Mathf.Sign(transform.forward.x) != Mathf.Sign(dir.x) || Mathf.Sign(transform.forward.z) != Mathf.Sign(dir.z))
            {
                //�츮�� �̵��� �� x �� z �ۿ� ����� ���ϹǷ�
                transform.Rotate(0, 1, 0); // ��¦�� ȸ��
                //�� �ݴ������ ������ ȸ�����ϴ� ���� ���� 
                //�̸� ȸ���� ���� ���Ѽ� ���ݴ��� ��츦 ����
            }
            transform.forward = Vector3.Lerp(transform.forward, dir, rotSpeed * Time.deltaTime);
            // Slerp�� ���� Lerp�� ���� ���Ǹ� �غ��� �� �� ���ƿ� 
            // ĳ������ �չ����� dir Ű���带 ���� �������� ĳ���� ȸ��
            //Lerp�� ���� ���ϴ� ������� ������ ȸ��
        }

        // �̵��� ����
        myRigid.MovePosition(this.gameObject.transform.position + dir * speed * Time.deltaTime);
        //���� �� �ϳ��� ���µ� �̵��� �ؾ��� �������� �־�����

        if (run) // �޸���
        {
            myRigid.MovePosition(this.gameObject.transform.position + dir * dash * Time.deltaTime);
        }


    }

    void CheckGround() // �������� ����, ������ ���� ���� ����
    {
        //����ĳ��Ʈ�� ���
        RaycastHit hit;

        //�Ǻ� ��ġ�� �߳��̱� ������ ĳ���� ���� ���� �پ������ ������ �� ���� ������ (Vector3.up * 0.2f)�� ��¦ �÷��� ���̸� ��
        // Vector3.down �Ʒ��ϱ� �Ʒ��� ���� ��
        // �󸶸�ŭ�� �Ÿ��� �������� ����� = 0.4f
        // = �������� ��ǵ� ĳ������ �� ������ 0.2 ��ŭ ���� ��ġ���� �Ʒ��������� ����̰� 0.4 ��ŭ�� �������� �߻� �ɰ��̴�
        // �� ���� �ȿ��� �츮�� ������ ���̾ ������ �Ǹ� �� ������ out hit �� ��ƶ�

        // ���� ������Ʈ�� �ű�� �������� ���� ��ġ��(0.4f, 0.2f) �� �����ϰ� �ؾ��ϴ� ������ �� �ֳ׿� 
        if (Physics.Raycast(this.transform.position + (Vector3.up * 0.1f), Vector3.down, out hit, 0.4f, layer))
        {
            ground = true;
        }
        else
        {
            ground = false;
        }


    }
}