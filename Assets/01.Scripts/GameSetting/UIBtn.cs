//작성자 : 이현호
//설명 : 
using UnityEngine;

public class UIBtn : MonoBehaviour
{
    public GameObject questBtn;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            questBtn.gameObject.SetActive(!questBtn.gameObject.activeSelf);
        }
    }
}
