using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

// 전정우
// 1030


public class PlayerMovement : MonoBehaviour 
{
    public GameObject Kong;
    public GameObject Jin;
    public GameObject Ember;

    //UI
    public GameObject KongUI;
    public GameObject JinUI;
    public GameObject EmberUI;
    public Animator curAnimator;
    public Animator myStaminaAnim;

    [SerializeField]
    public GameManager theManager;

    public enum CHARACTER
    {
        Kong, Ember, Jin
    }

    public CHARACTER myCharacter = CHARACTER.Kong;


    public Transform myCamRot; // 카메라 회전값 
    public Slider mySlider;
    public GameObject myStaminaBar; // 스태미나 바의 사라짐과 재출현
    
    
    new // 지우지마세용 에러 방지용 입니다.

        // 우리 스크립트는 리지드바디를 활용한 움직임
        Rigidbody rigidbody; // 지우거나 주석하지 마세요
    [SerializeField] private float speed = 3f;
    [SerializeField] private float jumpHeight = 4f; //점프
    [SerializeField] private float dash = 6f; // 달리기 속도 (대시 기능 나중에 구현할지 모르니 일단 이름은 이대로)
    [SerializeField] private float rotSpeed = 10f; //deltatime 만 곱해주면 느리기 때문에 rotSpeed로 회전 속도를 조절 해 주자

    // 토글카메라
    //public Camera _camera;
    //public bool toggleCameraRotation; // Idle 일때 둘러보기 기능
    //private float smoothness = 10.0f;

    private Vector3 dir = Vector3.zero;// 이동
    private float totalDist;

    public bool run; // 달리기
    public bool canRun = true; // 달리기와 스태미너바에 연관

    //캐릭터 중복쿨타임 방지
    private bool KongTheSame = false;
    private bool JinTheSame = false;
    private bool EmberTheSame = false;


    // 연속점프방지
    private bool ground = false;
    [SerializeField] private LayerMask layer;

    //딜레이
    private bool giveDelay = false;

    void ChangeState(CHARACTER myCha)
    {
        Vector3 summonPosition = new Vector3(0, 1.3f, 0); // 캐릭터 교체시 소환되는 높이값

        if (myCharacter == myCha) return;
        myCharacter = myCha;
        switch (myCharacter)
        {
            case CHARACTER.Kong:
                this.transform.position = this.transform.transform.position + summonPosition;
                Instantiate(Resources.Load("Prefabs/Summon"), this.transform.position, Quaternion.identity);
                Kong.SetActive(true);
                Ember.SetActive(false);
                Jin.SetActive(false);
                curAnimator = Kong.GetComponent<Animator>();
                //UI
                //KongUI.SetActive(true);
                //EmberUI.SetActive(false);
                //JinUI.SetActive(false);
                break;
            
            case CHARACTER.Jin:
                this.transform.position = this.transform.transform.position + summonPosition;
                Instantiate(Resources.Load("Prefabs/Summon"), this.transform.position, Quaternion.identity);
                Kong.SetActive(false);
                Ember.SetActive(false);
                Jin.SetActive(true);
                curAnimator = Jin.GetComponent<Animator>();
                //UI
                //KongUI.SetActive(false);
                //EmberUI.SetActive(false);
                //JinUI.SetActive(true);
                break;

            case CHARACTER.Ember:
                this.transform.position = this.transform.transform.position + summonPosition;
                Instantiate(Resources.Load("Prefabs/Summon"), this.transform.position, Quaternion.identity);
                Kong.SetActive(false);
                Ember.SetActive(true);
                Jin.SetActive(false);
                curAnimator = Ember.GetComponent<Animator>();
                //UI
                //KongUI.SetActive(false);
                //EmberUI.SetActive(true);
                //JinUI.SetActive(false);
                break;
        }
    }

    // NPC와 대화 상태에서 무브먼트 제어  
    void StateProcess()
    {
        switch (myCharacter)
        {
            case CHARACTER.Kong:
                break;
            case CHARACTER.Jin:
                break;
            case CHARACTER.Ember:
                break;
        }

    }
    void Start()
    {
        curAnimator = Kong.GetComponent<Animator>(); // 기본캐릭터는 '공'으로 시작
        ChangeState(CHARACTER.Kong);
        KongTheSame = true; // 같은 캐릭터로의 변경을 막기 위해
        rigidbody = this.GetComponent<Rigidbody>(); // 리지드바디로 움직임 구현을 위함
    }

    // Update is called once per frame
    void Update()
    {
        CheckGround(); // 연속점프 감지
        HideStaminaBar(); // 스태미나 바 숨기기

        if (ground && !theManager.isAction)
        {
            //PlayerJump();
            
            PlayerMove();
            SwitchingCharacter();
            StateProcess(); //캐릭터 교체

            /*if (!theManager.isAction)
            {
                
                Dash(); // 달리기
                run = false;
            }*/
        }

        /*if (!ground && Input.GetKey(KeyCode.Space)) // 대화안하고있을떄만 점프하게 (영준수정)
        {

            StateProcess(); //캐릭터 교체
        }*/


    }


    
    private void FixedUpdate()
    //캐릭터의 부드러운 회전을 위해
    //물리적인 이동이나 회전을 할 때 쓰면 좋다
    {
        PlayerRotation();

        if (ground && !theManager.isAction)
        {
            Dash();
        }
        else
        {
            DashCancel();
        }
    }

    void PlayerJump()
    {
        // 점프
        // 유니티 기본설정 Jump 키를 불러와서 스페이스바로 가능
        if (Input.GetButtonDown("Jump") && ground) // 연속점프 방지 = && ground 그라운드가 참일 때
        {
            Vector3 jumpPower = Vector3.up * jumpHeight;
            rigidbody.AddForce(jumpPower, ForceMode.VelocityChange);
            //점프를 했을 때 위로 뛸 수 있도록
            curAnimator.SetTrigger("Jump");

        }
    }



