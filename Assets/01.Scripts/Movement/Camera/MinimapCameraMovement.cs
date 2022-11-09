using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapCameraMovement : MonoBehaviour
{

    //이현호


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
        //프레임마다 인풋을 받음
        /*rotX += -(Input.GetAxis("Mouse Y")) * sensitivity * Time.deltaTime;
        // 마우스 상,하 이동으로 y 축의 이동을 받음 -()이유는 화면 위 최대로 마우스를 올렸을 때 내려가는것을 방지
        rotY += Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;

        rotX = Mathf.Clamp(rotX, -clampAngle, clampAngle); // 최소값은 - 40, 최대값은 40
        //회전*/
        rotY = camRot.rotation.eulerAngles.y;
        Quaternion rot = Quaternion.Euler(90, rotY, 0);
        transform.rotation = rot;
    }


}