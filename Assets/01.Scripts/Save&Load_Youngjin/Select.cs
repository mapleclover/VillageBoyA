//작성자 : 이영진
//설명 :
using System;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
//using static UnityEngine.UIElements.UxmlAttributeDescription;

[Serializable]
public class Select : MonoBehaviour
{
    public static Select instance = null;
    public TextMeshProUGUI[] slotText;
    public bool[] savefile = new bool[3];
    public GameObject[] buttonList;
    public GameObject[] myMember;
    public GameObject mySaveLoad;

    public GameObject fullLoad;
    int orgslot;
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {

        //ShowUI();
        instance = this;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Slot(Button_New.cur);
        }
    }

    public void Slot(int num)
    {
        DataController.instance.nowSlot = num;
        // DataController.instance.gameData.curSlot = num;
       // ShowUI();
        if (savefile[num])
        {
            DataController.instance.LoadGameData();
        }

        Game();
    }
    public void StartSlot()
    {
        DataController.instance.Save();
        SceneManager.LoadScene(2);
    }


    public void Game()
    {
       // ShowUI();
        if (!savefile[DataController.instance.nowSlot]) 
        {
            DataController.instance.gameData.savedTime = DateTime.Now.ToString(("yyyy-MM-dd HH:mm:ss tt"));

            savefile[DataController.instance.nowSlot] = true;  
            DataController.instance.Save();
            SceneManager.LoadScene(2);
        }
        else
        {
            SceneLoad.Instance.ChangeScene("06.Field");
        }

    }

    public void ShowUI()
    {
        Show();

        DataController.instance.nowSlot = orgslot;
    }
    public void Show()
    {
        orgslot = DataController.instance.nowSlot;
        for (int i = 0; i < 3; i++)
        {
            if (File.Exists(DataController.instance.filePath + $"{i}"))
            {
                savefile[i] = true;
                DataController.instance.nowSlot = i;
                DataController.instance.LoadGameDataTemp();
                Vector2 temp = new Vector2(-110, 0);
                slotText[i].transform.localPosition = temp;
                slotText[i].text = "Saved Date:" + DataController.instance.tempData.savedTime;
                slotText[i].text += $"\nMy Progress={DataController.instance.tempData.myProgress}%";

                Vector3 position = myMember[0].transform.localPosition;
                position.x = 140;
                position.y = buttonList[i].transform.localPosition.y;
                GameObject obj = Instantiate(myMember[0], position, Quaternion.identity);

                if (DataController.instance.tempData.Kong.isLeader)
                {
                    obj.GetComponent<RectTransform>().sizeDelta = new Vector2(70.0f, 70.0f);
                }

                obj.transform.SetParent(mySaveLoad.transform);
                obj.transform.localPosition = position;
                obj.SetActive(true);



                position = myMember[1].transform.localPosition;
                position.x = 220;
                position.y = buttonList[i].transform.localPosition.y;
                obj = Instantiate(myMember[1], position, Quaternion.identity);

                if (DataController.instance.gameData.Jin.isLeader)
                {
                    obj.GetComponent<RectTransform>().sizeDelta = new Vector2(70.0f, 70.0f);
                }

                obj.transform.SetParent(mySaveLoad.transform);
                obj.transform.localPosition = position;
                obj.SetActive(true);



                position = myMember[2].transform.localPosition;
                position.x = 300;
                position.y = buttonList[i].transform.localPosition.y;
                obj = Instantiate(myMember[2], position, Quaternion.identity);


                if (DataController.instance.gameData.Ember.isLeader)
                {
                    obj.GetComponent<RectTransform>().sizeDelta = new Vector2(70.0f, 70.0f);
                }

                obj.transform.SetParent(mySaveLoad.transform);
                obj.transform.localPosition = position;
                obj.SetActive(true);


            }

            else
            {
                slotText[i].text = "<color=grey>No Saved Data</color> ";
            }

        }
    }
    public void clickload()
    {
        mySaveLoad.SetActive(true);
        ShowUI();
    }
}
//���: C:/Users/user/AppData/LocalLow/DefaultCompany/New Unity ProjectVillageBoyA.json