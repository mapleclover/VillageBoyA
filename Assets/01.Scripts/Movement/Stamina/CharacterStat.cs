using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

// 전정우
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
            //Stamina = Mathf.Clamp(value, 0.0f, maxStamina);
            //changeStamina?.Invoke(Stamina/ maxStamina);
            // 왜 맥스를 못받는지 확인좀 해봐 정우야
        }
    }
}
