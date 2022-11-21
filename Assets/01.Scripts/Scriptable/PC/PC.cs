//작성자 : 유은호
//설명 : 캐릭터 기본 전투 정보를 담을 저장소
using UnityEngine;

[CreateAssetMenu(fileName = "new pc", menuName = "ScriptableObject/PC")]
public class PC : ScriptableObject
{
    [SerializeField] private string _name; //캐릭터 이름

    public string Name
    {
        get => _name;
    }

    [SerializeField] private int _baseAttackDamage; //기본 데미지 
    [SerializeField] private int _speed; //속도

    public int Speed
    {
        get => _speed;
    }

    [SerializeField] private int _health; //체력

    public int Health
    {
        get => _health;
    }

    public enum Type
    {
        Player,
        Enemy,
        Boss
    }

    [SerializeField] private Type _battleType;

    public Type BattelType
    {
        get => _battleType;
    }

    //스킬 이름, 데미지, 광역, 크리확률
    [SerializeField] private string[] _skillName; //스킬명

    public string[] SkillName
    {
        get => _skillName;
    }
    
    [SerializeField] private string[] _skillDescription; //스킬명

    public string[] SkillDescription
    {
        get => _skillDescription;
    }

    [SerializeField] private int[] _skillCost; //체력

    public int[] SkillCost
    {
        get => _skillCost;
    }
    
    [SerializeField] private float[] _damageMultiplier; //데미지 배율
    [SerializeField] private float[] _criticalPercentage; //크리티컬 확률
    [SerializeField] private float[] _criticalRatio; //크리티컬 데미지 배율

    public float[] CriticalRatio
    {
        get => _criticalRatio;
    }

    [SerializeField] private bool[] _isAOE; //광역기인가? Area Of Effect

    public bool[] IsAOE
    {
        get => _isAOE;
    }

    [SerializeField] private bool[] _isRangeAttack; //원거리 공격인가?

    public bool[] IsRangeAttack
    {
        get => _isRangeAttack;
    }

    public bool IsCritical(int skillNumber)
    {
        float rnd = Random.Range(0.0f, 100.0f);
        {
            if (rnd < _criticalPercentage[skillNumber])
            {
                return true;
            }
        }
        return false;
    } //크리떳냐?

    public float GetDamage(int skillNumber)
    {
        if (skillNumber >= _skillName.Length) //스킬 리스트보다 큰 수 입력시 에러 호출
        {
            Debug.LogError($"스킬번호{skillNumber}은(는) 존재하지 않습니다");
            return -1.0f;
        }

        float damage = _damageMultiplier[skillNumber] * _baseAttackDamage * Random.Range(0.9f, 1.1f);
        return damage;
    } //기본 데미지 * 스킬 데미지 배율 * ±10%는 얼마야?
}