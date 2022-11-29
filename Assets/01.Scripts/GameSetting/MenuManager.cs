//작성자 : 이현호
//설명 : 

using UnityEngine;

public class MenuManager : MonoBehaviour
{
    // public Action
    public GameObject optionsMenu;
    public GameObject settingMenu;
    public GameObject Waring;   


    // Update is called once per frame
    void Update()
    {
        if (!optionsMenu.gameObject.activeSelf && Input.GetKeyDown(KeyCode.Escape))
        {
            optionsMenu.gameObject.SetActive(true);
        }
        else if (Waring.gameObject.activeSelf && Input.GetKeyDown(KeyCode.Escape))
        {
            Waring.gameObject.SetActive(false);
        }
        else if (settingMenu.gameObject.activeSelf && Input.GetKeyDown(KeyCode.Escape))
        {
            settingMenu.gameObject.SetActive(false);
        }       
    }
}