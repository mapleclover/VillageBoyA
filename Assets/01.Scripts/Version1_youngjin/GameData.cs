using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]  //꼭 serializable이여야 한다.
public class GameData
{
    public long lastUpdated;
    //원하는 게임 데이터 넣음
    /*
        public int BGMSound=0;      
        public int EffectSound=0;
     */
    public string name; //플레이어 이름

}
