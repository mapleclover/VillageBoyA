using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterButton : MonoBehaviour
{
    public Button myButton;
    public GameObject myCharacter;
    public GameObject myAttack;
    public GameObject mySelectAttack;
    Vector3 pos;
    void Start()
    {
        pos = transform.position;
        float y = (myButton.gameObject.GetComponent<RectTransform>().rect.height) * 0.7f;
        pos.y += y;
    }
    
    void Update()
    {
        
    }
    public void SelectedCharacter()
    {
        SelectedCharacterAttack.Inst.mySelectedAttack.SetActive(false);
        TurnBattle.Inst.SelectedCharacter=myCharacter;
        mySelectAttack.gameObject.SetActive(false);
        myAttack.gameObject.SetActive(true);        
        myAttack.gameObject.transform.position = pos;

    }
    
}
