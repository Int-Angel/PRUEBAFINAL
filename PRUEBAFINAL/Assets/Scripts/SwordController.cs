using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordController : MonoBehaviour
{


    public int damage;

    private void Awake()
    {
        PlayerEvents.askingForDamage.AddListener(getDamage);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }

    public void getDamage()
    {
        PlayerEvents.returningDamage.Invoke(damage);
    }

   
}
