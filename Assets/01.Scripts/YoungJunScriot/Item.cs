using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new item", menuName = "new item/item")]
public class Item : ScriptableObject
{
    public string itemName;
    public NpcType itemType;
    public GameObject itemPrefab;


    public enum NpcType
    {
        Equiment,
        Used,
        Ingredient
    }
}
