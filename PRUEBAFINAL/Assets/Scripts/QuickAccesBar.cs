using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickAccesBar : UserInterface
{
    // Start is called before the first frame update
    private void Awake()
    {
        PlayerEvents.playerInventoryChanged.AddListener(updateInventoryUI);
    }
    private void Start()
    {
        updateInventoryUI();
        Active();
    }

    // Update is called once per frame
    void Update()
    {
       

    }
}
