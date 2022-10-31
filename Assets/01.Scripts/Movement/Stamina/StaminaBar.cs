using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{
  

    public Slider staminaBar;
    public GameObject Player;
    float delta = 0.0f;
    private float curST = 100f;
    private float maxST = 100f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        HandleStamina();
    }
    void HandleStamina()
    {
        curST = Mathf.Clamp(curST, 0.1f, maxST); // clamp 값을 0.1로 잠궈서 delta가 줄어들지 않는 현상을 방지
        // 시프트를 여러번 누르면 게이지가 일정시간 경과 후 다시 차오르지 않는 현상을 방지

        delta -= (1.0f * Time.deltaTime);
        if (delta < 0.0f)
        {
            delta = 0.0f;
        }
        if (Player.GetComponent<PlayerMovement>().run) // PlayerMovement 스크립트 안에 bool 값을 가져옴
        {
            curST -= 20 * Time.deltaTime;
        }
        else if (!Player.GetComponent<PlayerMovement>().run)
        {
            if (delta == 0.0f)
            {
                curST += 20 * Time.deltaTime;
            }
        }
        if (Mathf.Approximately(staminaBar.value, 0.0f))
        {
            delta = 2.0f;
        }

        //바의 부드러운 감소
        staminaBar.value = Mathf.Lerp(staminaBar.value, curST / maxST * 100f, 10f * Time.deltaTime);
        // a 와 b 사이의 t 만큼의 값을 반환

    }






}
