using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement2 : MonoBehaviour
{

    //������
    public GameObject myInventory;
    public Transform objectToFollow;
    public float followSpeed = 10.0f;
    public float sensitivity = 30.0f;
    public float clampAngle = 40.0f;

    private float rotX; //���콺 ��ǲ��
    private float rotY;

    public Transform realCamera;
    private Vector3 dirNormalized;
    public Vector3 finalDir; // ��������
    public float smoothness = 10.0f;
    public float maxDistance;

    public bool cursorOn = false;


    //���ع��� ���� �����ϴ�. (�ֺ� �Ĺ����� ������ ���̾ ��� �׶���� ����)
    /*public float minDistance; // ĳ���Ͱ� �ְ� ī�޶�� �ִ� ���̿� ���ع��� ������ ���ع� 
                          // �������� ī�޶�� �̵��� ���̰� �� ��ü���� �ּҰŸ��� ����
    
    private float finalDistance;*/


    void Start()
    {
        //�ʱ�ȭ
        rotX = transform.localRotation.eulerAngles.x;
        rotY = transform.localRotation.eulerAngles.y;

        //���Ͱ� �ʱ�ȭ
        dirNormalized = realCamera.localPosition.normalized;
        //finalDistance = realCamera.localPosition.magnitude;

        // Ŀ���� ȭ�鿡�� �Ⱦ��� ������ Ȱ��ȭ

    }

    void Update()
    {
        //�����Ӹ��� ��ǲ�� ����
        rotX += -(Input.GetAxis("Mouse Y")) * sensitivity * Time.deltaTime;
        // ���콺 ��,�� �̵����� y ���� �̵��� ���� -()������ ȭ�� �� �ִ�� ���콺�� �÷��� �� �������°��� ����
        rotY += Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;

        rotX = Mathf.Clamp(rotX, -clampAngle, clampAngle); // �ּҰ��� - 40, �ִ밪�� 40
        //ȸ��
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


    // ������Ʈ�� ���� ������ ����
    void LateUpdate()
    {
        // ī�޶� ������Ʈ�� ����
        transform.position = Vector3.MoveTowards(transform.position, objectToFollow.position, followSpeed * Time.deltaTime);
        // ���� ������ ����
        finalDir = transform.TransformPoint(dirNormalized * maxDistance); // ���� * �ִ�Ÿ�


        //���ع��� ������Ʈ�� �����ϴ� ����
        //������ ���ع��� �ʹ� ���� �����ϴ�.
        /*RaycastHit hit;
        if (Physics.Linecast(transform.position, finalDir, out hit))
        {
            // ���� ���� �ִٸ�
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