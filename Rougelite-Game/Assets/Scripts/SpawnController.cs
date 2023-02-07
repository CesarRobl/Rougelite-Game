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
        Invoke("SpawnEnemy", 1.5f);
    }

    private void Update()
    {

       
    }

    void CheckPlayer()
    {
        
    }

    void SpawnEnemy()
    {
        if (!stop)
        {
            ran = Random.Range(0, 100);
            if (ran <= 60)
            {
                int enemytype = Random.Range(0, GMController.gm.oc.elist.etype.Length );
                Instantiate(GMController.gm.oc.elist.etype[enemytype], transform.position, Quaternion.Euler(0,0,0));
            }
            stop = true;
        }
    }
}
