//�ۼ��� : ������
//���� : 

using UnityEngine;

public class CameraMovement2 : MonoBehaviour
{
    [Header("ObjectToFollow")] public Transform objectToFollow;

    [Header("Controller")] public float followSpeed = 10.0f;
    public float sensitivity = 30.0f;
    public float clampAngle = 40.0f;
    public float smoothness = 10.0f;
    public Vector3 finalDir; // ��������
    public float maxDistance;

    [Header("Inventory")] public GameObject myInventory;
    public GameObject myESC;
    public GameObject mySave;
    public GameObject myLoad;
    public GameObject askingUI;
    public GameObject myStatus;
    public GameObject Shop;


    [Header("MainCamera")] public Transform realCamera;


    private Vector3 dirNormalized;
    private float rotX; //���콺 ��ǲ��
    private float rotY;

    //���ع��� ���� �����ϴ�. (�ֺ� �Ĺ����� ������ ���̾ ��� �׶���� ����)

    void Start()
    {
        //�ʱ�ȭ
        rotX = transform.localRotation.eulerAngles.x;
        rotY = transform.localRotation.eulerAngles.y;
        //���Ͱ� �ʱ�ȭ
        dirNormalized = realCamera.localPosition.normalized;
    }

    void Update()
    {
        OnCursor();
        // ī�޶� ������Ʈ�� ����
        transform.position = Vector3.MoveTowards(transform.position, objectToFollow.position, followSpeed * Time.deltaTime);
        // ���� ������ ����
        finalDir = transform.TransformPoint(dirNormalized * maxDistance); //���� * �ִ�Ÿ�
    }

    void OnCursor()
    {
        if (myInventory.activeSelf || myESC.activeSelf || mySave.activeSelf || myLoad.activeSelf || askingUI.activeSelf || myStatus.activeSelf||Shop.activeSelf)
        {
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            if (Input.GetKey(KeyCode.LeftAlt))
            {
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                CameraRotation();
            }
        }
    }

    void CameraRotation()
    {
        //�����Ӹ��� ��ǲ�� ����
        rotX += -(Input.GetAxis("Mouse Y")) * sensitivity * Time.deltaTime;
        // ���콺 ��,�� �̵����� y ���� �̵��� ���� -()������ ȭ�� �� �ִ�� ���콺�� �÷��� �� �������°��� ����
        rotY += Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;

        rotX = Mathf.Clamp(rotX, -clampAngle, clampAngle); // �ּҰ��� - 40, �ִ밪�� 40
        //ȸ��
        Quaternion rot = Quaternion.Euler(rotX, rotY, 0);
        transform.rotation = rot;
    }
}