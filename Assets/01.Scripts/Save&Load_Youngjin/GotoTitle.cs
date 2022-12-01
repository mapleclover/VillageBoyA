using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GotoTitle : MonoBehaviour
{
    public GameObject credit;

    public void OnClickGoBack()
    {
        SceneManager.LoadScene(1);
    }
}
