// dwlt ��������!
// Test1
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public Transform Character;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    //123
    // Update is called once per frame
    void Update()
    {
        this.transform.position = Character.position;
    }
}
