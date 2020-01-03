using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickSlotBehaviour : MonoBehaviour
{
    public GameObject selectedIcon;
    public int keyAction;
    static int selected = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("" + keyAction) || selected == keyAction)
        {
            selectedIcon.SetActive(true);
            selected = keyAction;
            /*poner accion al jugador que se active con la tecla e*/
        }
        else
        {
            selectedIcon.SetActive(false);
        }
        
    }
}
