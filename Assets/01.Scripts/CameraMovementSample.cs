using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// �������� 10�� 10�� 10�� 42��
// ������

public class CameraMovementSample : MonoBehaviour
{
    public GameObject player;
    private Vector3 pos = new Vector3(0, 4, -8);
    void Start()
    {
    }
    void Update()
    {
        // ����ķ�� 0, 15, -10 ������
        // �÷��̾� ��ǥ���� 0, 15, -10 �� �����ָ�
        this.gameObject.transform.position = player.transform.position + pos;
        //ī�޶��� Ʈ������ = �÷��̾� Ʈ������ + ������������
    }
}
