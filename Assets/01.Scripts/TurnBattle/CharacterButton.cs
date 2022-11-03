///박진
///캐릭터 버튼
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterButton : MonoBehaviour
{
    public Button myButton;
    public GameObject myCharacter;
    public GameObject myAttack;
    public GameObject mySelectAttack;
    public TMPro.TMP_Text myActvieTxt = null;
    public GameObject mySelectCharacter;
    Vector3 pos;
    void Start()
    {
        for (int i = 0; i < 3; i++)
        {
            if (myCharacter != null)
            {
                if (myCharacter.GetComponent<BattleCharacter>().Skill == i)
                {
                    if (i == 0)
                    {
                        myActvieTxt.text = "공격1";
                    }
                    else if (i == 1)
                    {
                        myActvieTxt.text = "공격2";
                    }
                    else if (i == 2)
                    {
                        myActvieTxt.text = "공격3";
                    }
                }
            }
        }
    }
    
    void Update()
    {
        if (TurnBattle.Inst.myState == TurnBattle.State.Choice)
        {
            for (int i = 0; i < 3; i++)
            {
                if (!myCharacter.GetComponent<BattleCharacter>().ActiveHeal)
                {
                    if (myCharacter.GetComponent<BattleCharacter>().Skill == i)
                    {
                        if (i == 0)
                        {
                            myActvieTxt.text = "공격1";
                        }
                        else if (i == 1)
                        {
                            myActvieTxt.text = "공격2";
                        }
                        else if (i == 2)
                        {
                            myActvieTxt.text = "공격3";
                        }
                    }
                }
            }
        }
    }
    public void SelectedCharacter()
    {
        mySelectCharacter.SetActive(true);
        mySelectCharacter.transform.position = transform.position;
        TurnBattle.Inst.SelectedCharacter=myCharacter;
        mySelectAttack.gameObject.SetActive(false);
        pos = transform.position;
        float y = myAttack.gameObject.transform.position.y;
        pos.y = y;
        myAttack.gameObject.SetActive(true);        
        myAttack.gameObject.transform.position = pos;
        SelectedCharacterAttack.Inst.myActvieTxt = myActvieTxt;
        SelectedCharacterAttack.Inst.myActiveAttack.SetActive(false);
    }
    
}
