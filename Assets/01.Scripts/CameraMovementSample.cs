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
    private float speed;

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
        this.transform.position = Vector3.Lerp(this.transform.position, followTarget.transform.position + difValue, speed);
        // 러프로 카메라가 부드럽게 움직이면 좋겠어서요 ..
    }
}
