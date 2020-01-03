using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    bool GO;
    public float speed;
    public Vector3 target;
    Vector3 lastPos, currentPos;
    public GameObject destroyObject;
    // Start is called before the first frame update
    void Start()
    {
        GO = true;
    }

    // Update is called once per frame
    void Update()
    {
        /* if (Input.GetMouseButtonDown(0))
         {

             target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
             Debug.Log(target);
         }

         if (GO)
         {
             //speed += 0.1f;
             transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);

         }*/
        currentPos = transform.position;
         if(GO)
            transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
        lastPos = currentPos;
        if(lastPos == transform.position)
        {
            Instantiate(destroyObject, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
