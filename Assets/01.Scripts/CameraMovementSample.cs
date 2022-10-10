using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 수정일자 10월 10일 10시 42분
// 전정우

public class CameraMovementSample : MonoBehaviour
{
    public GameObject player;
    private Vector3 pos = new Vector3(0, 4, -8);
    void Start()
    {
    }
    void Update()
    {
        // 메인캠이 0, 15, -10 였으니
        // 플레이어 좌표에서 0, 15, -10 를 더해주면
        this.gameObject.transform.position = player.transform.position + pos;
        //카메라의 트랜스폼 = 플레이어 트랜스폼 + 더해줄포지션
    }
}
