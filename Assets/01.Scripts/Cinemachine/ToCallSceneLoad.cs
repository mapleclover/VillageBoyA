//작성자 : 유은호
//설명 : 시네머신이 끝났을 때 다음 씬으로 넘어가기 위해 만든 스크립트
using UnityEngine;

public class ToCallSceneLoad : MonoBehaviour
{
    public void NextScene()
    {
        SceneLoad.Instance.ChangeScene(3);
    }
}
