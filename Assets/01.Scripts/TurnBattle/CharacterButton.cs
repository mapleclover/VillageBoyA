//작성자 : 박진
//설명 : 캐릭터 버튼
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterButton : MonoBehaviour
{
    public Button myButton;
    public GameObject MyChosenAttack;
    public GameObject myCharacter;
    public GameObject myAttack;
    public GameObject mySelectAttack;
    public TMP_Text myActvieTxt = null;
    public GameObject mySelectCharacter;
    Vector3 pos;

    void Start()
    {
        for (int i = 0; i < 3; i++)
        {
            if (myCharacter == null) return;
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

    void Update()
    {
        if (myCharacter.GetComponent<BattleCharacter>().State == STATE.Die)
        {
            myButton.interactable = false;
            MyChosenAttack.GetComponent<CharacterImgaeAlpha>().DieCheck = true;
        }

        if (TurnBattle.Inst.myState == TurnBattle.State.Choice)            
                if (!myCharacter.GetComponent<BattleCharacter>().ActiveHeal)
                   myActvieTxt.text = myCharacter.GetComponent<BattleCharacter>().myStat.orgData.SkillName[myCharacter.GetComponent<BattleCharacter>().Skill];

                    
    }

    public void SelectedCharacter()
    {
        mySelectCharacter.SetActive(true);
        mySelectCharacter.transform.position = transform.position;
        TurnBattle.Inst.SelectedCharacter = myCharacter;
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