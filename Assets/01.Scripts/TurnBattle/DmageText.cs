using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DmageText : MonoBehaviour
{
    private float moveSpeed;
    private float destroyTime;
    public TMPro.TMP_Text Dmg = null;
    float _dmg = 0;
    public Color color;
    public float dmg
    {
        get => _dmg;
        set
        {
            _dmg = value;
            Dmg.text = _dmg.ToString();
        }
    }
    void Start()
    {
        moveSpeed = 20.0f;
        destroyTime = 2.0f;
        Invoke("DestroyObject", destroyTime);
        Dmg.color = color;
    }

   
    void Update()
    {
        
        transform.Translate(0, moveSpeed * Time.deltaTime, 0);
    }
    private void DestroyObject()
    {
        Destroy(gameObject);
    }
}