    void PlayerMove()
    {
        // 이동과 카메라
        dir.x = Input.GetAxis("Horizontal"); // Raw를 넣을지 말지 상의가 필요할 것 같아용
                                             // A 와 D 키를 눌렀을 때 이동방향
        dir.z = Input.GetAxis("Vertical"); // W 와 S 를 눌렀을 때 앞 뒤 이동방향 입력받음
        totalDist = dir.magnitude;

        // 카메라 회전이 트랜스폼의 회전에 영향을 줄 수 있도록
        dir = myCamRot.rotation * dir;
        dir.y = 0.0f;
        dir.Normalize();


        // 이동을 구현
        GetComponent<Rigidbody>().MovePosition(this.transform.position + dir * speed * Time.deltaTime);
        // 걷는 애니메이션
        if (totalDist > 0.0f)
        {
            curAnimator.SetBool("IsWalking", true);
        }
        if (totalDist <= 0.0f)
        {
            curAnimator.SetBool("IsWalking", false);
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

            if (!myStaminaBar.activeSelf)
            {
                myStaminaBar.SetActive(true);
                myStaminaAnim.SetTrigger("FadeIn");
            }

        }
    }

    void PlayerRotation()
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

    }

    void Dash()
    {
        if (run) // 달리기
        {
            GetComponent<Rigidbody>().MovePosition(this.gameObject.transform.position + dir * dash * Time.deltaTime);
        }

        if (Mathf.Approximately(mySlider.value, 0f))
        //스태미너 바의 밸류가 0에 근사치에 닿을 때
        {
            
            canRun = false;
            if (totalDist > 0.0f) // 캐릭터의 움직임이 없다면
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
            
            if (Input.GetKey(KeyCode.LeftShift) && totalDist > 0.0f && canRun)
            // 시프트를 눌렀고, 이동거리가 있으며 canRun 이 false가 아닐 때
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
        // 시프트 키를 떼었고, canRun 이 false일 때
        {
             
                canRun = true;
            
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
    
    private void DashCancel()
    {
        curAnimator.SetBool("IsWalking", false);
        curAnimator.SetBool("IsRunning", false);
        run = false;
    }

    void SwitchingCharacter()
    {
        // 1, 2, 3 키로 캐릭터 교체
        if (Input.GetKeyDown(KeyCode.Alpha1) && giveDelay == false && KongTheSame == false)
        {
            KongUI.GetComponent<Animator>().SetTrigger("Expansion");
            JinUI.GetComponent<Animator>().SetTrigger("Reduction");
            if (!JinTheSame)
            {
                EmberUI.GetComponent<Animator>().SetTrigger("Reduction");
            }
            ChangeState(CHARACTER.Kong);
            StartCoroutine(CoolTime(5f));
            KongTheSame = true;
            JinTheSame = false;
            EmberTheSame = false;

        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && giveDelay == false && JinTheSame == false)
        {
            JinUI.GetComponent<Animator>().SetTrigger("Expansion");
            if (!EmberTheSame)
            {
                KongUI.GetComponent<Animator>().SetTrigger("Reduction");
            }
            if (!KongTheSame)
            {
                EmberUI.GetComponent<Animator>().SetTrigger("Reduction");
            }
            ChangeState(CHARACTER.Jin);
            StartCoroutine(CoolTime(5f));
            KongTheSame = false;
            JinTheSame = true;
            EmberTheSame = false;

        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && giveDelay == false && EmberTheSame == false)
        {
            EmberUI.GetComponent<Animator>().SetTrigger("Expansion");
            JinUI.GetComponent<Animator>().SetTrigger("ReductionFromEmber");
            if (!JinTheSame)
            {
                KongUI.GetComponent<Animator>().SetTrigger("Reduction");
            }

            ChangeState(CHARACTER.Ember);
            StartCoroutine(CoolTime(5f));
            KongTheSame = false;
            JinTheSame = false;
            EmberTheSame = true;

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
        if (Physics.Raycast(this.transform.position + (Vector3.up * 0.1f), Vector3.down, out hit, 0.2f, layer))
        {
            ground = true;
            curAnimator.SetBool("InAir", false);
        }
        else
        {
            ground = false;
            curAnimator.SetBool("InAir", true);
        }


    }

    //쿨타임
    IEnumerator CoolTime(float cool)
    {
       
        float coolTime = cool;
        while (cool > 0.0f)
        {
            giveDelay = true; //트루를 주고
            cool -= Time.deltaTime;
            if(KongTheSame)
            {
                KongUI.GetComponentsInChildren<Image>()[1].fillAmount = 1f;
                JinUI.GetComponentsInChildren<Image>()[1].fillAmount = 1f - (cool / coolTime);
                EmberUI.GetComponentsInChildren<Image>()[1].fillAmount = 1f - (cool / coolTime);
            }

            if (JinTheSame)
            {
                JinUI.GetComponentsInChildren<Image>()[1].fillAmount = 1f;
                EmberUI.GetComponentsInChildren<Image>()[1].fillAmount = 1f - (cool / coolTime);
                KongUI.GetComponentsInChildren<Image>()[1].fillAmount = 1f - (cool / coolTime);
            }

            if(EmberTheSame)
            {
                EmberUI.GetComponentsInChildren<Image>()[1].fillAmount = 1f;
                KongUI.GetComponentsInChildren<Image>()[1].fillAmount = 1f - (cool / coolTime);
                JinUI.GetComponentsInChildren<Image>()[1].fillAmount = 1f - (cool / coolTime);
                
            }
            

            yield return null;
        }
        giveDelay = false; //시간이 끝나면
       
    }

}