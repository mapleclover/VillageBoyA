using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

// ÀüÁ¤¿ì
// 2022-10-17


[Serializable]
public struct CharacterStat
{
    [SerializeField] float Stamina;

    public UnityAction<float> changeStamina;

    public float STAMINA
    {
        get => Stamina;
        set
        {
            //Stamina = Mathf.Clamp(value, 0.0f, maxST);
            //changeStamina?.Invoke(Stamina/ maxST);
            
        }
    }
}
