using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ITEMTYPE
{
    Weapon, Armor, Potion, Quest
}
[CreateAssetMenu(fileName = "ItemData", menuName = "ScriptableObjects/ItemData", order = -1)]
public class ItemData :ScriptableObject
{

    [SerializeField] ITEMTYPE _type;
    public ITEMTYPE Type
    {
        get => _type;
    }

    [SerializeField] string _name;
    public string Name
    {
        get => _name;
    }
    [SerializeField] int _price;

    public int Price
    {
        get => _price;
    }
    [SerializeField] int _value;
    public int Value
    {
        get => _value;
    }
}
