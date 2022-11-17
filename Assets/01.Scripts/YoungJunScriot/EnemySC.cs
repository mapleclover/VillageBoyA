using UnityEngine;

[CreateAssetMenu(fileName = "new enemy", menuName = "ScriptableObject/Enemy")]
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

    public Sprite[] IconImages;
    MinimapIcon myEnemy = null;

    public enum EnemyType
    {
        normal,
        Boss
    }
    public EnemyType enemyIcon = EnemyType.normal;
    public void ChangeState(EnemyType s)
    {
        if (enemyIcon == s) return;
        enemyIcon = s;

        switch (enemyIcon)
        {
            case EnemyType.normal:            
                break;
            case EnemyType.Boss:               
                break;
        }
    }
}
