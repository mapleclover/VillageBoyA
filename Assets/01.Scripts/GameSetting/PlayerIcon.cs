using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIcon : MonoBehaviour
{
    MinimapIcon myIcon = null;
    // Start is called before the first frame update
    void Start()
    {
        GameObject obj = Instantiate(Resources.Load("Prefabs/MinimapIcon"), SceneData.Inst.Minimap) as GameObject;
        myIcon = obj.GetComponent<MinimapIcon>();
        myIcon.Initialize(transform, Color.green);
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
