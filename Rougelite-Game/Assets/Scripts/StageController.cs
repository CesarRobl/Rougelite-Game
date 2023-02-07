using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class StageController : MonoBehaviour
{
    [Range(1,4)]
    public int NumDir;

    public bool spawned;
    private int ran,ran2,SaveDir;
    private bool touching,stop;
    void Awake()
    {
        Invoke("SpawnRoom", .1f);
        Invoke("SpawnCornerRoom", .2f);
        
    }

    private void Update()
    {
        
    }

    
   
    void SpawnRoom()
    {
        if (GMController.gm.roomint <= GMController.gm.roommax - 2)
        {
            if (!touching)
            {

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
            }
            GMController.gm.roomint++;
        }
      
    }

    void SpawnCornerRoom()
    {
         if(GMController.gm.roomint > GMController.gm.roommax - 2 & !touching)
        {
            Instantiate(Roomlist.rl.cornerrooms[NumDir - 1], transform.position, Quaternion.identity);
            
            GMController.gm.roomint++;
        }
    }

    void FixRoomSpawn()
    {
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
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
       
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.layer == 3)Destroy(gameObject);
        
        StageController sc = col.gameObject.GetComponent<StageController>();
        if (col.gameObject.CompareTag("StageSpawn"))
        {
            float time = NumDir;
            touching = true;
            if (GMController.gm.roomint <= GMController.gm.roommax - 2) Invoke("FixRoomSpawn", time / 10 );
            // Invoke("SpawnBlock", .2f);

        }
      
    }
}
