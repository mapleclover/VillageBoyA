using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//박영준 오브젝트 생성자.
public class ObjData : MonoBehaviour
{
    public int id;
    public bool isNpc;

    MinimapIcon myIcon = null;

    void Start()
    {
        GameObject obj = Instantiate(Resources.Load("Prefabs/NpcIcon"), SceneData.Inst.Minimap) as GameObject;
        myIcon = obj.GetComponent<MinimapIcon>();
        myIcon.Initialize(transform, Color.yellow);
    }
   
}
