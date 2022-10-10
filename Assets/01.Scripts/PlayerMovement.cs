using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;


//������ ���� 10�� 10�� ���� 10��
//������

public class PlayerMovement : CharacterProperty // ĳ����������Ƽ ��������־ �����Խ��ϴ�
{
    //������ٵ� Ȱ���Ͽ� �������� ����
    public Rigidbody rigidbody;
    public float speed = 5f;
    public float jumpHeight = 4f; //���� ����
    public float dash = 10f; // ��� - �ϴ� �޸��� �ӵ� ������ ���� �� �ּ���
    public float rotSpeed = 5f; // deltatime �� �����ָ� ������ ������ rotSpeed�� ȸ�� �ӵ��� ���� �� ����

    private Vector3 dir = Vector3.zero;

    private bool ground = false; // ������������
    public LayerMask layer; // ������������

    public bool run; // �޸���
    public float finalSpeed; // �⺻�ӵ��� �޸��� �ӵ��� ����

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = this.GetComponent<Rigidbody>(); // ������ٵ� �� ��ü�� ����
        // ����Ƽ���� ���ε� �� �� �ʿ� ����
    }

    // Update is called once per frame
    void Update()
    {
        dir.x = Input.GetAxis("Horizontal"); // Raw�� ������ ���� ���ǰ� �ʿ��� �� ���ƿ�
        // A �� D Ű�� ������ �� �̵�����
        dir.z = Input.GetAxis("Vertical");
        // W �� S �� ������ �� �� �� �̵����� �Է¹���
        float totalDist = dir.magnitude;
        dir.Normalize(); // ���� �׻� 1�� �����ϰ� ó���ϰ� �밢������ �̵��ϴ��� �ӵ��� �������� ���� ����

        CheckGround(); // �������� ����

        if (totalDist > 0.0f)
        {
            myAnim.SetBool("IsMoving", true);
        }
        if (totalDist <= 0.0f)
        {
            myAnim.SetBool("IsMoving", false);

        }


        //����
        // ����Ƽ �⺻���� Jump Ű�� �ҷ��ͼ� �����̽��ٷ� ����
        if (Input.GetButtonDown("Jump") && ground) // �������� ���� = && ground �׶��尡 ���� �� 
        {
            Vector3 jumpPower = Vector3.up * jumpHeight;
            rigidbody.AddForce(jumpPower, ForceMode.VelocityChange);
            //������ ���� �� ���� �� �� �ֵ���

        }

        // �޸���
        if (Input.GetKey(KeyCode.LeftShift))
        {
            run = true;
            myAnim.SetBool("IsRunning", true);

        }
        else
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
        rigidbody.MovePosition(this.gameObject.transform.position + dir * speed * Time.deltaTime);
        //���� �� �ϳ��� ���µ� �̵��� �ؾ��� �������� �־�����

        if (run) // �޸���
        {
            rigidbody.MovePosition(this.gameObject.transform.position + dir * dash * Time.deltaTime);
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
        if (Physics.Raycast(transform.position + (Vector3.up * 1.2f), Vector3.down, out hit, 0.8f, layer))
        {
            ground = true;
        }
        else
        {
            ground = false;
        }


    }
}