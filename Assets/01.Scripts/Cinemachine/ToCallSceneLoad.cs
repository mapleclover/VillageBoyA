using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToCallSceneLoad : MonoBehaviour
{
    public void NextScene()
    {
        SceneLoad.Instance.ChangeScene(3);
    }
}
