using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{

    Animator animator;
    CircleCollider2D actionRadius;
    new Rigidbody2D rigidbody;
    public Slider healthBar;
    new SpriteRenderer renderer;

    int nBullet;
    BulletMovement bulletMovement;

    [Header("Vida BOSS"), Range(0,100)]
    public float life;
    public int bulletDamage;
    [Header("BULLET PREFAB")]
    public GameObject bullet;
    public Rigidbody2D bulletOrigin;
    int damage;
    bool active;
    bool bulletMode;

    public bool countingWaitTimeHit, countingWaitTimeBulletMode, countingWaitTimeFireBlow;

    private void Awake()
    {
        countingWaitTimeBulletMode = false;
        countingWaitTimeFireBlow = false;
        countingWaitTimeHit = false;

        bulletMode = false;
        PlayerEvents.returningDamage.AddListener(setDamage);
        PlayerEvents.returningDamageObject.AddListener(ApplyDamage);
    }

    // Start is called before the first frame update
    void Start()
    {
        active = false;
        animator = GetComponent<Animator>();
        actionRadius = GetComponent<CircleCollider2D>();
        rigidbody = GetComponent<Rigidbody2D>();
        renderer = GetComponent<SpriteRenderer>();
  
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !active)
        {
            animator.SetBool("appear", true);
            actionRadius.radius = 2f;
            StartCoroutine(appearCo());
            PlayerEvents.showBossHealthBar.Invoke();
            
            healthBar.value = life;
            

        }
        if (collision.CompareTag("weapon") )
        {
            PlayerEvents.askingForDamage.Invoke();
            Damage();
        }
       
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && bulletMode)
        {

            SpriteRenderer r = collision.gameObject.GetComponent<SpriteRenderer>();


            PlayerEvents.playerRecivingDamage.Invoke(bulletDamage);


            Color color = new Color();
            color.a = 255;
            color.r = 255;
            color.g = 0;
            color.b = 0;

            r.color = color;
            StartCoroutine(colorCo(r));
        }

        if (bulletMode)
        {
            animator.SetBool("BulletMode", false);
            bulletMode = false;
        }
    }

    private void Damage()
    {
        life = life - this.damage;
      
        healthBar.value = life;
        if (life <= 0)
        {
            Destroy();
        }
        else
        {
            Color color = new Color();
            color.a = 255;


            color.r = 255;
            color.g = 0;
            color.b = 0;
            renderer.color = color;
            StartCoroutine(colorCo());
        }

        if(life <= 60)
        {
            animator.SetBool("Fase2", true);
        }
        if (life <= 25)
        {
            animator.SetBool("Fase3", true);
        }

    }
    private void Destroy()
    {
        active = false;
        animator.SetBool("appear", false);
        animator.SetBool("death", true);
        StartCoroutine(breakCo());
        PlayerEvents.unshowBossHealthBar.Invoke();
    }

    private void setDamage(float damage)
    {
        this.damage = (int)damage;
    }
    private void ApplyDamage(float damage)
    {
        this.damage = (int)damage;
        Damage();
    }
    IEnumerator appearCo()
    {
        yield return new WaitForSeconds(.5f);
        animator.SetBool("accion", true);
        animator.SetBool("appear", false);
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
    IEnumerator colorCo(SpriteRenderer r)
    {
        yield return new WaitForSeconds(.3f);
        Color color = new Color();
        color.a = 255;

        color.r = 255;
        color.g = 255;
        color.b = 255;
        r.color = color;

    }

    IEnumerator breakCo()
    {
        yield return new WaitForSeconds(3f);
        this.gameObject.SetActive(false);
        Destroy(gameObject);
    }

    IEnumerator GOCo()
    {
        yield return new WaitForSeconds(1f);
        bulletMovement.setGO(true);
    }

    void setBulletModeOn()
    {
        animator.SetBool("BulletMode",true);
        bulletMode = true;
       
    }

    public void fireBlow()
    {
        nBullet = animator.GetInteger("Nbullet");
        bullet.name = "sunBurn_" + nBullet;
        animator.SetInteger("Nbullet", nBullet + 1);
      
        Instantiate(bullet,bulletOrigin.position , Quaternion.identity);
       
        
    }

    public void doHitCo(float time)
    {
        StartCoroutine(hitCo(time));
    }

    public void doBulletModeCo(float time)
    {
        StartCoroutine(bulletModeCo(time));
    }

    public void doFireBlowCo(float time)
    {
        StartCoroutine(fireBlowCo(time));
    }

    IEnumerator hitCo(float time)
    {
        yield return new WaitForSeconds(time);
        countingWaitTimeHit = false;
    }

    IEnumerator bulletModeCo(float time)
    {
        yield return new WaitForSeconds(time);
        countingWaitTimeBulletMode = false;
    }

    IEnumerator fireBlowCo(float time)
    {
        yield return new WaitForSeconds(time);
        countingWaitTimeFireBlow = false;
    }

}
