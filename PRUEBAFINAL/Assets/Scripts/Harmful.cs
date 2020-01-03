using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Harmful : MonoBehaviour
{

    public float damage;
   
   
    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

 

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            SpriteRenderer renderer = collision.gameObject.GetComponent<SpriteRenderer>();

            PlayerEvents.playerRecivingDamage.Invoke(damage);
        }
    }

    public void setDamage(float damage)
    {
        this.damage = damage;
    }
    public float getDamage()
    {
        return damage;
    }

}
