using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIcon : MinimapIcon
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject obj = Instantiate(Resources.Load("Prefabs/MinimapIcon"), SceneData.Inst.Minimap) as GameObject;      
        ChangeState(IconType.enemy);
        Initialize(transform);
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
