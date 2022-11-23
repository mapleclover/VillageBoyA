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

    public void OnPointerEnter(PointerEventData eventData)
    {
        SkillDescrip.SetActive(true);
        Skills.text = TurnBattle.Inst.SelectedCharacter.GetComponent<BattleCharacter>().myStat.orgData.SkillDescription[skills];
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        SkillDescrip.SetActive(false);
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        SkillDescrip.SetActive(false);
    }
}
