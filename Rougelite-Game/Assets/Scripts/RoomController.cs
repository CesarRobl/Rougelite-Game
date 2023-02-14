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
    [SerializeField] private GameObject[] walls;
    [SerializeField] private GameObject[] wendydoorsopen;
    [SerializeField] private GameObject[] wendydoorsclosed;
    [SerializeField] private GameObject[] mapwalls;
    public Transform[] startingloc;
    public List<GameObject> enemycount;
     public SpawnController[] spawner;

     private bool stop;
    private void Awake()
    {
        
        Invoke("Addlist",.2f);
        for (int i = 0; i < mapwalls.Length; i++) mapwalls[i].GetComponent<SpriteRenderer>().color = Color.clear;
       
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
        int n = doors.Count - 1;
        doors[n].bossdoor = true;
        doors[n].GetComponent<SpriteRenderer>().color = Color.yellow;
        GMController.gm.info.bossdoors.Add( doors[n]);
        GMController.gm.info.startingloc[1] = doors[n].startingpoint;
        doors.Remove(doors[n]);
        
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

    void OpenDoor()
    {
        for (int i = 0; i < wendydoorsopen.Length; i++)
        {
            wendydoorsopen[i].SetActive(true);
            wendydoorsclosed[i].SetActive(false);
        }
    }

    public void CheckDoor()
    {
        Vector3 rot = transform.eulerAngles;
        for (int i = 0; i < doors.Count; i++)
        {
            if (!doors[i].doorinfront & !doors[i].bossdoor)
            {
                rot.z = 0;
                if (doors[i].dir.y == 10) rot.z = 180;
                else if (doors[i].dir.x == 10) rot.z = 90;
                else if (doors[i].dir.x == -10) rot.z = 270;

                Instantiate(GMController.gm.oc.doorwalls, doors[i].transform.position, Quaternion.Euler(rot));
            }
        }
    }
    
    void CloseDoor()
    {
        for (int i = 0; i < wendydoorsopen.Length; i++)
        {
            wendydoorsopen[i].SetActive(false);
            wendydoorsclosed[i].SetActive(true);
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
        OpenDoor();
    }
    void Setup()
    {
        CloseDoor();
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
        if(tp != null) for (int i = 0; i < mapwalls.Length; i++) mapwalls[i].GetComponent<SpriteRenderer>().color = Color.yellow;
           
    }
    
    private void OnTriggerExit2D(Collider2D col)
    {
        TempPlayer tp = col.gameObject.GetComponent<TempPlayer>();
        if (tp != null)
        {
            for (int i = 0; i < mapwalls.Length; i++) mapwalls[i].GetComponent<SpriteRenderer>().color = Color.white;
            playerin = false;
        }
    }
}
