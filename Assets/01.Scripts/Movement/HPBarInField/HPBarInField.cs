using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBarInField : MonoBehaviour
{
    [Header("HP BARS")]
    public Slider KongHP;
    public Slider JinHP;
    public Slider EmberHP;



    public enum MYSTATE
    {
        Alive, Death
    }

    public MYSTATE state = MYSTATE.Alive;

    void ChangeState(MYSTATE myState)
    {
        if (state == myState) return;
        state = myState;
        switch (state)
        {
            case MYSTATE.Alive:
                if (KongHP.value <= 0.0f)
                {
                    ChangeState(MYSTATE.Death);
                }
                if (JinHP.value <= 0.0f)
                {
                    ChangeState(MYSTATE.Death);
                }
                if (EmberHP.value <= 0.0f)
                {
                    ChangeState(MYSTATE.Death);
                }
                break;
            case MYSTATE.Death:

                break;
        }
    }
    void StateProcess()
    {
        switch (state)
        {
            case MYSTATE.Alive:
                
                break;
            case MYSTATE.Death:
                break;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        //KongHP.value = _myHp;
        //JinHP.value = _myHp;
        //EmberHP.value = _myHp;




    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
