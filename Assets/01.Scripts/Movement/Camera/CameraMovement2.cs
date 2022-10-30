using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement2 : MonoBehaviour
{

    //전정우
    public GameObject myInventory;
    public Transform objectToFollow;
    public float followSpeed = 10.0f;
    public float sensitivity = 30.0f;
    public float clampAngle = 40.0f;

    private float rotX; //마우스 인풋값
    private float rotY;

    public Transform realCamera;
    private Vector3 dirNormalized;
    public Vector3 finalDir; // 최종방향
    public float smoothness = 10.0f;
    public float maxDistance;

    //public bool cursorOn = false;


    //방해물이 많아 껏습니다. (주변 식물들을 마땅한 레이어가 없어서 그라운드로 설정)
    /*public float minDistance; // 캐릭터가 있고 카메라기 있는 사이에 방해물이 있으면 방해물 
                          // 뒤편으로 카메라는 이동할 것이고 그 객체와의 최소거리를 만듦
    
    private float finalDistance;*/


    void Start()
    {
        //초기화
        rotX = transform.localRotation.eulerAngles.x;
        rotY = transform.localRotation.eulerAngles.y;

        //벡터값 초기화
        dirNormalized = realCamera.localPosition.normalized;
        //finalDistance = realCamera.localPosition.magnitude;

        // 커서를 화면에서 안쓰고 싶으면 활성화

    }

    void Update()
    {
        //프레임마다 인풋을 받음
        rotX += -(Input.GetAxis("Mouse Y")) * sensitivity * Time.deltaTime;
        // 마우스 상,하 이동으로 y 축의 이동을 받음 -()이유는 화면 위 최대로 마우스를 올렸을 때 내려가는것을 방지
        rotY += Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;

        rotX = Mathf.Clamp(rotX, -clampAngle, clampAngle); // 최소값은 - 40, 최대값은 40
        //회전
        Quaternion rot = Quaternion.Euler(rotX, rotY, 0);
        transform.rotation = rot;

        if (myInventory.activeSelf)
        {
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            OnCursor();
        }

    }


    // 업데이트가 끝난 다음에 실행
    void LateUpdate()
    {
        // 카메라가 오브젝트를 따라감
        transform.position = Vector3.MoveTowards(transform.position, objectToFollow.position, followSpeed * Time.deltaTime);
        // 최종 정해진 방향
        finalDir = transform.TransformPoint(dirNormalized * maxDistance); // 방향 * 최대거리


        //방해물의 오브젝트를 저장하는 변수
        //지금은 방해물이 너무 많아 껐습니다.
        /*RaycastHit hit;
        if (Physics.Linecast(transform.position, finalDir, out hit))
        {
            // 만약 뭔가 있다면
            finalDistance = Mathf.Clamp(hit.distance, minDistance, maxDistance);
        }
        else
        {
            finalDistance = maxDistance;
        }
        realCamera.localPosition = Vector3.Lerp(realCamera.localPosition, dirNormalized * finalDistance, Time.deltaTime * smoothness);*/


    }

    public void OnCursor()
    {

        if (Input.GetKey(KeyCode.LeftAlt))
        {
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

}