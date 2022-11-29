//작성자 : 이현호
//설명 : 

using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject PauseUI;

    private bool paused = false;
    
    void Start()
    {
        paused = false;
        PauseUI.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(ShopManager.isAction)
            PauseUI.gameObject.SetActive(!PauseUI.gameObject.activeSelf);

            if (PauseUI.gameObject.activeSelf)
            {
                paused = true;
            }
            else
            {
                paused = false;
            }
        }

        if (paused)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }
}