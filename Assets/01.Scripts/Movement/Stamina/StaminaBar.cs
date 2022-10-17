using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : CharacterProperty
{
    public Slider staminaBar;
    public Animator myAnimator;

    private float curST = 100f;
    private float maxST = 100f;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
       curST = Mathf.Clamp(curST, 0.0f, maxST);
        

       if (staminaBar.value > 0)
       {

       }


       if (myAnimator.GetBool("IsRunning") == true)
       {
            curST -= 10 * Time.deltaTime;
       }
       else
       {
            curST += 10 * Time.deltaTime;
       }
       //Debug.Log(staminaBar.value);
   
       HandleStamina();

    }
    private void HandleStamina()
    {
        //���� �ε巯�� ����
        staminaBar.value = Mathf.Lerp(staminaBar.value, curST / maxST * 100f, Time.deltaTime);
        // a �� b ������ t ��ŭ�� ���� ��ȯ
    }


}
