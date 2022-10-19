using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.InputSystem.XR;

//마지막 수정 10월 17일
//전정우


public class PlayerMovement : MonoBehaviour // 캐릭터프로퍼티 만들어져있어서 가져왔습니다
{

    public GameObject Kong;
    public GameObject Ember;
    public GameObject Jin;
    

    public Animator curAnimator;
    public enum CHARACTER
    {
        Kong, Ember, Jin
    }

    public CHARACTER myCharacter = CHARACTER.Kong;


    public Transform myCamRot; // 카메라 회전값을 받기 위해
    public Slider mySlider;
    public GameObject myStaminaBar; // 스태미나 바의 사라짐과 재출현 구현

    //리지드바디를 활용하여 움직임을 구현
    public Rigidbody rigidbody;
    [SerializeField] private float speed = 3f;
    [SerializeField] private float jumpHeight = 4f; //점프 높이
    [SerializeField] private float dash = 6f; // 대시 - 일단 달리기 속도 값으로 이해 해 주세요
    [SerializeField] private float rotSpeed = 10f; // deltatime 만 곱해주면 느리기 때문에 rotSpeed로 회전 속도를 조절 해 주자

    // 토글카메라
    //public Camera _camera;
    //public bool toggleCameraRotation; // Idle 일때 둘러보기 기능
    //private float smoothness = 10.0f;

    private Vector3 dir = Vector3.zero;//이동
    private float totalDist;

    public bool run; // 달리기
    private bool ground = false; // 연속점프방지

    [SerializeField] private LayerMask layer; // 연속점프방지

    public bool canRun = true;

    void ChangeState(CHARACTER myCha)
    {
        if (myCharacter == myCha) return;
        myCharacter = myCha;
        switch (myCharacter)
        {
            case CHARACTER.Kong:
                Kong.SetActive(true);
                Ember.SetActive(false);
                Jin.SetActive(false);
                curAnimator = Kong.GetComponent<Animator>();
                break;
            case CHARACTER.Ember:
                Kong.SetActive(false);
                Ember.SetActive(true);
                Jin.SetActive(false);
                curAnimator = Ember.GetComponent<Animator>();
                break;
            case CHARACTER.Jin:
                Kong.SetActive(false);
                Ember.SetActive(false);
                Jin.SetActive(true);
                curAnimator = Jin.GetComponent<Animator>();
                break;
        }
    }
    void StateProcess()
    {
        switch (myCharacter)
        {
            case CHARACTER.Kong:
                /*if (Input.GetKeyDown(KeyCode.Tab))
                {
                    Xiao.SetActive(false);
                    Ember.SetActive(true);
                    myCharacter = CHARACTER.Ember;
                    curAnimator = Ember.GetComponent<Animator>();
                }*/
                break;
            case CHARACTER.Ember:
                /*if (Input.GetKeyDown(KeyCode.Tab))
                {
                    Ember.SetActive(false);
                    Jin.SetActive(true);
                    myCharacter = CHARACTER.Jin;
                    curAnimator = Jin.GetComponent<Animator>();
                }*/
                break;
            case CHARACTER.Jin:
                /*if (Input.GetKeyDown(KeyCode.Tab))
                {
                    Jin.SetActive(false);
                    Xiao.SetActive(true);
                    myCharacter = CHARACTER.Xiao;
                    curAnimator = Xiao.GetComponent<Animator>();
                }*/
                break;

        }

    }
    void Start()
    {
        curAnimator = GameObject.Find("Ember").GetComponent<Animator>();
        // CharacterProperty에서 myRigid 가져와 쓰는데 나중에 문제 생길지 모르니 우선 둘게요
        rigidbody = this.GetComponent<Rigidbody>(); // 리지드바디를 이 객체에 연결
        // 유니티에서 바인딩 해 줄 필요 없음
        ChangeState(CHARACTER.Ember);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            ChangeState(CHARACTER.Kong);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ChangeState(CHARACTER.Jin);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ChangeState(CHARACTER.Ember);
        }
        StateProcess();
        dir.x = Input.GetAxis("Horizontal"); // Raw를 넣을지 말지 상의가 필요할 것 같아용
        // A 와 D 키를 눌렀을 때 이동방향
        dir.z = Input.GetAxis("Vertical");
        // W 와 S 를 눌렀을 때 앞 뒤 이동방향 입력받음

        // 키보드 입력값으로 캐릭터 이동을 위함
        totalDist = dir.magnitude;
        //dir.Normalize(); // 값을 항상 1로 동일하게 처리하고 대각선으로 이동하더라도 속도가 빨리지는 현상 방지

        // 카메라 회전이 트랜스폼의 회전에 영향을 줄 수 있도록
        dir = myCamRot.rotation * dir;
        dir.y = 0.0f;
        dir.Normalize();


        HideStaminaBar(); // 스태미나 바 숨기기

        CheckGround(); // 연속점프 감지

        // 걷는 애니메이션
        if (totalDist > 0.0f)
        {
            curAnimator.SetBool("IsWalking", true);
        }
        if (totalDist <= 0.0f)
        {
            curAnimator.SetBool("IsWalking", false);
        }

        Dash(); // 달리기

