//작성자 : 이현호
//설명 : 

using UnityEngine;

public class SceneData : MonoBehaviour
{
    public static SceneData Inst = null;
    public Transform Minimap;
    public Canvas canVas;

    private void Awake()
    {
        Inst = this;
    }
}