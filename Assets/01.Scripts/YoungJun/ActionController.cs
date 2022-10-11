using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// 영준 상호작용 
public class ActionController : MonoBehaviour
{
    [SerializeField]
    private float range;

    private bool pickNpc = false; // 엔피시 상호작용 체크

    private RaycastHit hitInfo;

    [SerializeField]
    private LayerMask layerMask;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void CheckNpc()
    {
        if(Physics.Raycast(transform.position, transform.forward, out hitInfo, range, layerMask))
        {
            if(hitInfo.transform.tag == "NPC")
               NpcInfoAppear();
        }
        else
        {
            NpcInfoDisappear();
        }
    }
    private void NpcInfoAppear()
    {

    }
    private void NpcInfoDisappear()
    {

    }
}
