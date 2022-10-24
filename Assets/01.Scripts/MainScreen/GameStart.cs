using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameStart : MonoBehaviour
{
    public void OnCilck()

    { 
         SceneLoad.Instance.ChangeScene(2); 
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()

    {
        
    }
}
