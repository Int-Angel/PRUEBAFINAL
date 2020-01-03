using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowDaddy : MonoBehaviour
{
    public Rigidbody2D dadRigidbody;
    new Rigidbody2D rigidbody;
    Vector2 diff;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();

        diff = dadRigidbody.position - rigidbody.position;
    }

    // Update is called once per frame
    void Update()
    {
        rigidbody.MovePosition(dadRigidbody.position + diff);
    }
}
