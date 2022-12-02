using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//�ۼ��� : ����ȣ

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
}

public class SoundTest : MonoBehaviour
{
    public static SoundTest instance;
    public AudioSource[] BGMSFXSource; //0���� BGM 1���� SFX

    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey("BGMVolume"))
        {
            PlayerPrefs.SetFloat("BGMVolume", 0.0f);
        }

        if (!PlayerPrefs.HasKey("SFXVolume"))
        {
            PlayerPrefs.SetFloat("SFXVolume", 0.0f);
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
                Debug.Log("AudioSource�� ������Դϴ�");
                return;
            }

        }
        Debug.Log(_name + "���尡 SoundTest�� ��ϵ��� �ʾҽ��ϴ�.");
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
        Debug.Log("��� ����" + _name + "���尡 �����ϴ�.");
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
        Debug.Log(_name + "���尡 SoundTest�� ��ϵ��� �ʾҽ��ϴ�.");
    }



    public void StopAllBGM()
    {
            audioSourceBgm.Stop();
    }

    public void StopBGM(string _name)
    {
        audioSourceBgm.Stop();
        return;
        Debug.Log("��� ����" + _name + "���尡 �����ϴ�!");
    }


    public void ChangeVolume(float value, int type)
    {
        if (type == 0)
        {
            BGMSFXSource[type].volume = value;
        }
        else
        {
            for(int j = 1; j < BGMSFXSource.Length - 1; j++)
            {
                BGMSFXSource[type].volume = value;
            }
        }
        Save(value, type);
    }

    public void Load()
    {
        Slider BGMSlider = GameObject.Find("BGM").GetComponent<Slider>();
        Slider SFXSlider = GameObject.Find("SFX").GetComponent<Slider>();

        BGMSlider.value = 1.0f - PlayerPrefs.GetFloat("BGMVolume");
        SFXSlider.value = 1.0f - PlayerPrefs.GetFloat("SFXVolume");
    }

    private void Save(float value, int type)
    {
        if(type == 0)
          PlayerPrefs.SetFloat("BGMVolume", 1.0f - value);
        else
          PlayerPrefs.SetFloat("SFXVolume", 1.0f - value);
    }
}