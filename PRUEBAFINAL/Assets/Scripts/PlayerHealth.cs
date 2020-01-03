using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NaughtyAttributes;

public class PlayerHealth : MonoBehaviour
{
    const int HEALTH_MAX = 100;
    const int SHIELD_AMOUNT_PER_SEGMENT = 25;
    const float MAX_FADE_TIME = 1f;

    Image healthBar, damageHealthBar, flashingBar;
    Image shield1, shield2, shield3;
    Image[] shieldArray;

    int previousLife;
    [Header("Vida"), Range(0,HEALTH_MAX)]
    public int life;
    [Header("Escudo"), Range(0, SHIELD_AMOUNT_PER_SEGMENT*3)]
    public int shield;

    float damagedHealthFadeTimer;
    float alphaChange = 0.1f;


    private void Awake()
    {
        healthBar = GameObject.Find("PlayerCanvas").transform.Find("Healthbar").Find("Bar").GetComponent<Image>();
        damageHealthBar = GameObject.Find("PlayerCanvas").transform.Find("Healthbar").Find("Damaged").GetComponent<Image>();
        flashingBar = GameObject.Find("PlayerCanvas").transform.Find("Healthbar").Find("Flashing").GetComponent<Image>();
        shield1 = GameObject.Find("PlayerCanvas").transform.Find("Healthbar").Find("ShieldBar").Find("First_Shield").Find("Shield").GetComponent<Image>();
        shield2 = GameObject.Find("PlayerCanvas").transform.Find("Healthbar").Find("ShieldBar").Find("Second_Shield").Find("Shield").GetComponent<Image>();
        shield3 = GameObject.Find("PlayerCanvas").transform.Find("Healthbar").Find("ShieldBar").Find("Third_Shield").Find("Shield").GetComponent<Image>();

        shieldArray = new Image[]{ shield1, shield2, shield3};
        PlayerEvents.playerRecivingDamage.AddListener(calculatingDamage);
        PlayerEvents.returningDamageObjectToPlayer.AddListener(calculatingDamage);
    }
    // Start is called before the first frame update
    void Start()
    {
        //life = HEALTH_MAX;
        //shield = SHIELD_AMOUNT_PER_SEGMENT * 3;
        updateBars();
        previousLife = life;
    }

    // Update is called once per frame
    void Update()
    {
       
        if (damageHealthBar.color.a > 0)
        {
            damagedHealthFadeTimer -= Time.deltaTime;
            if (damagedHealthFadeTimer < 0)
            {
                Color fadeColor = damageHealthBar.color;
                fadeColor.a -= Time.deltaTime * 2f;
                damageHealthBar.color = fadeColor;
            }
        }
        if (life <= 30)
        {
            Color fadeColor2 = flashingBar.color;
            fadeColor2.a += alphaChange;
            if (fadeColor2.a > 1f)
            {
                alphaChange *= -1f;
                fadeColor2.a = 1f;
            }
            if (fadeColor2.a < 0f)
            {
                alphaChange *= -1f;
                fadeColor2.a = 0f;
            }
            flashingBar.color = fadeColor2;
        }
        else
        {
            Color color = flashingBar.color;
            color.a = 0f;
            flashingBar.color = color;
        }
    }

    
    void calculatingDamage(float damage)
    {
        if (damage < shield)
        {
            shield -= (int)damage;
        }
        else
        {
            previousLife = life;
            life -= (int)(damage - shield);
            shield = 0;
        }

        damagedHealthFadeTimer = MAX_FADE_TIME;
        if (damageHealthBar.color.a <= 0)
        {
            damageHealthBar.fillAmount = (float)previousLife / HEALTH_MAX;

        }
        Color color = damageHealthBar.color;
        color.a = 1f;
        damageHealthBar.color = color;

        updateBars();

        if (life <= 0)
        {
            PlayerEvents.gameOver.Invoke();
        }
    }

    void updateBars()
    {
        healthBar.fillAmount = (float)life / HEALTH_MAX;

        for (int i = 0; i<3; i++)
        {
            int min = i * SHIELD_AMOUNT_PER_SEGMENT;
            int max = (i + 1) * SHIELD_AMOUNT_PER_SEGMENT;

            if (shield <= min)
            { // esta vacio
                shieldArray[i].fillAmount = 0f;
            }
            else
            {
                if (shield >= max)
                {// esta lleno
                    shieldArray[i].fillAmount = 1f;
                }
                else
                { // esta en el medio 
                    shieldArray[i].fillAmount = (float)(shield - min) / SHIELD_AMOUNT_PER_SEGMENT;
                }
            }

        }
      
    }

    public void Health(float health)
    {
        if(life < HEALTH_MAX)
        {
            life = life + (int)health;
            health = life - HEALTH_MAX;
            if (health>0)
            {
                life = HEALTH_MAX;
            }
        }
        if (health > 0)
        {
            shield += (int)health;
            health = shield - (SHIELD_AMOUNT_PER_SEGMENT * 3);
            if(health>0)
            {
                shield = SHIELD_AMOUNT_PER_SEGMENT * 3;
            }
        }
        updateBars();
    }
    
    public void Respawn()
    {
        shield = SHIELD_AMOUNT_PER_SEGMENT * 3;
        life = HEALTH_MAX;
        updateBars();
    }
}
