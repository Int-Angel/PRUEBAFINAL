using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicInterface : UserInterface
{
    // Start is called before the first frame update
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

}
