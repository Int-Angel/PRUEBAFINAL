using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableObject : MonoBehaviour
{

    new  SpriteRenderer renderer;
    new  Rigidbody2D rigidbody;
    int damage;
    int impulse;

    Vector2 currentPos, pasPos;

    bool moving;

    private void Awake()
    {
        
        PlayerEvents.returningDamage.AddListener(setImpulse);
        PlayerEvents.askingForDamageObject.AddListener(returningDamage);
    }

    // Start is called before the first frame update
    void Start()
    {
        
        rigidbody = GetComponent<Rigidbody2D>();
        renderer = GetComponent<SpriteRenderer>();

        moving = false;
        currentPos = rigidbody.position;
        pasPos = currentPos;
    }

    // Update is called once per frame
    void Update()
    {
        currentPos = rigidbody.position;
        moving = !(pasPos == currentPos);
        pasPos = currentPos;
    }

   

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("weapon"))
        {

            PlayerEvents.askingForDamage.Invoke();

            ContactPoint2D point;
            int power = 300 * impulse;
        

            point = collision.GetContact(0);
            Vector2 force = new Vector2((point.normal.x) * power, (point.normal.y) * power);
            damage = (int)rigidbody.mass * impulse;
            rigidbody.AddForce(force,ForceMode2D.Impulse);
        }
        if (collision.gameObject.CompareTag("boss"))
        {
            if (moving)
            {
                calculatingDamage();
                PlayerEvents.returningDamageObject.Invoke(damage);
            }
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            if (moving)
            {
                calculatingDamage();
                PlayerEvents.returningDamageObjectToPlayer.Invoke(damage);
            }
        }
    }

    private void setImpulse(float impulse)
    {
        this.impulse = (int)impulse;
    }

    private void returningDamage()
    {
        PlayerEvents.returningDamageObject.Invoke(damage);
        Debug.Log("return: " + damage);
    }
    private void calculatingDamage()
    {
        if (!moving)
        {
            damage = 0;
        }
    }
}

/*
 MEJORAR SISTEMA DE DAÑO
     
     
     */
