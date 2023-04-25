using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnController : MonoBehaviour
{
    [SerializeField] private int ran;
    [SerializeField] public GameObject enemy;
    
    [SerializeField] private GameObject boss;
    private RoomController rc;
    private bool stop;
    void Awake()
    {
      rc = GetComponentInParent<RoomController>();
    }

    private void Update()
    {
        
       
    }
    void SpawnSingleEnemy()
    {
        if (!stop)
        {
            GameObject ec = Instantiate(enemy, transform.position, Quaternion.Euler(0,0,0));
            rc.enemycount.Add(ec);
            stop = true;
        }
    }

    void SpawnRandomEnemy()
    {
        if (!stop)
        {
            ran = Random.Range(0, 100);
            if (ran <= 60)
            {
                int enemytype = Random.Range(0, GMController.gm.oc.elist.etype.Length );
                GameObject ec = Instantiate(GMController.gm.oc.elist.etype[enemytype], transform.position, Quaternion.Euler(0,0,0));
                rc.enemycount.Add(ec);
                ec = null;
            }
            stop = true;
        }
    }
    

   public void SpawnEnemy()
    {
        if(enemy != null) SpawnSingleEnemy();
        else SpawnRandomEnemy();
    }
}
