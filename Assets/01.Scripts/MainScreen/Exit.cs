//작성자 : 이현호
//설명 : 
using UnityEditor;
using UnityEngine;

public class Exit : MonoBehaviour
{
    public void ClickEnd()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        UnityEngine.Application.Quit();
#endif
    }
}