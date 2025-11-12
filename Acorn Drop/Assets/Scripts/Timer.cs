using System;
using Unity.VisualScripting;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    [SerializeField] GameObject butterfly1;
    [SerializeField] GameObject butterfly2;
    [SerializeField] GameObject butterfly3;

    [SerializeField] float timer = GameManager.instance.butterflytimer - 1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(timer > 0)
        {
            timer -= Time.deltaTime;
        }
        
        if (timer<= 0)
        {
            //Debug.Log("Time's up!");
            //Debug.Break();
            Destroy(butterfly1);
            Destroy(butterfly2);
            Destroy(butterfly3);

        }
    }
}
