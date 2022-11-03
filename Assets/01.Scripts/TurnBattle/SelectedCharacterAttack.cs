///����
///ĳ���� ���ݼ��ù�ư
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
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
    public Button AttackStartButton;
    public Button RunButton;
    public Button[] CharacterButton;

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
    public void BattleStart() //���ݹ�ư Ŭ���� �Լ�
    {

        if (!myAttack.activeSelf && !mySelectAttack.activeSelf)
        {
            TurnBattle.Inst.BattleStart();
            AttackStartButton.interactable = false;
            RunButton.interactable = false;
            for (int i = 0; i < CharacterButton.Length; ++i)
            {
                CharacterButton[i].interactable = false;  //��ư��Ȱ��ȭ
                CharacterButton[i].GetComponent<CharacterButton>().mySelectCharacter.SetActive(false);
            }
        }
        //Ŭ���� ����ĳ���� null������ ���� ��ư�� ��Ȱ��ȭ
        Inst.myAttack.SetActive(false);
        mySelectAttack.SetActive(false);
        myActiveAttack.SetActive(true);
    }
    public void ButtonFalse()
    {
        for (int i = 0; i < CharacterButton.Length; ++i)
        {
            CharacterButton[i].interactable = true;
        }
        AttackStartButton.interactable = true;
        RunButton.interactable = true;
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
        myActvieTxt.text = "����1";
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
        myActvieTxt.text = "����2";
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
        myActvieTxt.text = "����3";
        myActiveAttack.SetActive(true);
    }
    public void Healing()
    {
        if (!TurnBattle.Inst.SelectedCharacter.GetComponent<BattleCharacter>().ActiveHeal)
        {            
            TurnBattle.Inst.HealingPotion -= 1;
        }
        myAttack.gameObject.SetActive(false);
        myActvieTxt.text = "ȸ��";
        myActiveAttack.SetActive(true);
        TurnBattle.Inst.SelectedCharacter.GetComponent<BattleCharacter>().ActiveHeal=true;
        
    }

}