        // 점프
        // 유니티 기본설정 Jump 키를 불러와서 스페이스바로 가능
        if (Input.GetButtonDown("Jump") && ground) // 연속점프 방지 = && ground 그라운드가 참일 때
        {
            Vector3 jumpPower = Vector3.up * jumpHeight;
            rigidbody.AddForce(jumpPower, ForceMode.VelocityChange);
            //점프를 했을 때 위로 뛸 수 있도록
            curAnimator.SetTrigger("Jump");

        }

        /*// 대시 구현 - 사용 안할 것 같아서 주석처리 해 놓음
        if(Input.GetButtonDown("Dash"))
        {
            Vector3 dashPower = this.transform.forward * -Mathf.Log(1/rigidbody.drag) * dash;
            // drag 공기저항값을 역수로 뒤집어서 로그로 바꾸고 - 를 넣어줘서 값을 구한 후 우리가 구한 대시양을 곱해준다 < 자연스러운 대시를 위해(무슨 소리인지 모르겠다.)
            rigidbody.AddForce(dashPower, ForceMode.VelocityChange);
        }
        // 대시 키의 기본값이 없어서 유니티 프로젝트 세팅에서 추가
        // 유니티에서 리지드바디 Drag를 10 정도 설정 해 주면 확인 할 수 있음
        // 하지만 공기저항값을 넣어주면 점프한 후 느리게 떨어지는 문제가 있으니 일단 주석처리 하고 달리기를 구현할 예정
        // 점프에서 떨어지는 이유는 리지드바디 중력값에 의한 것이고 천천히 떨어지는 이유는 드래그 공기저항값 때문이므로 두개를 조합해서 점프 문제를 해결하라*/

    }


    //캐릭터의 부드러운 회전을 위해
    private void FixedUpdate() // 물리적인 이동이나 회전을 할 때 쓰면 좋다
    {
        //회전
        if (dir != Vector3.zero) //벡터의 제로가 아니라면 키 입력이 됨
        {
            // 앞으로 나아갈 때 + 방향으로 나아가는데 반대방향으로 나가가는 키를 눌렀을 때 -방향으로 회전하면서 생기는 오류를 방지하기위해 (부호가 서로 반대일 경우를 체크해서 살짝만 미리 돌려주는 코드) 어렵네요... 
            // 지금 바라보는 방향의 부호 != 나아갈 방향 부호
            if (Mathf.Sign(transform.forward.x) != Mathf.Sign(dir.x) || Mathf.Sign(transform.forward.z) != Mathf.Sign(dir.z))
            {
                //우리는 이동할 때 x 와 z 밖에 사용을 안하므로
                transform.Rotate(0, 1, 0); // 살짝만 회전
                //정 반대방향을 눌러도 회전안하는 버그 방지
                //미리 회전을 조금 시켜서 정반대인 경우를 제거
            }
            transform.forward = Vector3.Lerp(transform.forward, dir, rotSpeed * Time.deltaTime);
            // Slerp를 쓸지 Lerp를 쓸지 상의를 해봐야 할 것 같아용 
            // 캐릭터의 앞방향은 dir 키보드를 누른 방향으로 캐릭터 회전
            //Lerp를 쓰면 원하는 방향까지 서서히 회전
        }

        // 이동을 구현
        rigidbody.MovePosition(this.transform.position + dir * speed * Time.deltaTime);


        if (run) // 달리기
        {
            rigidbody.MovePosition(this.gameObject.transform.position + dir * dash * Time.deltaTime);
        }

        
    }

    void HideStaminaBar()
    {
        // 100f 일 경우 스태미나 바 숨김
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
                curAnimator.SetBool("IsWalking", true);
            }
            else
            {
                curAnimator.SetBool("IsWalking", false);
            }
            curAnimator.SetBool("IsRunning", false);
            
            run = false;
        }
        else
        {
            // 달리기
            if (Input.GetKey(KeyCode.LeftShift) && totalDist > 0.0f && canRun)
            {
                run = true;
                curAnimator.SetBool("IsRunning", true);

            }
            else // 이동거리값이 0보다 작을 때 shift로 달리기 발동 안할 수 있도록
            {
                run = false;
                curAnimator.SetBool("IsRunning", false);
            }
        }

        if (Input.GetKeyUp(KeyCode.LeftShift) && !canRun)
        {
            canRun = true;
        }
    }


    void CheckGround() // 연속점프 방지, 점프를 땅에 있을 때만
    {
        //레이캐스트를 사용
        RaycastHit hit;

        //피봇 위치가 발끝이기 때문에 캐릭터 발이 땅에 붙어버리면 검출할 수 없기 때문에 (Vector3.up * 0.2f)로 살짝 올려서 레이를 쏨
        // Vector3.down 아래니까 아래로 쏴야 함
        // 얼마만큼의 거리에 레이저를 쏠건지 = 0.4f
        // = 레이저를 쏠건데 캐릭터의 발 끝보다 0.2 만큼 높은 위치에서 아래방향으로 쏠것이고 0.4 만큼만 레이저가 발사 될것이다
        // 이 길이 안에서 우리가 설정할 레이어가 검출이 되면 그 정보를 out hit 에 담아라

        // 이쪽 프로젝트로 옮기는 과정에서 원래 수치값(0.4f, 0.2f) 와 상이하게 해야하는 문제가 좀 있네요 
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