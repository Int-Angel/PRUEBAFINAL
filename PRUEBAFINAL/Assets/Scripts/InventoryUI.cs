using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using System;

public class InventoryUI : MonoBehaviour
{
    public GameObject inventoryUI;
    public static bool displayed = false;

    public InventoryObject inventory;

    public GameObject inventorySlot;
    public GameObject InventoryGrid;

    List<GameObject> itemsDisplayed = new List<GameObject>();
    static int? posA = null, posB = null;
    static bool isDragging;

    public GameObject quickAccessBar;
    

    private void Awake()
    {
        PlayerEvents.playerInventoryChanged.AddListener(updateInventoryUI);
    }
    private void Start()
    {
        updateInventoryUI();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (displayed)
            {
                Disactive();
            }
            else
            {
                Active();
            }
        }
       
    }

    public void Disactive()
    {
        inventoryUI.SetActive(false);
   
        displayed = false;
    }


    public void Active()
    {
        updateInventoryUI();
        inventoryUI.SetActive(true);
    
        displayed = true;
    }

   

    public void updateInventoryUI()
    {
        EraseDisplayedItems();
        for (int i = 0; i < inventory.itemList.Count; i++)
        {
            
            
           
            inventorySlot.GetComponent<InventoryComponents>().icon.sprite = inventory.itemList[i].item.icon;
            inventorySlot.GetComponent<InventoryComponents>().amount.text = "x" + inventory.itemList[i].amount;
            inventorySlot.GetComponent<InventoryComponents>().pos = i;

            if (inventory.itemList[i].item.description.Equals("EMPTY"))
            {
                inventorySlot.GetComponent<InventoryComponents>().amount.color = new Color(0, 0, 0, 0);
                inventorySlot.GetComponent<InventoryComponents>().icon.color = new Color(0, 0, 0, 0);

            }
            else
            {
                inventorySlot.GetComponent<InventoryComponents>().amount.color = new Color(156, 15, 15, 255);
                inventorySlot.GetComponent<InventoryComponents>().icon.color = new Color(255, 255, 255, 255);
            }
            var invItem = Instantiate(inventorySlot, InventoryGrid.GetComponent<RectTransform>(), true);

            AddEvent(invItem, EventTriggerType.PointerEnter, delegate { OnEnter(invItem); });
            AddEvent(invItem, EventTriggerType.BeginDrag, delegate { OnDragStart(invItem); });
            AddEvent(invItem, EventTriggerType.EndDrag, delegate { OnDragEnd(invItem); });
            AddEvent(invItem, EventTriggerType.PointerExit, delegate { OnExit(invItem); });


            itemsDisplayed.Add(invItem);
        }
    }

    private void OnExit(GameObject invItem)
    {
        ToolTipPro.HideToolTip_Static();
    }

    public void EraseDisplayedItems()
    {
        for (int i = 0; i < itemsDisplayed.Count; i++)
        {
            Destroy(itemsDisplayed[i]);
        }
        itemsDisplayed.Clear();
    }


    private void OnEnter(GameObject invItem)
    {
        if (isDragging && posA != null)
        {
            posB = invItem.GetComponent<InventoryComponents>().pos;
            inventory.Swap((int)posA, (int)posB);
            isDragging = false;
            posA = null;
            posB = null;
        }
        int i = invItem.GetComponent<InventoryComponents>().pos;
        if (!inventory.itemList[i].item.description.Equals("EMPTY"))
        {
            ToolTipPro.ShowToolTip_Static(inventory.itemList[i].item.description);
        }
    }

    private void OnDragEnd(GameObject invItem)
    {
        StartCoroutine(enterCo());
      
    }
   
    private void OnDragStart(GameObject invItem)
    {
        isDragging = true;
        posA = invItem.GetComponent<InventoryComponents>().pos;
        ToolTipPro.HideToolTip_Static();
     
    }

    private void AddEvent(GameObject obj, EventTriggerType type, UnityAction<BaseEventData> action)
    {
        EventTrigger trigger = obj.GetComponentInChildren<EventTrigger>();
        var eventTrigger = new EventTrigger.Entry();
        eventTrigger.eventID = type;
        eventTrigger.callback.AddListener(action);
        trigger.triggers.Add(eventTrigger);
    }
    IEnumerator enterCo()
    {
        yield return new WaitForSeconds(0.02f);
        isDragging = false;
    }

    
}
