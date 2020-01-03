using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Edible Object", menuName = "Inventory system/Items/Edible")]
public class EdibleObject : ItemObject
{
    [Header("PARAMETROS")]
    [Range(0,200)]
    public int restoreHealthValue;
    public bool restoreAllHealth = false;
    
    private void Awake()
    {
        type = ItemType.Edible;
    }
}
