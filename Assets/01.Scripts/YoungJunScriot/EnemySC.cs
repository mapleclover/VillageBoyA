using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "new enemy", menuName = "new enemy/enemy")]
public class EnemySC : ScriptableObject
{
    public string enemyName;
    public EnemyType enemyType;
    public float moveSpeed;
    [field: SerializeField]
    public float HP
    {
        get;
        private set;
    }
    public int Speed;
    public float[] AttackDmg = new float[3];
    public float Defend;
    public bool[] longAttack = new bool[3];
    public GameObject enemyPrefab;

    //public Sprite[] IconImages;

    public enum EnemyType
    {
        normal,
        Boss
    }
    /*
    public EnemyType EnemyIcon = EnemyType.normal;

    public void ChangeState(EnemyType s)
    {
        if (EnemyIcon == s) return;
        EnemyIcon = s;

        switch (EnemyIcon)
        {
            case EnemyType.normal:
                
                break;
            case EnemyType.Boss:
                
                break;
        }
    }*/
}
