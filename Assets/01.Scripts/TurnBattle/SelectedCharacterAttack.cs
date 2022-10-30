using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class SelectedCharacterAttack : MonoBehaviour
{
    public GameObject myAttack;
    public GameObject mySelectAttack;
    public GameObject mySelectedAttack;
    public GameObject[] SelectedAttack;
    public static SelectedCharacterAttack Inst = null;
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
        mySelectAttack.gameObject.transform.position = myAttack.transform.position;
    }
    public void Attack1()
    {
        TurnBattle.Inst.SelectedCharacter.GetComponent<BattleCharacter>().Skill = 0;
        mySelectAttack.gameObject.SetActive(false);
        mySelectedAttack.SetActive(true);     
        mySelectedAttack.gameObject.transform.position = myAttack.transform.position;
        foreach (GameObject act in SelectedAttack)
        {
            act.SetActive(false);
        }
        SelectedAttack[0].SetActive(true);
        
    }
    public void Attack2()
    {
        TurnBattle.Inst.SelectedCharacter.GetComponent<BattleCharacter>().Skill = 1;
        mySelectAttack.gameObject.SetActive(false);
        mySelectedAttack.SetActive(true);
        mySelectedAttack.gameObject.transform.position = myAttack.transform.position;
        foreach (GameObject act in SelectedAttack)
        {
            act.SetActive(false);
        }
        SelectedAttack[1].SetActive(true);
    }
    public void Attack3()
    {
        TurnBattle.Inst.SelectedCharacter.GetComponent<BattleCharacter>().Skill = 2;
        mySelectAttack.gameObject.SetActive(false);
        mySelectedAttack.SetActive(true);
        mySelectedAttack.gameObject.transform.position = myAttack.transform.position;
        foreach (GameObject act in SelectedAttack)
        {
            act.SetActive(false);
        }
        SelectedAttack[2].SetActive(true);
    }

}
