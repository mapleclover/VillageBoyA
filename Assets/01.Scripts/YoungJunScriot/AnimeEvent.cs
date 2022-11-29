using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimeEvent : MonoBehaviour
{
    private void WalkAnim()
    {
        SoundTest.instance.PlaySE("SFX_Step");
    }
    private void Attack_1()
    {
        SoundTest.instance.PlaySE("SFX_Attack1");
    }
    private void Attack_2()
    {
        SoundTest.instance.PlaySE("SFX_Attack2");
    }

}
