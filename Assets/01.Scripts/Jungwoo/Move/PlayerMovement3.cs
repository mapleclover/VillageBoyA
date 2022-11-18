//�ۼ��� : ������
//���� :
using UnityEngine;

public class PlayerMovement3 : MonoBehaviour
{
    // ����Ʈ���� �ִϸ��̼�
    // ��Ʃ�긦 �����߽��ϴ�.
    // ���� shift�� �޸���
    // �÷��̾ Character Controller
    // =>The Character Controller is mainly used for third-person or first-person player
    // control that does not make use of Rigidbody
    
    Animator animator;
    CharacterController controller;


    public float speed = 5.0f;
    public float runSpeed = 8.0f;
    public float finalSpeed; // Inputmovement���� �� �� �ְ�

    public bool run;
    public float smoothness = 10.0f;

    // ���ī�޶�
    public Camera _camera;
    public bool toggleCameraRotation; // Idle �϶� �ѷ����� ���

    //ĳ������Ʈ�ѷ� �߷°�
    //float gravity = -7f;
    Vector3 velocity;
    public CharacterController characterController;

    // ȸ�� �� �̵�
    private Vector3 dir = Vector3.zero;

    void Start()
    {
        animator = this.GetComponent<Animator>();
        controller = this.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        /*if (animator.GetFloat("Speed") > 0f) // ĳ������Ʈ�ѷ��� �׶��忡 ��� �ִ���
        {
            //https://www.youtube.com/watch?v=bTbV5Ql0Q3M&t=251s
            velocity.y = gravity * Time.deltaTime;
            characterController.Move(velocity * Time.deltaTime); //������ ������ ������ ���ν�Ƽ ��
        }*/


        // ���ó�� ��� ī�޶� �����̼��� Ȱ��ȭ
        if (Input.GetKey(KeyCode.LeftAlt))
            // ���ó�� ��� ī�޶� �����̼�
        {
            toggleCameraRotation = true; // �ѷ����� Ȱ��ȭ
        }
        else // �ƴ϶�� false
        {
            toggleCameraRotation = false; // �ѷ����� ��Ȱ��ȭ
        }


        if (Input.GetKey(KeyCode.LeftShift))
        {
            run = true;
        }
        else
        {
            run = false;
        }

        InputMovement();
    }

    void LateUpdate()
    {
        if (toggleCameraRotation != true)
        {
            Vector3 playerRotate = Vector3.Scale(_camera.transform.forward, new Vector3(1, 0, 1));
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(playerRotate),
                Time.deltaTime * smoothness);
        }
    }


    void InputMovement()
    {
        finalSpeed = (run) ? runSpeed : speed;
        // ���� �ڴٸ� = �޸��� ���ǵ�, �ƴ϶�� = ���뽺�ǵ�

        Vector3 forward = transform.TransformDirection(Vector3.forward); // local space to world space
        Vector3 right = transform.TransformDirection(Vector3.right);

        Vector3 moveDirection = forward * Input.GetAxisRaw("Vertical") + right * Input.GetAxisRaw("Horizontal");

        controller.Move(moveDirection.normalized * finalSpeed * Time.deltaTime);

        float percent = ((run) ? 1 : 0.5f) * moveDirection.magnitude; // ���� �ڴٸ� 1������ �ƴ϶�� 0.5�� ���´�

        animator.SetFloat("Speed", percent, 0.1f, Time.deltaTime); // Blend�� ����Ʈ���� �̸��� ��Ȯ�� ���ƾ� ��

        //�ִϸ������� Apply Root Motion �� üũ ��������
    }
}