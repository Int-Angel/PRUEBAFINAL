using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    Animator animator;
    new Rigidbody2D rigidbody;
    new Transform transform;
    Harmful harmful;
    Transform playerPos;
    Vector2 direction;
    
    public bool GO;
    public new CircleCollider2D collider2D;

    [Header("PARAMETROS"), Range(1, 10)]
    public float scale;
    [Range(0,25)]
    public float speed;       /*    TRUE                                 */
    public bool SxD;         // damage = damage * scale;
    public bool increaseScale;      // cambie su tamaño random
    public bool expSpeed;  // la velocidad aumente exponencialmente

    public GameObject destroyObject;

    bool counting;
    // Start is called before the first frame update
    private void Awake()
    {
        counting = false;
        GO = false;
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
        transform = GetComponent<Transform>();
        TryGetComponent<Harmful>(out harmful);
        playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        collider2D = GameObject.Find(this.gameObject.name ).GetComponent<CircleCollider2D>();
        collider2D.enabled = false;
    }
    void Start()
    {
       
            transform.localScale = new Vector3(scale, scale, 1);
            if (SxD && harmful != null)
                harmful.setDamage(harmful.getDamage() * (int)scale);


            direction = playerPos.position - transform.position;
            direction.x = (direction.x) * 10;
            direction.y = (direction.y * 10) - 3;

            int rand = Random.Range(0, 2);
            speed = Random.Range(3, 25);
            SxD = (rand == 0);
            scale = Random.Range(1, 4);
            rand = Random.Range(0, 2);
            expSpeed = (rand == 0);
            rand = Random.Range(0, 2);
            increaseScale = (rand == 0);

            StartCoroutine(GOCo());
            StartCoroutine(lifeCo());
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GO)
        {

            transform.position = Vector2.MoveTowards(transform.position, direction, speed * Time.deltaTime);
            if (expSpeed)
                speed *= 1.1f;
            if (increaseScale && transform.localScale.x < 6)
            {
                transform.localScale = new Vector3(transform.localScale.x * 1.01f, transform.localScale.y * 1.01f, 0);
                if (SxD && harmful != null && !counting)
                {
                    counting = true;
                    StartCoroutine(damageCo());
                }  
            }
               
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("wall") || collision.gameObject.CompareTag("object") || collision.gameObject.CompareTag("weapon"))
        {

            Instantiate(destroyObject, new Vector3(rigidbody.position.x, rigidbody.position.y, 0), Quaternion.identity);
            Destroy(gameObject);
     

           /* animator.SetBool("Destroy", true);
            collider2D.enabled = false;
            StartCoroutine(breakCo());
            Debug.Log(harmful.getDamage());*/
        }
    }


    IEnumerator breakCo()
    {
        yield return new WaitForSeconds(0.3f);
        Destroy(destroyObject);
    }

    IEnumerator lifeCo()
    {
        yield return new WaitForSeconds(5f);
        animator.SetBool("Destroy", true);
        collider2D.enabled = false;
        StartCoroutine(breakCo());
        Debug.Log(harmful.getDamage());
    }

    IEnumerator damageCo()
    {
        if (expSpeed)
        {
            yield return new WaitForSeconds(0.2f);
            harmful.setDamage((harmful.getDamage() * transform.localScale.x));
            counting = false;
          
        }
        else
        {
            yield return new WaitForSeconds(1f);
            harmful.setDamage((harmful.getDamage() * transform.localScale.x) / 1.4f);
            counting = false;
         
        }
    }


    IEnumerator GOCo()
    {
        yield return new WaitForSeconds(0.3f);
        collider2D.enabled = true;
        GO = true;
    }


    public void setGO(bool GO)
    {
        this.GO = GO;
    }
}
