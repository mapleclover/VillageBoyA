using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcIcon : MinimapIcon
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject obj = Instantiate(Resources.Load("Prefabs/NpcIcon"), SceneData.Inst.Minimap) as GameObject;
        ChangeState(IconType.npc);
        Initialize(transform);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
