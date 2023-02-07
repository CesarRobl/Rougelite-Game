using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public List<GameObject> PossibleEnemies;

    private void Start()
    {
        SpawnerController.SPcontrol.AllSpawners.Add(this);
    }


    public void Spawn()
    {
        GameObject g = Instantiate(PossibleEnemies[Random.Range(0, PossibleEnemies.Count)]);
        //add g to a list of all enemies

    }
}
