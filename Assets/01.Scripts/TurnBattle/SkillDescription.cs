using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class SkillDescription : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler,IPointerDownHandler
{
    public GameObject SkillDescrip;
    public TMP_Text Skills;
    public int skills;
    public ActionCost myActionCost;
    public void OnPointerEnter(PointerEventData eventData)//��ų�� ���콺�÷�����
    {
        SkillDescrip.SetActive(true);
        Skills.text = TurnBattle.Inst.SelectedCharacter.GetComponent<BattleCharacter>().myStat.orgData.SkillDescription[skills]+" Cost : "+$"{TurnBattle.Inst.SelectedCharacter.GetComponent<BattleCharacter>().myStat.orgData.SkillCost[skills]}"; 
        myActionCost.OnHoverSkill(TurnBattle.Inst.SelectedCharacter.GetComponent<BattleCharacter>().myStat.orgData.SkillCost[skills]
            , TurnBattle.Inst.SelectedCharacter.GetComponent<BattleCharacter>().myStat.orgData.SkillCost[TurnBattle.Inst.SelectedCharacter.GetComponent<BattleCharacter>().Skill]);
    }
    public void OnPointerExit(PointerEventData eventData)//��ų���� ���콺����������
    {
        SkillDescrip.SetActive(false);
        myActionCost.OnHoverSkillExit(TurnBattle.Inst.SelectedCharacter.GetComponent<BattleCharacter>().myStat.orgData.SkillCost[TurnBattle.Inst.SelectedCharacter.GetComponent<BattleCharacter>().Skill]);
    }
    public void OnPointerDown(PointerEventData eventData) //��ų�������
    {
        SkillDescrip.SetActive(false);
        
    }
    
}
