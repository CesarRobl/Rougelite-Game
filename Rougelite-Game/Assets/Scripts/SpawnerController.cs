using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MonoBehaviour
{
    public static SpawnerController SPcontrol;

    public List<EnemySpawner> AllSpawners;

    // Start is called before the first frame update
    void awake()
    {
        SPcontrol = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnRoomEnter()
    {
        foreach (EnemySpawner e in AllSpawners)
        {
            e.Spawn();
        }
    }
}
