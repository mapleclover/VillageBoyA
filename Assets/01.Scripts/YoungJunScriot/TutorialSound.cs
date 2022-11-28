using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialSound : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SoundTest.instance.PlayBGM("BGM_Tutorial");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
