//작성자 : 박진
//설명 :
using TMPro;
using UnityEngine;

public class CharacterTextAlpha : MonoBehaviour
{
    Color color;

    void Start()
    {
        color = this.GetComponent<TMP_Text>().color;
    }

    // Update is called once per frame
    void Update()
    {
        if (TurnBattle.Inst.myState != TurnBattle.State.Choice)
        {
            color.a = 0.5f;
            this.gameObject.GetComponent<TMP_Text>().color = color;
        }
        else
        {
            color.a = 1.0f;
            this.gameObject.GetComponent<TMP_Text>().color = color;
        }
    }
}