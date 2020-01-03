using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lifeTime : MonoBehaviour
{
    [Header("Timepo de vida en S:"), Range(0, 10)]
    public float time;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(lifeCo());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator lifeCo()
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
