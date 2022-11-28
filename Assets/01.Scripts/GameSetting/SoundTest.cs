using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//작성자 : 이현호시벌련

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
}

public class SoundTest : MonoBehaviour
{
    static public SoundTest instance;
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
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        else
            Destroy(gameObject);
    }

    public AudioSource[] audioSourceEffects;
    public AudioSource audioSourceBgm;

    public string[] playSoundName;

    [SerializeField]
    public Sound[] effectSounds;
    public Sound[] bgmSounds;

    public void PlaySE(string _name)
    {
        for (int i = 0; i < effectSounds.Length; i++)
        {
            if (_name == effectSounds[i].name)
            {
                for (int j = 0; j < audioSourceEffects.Length; j++)
                {
                    if (!audioSourceEffects[j].isPlaying)
                    {
                        //playSoundName[j] = effectSounds[i].name;
                        audioSourceEffects[j].clip = effectSounds[i].clip;
                        audioSourceEffects[j].Play();
                        return;
                    }
                }
                Debug.Log("AudioSource가 사용중입니다");
                return;
            }

        }
        Debug.Log(_name + "사운드가 SoundTest에 등록되지 않았습니다.");
    }



    public void StopAllSE()
    {
        for (int i = 0; i < audioSourceEffects.Length; i++)
        {
            audioSourceEffects[i].Stop();
        }
    }

    public void StopSE(string _name)
    {
        for (int i = 0; i < audioSourceEffects.Length; i++)
        {
            if (playSoundName[i] == _name)
            {
                audioSourceEffects[i].Stop();
                return;
            }
        }
        Debug.Log("재생 중인" + _name + "사운드가 없습니다.");
    }
    public void PlayBGM(string _name)
    {
        for (int i = 0; i < bgmSounds.Length; i++)
        {
            if (_name == bgmSounds[i].name)
            {
                //playSoundName[j] = effectSounds[i].name;
                audioSourceBgm.clip = bgmSounds[i].clip;
                audioSourceBgm.Play();
                return;
            }

        }
        Debug.Log(_name + "사운드가 SoundTest에 등록되지 않았습니다.");
    }



    public void StopAllBGM()
    {
            audioSourceBgm.Stop();
    }

    public void StopBGM(string _name)
    {
        audioSourceBgm.Stop();
        return;
        Debug.Log("재생 중인" + _name + "사운드가 없습니다.");
    }


    public void ChangeVolume(int i)
    {
        if (i == 0)
        {
            BGMSFXSource[i].volume = volumeSlider[i].value;
        }
        else
        {
            for(int j = 1; j < BGMSFXSource.Length - 1; j++)
            {
                BGMSFXSource[j].volume = volumeSlider[1].value;
            }
        }
        Save(i);
    }

    private void Load(int i)
    {
        if (i == 0)
        {
            volumeSlider[i].value = 1.0f - PlayerPrefs.GetFloat("BGMVolume");
        }
        else { volumeSlider[i].value = 1.0f - PlayerPrefs.GetFloat("SFXVolume"); }

        BGMSFXSource[i].volume = volumeSlider[i].value;
    }

    private void Save(int i)
    {
        if(i == 0)
          PlayerPrefs.SetFloat("BGMVolume", 1.0f - volumeSlider[i].value);
        else
          PlayerPrefs.SetFloat("SFXVolume", 1.0f - volumeSlider[i].value);
    }
}