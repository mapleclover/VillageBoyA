using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement2 : MonoBehaviour
{

    //������
    //Test1

    // ���� ������ �̾��ؿ� �Ƿ��� ���ڶ� ��Ʃ�긦 �����߽��ϴ�.


    public Transform objectToFollow;
    public float followSpeed = 10.0f;
    public float sensitivity = 100.0f;
    public float clampAngle = 70.0f;

    private float rotX;
    private float rotY;

    public Transform realCamera;
    public Vector3 dirNormalized;
    public Vector3 finalDir;
    public float minDistance; // ĳ���Ͱ� �ְ� ī�޶�� �ִ� ���̿� ���ع��� ������ ���ع� 
                              // �������� ī�޶�� �̵��� ���̰� �� ��ü���� �ּҰŸ��� ����
    public float maxDistance;
    public float finalDistance;

    public float smoothness = 10.0f;


    void Start()
    {
        rotX = transform.localRotation.eulerAngles.x;
        rotY = transform.localRotation.eulerAngles.y;

        //���Ͱ� �ʱ�ȭ
        dirNormalized = realCamera.localPosition.normalized;
        finalDistance = realCamera.localPosition.magnitude;

        //Cursor.lockState = CursorLockMode.Locked; // Ŀ���� ȭ�鿡�� �Ⱦ��� ������ Ȱ��ȭ
        //Cursor.visible = false;

    }

    void Update()
    {
        //�����Ӹ��� ��ǲ�� ����
        rotX += -(Input.GetAxis("Mouse Y")) * sensitivity * Time.deltaTime;
        rotY += Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;

        rotX = Mathf.Clamp(rotX, -clampAngle, clampAngle); // �ּҰ��� - 70, �ִ밪�� 70
        Quaternion rot = Quaternion.Euler(rotX, rotY, 0);
        transform.rotation = rot;


    }
    void LateUpdate()
    {
        // ������Ʈ�� ���� ������ ����
        transform.position = Vector3.MoveTowards(transform.position, objectToFollow.position, followSpeed * Time.deltaTime);
        finalDir = transform.TransformDirection(dirNormalized * maxDistance); // ���� * �ִ�Ÿ�

        RaycastHit hit;
        //���ع��� ������Ʈ�� �����ϴ� ����

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