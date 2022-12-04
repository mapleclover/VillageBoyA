//작성자 : 이현호
//설명 : 
using UnityEngine;

public class Title : MonoBehaviour
{
    public GameObject myCanvas;
    public GameObject myMainScreen;
    
    void Update()
    {
        if (Input.anyKeyDown)
        {
            myCanvas.SetActive(false);
            myMainScreen.SetActive(true);
        }
    }
    private void Start()
    {
        SoundTest.instance.PlayBGM("BGM_Title");
    }
}