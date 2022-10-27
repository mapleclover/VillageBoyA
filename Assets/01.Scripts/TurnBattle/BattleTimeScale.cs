using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class BattleTimeScale : MonoBehaviour
{
    private int speed = 0;
    public TextMeshProUGUI showspeed;
    public void SpeedChange()
    {
        speed = (speed+1)%4;//테스트용(테스트아닐시 3으로 변경)
        switch(speed)
        {
            case 0:
                Time.timeScale = 1.0f;
                showspeed.text = ">";
                break;
            case 1:
                Time.timeScale = 2.0f;
                showspeed.text = ">>";
                break;
            case 2:
                Time.timeScale = 4.0f;
                showspeed.text = ">>>";
                break;
            case 3://테스트용
                Time.timeScale = 30.0f;
                showspeed.text = "Text";
                break;
        }
    }
}
