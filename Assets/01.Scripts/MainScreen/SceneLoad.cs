//작성자 : 유은호 
//설명 : 씬이동 + 그사이에 끼워넣어줄 로드씬
//수정자 : 박영준
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
            if (null == instance)
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
    public int PlayerCount;

    
    private GameObject player;
    private GameObject camera;
    private QuestManager theQuestManager;
    private GameObject myInven;
    private Transform mySlots;
    private GameObject konghp;
    private GameObject jinhp;
    private GameObject emberhp;
    
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
            player = GameObject.FindWithTag("Player");
            camera = GameObject.FindWithTag("Camera");
            myInven = GameObject.FindWithTag("Inventory");
            konghp = GameObject.FindWithTag("KongHP");
            jinhp = GameObject.FindWithTag("JinHP");
            emberhp = GameObject.FindWithTag("EmberHP");
            mySlots = myInven.transform.GetChild(0);
             theQuestManager = FindObjectOfType<QuestManager>();

            //플레이어 위치값
            player.transform.position = DataController.instance.gameData.currentPosition;
            player.transform.eulerAngles = DataController.instance.gameData.currentRotation;
            //카메라 위치값
            camera.transform.position = DataController.instance.gameData.currentPosition;
            //퀘스트 진행도
            theQuestManager.questId = DataController.instance.gameData.questID;
            theQuestManager.questComplete = DataController.instance.gameData.questClear;
            theQuestManager.questActionIndex = DataController.instance.gameData.questActionIndex;
            theQuestManager.ControlObject();
            theQuestManager.ControlPopup();
            foreach (KeyValuePair<string, int> items in DataController.instance.gameData.savedInventory)
            {
                GameObject obj;
                for (int i = 0; i < InventoryController.Instance.curItem.Count; i++)
                {
                    if (InventoryController.Instance.curItem[i].GetComponent<Pickup>().item.itemName == items.Key)
                    {
                        obj = Instantiate(InventoryController.Instance.curItem[i]);
                        obj.transform.SetParent(mySlots.transform.GetChild(items.Value));
                        obj.transform.GetComponent<RectTransform>().localScale = new Vector3(1.0f, 1.0f, 1.0f);
                        obj.transform.GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(70, 70);
                        obj.transform.localPosition = Vector2.zero;
                        // if (obj.transform.childCount > 1)
                        //  {
                        //     Destroy(obj.transform.GetChild(1));
                        //  }
                    }
                    else continue;


                    if (InventoryController.Instance.curItem[i].layer == 7)
                    {
                        if (DataController.instance.gameData.Kong.myUsedItems.Contains(items.Key))
                        {
                            //UI에 표시
                            ShowPortrait(Instantiate(Resources.Load("Prefabs/MainCharacter")) as GameObject, i, items.Value);
                            break;
                        }
                        else if (DataController.instance.gameData.Jin.myUsedItems.Contains(items.Key))
                        {
                            //UI에 표시
                            ShowPortrait(Instantiate(Resources.Load("Prefabs/Jin")) as GameObject, i, items.Value);
                            break;
                        }
                        else if (DataController.instance.gameData.Ember.myUsedItems.Contains(items.Key))
                        {
                            //UI에 표시
                            ShowPortrait(Instantiate(Resources.Load("Prefabs/Ember")) as GameObject, i, items.Value);
                            break;
                        }
                    }
                }
            }

            for (int i = 0; i < DataController.instance.gameData.victoryComplete.Length; i++)
            {
                if (DataController.instance.gameData.victoryComplete[i])
                {
                    InventoryController.Instance.GetItem(InventoryController.Instance.curItem[8 + i]);
                    DataController.instance.gameData.myProgress += 20;
                    DataController.instance.gameData.victoryComplete[i] = false;
                }
            }
            konghp.GetComponent<Slider>().value= DataController.instance.gameData.Kong.HP /150f; 
            jinhp.GetComponent<Slider>().value = DataController.instance.gameData.Jin.HP*0.01f;
            emberhp.GetComponent<Slider>().value = DataController.instance.gameData.Ember.HP*0.008f;

        }
        else if (scene.name == "H_H")
        {
             player = GameObject.Find("Summons(Final)");
            camera = GameObject.Find("Camera");
             myInven = GameObject.Find("Inventory 1");

            mySlots = myInven.transform.GetChild(0);
            theQuestManager = FindObjectOfType<QuestManager>();

            //플레이어 위치값
            player.transform.position = DataController.instance.gameData.currentPosition;
            player.transform.eulerAngles = DataController.instance.gameData.currentRotation;
            //카메라 위치값
            camera.transform.position = DataController.instance.gameData.currentPosition;
            //퀘스트 진행도
            theQuestManager.questId = DataController.instance.gameData.questID;
            theQuestManager.questComplete = DataController.instance.gameData.questClear;
            theQuestManager.questActionIndex = DataController.instance.gameData.questActionIndex;
            theQuestManager.ControlObject();
            theQuestManager.ControlPopup();
        }
    }

    void ShowPortrait(GameObject portrait, int i, int index)
    {
        portrait.GetComponent<RectTransform>().sizeDelta = new Vector2(25, 25);
        portrait.transform.SetParent(mySlots.transform.GetChild(index).transform.GetChild(0));
        portrait.transform.localPosition =
            new Vector2(InventoryController.Instance.curItem[i].transform.localPosition.x + 20,
                InventoryController.Instance.curItem[i].transform.localPosition.y - 20);
        portrait.GetComponent<RawImage>().raycastTarget = false;
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

    public struct BattleResultData
    {
        public string Name;
        public bool victory;
    }

    public BattleResultData battleResult = new BattleResultData();

    public void ToBattleScene(string Name, bool backAttack, string monsterType, int monsterCount, int monsterSpeed, int playerCount = 3)
    {
        battleResult.Name = Name;
        battleResult.victory = false;
        BackAttack = backAttack;
        MonsterType = monsterType;
        MonsterCount = monsterCount;
        MonsterSpeed = monsterSpeed;
        PlayerCount = playerCount;
        if (!isChange)
        {
            StopAllCoroutines();
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

    IEnumerator LoadingTarget(Slider slider, int i)
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

        if (battleResult.victory)
        {
            GameObject monster = GameObject.Find(battleResult.Name);
            monster.GetComponent<Monster>().ChangeState(Monster.STATE.DEAD);
            StopAllCoroutines();
            StartCoroutine(MonsterRegeneration(monster));
        }
    }

    IEnumerator MonsterRegeneration(GameObject monster)
    {
        Debug.Log(monster);
        yield return new WaitForSeconds(20.0f);
        monster.SetActive(true);
    }
}