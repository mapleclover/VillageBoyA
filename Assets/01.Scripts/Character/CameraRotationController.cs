using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class CameraRotationController: MonoBehaviour
{

    public Transform target;
    //public Vector2 LookupRange = new Vector2(-60.0f, 80.0f);

    //Vector3 curRot = Vector3.zero;


    // Start is called before the first frame update
    void Start()
    {
        /*curRot.x = transform.localRotation.eulerAngles.x;
        curRot.y = transform.localRotation.eulerAngles.y;*/

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(1))
        {

            /*curRot.x -= Input.GetAxisRaw("Mouse Y") * 10f;
            curRot.x = Mathf.Clamp(curRot.x, LookupRange.x, LookupRange.y);

            curRot.y += Input.GetAxisRaw("Mouse X") * 10f;


            transform.localRotation = Quaternion.Euler(curRot.x, 0, 0);
            transform.localRotation = Quaternion.Euler(0, curRot.y, 0);
            //transform.parent.rotation = Quaternion.Euler(0, curRot.y, 0);*/


          /*  if (Input.GetAxis("Mouse X") < 0)
            {
                transform.RotateAround(target.position, Vector3.up, 150 * Time.deltaTime);

            }

            if (Input.GetAxis("Mouse X") > 0)
            {
                transform.RotateAround(target.position, Vector3.down, 150 * Time.deltaTime);

            }*/


        }

        if (Input.GetKey(KeyCode.E))
        {
            transform.RotateAround(target.position, Vector3.down, 150 * Time.deltaTime);
            // Ÿ�� �����ǿ��� ���� 
        }
        if (Input.GetKey(KeyCode.Q))
        {
            transform.RotateAround(target.position, Vector3.up, 150 * Time.deltaTime);
        }

        







    }
}
