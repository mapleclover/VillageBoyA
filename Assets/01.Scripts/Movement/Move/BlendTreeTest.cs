using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlendTreeTest : MonoBehaviour
{

    private Vector3 dir = Vector3.zero;
    public float rotSpeed = 10.0f;
    public float speed = 2.0f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        dir.x = Input.GetAxis("Horizontal");
        dir.z = Input.GetAxis("Vertical");

        this.transform.position = this.transform.position + dir * speed * Time.deltaTime;

        //this.transform.Translate( dir * Time.deltaTime * speed);
        transform.forward = Vector3.Lerp(transform.forward, dir, rotSpeed * Time.deltaTime);
    }

    
}
