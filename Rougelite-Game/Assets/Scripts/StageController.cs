using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class StageController : MonoBehaviour
{
    [Range(1,4)]
    public int NumDir;

    private BoxCollider2D col;
    [SerializeField] private int longorsmall;
    public bool spawned;
    private int ran,ran2,SaveDir;
    private bool touching,stop;
    void Awake()
    {
        col = GetComponent<BoxCollider2D>();
        longorsmall = Random.Range(0, 100);
        if (longorsmall > 10) Invoke("SpawnRoom", .1f);
        else if (longorsmall <= 10 )
        {
            CheckSpace();
            Invoke("SpawnLongRoom", .1f);
        }
        
        Invoke("SpawnCornerRoom", .2f);
        
    }

    private void Update()
    {
        
    }

    void CheckSpace()
    {
        if (NumDir == 1)
        {
            transform.localScale += new Vector3(0, 9, 0);
            transform.position += new Vector3(0, 3.7f, 0);
        }
        else if (NumDir == 2)
        {
            transform.localScale += new Vector3(9, 0, 0);
            transform.position += new Vector3(10, 0, 0);
        }
        else if (NumDir == 3)
        {
            transform.localScale += new Vector3(0, 9, 0);
            transform.position += new Vector3(0, -10, 0);
        }
        else if (NumDir == 4)
        {
            transform.localScale += new Vector3(9, 0, 0);
            transform.position += new Vector3(-3.7f, 0, 0);
        }
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

    void SpawnLongRoom()
    {
        if (GMController.gm.roomint <= GMController.gm.roommax - 2)
        {
            if (!touching)
            {

                if (NumDir == 1 || NumDir == 3)
                {
                    ran = Random.Range(0, Roomlist.rl.longUD.Length - 1);
                    Instantiate(Roomlist.rl.longUD[ran], transform.position, Quaternion.identity);
                }

                // Spawn any room with a left door
                if (NumDir == 2 || NumDir == 4)
                {
                    ran = Random.Range(0, Roomlist.rl.longLR.Length - 1);
                    Instantiate(Roomlist.rl.longLR[ran], transform.position, Quaternion.identity);
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
        if (longorsmall <= 10) Debug.Log( GetComponentInParent<GameObject>().gameObject.name + " " + col.gameObject.name + longorsmall);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (longorsmall <= 10) Debug.Log(gameObject.name + " " + col.gameObject.name + longorsmall); 
        StageController sc = col.gameObject.GetComponent<StageController>();
        if (col.gameObject.CompareTag("StageSpawn"))
        {
            
            float time = NumDir;
            touching = true;
            if (GMController.gm.roomint <= GMController.gm.roommax - 2) Invoke("FixRoomSpawn", time / 10 );
            // Invoke("SpawnBlock", .2f);

        }
         Destroy(gameObject);
        
       
      
    }
}
