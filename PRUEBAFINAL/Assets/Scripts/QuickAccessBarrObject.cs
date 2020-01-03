using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Quick access bar", menuName = "Inventory system/Quick access bar")]
public class QuickAccessBarrObject : ScriptableObject
{
    public List<InventorySlot> itemList = new List<InventorySlot>();

    public void AddItem(ItemObject item, int amount)
    {
        bool hasItem = false;
        for (int i = 0; i < itemList.Count; i++)
        {
            if (itemList[i].item == item) // el item ya esta en el inventario
            {
                itemList[i].setAmount(itemList[i].getAmount() + amount); // sumamos las cantidades

                hasItem = true;
                break;
            }
        }

        if (!hasItem)
        {
            itemList.Add(new InventorySlot(item, amount));
        }
    }

    public void Swap(int objA, int objB)
    {
        var temp = itemList[objA];
        itemList[objA] = itemList[objB];
        itemList[objB] = temp;
        PlayerEvents.playerInventoryChanged.Invoke();
    }
}
