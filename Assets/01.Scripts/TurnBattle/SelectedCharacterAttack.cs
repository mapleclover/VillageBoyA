///박진
///캐릭터 공격선택버튼
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.PlayerSettings;

public class SelectedCharacterAttack : MonoBehaviour
{
    public GameObject myAttack;
    public GameObject mySelectAttack;
    public GameObject myActiveAttack;
    public Button myHeal;
    public static SelectedCharacterAttack Inst = null;
    public TMPro.TMP_Text myActvieTxt = null;
   
    private void Awake()
    {
        Inst = this;        
    }

    // Update is called once per frame
    void Update()
    {
        
        if(TurnBattle.Inst.HealingPotion>0)
        {
            myHeal.interactable=true;
        }
        else
        {
            if (TurnBattle.Inst.SelectedCharacter != null)
            {
                if (TurnBattle.Inst.SelectedCharacter.GetComponent<BattleCharacter>().ActiveHeal)
                {
                    myHeal.interactable = true;
                }
                else
                {
                    myHeal.interactable = false;
                }
            }
        }
        
    }
    public void AttackSelected()
    {
        myAttack.gameObject.SetActive(false);
        mySelectAttack.gameObject.SetActive(true);        
        
    }
    public void Attack1()
    {
        if (TurnBattle.Inst.SelectedCharacter.GetComponent<BattleCharacter>().ActiveHeal)
        {
            TurnBattle.Inst.HealingPotion += 1;
            TurnBattle.Inst.SelectedCharacter.GetComponent<BattleCharacter>().ActiveHeal = false;
        }
        TurnBattle.Inst.SelectedCharacter.GetComponent<BattleCharacter>().Skill = 0;
        TurnBattle.Inst.SelectedCharacter.GetComponent<BattleCharacter>().longAttackCheck
            = TurnBattle.Inst.SelectedCharacter.GetComponent<BattleCharacter>().longAttack[0];
        mySelectAttack.gameObject.SetActive(false);
        myActvieTxt.text = "공격1";
        myActiveAttack.SetActive(true);
    }
    public void Attack2()
    {
        if (TurnBattle.Inst.SelectedCharacter.GetComponent<BattleCharacter>().ActiveHeal)
        {
            TurnBattle.Inst.HealingPotion += 1;
            TurnBattle.Inst.SelectedCharacter.GetComponent<BattleCharacter>().ActiveHeal = false;
        }
        TurnBattle.Inst.SelectedCharacter.GetComponent<BattleCharacter>().Skill = 1;
        TurnBattle.Inst.SelectedCharacter.GetComponent<BattleCharacter>().longAttackCheck
            = TurnBattle.Inst.SelectedCharacter.GetComponent<BattleCharacter>().longAttack[1];
        mySelectAttack.gameObject.SetActive(false);
        myActvieTxt.text = "공격2";
        myActiveAttack.SetActive(true);
    }
    public void Attack3()
    {
        if (TurnBattle.Inst.SelectedCharacter.GetComponent<BattleCharacter>().ActiveHeal)
        {
            TurnBattle.Inst.HealingPotion += 1;
            TurnBattle.Inst.SelectedCharacter.GetComponent<BattleCharacter>().ActiveHeal = false;
        }
        TurnBattle.Inst.SelectedCharacter.GetComponent<BattleCharacter>().Skill = 2;
        TurnBattle.Inst.SelectedCharacter.GetComponent<BattleCharacter>().longAttackCheck
            = TurnBattle.Inst.SelectedCharacter.GetComponent<BattleCharacter>().longAttack[2];
        mySelectAttack.gameObject.SetActive(false);
        myActvieTxt.text = "공격3";
        myActiveAttack.SetActive(true);
    }
    public void Healing()
    {
        if (!TurnBattle.Inst.SelectedCharacter.GetComponent<BattleCharacter>().ActiveHeal)
        {            
            TurnBattle.Inst.HealingPotion -= 1;
        }
        myAttack.gameObject.SetActive(false);
        myActvieTxt.text = "회복";
        myActiveAttack.SetActive(true);
        TurnBattle.Inst.SelectedCharacter.GetComponent<BattleCharacter>().ActiveHeal=true;
        
    }

}
