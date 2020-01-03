using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    /*
     Inventory handler
         */
    public InventoryObject inventory;
    private void Awake()
    {
        PlayerEvents.pickUpThisItem.AddListener(addItemToInventory);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void addItemToInventory(PickableObject pickableObject)
    {
        inventory.AddItem(pickableObject.item, pickableObject.amount);
    }

    
}
