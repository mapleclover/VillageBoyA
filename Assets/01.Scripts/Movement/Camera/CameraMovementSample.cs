using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// �������� 10�� 12��
// ������

public class CameraMovementSample : MonoBehaviour
{
    [SerializeField] private GameObject followTarget;
    [SerializeField] private float lerpspeed;

    //��
    [SerializeField] private float zoomSpeed = 0f;
    [SerializeField] private float zoomMax = 0f;
    [SerializeField] private float zoomMIn = 0f;

    float totalDist = 0f;
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

        this.transform.position = Vector3.Lerp(this.transform.position, followTarget.transform.position + difValue, lerpspeed);
        // ������ ī�޶� �ε巴�� �����̸� ���ھ�� ..


    }



}
