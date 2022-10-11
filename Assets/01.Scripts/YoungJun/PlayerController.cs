using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float applySpeed;
    public float lookSensitivity;

    [SerializeField]
    private Camera myCamera;
    private Rigidbody myRigid;

    // Start is called before the first frame update
    void Start()
    {
        myRigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        CharacterMovement();
    }

    void CharacterMovement()
    {
        float _moveX = Input.GetAxisRaw("Horizontal");
        float _moveZ = Input.GetAxisRaw("Vertical");

        Vector3 _moveHorizontal = transform.right * _moveX;
        Vector3 _moveVertical = transform.forward * _moveZ;
        Vector3 _velocity = (_moveHorizontal + _moveVertical).normalized;

        myRigid.MovePosition(transform.position + _velocity * Time.deltaTime * applySpeed);
    }

    void CharacterRotation()
    {
        float _rotY = Input.GetAxisRaw("Mouse X");
        Vector3 _characterRotY = new Vector3(0, _rotY, 0) * lookSensitivity;
        myRigid.MoveRotation(myRigid.rotation * Quaternion.Euler(_characterRotY));
    }
}
