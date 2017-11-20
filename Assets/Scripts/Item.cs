using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {

    public string itemName, itemDescription;
    public int health, armor, weight, damage, condition;
    public Material material;
    public bool equipA, equipB, equipC, equipD;
    public enum itemTypes
    {
        WeaponType
       ,ObjectType
       ,PlaterType
       ,EnemyType
    }
}
