using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class showBossHealthBar : MonoBehaviour
{

    public GameObject bossHealthBar;

    private void Awake()
    {
        PlayerEvents.showBossHealthBar.AddListener(show);
        PlayerEvents.unshowBossHealthBar.AddListener(unShow);
    }

    private void show()
    {
        bossHealthBar.SetActive(true);
    }
    private void unShow()
    {
        bossHealthBar.SetActive(false);
    }
}
