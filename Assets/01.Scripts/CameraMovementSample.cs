using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// �������� 10�� 10�� 11�� 46��
// ������

public class CameraMovementSample : MonoBehaviour
{
    [SerializeField]
    private GameObject followTarget;
    [SerializeField]
    private float lerpspeed;

    //��
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

        //��
        CameraZoom();

        this.transform.position = Vector3.Lerp(this.transform.position, followTarget.transform.position + difValue, lerpspeed);
        // ������ ī�޶� �ε巴�� �����̸� ���ھ�� ..

        

    }

    //��
    void CameraZoom()
    {
        // ������ ������ -1 �ڷ� ������ 1�� ����
        float zoomDirection = Input.GetAxis("Mouse ScrollWheel");
        

        if (transform.position.y <= zoomMax && zoomDirection > 0)
        {
            //���� ������ ���� �� ī�޶��� y���� �Ѱ谪���� �۴ٸ� �״�� ����
            return;
        }

        if(transform.position.y >= zoomMIn && zoomDirection < 0)
        {
            //���� �ڷ� ���� �� y���� �Ѱ谪���� ũ�ٸ� ����
            return;
        }


        // ���� ����Ƽ���� ���Ƿ� �����սô�
        followTarger2 = transform.position + transform.forward * zoomDirection * zoomSpeed;
        
    }


}
