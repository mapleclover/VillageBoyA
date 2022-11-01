using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 작성자 : 이현호

public class SoundManager : MonoBehaviour
{
    public Slider[] volumeSlider;
    public AudioSource[] BGMSFXSource; //0번은 BGM 1번은 SFX

    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey("BGMVolume"))
        {
            PlayerPrefs.SetFloat("BGMVolume", 0.0f);
        }
        else
        {
            Load(0);
        }

        if (!PlayerPrefs.HasKey("SFXVolume"))
        {
            PlayerPrefs.SetFloat("SFXVolume", 0.0f);
        }
        else
        {
            Load(1);
        }
    }
    public void ChangeVolume(int i)
    {
        BGMSFXSource[i].volume = volumeSlider[i].value;
        Save(i);
    }
    private void Load(int i)
    {
        switch (i)
        {
            case 0:
                volumeSlider[i].value = 1.0f - PlayerPrefs.GetFloat("BGMVolume");
                break;
            case 1:
                volumeSlider[i].value = 1.0f - PlayerPrefs.GetFloat("SFXVolume");
                break;
        }
        BGMSFXSource[i].volume = volumeSlider[i].value;
    }
    private void Save(int i)
    {
        switch (i)
        {
            case 0:
                PlayerPrefs.SetFloat("BGMVolume", 1.0f - volumeSlider[i].value);
                break;
            case 1:
                PlayerPrefs.SetFloat("SFXVolume", 1.0f - volumeSlider[i].value);
                break;
        }
    }

}
