using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnController : MonoBehaviour
{
    [SerializeField] private int ran;
    [SerializeField] public GameObject enemy, spawnEnemy;
    [SerializeField] private ETypeList enemyType;
    [SerializeField] private List<ETypeList> eList;
    [SerializeField] private GameObject boss;
    private RoomController rc;
    private bool stop, _dontSpawn;
    void Awake()
    {
        if (enemyType != null) enemy = enemyType.enemy;
      rc = GetComponentInParent<RoomController>();
      GetEnemy();
    }

    private void Update()
    {
        
       
    }

        void GetEnemy()
    {
        int ran = Random.Range(1, 101);
        List<ETypeList> possibleEnemy = new List<ETypeList>();
        foreach (var enemies in eList)
        {
            if(ran <= enemies.spawnChance ) possibleEnemy.Add(enemies);
            
        }

        if (possibleEnemy.Count > 0)
        {
            
           
            // ETypeList spawnEnemy = possibleEnemy[Random.Range(0, possibleEnemy.Count)];
            // Debug.Log(spawnEnemy.enemy.name);
            // this.spawnEnemy = spawnEnemy.enemy;
            // return;
            for (int i = 0; i < possibleEnemy.Count; i++)
            {
               
               
            
            
                float inf = Mathf.Infinity;
                if (possibleEnemy[i].spawnChance < inf)
                {
                    ETypeList spawnEnemy = possibleEnemy[i];
                    // Debug.Log(spawnEnemy.enemy.name);
                    this.spawnEnemy = spawnEnemy.enemy;
                    return;
                }
            }
        }
        
        // Debug.Log("No enemies?");
        _dontSpawn = true;
       
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
            Debug.Log("Dont Spawn is " + _dontSpawn);
            if (spawnEnemy != null)
            {
                GameObject enemyObject = spawnEnemy;

                GameObject ec = Instantiate(enemyObject, transform.position, Quaternion.Euler(0, 0, 0));
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
