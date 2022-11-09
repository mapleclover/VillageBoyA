using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapCameraMovement : MonoBehaviour
{

    //����ȣ


    [Header("ObjectToFollow")]
    public Transform objectToFollow;
    public Transform camRot;

    [Header("Controller")]
    public float followSpeed = 10.0f;
    private float rotY;

    void Start()
    {
        
    }

    void Update()
    {
        Vector3 pos = objectToFollow.position;
        pos.y = transform.position.y;
        /*transform.position = Vector3.MoveTowards(transform.position, pos, followSpeed * Time.deltaTime);*/
        transform.position = pos;
        /*Quaternion pos2 = transform.rotation;
        
        transform.rotation = pos2;
*/
        //�����Ӹ��� ��ǲ�� ����
        /*rotX += -(Input.GetAxis("Mouse Y")) * sensitivity * Time.deltaTime;
        // ���콺 ��,�� �̵����� y ���� �̵��� ���� -()������ ȭ�� �� �ִ�� ���콺�� �÷��� �� �������°��� ����
        rotY += Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;

        rotX = Mathf.Clamp(rotX, -clampAngle, clampAngle); // �ּҰ��� - 40, �ִ밪�� 40
        //ȸ��*/
        rotY = camRot.rotation.eulerAngles.y;
        Quaternion rot = Quaternion.Euler(90, rotY, 0);
        transform.rotation = rot;
    }


}