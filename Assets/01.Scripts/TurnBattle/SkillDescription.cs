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
    public void OnPointerEnter(PointerEventData eventData)//스킬에 마우스올렸을떄
    {
        SkillDescrip.SetActive(true);
        Skills.text = TurnBattle.Inst.SelectedCharacter.GetComponent<BattleCharacter>().myStat.orgData.SkillDescription[skills]+" Cost : "+$"{TurnBattle.Inst.SelectedCharacter.GetComponent<BattleCharacter>().myStat.orgData.SkillCost[skills]}"; 
        myActionCost.OnHoverSkill(TurnBattle.Inst.SelectedCharacter.GetComponent<BattleCharacter>().myStat.orgData.SkillCost[skills]
            , TurnBattle.Inst.SelectedCharacter.GetComponent<BattleCharacter>().myStat.orgData.SkillCost[TurnBattle.Inst.SelectedCharacter.GetComponent<BattleCharacter>().Skill]);
    }
    public void OnPointerExit(PointerEventData eventData)//스킬에서 마우스가나갔을떄
    {
        SkillDescrip.SetActive(false);
        myActionCost.OnHoverSkillExit(TurnBattle.Inst.SelectedCharacter.GetComponent<BattleCharacter>().myStat.orgData.SkillCost[TurnBattle.Inst.SelectedCharacter.GetComponent<BattleCharacter>().Skill]);
    }
    public void OnPointerDown(PointerEventData eventData) //스킬골랐을때
    {
        SkillDescrip.SetActive(false);
        
    }
    
}
