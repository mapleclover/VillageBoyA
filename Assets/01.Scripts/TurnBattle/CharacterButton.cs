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
    private BattleCharacter myBattle;
    void Start()
    {
        myBattle = myCharacter.GetComponent<BattleCharacter>();
    }

    void Update()
    {
        if (myBattle.State == STATE.Die)
        {
            myButton.interactable = false;
            MyChosenAttack.GetComponent<CharacterImgaeAlpha>().DieCheck = true;
        }
        if (TurnBattle.Inst.myState == TurnBattle.State.Choice)            
                if (!myBattle.ActiveHeal)
                   myActvieTxt.text = myBattle.myStat.orgData.SkillName[myCharacter.GetComponent<BattleCharacter>().Skill];                    
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