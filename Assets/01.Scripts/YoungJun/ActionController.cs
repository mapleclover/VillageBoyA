using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// ���� ��ȣ�ۿ� 
public class ActionController : MonoBehaviour
{
    [SerializeField]
    private float range;

    private bool pickNpc = false; // ���ǽ� ��ȣ�ۿ� üũ

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
