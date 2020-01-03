using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public int speed;
    
    
    public new  Rigidbody2D rigidbody;
    public Animator animator;
    Vector2 movement, lastMovement;
    bool roll;
    new SpriteRenderer renderer;

    public Rigidbody2D swordRigidbody;
    public Animator animatorSword;

    private void Awake()
    {
        PlayerEvents.gameOver.AddListener(GameOver);
        PlayerEvents.Respawn.AddListener(Respawn);
        renderer = GetComponent<SpriteRenderer>();
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal") * speed;
        movement.y = Input.GetAxisRaw("Vertical") * speed;
       
        if (Input.GetKeyDown("space")) // salto
        {
            roll = true;
            movement.x *= 2;
            movement.y *= 2;
            rigidbody.MovePosition(rigidbody.position + movement * speed * Time.deltaTime);
            
        }
        else
        {
            roll = false;
            rigidbody.velocity = (movement);
            
        }


        swordRigidbody.MovePosition(rigidbody.position + lastMovement * speed * 2 * Time.deltaTime);
        AnimationController(roll);
        SwordAnimatorController();
    }
   

    private void AnimationController(bool roll)
    {
        /*
            Pone los parametro del animador para las animaciones
         */
        animator.SetBool("Roll", roll);
        if (movement.x == 0 && movement.y == 0)
        {
            //no se esta moviendo
            animator.SetBool("Movement", false);
            animator.SetFloat("LastX", lastMovement.x);
            animator.SetFloat("LastY", lastMovement.y);
        }
        else
        {
            //hay movimiento
            animator.SetBool("Movement", true);
            animator.SetFloat("MoveX", movement.x);
            animator.SetFloat("MoveY", movement.y);
            lastMovement = movement;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("boss") || collision.gameObject.CompareTag("harm"))
        {
           

              ContactPoint2D point;
              int power = 5000;

             point = collision.GetContact(0);
        
            Vector2 force = new Vector2((point.normal.x) * power, (point.normal.y) * power);
        
            rigidbody.AddForce(force);

            Color color = new Color();
            color.a = 255;
            color.r = 255;
            color.g = 0;
            color.b = 0;

            renderer.color = color;
            StartCoroutine(colorCo(renderer));

        }
    }

    IEnumerator colorCo(SpriteRenderer r)
    {
        yield return new WaitForSeconds(.1f);
        Color color = new Color();
        color.a = 255;

        color.r = 255;
        color.g = 255;
        color.b = 255;
        r.color = color;
    }

    private void SwordAnimatorController()
    {
        if (Input.GetKeyDown("e") || Input.GetKeyDown("p"))
        {
            animatorSword.SetBool("attack", true);
        }
        else
        {
            animatorSword.SetBool("attack", false);
        }

        animatorSword.SetFloat("X", lastMovement.x);
        animatorSword.SetFloat("Y", lastMovement.y);
    }

    private void GameOver()
    {
       
        this.gameObject.SetActive(false);

    }

    private void Respawn()
    {
        this.gameObject.SetActive(true);
        Vector3 startPosition = new Vector3(0,0,0);

        transform.position = startPosition;

        GetComponent<PlayerHealth>().Respawn();
    }


}
