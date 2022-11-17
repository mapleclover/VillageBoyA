using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LookAtPlayer : MonoBehaviour
{
    public GameObject myTarget;
    LayerMask PlayerMask;
    Quaternion orgRot;
    void Start()
    {
        PlayerMask = 1 << LayerMask.NameToLayer("Player");
        orgRot = Quaternion.LookRotation(transform.forward);

    }
    
    void Update()
    {
        
    }

    void PlayerCheck()
    {

        Collider[] col = Physics.OverlapSphere(transform.position, 5.0f, PlayerMask);
        while(col.Length > 0)
        {
            StopAllCoroutines();
            StartCoroutine(Targetting());
        }

         
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if ((PlayerMask & 1 << other.gameObject.layer) != 0)
        {            
            if (myTarget == null)
            {
                myTarget = other.gameObject;
                StopAllCoroutines();
                StartCoroutine(Targetting());
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        myTarget = null;
        StopAllCoroutines();
        StartCoroutine(OrgTargetting());
    }
    protected IEnumerator Targetting()
    {
        
        while (myTarget != null)
        {
            Quaternion rot = Quaternion.LookRotation((myTarget.transform.position - transform.position).normalized);
            transform.rotation = Quaternion.Slerp(transform.rotation, rot, Time.deltaTime );
            yield return null;
        }
    }
    protected IEnumerator OrgTargetting()
    {
        
        
        while (transform.rotation != orgRot)
        {            
            transform.rotation = Quaternion.Slerp(transform.rotation, orgRot, Time.deltaTime);
            yield return null;
        }
    }
}

      
