using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestPopupManager : MonoBehaviour
{
    [SerializeField]
    private Button openButton;
    [SerializeField]
    private Button closeButton;
    [SerializeField]
    private Image panel;

    private Animator myAnim;

    // Start is called before the first frame update
    void Start()
    {
        myAnim = panel.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnOpen()
    {
        openButton.gameObject.SetActive(false);
        closeButton.gameObject.SetActive(true);
        myAnim.SetTrigger("OpenPopup");
        //while (totalSize > 0.0f)
        //{
        //    float delta = Time.deltaTime * 2.0f;
        //    if (totalSize < delta)
        //    {
        //        delta = totalSize;
        //    }
        //    totalSize -= delta;
        //    panel.rect.bottom += delta;
        //}
        //totalSize = startSize;
    }

    public void OnClose()
    {
        openButton.gameObject.SetActive(true);
        closeButton.gameObject.SetActive(false);
        myAnim.SetTrigger("ClosePopup");
        //while (totalSize > 0.0f)
        //{
        //    float delta = Time.deltaTime * 2.0f;
        //    if(totalSize < delta)
        //    {
        //        delta = totalSize;
        //    }
        //    totalSize -= delta;
        //    panelRect.rect.bottom -= delta;
        //}
        //totalSize = startSize;
    }
}
