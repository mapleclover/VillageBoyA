using UnityEngine;

[CreateAssetMenu(fileName = "new pc", menuName = "ScriptableObject/PC")]
public class PC : ScriptableObject
{
    [SerializeField] private string _name;//캐릭터 이름
    public string Name
    {
        get => _name;
    }
    [SerializeField] private int _baseAttackDamage;//기본 데미지
    //스킬 이름, 데미지, 광역, 크리확률
    [SerializeField] private string[] _skillName;//스킬명
    public string[] SkillName
    {
        get => _skillName;
    }
    [SerializeField] private float[] _damageMultiplier;//데미지 배율
    [SerializeField] private float[] _criticalPercentage;//크리티컬 확률
    [SerializeField] private float[] _criticalRatio;//크리티컬 데미지 배율
    [SerializeField] private bool[] _isAOE;//광역기인가? Area Of Effect
    public bool[] IsAOE
    {
        get => _isAOE;
    }
    [SerializeField] private bool[] _isRangeAttack;//원거리 공격인가?
    public bool[] IsRangeAttack
    {
        get => _isRangeAttack;
    }

    private bool IsCritical(int skillNumber)
    {
        float rnd = Random.Range(0.0f, 100.0f);
        {
            if (rnd < _criticalPercentage[skillNumber])
            {
                return true;
            }
        }
        return false;
    }
    public float GetDamage(int skillNumber)
    {
        if (skillNumber >= _skillName.Length)//스킬 리스트보다 큰 수 입력시 에러 호출
        {
            Debug.LogError($"스킬번호{skillNumber}은(는) 존재하지 않습니다");
            return -1.0f;
        }
        float crit = 1.0f;
        if (IsCritical(skillNumber))
        {
            crit = _criticalRatio[skillNumber];
        }
        float damage = crit * _damageMultiplier[skillNumber] * _baseAttackDamage * Random.Range(0.9f, 1.1f);
        
        return damage;
    }
}
