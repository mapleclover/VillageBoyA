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
        if (null == instance)
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
    public bool BackAttack = false;
    public string MonsterType;
    public int MonsterCount;
    public int MonsterSpeed;


    private GameObject player;


    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "06.Field")
        {
            player = GameObject.Find("Summons(Final)");
            Debug.Log("캐릭터위치 : "+ DataController.instance.gameData.currentPosition);
            player.transform.position = DataController.instance.gameData.currentPosition;
            player.transform.eulerAngles = DataController.instance.gameData.currentRotation;
        }
    }

    public void ChangeScene(int i)
    {
        if (!isChange)
        {
            StartCoroutine(Loading(i));
        }
    }
    public void ChangeScene(string i)
    {
        if (!isChange)
        {
            StartCoroutine(Loading(i));
        }
    }

    public void ToBattleScene(bool backAttack, string monsterType, int monsterCount, int monsterSpeed)
    {
        BackAttack = backAttack;
        MonsterType = monsterType;
        MonsterCount = monsterCount;
        MonsterSpeed = monsterSpeed;
        if (!isChange)
        {
            StartCoroutine(Loading("05.Battle"));
        }
    }

    IEnumerator Loading(int i)
    {
        isChange = true;
        yield return SceneManager.LoadSceneAsync("00.LoadingScene");
        GameObject obj = GameObject.Find("LoadingGauge");
        Slider slider = obj.GetComponent<Slider>();
        slider.value = 0.0f;
        StartCoroutine(LoadingTarget(slider, i));
        isChange = false;
    }
    IEnumerator Loading(string mapName)
    {
        isChange = true;
        yield return SceneManager.LoadSceneAsync("00.LoadingScene");
        GameObject obj = GameObject.Find("LoadingGauge");
        Slider slider = obj.GetComponent<Slider>();
        slider.value = 0.0f;
        StartCoroutine(LoadingTarget(slider, mapName));
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
                // 씬로딩 끝
                ao.allowSceneActivation = true;
            }
            yield return null;
            Cursor.lockState = CursorLockMode.None;
        }
    }
    IEnumerator LoadingTarget(Slider slider, string mapName)
    {
        AsyncOperation ao = SceneManager.LoadSceneAsync(mapName);
        ao.allowSceneActivation = false;
        while (!ao.isDone)
        {
            slider.value = ao.progress / 0.9f;
            if (Mathf.Approximately(slider.value, 1.0f))
            {
                yield return new WaitForSeconds(1.0f);
                // 씬로딩 끝
                ao.allowSceneActivation = true;
            }
            yield return null;
            Cursor.lockState = CursorLockMode.None;
        }
    }
}