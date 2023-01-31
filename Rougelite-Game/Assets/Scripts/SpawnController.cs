using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnController : MonoBehaviour
{
    [SerializeField] private int ran;
    private bool stop;
    void Awake()
    {
       
       
    }

    private void Update()
    {

        if (!stop)
        {
            ran = Random.Range(0, 100);
            if(ran <= 60)Instantiate(GMController.gm.oc.enemy, transform.position, Quaternion.Euler(0,0,0));
            stop = true;
        }
    }
}
