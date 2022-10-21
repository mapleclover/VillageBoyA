using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using Unity.VisualScripting;
using UnityEditor.UIElements;

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
        curST = Mathf.Clamp(curST, 0.1f, maxST); // clamp ���� 0.1�� ��ż� delta�� �پ���� �ʴ� ������ ����
        // ����Ʈ�� ������ ������ �������� �����ð� ��� �� �ٽ� �������� �ʴ� ������ ����

        delta -= (1.0f * Time.deltaTime);
        if (delta < 0.0f)
        {
            delta = 0.0f;
        }
        if (Player.GetComponent<PlayerMovement>().run) // PlayerMovement ��ũ��Ʈ �ȿ� bool ���� ������
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

        //���� �ε巯�� ����
        staminaBar.value = Mathf.Lerp(staminaBar.value, curST / maxST * 100f, 10f * Time.deltaTime);
        // a �� b ������ t ��ŭ�� ���� ��ȯ

    }






}
