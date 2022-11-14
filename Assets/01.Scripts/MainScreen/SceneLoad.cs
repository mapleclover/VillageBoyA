using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEditor.Progress;


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
    private GameObject camera;
    private QuestManager theQuestManager;
    private GameObject myInven;
    private Transform mySlots;


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
            camera = GameObject.Find("Camera");
            theQuestManager = FindObjectOfType<QuestManager>();

            //플레이어 위치값
            player.transform.position = DataController.instance.gameData.currentPosition;
            player.transform.eulerAngles = DataController.instance.gameData.currentRotation;
            //카메라 위치값
            camera.transform.position = DataController.instance.gameData.currentPosition;
            //퀘스트 진행도
            theQuestManager.questId = DataController.instance.gameData.questID;
            theQuestManager.questComplete = DataController.instance.gameData.questClear;
            //if (theQuestManager.questComplete)
            //{
            theQuestManager.questActionIndex = DataController.instance.gameData.questActionIndex;
            theQuestManager.ControlObject();
            theQuestManager.ControlPopup();
            //}
            //else
            //theQuestManager.questActionIndex = DataController.instance.gameData.questActionIndex;


        }
        else if (scene.name == "10.Field_LYJ")
        {
            player = GameObject.Find("Summons(Final)");
            camera = GameObject.Find("Camera");
            myInven = GameObject.Find("Inventory 1");
            mySlots = myInven.transform.GetChild(0);
            Debug.Log(mySlots.name);
            theQuestManager = FindObjectOfType<QuestManager>();

            //플레이어 위치값
            player.transform.position = DataController.instance.gameData.currentPosition;
            player.transform.eulerAngles = DataController.instance.gameData.currentRotation;
            //카메라 위치값
            camera.transform.position = DataController.instance.gameData.currentPosition;
            //퀘스트 진행도
            theQuestManager.questId = DataController.instance.gameData.questID;
            theQuestManager.questComplete = DataController.instance.gameData.questClear;
            //if (theQuestManager.questComplete)
            //{
            theQuestManager.questActionIndex = ++DataController.instance.gameData.questActionIndex;


            Debug.Log(DataController.instance.gameData.savedInventory.Count);

            /*     foreach (KeyValuePair<string,int>items in DataController.instance.gameData.savedInventory) {
                     if (items.Key != null)
                     {
                         Debug.Log($"{items.Key}+{items.Value}");
                         items.Key.transform.SetParent(mySlots.transform.GetChild(items.Value));
                         items.Key.transform.GetComponent<RectTransform>().localScale = new Vector3(1.0f, 1.0f, 1.0f);
                         items.Key.transform.GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(70, 70);
                         items.Key.transform.localPosition = Vector2.zero;
                     }
                     else
                     {
                         Debug.Log("null");
                     }
                 }
            */
        foreach (KeyValuePair<string, int> items in DataController.instance.gameData.savedInventory)
        {
            for (int i = 0; i < InventoryController.Instance.curItem.Count; i++)
            {
                    if (InventoryController.Instance.curItem[i].GetComponent<Pickup>().item.itemName == items.Key)
                    {
                        GameObject obj = Instantiate(InventoryController.Instance.curItem[i]);
                        obj.transform.SetParent(mySlots.transform.GetChild(items.Value));
                        obj.transform.GetComponent<RectTransform>().localScale = new Vector3(1.0f, 1.0f, 1.0f);
                        obj.transform.GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(70, 70);
                        obj.transform.localPosition = Vector2.zero;
                    }
            }
        }

        
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

    public void ToBattleScene(bool backAttack, string monsterType , int monsterCount, int monsterSpeed)
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