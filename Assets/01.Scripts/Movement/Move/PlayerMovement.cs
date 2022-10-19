using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.InputSystem.XR;

//������ ���� 10�� 17��
//������


public class PlayerMovement : CharacterProperty // ĳ����������Ƽ ��������־ �����Խ��ϴ�
{

    public GameObject Ember;
    public GameObject Jin;
    public GameObject Xiao;


    public enum CHARACTER
    {
        Ember, Jin, Xiao
    }

    public CHARACTER myCharacter = CHARACTER.Ember;


    public Transform myCamRot; // ī�޶� ȸ������ �ޱ� ����
    public Slider mySlider;
    public GameObject myStaminaBar; // ���¹̳� ���� ������� ������ ����

    //������ٵ� Ȱ���Ͽ� �������� ����
    public Rigidbody rigidbody;
    [SerializeField] private float speed = 3f;
    [SerializeField] private float jumpHeight = 4f; //���� ����
    [SerializeField] private float dash = 6f; // ��� - �ϴ� �޸��� �ӵ� ������ ���� �� �ּ���
    [SerializeField] private float rotSpeed = 10f; // deltatime �� �����ָ� ������ ������ rotSpeed�� ȸ�� �ӵ��� ���� �� ����

    // ���ī�޶�
    //public Camera _camera;
    //public bool toggleCameraRotation; // Idle �϶� �ѷ����� ���
    //private float smoothness = 10.0f;

    private Vector3 dir = Vector3.zero;//�̵�
    private float totalDist;

    public bool run; // �޸���
    private bool ground = false; // ������������

    [SerializeField] private LayerMask layer; // ������������

    public bool canRun = true;

    void ChangeState()
    {
        switch (myCharacter)
        {
            case CHARACTER.Ember:
                if (Input.GetKeyDown(KeyCode.Tab))
                {
                    Ember.SetActive(false);
                    Jin.SetActive(true);
                    myCharacter = CHARACTER.Jin;
                }
                break;
            case CHARACTER.Jin:
                break;
            case CHARACTER.Xiao:
                break;
        }
    }
    void StateProcess()
    {
        switch (myCharacter)
        {
            case CHARACTER.Ember:
                if (Input.GetKeyDown(KeyCode.Tab))
                {
                    Ember.SetActive(false);
                    Jin.SetActive(true);
                    myCharacter = CHARACTER.Jin;
                }
                break;
            case CHARACTER.Jin:
                if (Input.GetKeyDown(KeyCode.Tab))
                {
                    Jin.SetActive(false);
                    Xiao.SetActive(true);
                    myCharacter = CHARACTER.Xiao;
                }
                break;
            case CHARACTER.Xiao:
                if (Input.GetKeyDown(KeyCode.Tab))
                {
                    Xiao.SetActive(false);
                    Ember.SetActive(true);
                    myCharacter = CHARACTER.Ember;
                }
                break;
        }

    }
    void Start()
    {
        // CharacterProperty���� myRigid ������ ���µ� ���߿� ���� ������ �𸣴� �켱 �ѰԿ�
        rigidbody = this.GetComponent<Rigidbody>(); // ������ٵ� �� ��ü�� ����
        // ����Ƽ���� ���ε� �� �� �ʿ� ����
        ChangeState();
    }

    // Update is called once per frame
    void Update()
    {

        StateProcess();
        dir.x = Input.GetAxis("Horizontal"); // Raw�� ������ ���� ���ǰ� �ʿ��� �� ���ƿ�
        // A �� D Ű�� ������ �� �̵�����
        dir.z = Input.GetAxis("Vertical");
        // W �� S �� ������ �� �� �� �̵����� �Է¹���

        // Ű���� �Է°����� ĳ���� �̵��� ����
        totalDist = dir.magnitude;
        //dir.Normalize(); // ���� �׻� 1�� �����ϰ� ó���ϰ� �밢������ �̵��ϴ��� �ӵ��� �������� ���� ����

        // ī�޶� ȸ���� Ʈ�������� ȸ���� ������ �� �� �ֵ���
        dir = myCamRot.rotation * dir;
        dir.y = 0.0f;
        dir.Normalize();


        HideStaminaBar(); // ���¹̳� �� �����

        CheckGround(); // �������� ����

        // �ȴ� �ִϸ��̼�
        if (totalDist > 0.0f)
        {
            myAnim.SetBool("IsWalking", true);
        }
        if (totalDist <= 0.0f)
        {
            myAnim.SetBool("IsWalking", false);
        }

        Dash(); // �޸���

        // ����
        // ����Ƽ �⺻���� Jump Ű�� �ҷ��ͼ� �����̽��ٷ� ����
        if (Input.GetButtonDown("Jump") && ground) // �������� ���� = && ground �׶��尡 ���� ��
        {
            Vector3 jumpPower = Vector3.up * jumpHeight;
            myRigid.AddForce(jumpPower, ForceMode.VelocityChange);
            //������ ���� �� ���� �� �� �ֵ���
            myAnim.SetTrigger("Jump");

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
        rigidbody.MovePosition(this.transform.position + dir * speed * Time.deltaTime);


        if (run) // �޸���
        {
            rigidbody.MovePosition(this.gameObject.transform.position + dir * dash * Time.deltaTime);
        }

        
    }

    void HideStaminaBar()
    {
        // 100f �� ��� ���¹̳� �� ����
        if (Mathf.Approximately(mySlider.value, 100f))
        {
            myStaminaBar.SetActive(false);
        }
        else
        {
            myStaminaBar.SetActive(true);
        }
    }



    void Dash()
    {
        if (Mathf.Approximately(mySlider.value, 0.0f))
        {
            canRun = false;
            if (totalDist > 0.0f)
            {
                myAnim.SetBool("IsWalking", true);
            }
            else
            {
                myAnim.SetBool("IsWalking", false);
            }
            myAnim.SetBool("IsRunning", false);
            
            run = false;
        }
        else
        {
            // �޸���
            if (Input.GetKey(KeyCode.LeftShift) && totalDist > 0.0f && canRun)
            {
                run = true;
                myAnim.SetBool("IsRunning", true);

            }
            else // �̵��Ÿ����� 0���� ���� �� shift�� �޸��� �ߵ� ���� �� �ֵ���
            {
                run = false;
                myAnim.SetBool("IsRunning", false);
            }
        }

        if (Input.GetKeyUp(KeyCode.LeftShift) && !canRun)
        {
            canRun = true;
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