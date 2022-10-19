using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;



// ���� ���� ���� 10�� 12��
// ������
// ȥ�� �ϴ� ������ �ȵż� �������� FollowingCamera ����


public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform myTarget;
    // Neck�� ���󰡰� �ϴ°� ���� �ڿ����������ϴ�.

    [SerializeField] private float lerpspeed;
    [SerializeField] private Vector2 ZoomRange = new Vector2(2.7f, 7.0f);
    [SerializeField] private float ZoomSpeed;

    Vector3 myDir = Vector3.zero;
    float targetDist = 0.0f;
    float dist = 0.0f;


    // Start is called before the first frame update
    void Start()
    {
        myDir = transform.position - myTarget.position;
        targetDist = dist = myDir.magnitude;
        myDir.Normalize();

    }

    // Update is called once per frame
    void Update()
    {
        targetDist += Input.GetAxis("Mouse ScrollWheel") * ZoomSpeed;
        targetDist = Mathf.Clamp(targetDist , ZoomRange.x, ZoomRange.y);

        dist = Mathf.Lerp(dist, targetDist, Time.deltaTime * 5.0f);

        // ����Ƽ���� lerpspeed ���� 1 �� �ٲٸ� �����ϰ� ���� �� �ֵ��� ���� ����
        this.transform.position = Vector3.Lerp(this.transform.position, myTarget.transform.position + myDir * dist, lerpspeed);
    }

}
