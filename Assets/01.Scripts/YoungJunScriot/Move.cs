using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�ڿ��� ĳ���� ������ ���� �� �����

public class Move : MonoBehaviour
{
    [SerializeField]
    private float walkSpeed; // �ȱ�ӵ�

    private float applySpeed; // �ߺ��������� ����� �ӵ�

    [SerializeField]
    private float lookSensitivityX;// x�� �ΰ���
    [SerializeField]
    private float lookSensitivityY;// y�� �ΰ���

    //ī�޶� �̵��Ѱ�ġ
    [SerializeField]
    private float cameraRotationLimit;
    private float currentCameraRotation = 0;

    //�ʿ��� ������Ʈ
    [SerializeField]
    private Camera theCamera;
    private Rigidbody myRigid;

    // Start is called before the first frame update
    void Start()
    {
        myRigid = GetComponent < Rigidbody >();

        applySpeed = walkSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        CharacterMove();
        if (Input.GetMouseButton(1))
        {
            CharacterRotation();
            CameraRotation();
        }
        
    }

    private void CharacterMove()
    {
        float _moveDirectX = Input.GetAxisRaw("Horizontal");
        float _moveDirectZ = Input.GetAxisRaw("Vertical");

        Vector3 _moveHorizontal = transform.right * _moveDirectX;
        Vector3 _moveVertical = transform.forward * _moveDirectZ;

        Vector3 _velocity = (_moveHorizontal + _moveVertical).normalized * applySpeed;

        myRigid.MovePosition(transform.position + _velocity * Time.deltaTime);
    }

    private void CharacterRotation()
    {
        float _rotY = Input.GetAxisRaw("Mouse X");
        Vector3 _CharacterRotY = new Vector3(0.0f, _rotY, 0.0f) * lookSensitivityX;
        myRigid.MoveRotation(myRigid.rotation * Quaternion.Euler(_CharacterRotY));
    }

    private void CameraRotation()
    {
        float _roxX = Input.GetAxisRaw("Mouse Y");
        float _cameraRotationX = _roxX * lookSensitivityY;
        currentCameraRotation -= _cameraRotationX;
        currentCameraRotation = Mathf.Clamp(currentCameraRotation, -cameraRotationLimit, cameraRotationLimit);

        theCamera.transform.localEulerAngles = new Vector3(currentCameraRotation, 0.0f, 0.0f);
    }
}
