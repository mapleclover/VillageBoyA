using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoad : MonoBehaviour
{
    private static SceneLoad instance = null;

    private void Awake()
    {
        if(null == instance)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    
    public static SceneLoad Instance
    {
        get
        {
            if(null == instance)
            {
                return null;
            }
            return instance;
        }
    }

    bool isChange = false;

    public void ChangeScene(int i)
    {
        if (!isChange)
        {
            StartCoroutine(Loading(i));
        }
    }

    IEnumerator Loading(int i)
    {
        isChange = true;
        yield return SceneManager.LoadSceneAsync(0);
        GameObject obj = GameObject.Find("LoadingGage");
        Slider slider = obj.GetComponent<Slider>();
        slider.value = 0.0f;
        StartCoroutine(LoadingTarget(slider, i));
        isChange = false;
    }

    IEnumerator LoadingTarget(Slider slider,int i)
    {
        AsyncOperation ao = SceneManager.LoadSceneAsync(i);
        ao.allowSceneActivation = false;
        while (!ao.isDone)
        {
            slider.value = ao.progress / 0.9f;
            if (Mathf.Approximately(slider.value, 1.0f))
            {
                yield return new WaitForSeconds(1.0f);
                // ¾À·Îµù ³¡
                ao.allowSceneActivation = true;
            }
            yield return null;
        }
    }
}
