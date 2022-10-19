using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new enemy" ,menuName = "new enemy/enemy")]
public class EnemySC : ScriptableObject
{
    public string enemyName;
    public EnemyType enemyType;
    public float moveSpeed;
    public GameObject enemyPrefab;
    


    public enum EnemyType
    {
        normal,
        Boss
    }
}
