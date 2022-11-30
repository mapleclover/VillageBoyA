//작성자 : 유은호
//설명 : 배틀 속도 조정 스크립트
using TMPro;
using UnityEngine;

public class BattleTimeScale : MonoBehaviour
{
    private int speed = 0;
    public TextMeshProUGUI showspeed;

    private void Start()
    {
        speed=DataController.instance.gameData.turnBattleTimeSpeed-1;
        SpeedChange();
    }
    public void TurnBattleSpeedSave()
    {
        DataController.instance.gameData.turnBattleTimeSpeed=speed;
    }
    public void SpeedChange()
    {
        speed = (speed + 1) % 4; //�׽�Ʈ��(�׽�Ʈ�ƴҽ� 3���� ����)
        switch (speed)
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
            case 3: //�׽�Ʈ��
                Time.timeScale = 30.0f;
                showspeed.text = "Text";
                break;
        }
    }
}