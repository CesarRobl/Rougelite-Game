using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class StageController : MonoBehaviour
{
    [Range(1,4)]
    public int NumDir;

    private int ran;
    private bool spawned, touching;
    void Awake()
    {
        if(!spawned && !touching)Invoke("SpawnRoom", 15f * Time.deltaTime);
    }

    

    void SpawnRoom()
    {
        // Spawn any room with a bottom door
        if (NumDir == 1)
        {
            ran = Random.Range(0, Roomlist.rl.bottomrooms.Length);
            Instantiate(Roomlist.rl.bottomrooms[ran], transform.position, Quaternion.identity);
        }
        // Spawn any room with a left door
        if (NumDir == 2)
        {
            ran = Random.Range(0, Roomlist.rl.leftrooms.Length);
            Instantiate(Roomlist.rl.leftrooms[ran], transform.position, Quaternion.identity);
        }
        // Spawn any room with a top door
        if (NumDir == 3)
        {
            ran = Random.Range(0, Roomlist.rl.uprooms.Length);
            Instantiate(Roomlist.rl.uprooms[ran], transform.position, Quaternion.identity);
        }
        // Spawn any room with a right door
        if (NumDir == 4)
        {
            ran = Random.Range(0, Roomlist.rl.rightrooms.Length);
            Instantiate(Roomlist.rl.rightrooms[ran], transform.position, Quaternion.identity);
        }

        spawned = true;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
       
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        StageController sc = col.GetComponent<StageController>();

        if (sc != null)
        {
            Debug.Log(sc.gameObject.name);
            touching = true;
        }
        if(col.gameObject.layer == 3)Destroy(gameObject);
      
    }
}
