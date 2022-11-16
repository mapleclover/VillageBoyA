using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconManager : MonoBehaviour
{

    MinimapIcon my_Icon = null;
    // Start is called before the first frame update
    void Start()
    {
        GameObject obj = Instantiate(Resources.Load("Prefabs/EnemyIcon"), SceneData.Inst.Minimap) as GameObject;
        my_Icon = obj.GetComponent<MinimapIcon>();
        my_Icon.Initialize(transform, Color.green);
    }

  

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
