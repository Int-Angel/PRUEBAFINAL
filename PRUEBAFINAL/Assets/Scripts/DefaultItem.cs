using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Default Object",menuName ="Inventory system/Items/Default")]
public class DefaultItem : ItemObject
{
    private void Awake()
    {
        type = ItemType.Default;
    }
}
