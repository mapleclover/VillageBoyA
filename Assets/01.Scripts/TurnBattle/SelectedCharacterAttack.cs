using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class SelectedCharacterAttack : MonoBehaviour
{
    public GameObject myAttack;
    public GameObject mySelectAttack;
    public GameObject myActiveAttack;
    public static SelectedCharacterAttack Inst = null;
    public TMPro.TMP_Text myActvieTxt = null;
    private void Awake()
    {
        Inst = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void AttackSelected()
    {
        myAttack.gameObject.SetActive(false);
        mySelectAttack.gameObject.SetActive(true);        
        
    }
    public void Attack1()
    {
        TurnBattle.Inst.SelectedCharacter.GetComponent<BattleCharacter>().Skill = 0;
        mySelectAttack.gameObject.SetActive(false);
        myActvieTxt.text = "공격1";
        myActiveAttack.SetActive(true);
    }
    public void Attack2()
    {
        TurnBattle.Inst.SelectedCharacter.GetComponent<BattleCharacter>().Skill = 1;
        mySelectAttack.gameObject.SetActive(false);
        myActvieTxt.text = "공격2";
        myActiveAttack.SetActive(true);
    }
    public void Attack3()
    {
        TurnBattle.Inst.SelectedCharacter.GetComponent<BattleCharacter>().Skill = 2;
        mySelectAttack.gameObject.SetActive(false);
        myActvieTxt.text = "공격3";
        myActiveAttack.SetActive(true);
    }

}
