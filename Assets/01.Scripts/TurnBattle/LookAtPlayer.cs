//작성자 : 박진
//설명 :
using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    GameObject myTarget;
    LayerMask PlayerMask;
    Quaternion orgRot;
    public float rotSpeed = 1.0f;

    void Start()
    {
        PlayerMask = 1 << LayerMask.NameToLayer("Player");
        orgRot = Quaternion.LookRotation(transform.forward);
    }

    void Update()
    {
        PlayerCheck();
    }

    void PlayerCheck()
    {
        Collider[] col = Physics.OverlapSphere(transform.position, 5.0f, PlayerMask);
        if (Physics.OverlapSphereNonAlloc(transform.position, 5.0f, col, PlayerMask) > 0)
        {
            myTarget = col[0].gameObject;
            Quaternion rot = Quaternion.LookRotation((myTarget.transform.position - transform.position).normalized);
            transform.rotation = Quaternion.Slerp(transform.rotation, rot, Time.deltaTime * rotSpeed);
        }
        else if (Physics.OverlapSphereNonAlloc(transform.position, 5.0f, col, PlayerMask) == 0)
        {
            myTarget = null;
            transform.rotation = Quaternion.Slerp(transform.rotation, orgRot, Time.deltaTime * rotSpeed);
        }
    }
}