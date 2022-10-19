using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class StaminaBar : MonoBehaviour
{
    public Slider staminaBar;
    public Animator myAnimator;
    /*
    [SerializeField] float Stamina;
    public UnityAction<float> changeStamina;*/

    private float curST = 100f;
    private float maxST = 100f;


    // Start is called before the first frame update
    void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {
        //Debug.Log(staminaBar.value);

        HandleStamina();
    }
    void HandleStamina()
    {
        curST = Mathf.Clamp(curST, 0.0f, maxST);

        if (myAnimator.GetBool("IsRunning") == true)
        {
            curST -= 20 * Time.deltaTime;
        }
        else
        {
            curST += 20 * Time.deltaTime;
        }

        //���� �ε巯�� ����
        staminaBar.value = Mathf.Lerp(staminaBar.value, curST / maxST * 100f, 10f * Time.deltaTime);
        // a �� b ������ t ��ŭ�� ���� ��ȯ

    }


}
