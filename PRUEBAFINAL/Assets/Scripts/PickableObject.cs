using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent (typeof(CircleCollider2D))]
public class PickableObject : MonoBehaviour
{
    public float radius = 3f;
    public ItemObject item;
    [Range(0,50)]
    public int amount;
    public Text Instructions;
    bool active;

    new CircleCollider2D collider2D;

    private void Awake()
    {
        collider2D = GetComponent<CircleCollider2D>();
        collider2D.radius = radius;
        collider2D.isTrigger = true;
        Instructions.enabled = false;
        active = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            if (Input.GetKeyDown("f"))
            {
                PlayerEvents.pickUpThisItem.Invoke(gameObject.GetComponent<PickableObject>());
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Instructions.enabled = true;
            active = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Instructions.enabled = false;
            active = false;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    
}
