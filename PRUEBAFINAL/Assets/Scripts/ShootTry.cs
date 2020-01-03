using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootTry : MonoBehaviour
{
    public GameObject bullet;
    public GameObject bulletOrigin;
    Vector3 target;
    Vector3 direction;
    bool GO;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            target = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            direction = (target - bulletOrigin.transform.position).normalized;

            bullet.GetComponent<PlayerBullet>().target = target;
         
            bullet.GetComponent<PlayerBullet>().speed = 40;
            Instantiate(bullet, bulletOrigin.GetComponent<Rigidbody2D>().position, Quaternion.identity);
        }
    }
}
