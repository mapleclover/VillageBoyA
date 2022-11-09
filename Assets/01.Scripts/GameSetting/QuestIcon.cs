using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class QuestIcon : MinimapIcon
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject obj = Instantiate(Resources.Load("Prefabs/QuestIcon"), SceneData.Inst.Minimap) as GameObject;
        ChangeState(IconType.quest);
        Initialize(transform);
    }
    // Update is called once per frame
    void Update()
    {
      
    }
}
