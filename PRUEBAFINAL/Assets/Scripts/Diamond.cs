using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diamond : MonoBehaviour
{

    Animator animator;
    new SpriteRenderer renderer;
    public int life;
    int damage;

    private void Awake()
    {
        PlayerEvents.returningDamage.AddListener(setDamage);
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        renderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Destroy()
    {
        animator.SetBool("Destroy",true);
        StartCoroutine(breakCo());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.CompareTag("weapon"))
        {
            PlayerEvents.askingForDamage.Invoke();

          
            life = life - damage;
            Debug.Log("Damage: " + damage);
            if (life <= 0)
            {
                Destroy();
            }
            else
            {
                Color color = new Color();
                color.a = 255;


                color.r = 137;
                color.g = 255;
                color.b = 0;
                renderer.color = color;
                StartCoroutine(colorCo());
            }

        }
    }

    IEnumerator breakCo()
    {
        yield return new WaitForSeconds(1f);
        this.gameObject.SetActive(false);
    }

    IEnumerator colorCo()
    {
        yield return new WaitForSeconds(.3f);
        Color color = new Color();
        color.a = 255;

        color.r = 255;
        color.g = 255;
        color.b = 255;
        renderer.color = color;

    }

    public void setDamage( float _damage)
    {
        damage = (int)_damage;
    }
}
