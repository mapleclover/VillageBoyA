using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimeEvent : MonoBehaviour
{
    private void WalkAnim()
    {
        SoundTest.instance.PlaySE("SFX_Step");                 
    }
    private void KongAttack_1()
    {
        SoundTest.instance.PlaySE("SFX_KongAttack1");
    }
    private void KongAttack_2()
    {
        SoundTest.instance.PlaySE("SFX_KongAttack2");
    }

    private void EmberAttack_2()
    {
        SoundTest.instance.PlaySE("SFX_EmberAttack2");
    }
}
