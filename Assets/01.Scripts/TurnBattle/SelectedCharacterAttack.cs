//작성자 : 박진
//설명 : 캐릭터 공격선택버튼

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectedCharacterAttack : MonoBehaviour
{
    public GameObject myAttack;
    public GameObject mySelectAttack;
    public GameObject myActiveAttack;
    public Button myHeal;
    public static SelectedCharacterAttack Inst = null;
    public TMP_Text myActvieTxt = null;
    public TMP_Text[] myAttacks;
    public ActionCost myActionCost;
    public GameObject mySelectCharacter;
    private BattleCharacter x;
    private void Awake()
    {
        Inst = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (TurnBattle.Inst.HealingPotion > 0)
        {
            myHeal.interactable = true;
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
        x = TurnBattle.Inst.SelectedCharacter.GetComponent<BattleCharacter>();
        myAttack.gameObject.SetActive(false);
        mySelectAttack.gameObject.SetActive(true);
        for (int i = 0; i < myAttacks.Length; i++)
        {
            myAttacks[i].text = x.myStat.orgData.SkillName[i];
        }

        int currentCost = x.myStat.orgData
            .SkillCost[x.Skill];
        myActionCost.OnHoverSkill(currentCost, currentCost);
    }

    public void Attack(int a)
    {
        x = TurnBattle.Inst.SelectedCharacter.GetComponent<BattleCharacter>();
        mySelectCharacter.SetActive(false);
        if (x.ActiveHeal)
        {
            TurnBattle.Inst.HealingPotion += 1;
            x.ActiveHeal = false;
        }
        x.Skill = a;
        TurnBattle.Inst.OnTotalCost();
        x.longAttackCheck
            = x.myStat.longAttack[a];
        mySelectAttack.gameObject.SetActive(false);
        myActvieTxt.text = x.myStat.orgData.SkillName[a];
        myActiveAttack.SetActive(true);
        
    }

    public void Healing()
    {
        x = TurnBattle.Inst.SelectedCharacter.GetComponent<BattleCharacter>();
        mySelectCharacter.SetActive(false);
        if (!x.ActiveHeal)
        {
            TurnBattle.Inst.HealingPotion -= 1;
        }

        myAttack.gameObject.SetActive(false);
        myActvieTxt.text = "회복";
        myActiveAttack.SetActive(true);
        x.ActiveHeal = true;
    }
}