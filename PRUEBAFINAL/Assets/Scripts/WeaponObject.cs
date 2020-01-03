using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon Object", menuName = "Inventory system/Items/Weapon")]
public class WeaponObject : ItemObject
{
    [Range(0,100)]
    public int Damage;
    private void Awake()
    {
        type = ItemType.Weapon;
    }
}
