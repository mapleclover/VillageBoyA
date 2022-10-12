using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 수정일자 10월 10일 11시 46분
// 전정우

public class CameraMovementSample : MonoBehaviour
{
    [SerializeField]
    private GameObject followTarget;
    [SerializeField]
    private float lerpspeed;

    //줌
    [SerializeField] private float zoomSpeed = 0f;
    [SerializeField] private float zoomMax = 0f;
    [SerializeField] private float zoomMIn = 0f;

    private Vector3 followTarger2;

    private Vector3 difValue;

    // Start is called before the first frame update
    void Start()
    {
        difValue = transform.position - followTarget.transform.position;
        difValue = new Vector3(difValue.x, difValue.y, difValue.z);


    }

    // Update is called once per frame
    void Update()
    {

        //줌
        CameraZoom();

        this.transform.position = Vector3.Lerp(this.transform.position, followTarget.transform.position + difValue, lerpspeed);
        // 러프로 카메라가 부드럽게 움직이면 좋겠어서요 ..

        

    }

    //줌
    void CameraZoom()
    {
        // 앞으로 굴리면 -1 뒤로 굴리면 1이 리턴
        float zoomDirection = Input.GetAxis("Mouse ScrollWheel");
        

        if (transform.position.y <= zoomMax && zoomDirection > 0)
        {
            //휠을 앞으로 굴릴 때 카메라의 y값이 한계값보다 작다면 그대로 리턴
            return;
        }

        if(transform.position.y >= zoomMIn && zoomDirection < 0)
        {
            //휠을 뒤로 굴릴 때 y값이 한계값보다 크다면 리턴
            return;
        }


        // 값은 유니티에서 임의로 설정합시다
        followTarger2 = transform.position + transform.forward * zoomDirection * zoomSpeed;
        
    }


}
