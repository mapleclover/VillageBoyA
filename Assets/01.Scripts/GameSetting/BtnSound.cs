using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnSound : MonoBehaviour
{
    public void OnClick()
    {
        SoundTest.instance.PlaySE("SFX_Button");
    }
}
