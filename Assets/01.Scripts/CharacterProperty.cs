//작성자 : 박영준
//설명 : 
using UnityEngine;

public class CharacterProperty : MonoBehaviour
{
    //public CharacterStat Stat;
    Animator _anim = null;

    protected Animator myAnim
    {
        get
        {
            if (_anim == null)
            {
                _anim = GetComponent<Animator>();
                if (_anim == null)
                {
                    _anim = GetComponentInChildren<Animator>();
                }
            }

            return _anim;
        }
    }

    Rigidbody _rigid = null;

    protected Rigidbody myRigid
    {
        get
        {
            if (_rigid == null)
            {
                _rigid = GetComponent<Rigidbody>();
            }

            return _rigid;
        }
    }
}