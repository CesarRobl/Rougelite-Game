using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cinemachine;
using Pathfinding;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class GMController : MonoBehaviour
{
   
    public static GMController gm;
    [HideInInspector] public static bool showcrosshair = true;
    public static float volume, MusicVolume, fadespeed = 1.3f;
    public TempPlayer temp;
    public Transform player;
    public ObjectController oc;
    public AniController ani;
    public DialogueSystem talksystem;
    public List<RoomController> rc;
    public GameObject crosshair;
    public GameObject arrow;
    public CinemachineVirtualCamera virtualCam;
    [HideInInspector] public Transform holder;
    [SerializeField] private Transform sword;
    [HideInInspector]public RoomInfo info;
    [HideInInspector] public UIController ui;
    [HideInInspector] public IdSystem id;
    
    
    public int roomint, roommax;
    private float maxhealth;
    public float playerhealth;
    public float pelletspeed, hurtdelay, maxforce, forcedelay;
    private float timer;
    [HideInInspector]public Vector3 pos;
    public Vector2 dir;
    public bool playerhurt, testscene;
    private bool navdone,stop;
    [HideInInspector] public bool spawnedboss, loading, tutdone,playerDead,dialogue;
    public float smallhealthpercent, bighealthpercent;
    [HideInInspector] public AstarPath path;
    
    void Start()
    {
        crosshair.SetActive(showcrosshair);
        if (testscene) crosshair.SetActive(true);
        info = GetComponent<RoomInfo>();
        ui = GetComponent<UIController>();
        id = GetComponent<IdSystem>();
        timer = hurtdelay;
        
       Invoke("Setup", .5f);
        gm = this;
    }

    void Setup()
    {
        path = GetComponentInChildren<AstarPath>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (tutdone & !stop)
        {
            talksystem.gameObject.SetActive(true);
            stop = true;
        }
        
        if(loading & !SoundControl.Soundcntrl.MusicAS.isPlaying)SoundControl.Soundcntrl.MusicAS.Play();

        
       if(!spawnedboss )Invoke("SpawnBossRoom", 2.5f);
        if (Input.GetKeyDown(KeyCode.R)) SceneManager.LoadScene(SceneManager.GetActiveScene().name);
       
        if(ui.health.health <= 0) PlayerDie();
      
        Holder();
        
    }

    private void FixedUpdate()
    {
        FollowCursor();
        
    }

    public void MoveCamera()
    {
        
    }

    public void UpAndDownBounds()
    {
        RaycastHit2D hit = Physics2D.Raycast(temp.cam.transform.position, Vector2.up, 10, LayerMask.NameToLayer("Water"));
        if (hit.collider != null)
        {
            
        }

    }

    // prevents the player from getting hurt from the same thing multiple times in a single second
    public IEnumerator IFrames()
    {
        
        temp.shield.Play();
        yield return new WaitForSeconds(hurtdelay);
        temp.shield.Stop();
            timer = hurtdelay;
            playerhurt = false;
      
    }

    // Allows the holder to rotate towards where the cursor is
    void FollowCursor()
    {
         pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        crosshair.gameObject.transform.position = new Vector3(pos.x, pos.y, 0);
        
        Vector2 dir = pos - arrow.transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        arrow.transform.rotation = Quaternion.Slerp(arrow.transform.rotation, rotation, 10f * Time.deltaTime);
    }
    
    // Spawn the room that leads to the boss also starts the function to cover up doors that lead outside of the level
    void SpawnBossRoom()
    {
        RoomController[] roomarray = rc.ToArray();
        for (int i = 0; i < roomarray.Length; i++)
        {
            if (i == roomarray.Length - 1 & !spawnedboss & !testscene)
            { 
                rc[i].gameObject.SetActive(false);
                Instantiate(Roomlist.rl.bossroom, rc[i].transform.position, Quaternion.identity);
               
                Destroy(roomarray[i].gameObject);
                rc.Remove(roomarray[i]);
                
                spawnedboss = true;
            }

            if (!roomarray[i].bossroom & !testscene)
            {
                roomarray[i].Invoke("CheckDoor", .5f);
               
            }
        }
        if(!navdone) CreateNav();
           
    }

    //creates the nav for the ai
    void CreateNav()
    {
        path.Scan();
        navdone = true; 
        loading = true;
    }

   
    // use the holder position for the sword
    void Holder()
    {
        holder.position = new Vector2(gm.temp.transform.position.x, gm.temp.transform.position.y);
    }
    
    // If a player dies that play this function that resets the scene
    public void PlayerDie()
    {
         
        if (playerDead & SoundControl.Soundcntrl.MusicAS.isPlaying)
            SoundControl.Soundcntrl.MusicAS.Stop();
        
        if(!ani.stopAni)StartCoroutine(ani.PlayerDeath());
    }

   

    // If an enemy's hp reaches zero then play this code that destroys the game object and determines if it drops an item or not
    public void Die(GameObject enemy, LootSystem system)
    {
        system.SpawnLoot(enemy.transform.position);
        Destroy(enemy);
    }

    public void ShowDialogue(DialogueList talks)
    {
        if (!dialogue)
        {
            talksystem.gameObject.SetActive(true);
            talksystem.StartDialogue(talks);
        }
    }

    // Use this function when the boss health reaches 0
    public void BossDeath(GameObject boss)
    {
        ui.BossBar.SetActive(false);
        Destroy(boss);
    }
    
    
}
