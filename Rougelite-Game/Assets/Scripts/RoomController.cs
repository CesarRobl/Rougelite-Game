using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RoomController : MonoBehaviour
{
    public bool playerin;
    public bool bossroom;
    private bool complete;
    [SerializeField] private bool hidedoor;
    [SerializeField] private List<DoorScript> doors;
    [SerializeField] private GameObject[] doorwalls;
    public Transform[] startingloc;
    public List<GameObject> enemycount;
     public SpawnController[] spawner;

     private bool stop;
    private void Awake()
    {
        Invoke("Addlist",.2f);
        for (int i = 0; i < doorwalls.Length; i++)
        {
            doorwalls[i].SetActive(false);
        }
        if (bossroom & !stop) Invoke("ChooseBossDoor", .5f);
    }
    
    

    // Update is called once per frame
    void Update()
    {
        if(playerin & !complete)CheckEnemy();
        HideDoor();
        
    }

    void Addlist()
    {
        GMController.gm.rc.Add(this);
    }

    void ChooseBossDoor()
    {
        
        
        doors[doors.Count - 1].bossdoor = true;
        doors[doors.Count - 1].GetComponent<SpriteRenderer>().color = Color.yellow;
        GMController.gm.info.bossdoors.Add( doors[doors.Count - 1]);
        GMController.gm.info.startingloc[1] = startingloc[doors.Count - 1];
        doors.Remove(doors[doors.Count - 1]);
        
        // for (int i = 0; i < doors.Count; i++) SetWall(i);
            
        stop = true;
    }
    void CheckEnemy()
    {
        for (int i = 0; i < enemycount.Count; i++) if (enemycount[i] == null) enemycount.Remove(enemycount[i].gameObject);

        if (enemycount.Count == 0)
        {
            Unblock();
            complete = true;
        }
        
    }

    void HideDoor()
    {
        for (int i = 0; i < doors.Count; i++) if (doors[i].doorinfront) doors.Remove(doors[i]);
        for (int i = 0; i < doors.Count; i++)
        {
            if (doors[i].wallinfront)
            {
                if (bossroom) doors.Remove(doors[i]);
                SetWall(i);
            }
        }
    }

    void SetWall(int n)
    {
        doorwalls[n].SetActive(true);
        doorwalls[n].GetComponent<SpriteRenderer>().color = Color.white;
    }
    void Unblock()
    {
        for (int i = 0; i < doorwalls.Length; i++)
        {
            doorwalls[i].SetActive(false);
        }
    }
    void Setup()
    {
        for (int i = 0; i < doorwalls.Length; i++)
        {
            doorwalls[i].SetActive(true);
        }
        for (int i = 0; i < spawner.Length; i++)
        {
            spawner[i].SpawnEnemy();
        }
        
        playerin = true;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        TempPlayer tp = col.gameObject.GetComponent<TempPlayer>();
        if (tp != null & !playerin & !complete)
        {
           Setup();
        }
    }
    
    private void OnTriggerExit2D(Collider2D col)
    {
        TempPlayer tp = col.gameObject.GetComponent<TempPlayer>();
        if (tp != null) playerin = false;
    }
}
