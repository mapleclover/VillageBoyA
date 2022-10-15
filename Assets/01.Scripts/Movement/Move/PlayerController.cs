using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : CharacterProperty
{
    //https://mayquartet.tistory.com/44

    public float Speed = 10.0f;

    public float rotateSpeed = 10.0f; // ȸ�� �ӵ�

    float h, v;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");

        Vector3 dir = new Vector3(h, 0, v); // new Vector3(h, 0, v)�� ���� ���̰� �Ǿ����Ƿ� dir�̶�� ������ �ְ� ���� ���ϰ� ����� �� �ְ� ��

        /*float x = Mathf.Lerp(myAnim.GetFloat("x"), h, Time.deltaTime * Speed);
        float y = Mathf.Lerp(myAnim.GetFloat("y"), v, Time.deltaTime * Speed);
        myAnim.SetFloat("x", x);
        myAnim.SetFloat("y", y);*/



        // �ٶ󺸴� �������� ȸ�� �� �ٽ� ������ �ٶ󺸴� ������ ���� ���� ����
        if (!(h == 0 && v == 0))
        {
            // �̵��� ȸ���� �Բ� ó��
            transform.position += dir * Speed * Time.deltaTime;
            // ȸ���ϴ� �κ�. Point 1.
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * rotateSpeed);
        }
    }
}