using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowGameOver : MonoBehaviour
{
    public GameObject obj;
    private void Awake()
    {
        PlayerEvents.gameOver.AddListener(Activate);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Activate()
    {
        obj.SetActive(true);
    }

    public void Respawn()
    {
        PlayerEvents.Respawn.Invoke();
        obj.SetActive(false);
    }
}
