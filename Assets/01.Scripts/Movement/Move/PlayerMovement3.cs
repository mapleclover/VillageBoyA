using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement3 : MonoBehaviour
{

    //전정우
    //Test1

    // 블렌더트리로 애니메이션
    // 유튜브를 참고했습니다.
    // 왼쪽 shift로 달리기
    // 플레이어에 Character Controller
    // =>The Character Controller is mainly used for third-person or first-person player
    // control that does not make use of Rigidbody


    Animator animator;
    CharacterController controller;

    public Camera _camera;
    public float speed = 5.0f;
    public float runSpeed = 8.0f;
    public float finalSpeed; // Inputmovement에서 쓸 수 있게

    public bool toggleCameraRotation; // Idle 일때 둘러보기 기능
    public bool run;

    public float smoothness = 10.0f;

    void Start()
    {
        animator = this.GetComponent<Animator>();
        controller = this.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {

        //if (Input.GetKey(KeyCode.LeftAlt)) // 배그처럼 토글 카메라 로테이션을 활성화
        if (Mathf.Approximately(animator.GetFloat("Speed"), 0f)) 
            // 배그처럼 토글 카메라 로테이션
        {
            toggleCameraRotation = true; // 둘러보기 활성화
        }
        else // 아니라면 false
        {
            toggleCameraRotation = false; // 둘러보기 비활성화
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
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(playerRotate), Time.deltaTime * smoothness);
        }

    }
    void InputMovement()
    {
        finalSpeed = (run) ? runSpeed : speed;
        // 만약 뛴다면 = 달리는 스피드, 아니라면 = 보통스피드

        Vector3 forward = transform.TransformDirection(Vector3.forward); // localspace to worldspace
        Vector3 right = transform.TransformDirection(Vector3.right);
        Vector3 moveDirection = forward * Input.GetAxisRaw("Vertical") + right * Input.GetAxisRaw("Horizontal"); 
        //Raw를 써서 키보드의 부드러움을 일단 빼줌

        controller.Move(moveDirection.normalized * finalSpeed * Time.deltaTime);

        float percent = ((run) ? 1 : 0.5f) * moveDirection.magnitude; // 만약 뛴다면 1을갖고 아니라면 0.5를 갖는다

        animator.SetFloat("Speed", percent, 0.1f, Time.deltaTime); // Speed는 블렌드트리와 이름이 정확히 같아야 함

        //애니메이터의 Apply Root Motion 을 체크 해제해줌
    }

}
