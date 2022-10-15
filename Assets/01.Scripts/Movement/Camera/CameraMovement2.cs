using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement2 : MonoBehaviour
{

    //전정우
    //Test1

    // 팀원 여러분 미안해요 실력이 모자라서 유튜브를 참고했습니다.


    public Transform objectToFollow;
    public float followSpeed = 10.0f;
    public float sensitivity = 100.0f;
    public float clampAngle = 70.0f;

    private float rotX;
    private float rotY;

    public Transform realCamera;
    public Vector3 dirNormalized;
    public Vector3 finalDir;
    public float minDistance; // 캐릭터가 있고 카메라기 있는 사이에 방해물이 있으면 방해물 
                              // 뒤편으로 카메라는 이동할 것이고 그 객체와의 최소거리를 만듦
    public float maxDistance;
    public float finalDistance;

    public float smoothness = 10.0f;


    void Start()
    {
        rotX = transform.localRotation.eulerAngles.x;
        rotY = transform.localRotation.eulerAngles.y;

        //벡터값 초기화
        dirNormalized = realCamera.localPosition.normalized;
        finalDistance = realCamera.localPosition.magnitude;

        //Cursor.lockState = CursorLockMode.Locked; // 커서를 화면에서 안쓰고 싶으면 활성화
        //Cursor.visible = false;

    }

    void Update()
    {
        //프레임마다 인풋을 받음
        rotX += -(Input.GetAxis("Mouse Y")) * sensitivity * Time.deltaTime;
        rotY += Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;

        rotX = Mathf.Clamp(rotX, -clampAngle, clampAngle); // 최소값은 - 70, 최대값은 70
        Quaternion rot = Quaternion.Euler(rotX, rotY, 0);
        transform.rotation = rot;


    }
    void LateUpdate()
    {
        // 업데이트가 끝난 다음에 실행
        transform.position = Vector3.MoveTowards(transform.position, objectToFollow.position, followSpeed * Time.deltaTime);
        finalDir = transform.TransformDirection(dirNormalized * maxDistance); // 방향 * 최대거리

        RaycastHit hit;
        //방해물의 오브젝트를 저장하는 변수

        if (Physics.Linecast(transform.position, finalDir, out hit))
        {
            finalDistance = Mathf.Clamp(hit.distance, minDistance, maxDistance);
        }
        else
        {
            realCamera.localPosition = Vector3.Slerp(realCamera.localPosition, dirNormalized * finalDistance, Time.deltaTime * smoothness);
        }
    }


}