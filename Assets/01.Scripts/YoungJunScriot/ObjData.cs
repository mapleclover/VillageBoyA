using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//박영준 오브젝트 생성자.
public class ObjData : MonoBehaviour
{
    public int id;
    public bool isNpc;

    public GameObject obj;
    public GameObject Klee_1000;
    public GameObject Hodu_2000;

    MinimapIcon my_Icon = null;

    void Start()
    {
        GameObject obj = Instantiate(Resources.Load("Prefabs/NpcIcon"), SceneData.Inst.Minimap) as GameObject;
        my_Icon = obj.GetComponent<MinimapIcon>();
        my_Icon.Initialize(transform, Color.yellow);
    }

    void Update()
    {
        
    }
}
