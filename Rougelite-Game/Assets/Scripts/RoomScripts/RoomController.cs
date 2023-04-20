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
    private List<GameObject> colorwall;
    private GameObject[] hiddenWall;
    public Transform[] startingloc;
    public List<GameObject> enemycount;
     public SpawnController[] spawner;
     [SerializeField] private GameObject[] bossdoorsprite;
     
     private AstarPath path;

     private bool stop, stop2;
    private void Awake()
    {

        path = GetComponentInChildren<AstarPath>();
        Invoke("Addlist",.2f);
        for (int i = 0; i < mapwalls.Length; i++) mapwalls[i].GetComponent<SpriteRenderer>().color = Color.grey;
       
        for (int i = 0; i < doorwalls.Length; i++)
        {
            doorwalls[i].SetActive(false);
        }
        if (bossroom & !stop) Invoke("ChooseBossDoor", .5f);
    }

   

   // Update is called once per frame
    void Update()
    {
        // Debug.Log("My hidden wall count is " + hiddenWall.Length);
        if(playerin & !complete)CheckEnemy();
        // HideDoor();
        
    }

    // adds this function to a list in the gamemanager
    void Addlist()
    {
        GMController.gm.rc.Add(this);
    }

    // chooses the door that will lead to the bossroom
    void ChooseBossDoor()
    {
        Debug.Log("I am boss Door");
        for (int n = 0; n < doors.Count; n++)
        {
            // if(doors[n].doorinfront) doors.Remove(doors[n]);
            
            if (!doors[n].doorinfront & !doors[n].wallinfront & !stop2)
            {
                doors[n].bossdoor = true;
                doors[n].GetComponent<SpriteRenderer>().color = Color.yellow;
                bossdoorsprite[n].SetActive(true);
                GMController.gm.info.bossdoors.Add(doors[n]);
                GMController.gm.info.startingloc[1] = doors[n].startingpoint;
                doors.Remove(doors[n]);
                stop2 = true;
            }
        }
        
        if(stop2)Invoke("HideDoor", .1f);
        
        // for (int i = 0; i < doors.Count; i++) SetWall(i);
            
        stop = true;
    }
    // checks how many enemies are present in the player's current room. If the enemy count is zero then play the unblock function
    
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
        Vector3 rot = transform.eulerAngles;
        for (int i = 0; i < doors.Count; i++)
        {
            if (!doors[i].doorinfront)
            {
                rot.z = 0;
                if (doors[i].dir.y == 10) rot.z = 180;
                else if (doors[i].dir.x == 10) rot.z = 90;
                else if (doors[i].dir.x == -10) rot.z = 270;

                GameObject wall = Instantiate(GMController.gm.oc.doorwalls, doors[i].transform.position, Quaternion.Euler(rot));
                wall.GetComponent<SpriteRenderer>().color = Color.grey;
               
            }

        }
    }

    // changes the door sprite from open to closed when a player has cleared a room
    void OpenDoor()
    {
        TempSound.soundtemp.tempstorage[2].PlayOneShot(  TempSound.soundtemp.clipstorage[2]);
        for (int i = 0; i < wendydoorsopen.Length; i++)
        {
            wendydoorsopen[i].SetActive(true);
            wendydoorsclosed[i].SetActive(false);
        }
    }

    // if a door is hitting nothing or a wall then spawn a wall that will block and cover the door in question
    public void CheckDoor()
    {
        Vector3 rot = transform.eulerAngles;
        for (int i = 0; i < doors.Count; i++)
        {
            if (!doors[i].doorinfront || doors[i].wallinfront)
            {
                rot.z = 0;
                if (doors[i].dir.y == 10) rot.z = 180;
                else if (doors[i].dir.x == 10) rot.z = 90;
                else if (doors[i].dir.x == -10) rot.z = 270;

                GameObject wall = Instantiate(GMController.gm.oc.doorwalls, doors[i].transform.position, Quaternion.Euler(rot));
                if (wall != null)
                {
                    wall.GetComponent<SpriteRenderer>().color = Color.grey;
                   
                }

                doors.Remove(doors[i]);
            }
        }
    }
    
    // changes the door sprite from open to close when a player has entered a room
    void CloseDoor()
    {
        TempSound.soundtemp.tempstorage[1].PlayOneShot(  TempSound.soundtemp.clipstorage[1]);
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
    // once the player has entered the room then this function will play that will close the doors and spawn the enemies
    void Setup()
    {
        CloseDoor();
        for (int i = 0; i < doorwalls.Length; i++)
        {
            doorwalls[i].SetActive(true);
        }
        for (int i = 0; i < spawner.Length; i++)
        {
            if(spawner[i].enemy != null) spawner[i].SpawnSingleEnemy();
            else spawner[i].SpawnEnemy();
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

        if (tp != null)
        {
          
            for (int i = 0; i < mapwalls.Length; i++) mapwalls[i].GetComponent<SpriteRenderer>().color = Color.yellow;
            // if (hiddenWall.Count > 0)
            // {
            //     for (int i = 0; i < hiddenWall.Count; i++)
            //     {
            //         hiddenWall[i].GetComponent<SpriteRenderer>().color = Color.yellow;
            //     }
            // }
        }
           
    }
    
    private void OnTriggerExit2D(Collider2D col)
    {
        TempPlayer tp = col.gameObject.GetComponent<TempPlayer>();
        if (tp != null)
        {
            
            for (int i = 0; i < mapwalls.Length; i++) mapwalls[i].GetComponent<SpriteRenderer>().color = Color.white;
            // if (hiddenWall.Count > 0)
            // {
            //     for (int i = 0; i < hiddenWall.Count; i++)
            //     {
            //         hiddenWall[i].GetComponent<SpriteRenderer>().color = Color.white;
            //     }
            // }

            playerin = false;
        }
    }
}
