using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;


public class TempPlayer : MonoBehaviour
{
    

    public GameObject cam;
     public Rigidbody2D rb;
    [SerializeField] private float timer,shootdelay,movedelay;
    [HideInInspector] public float speed;
    [HideInInspector] public Vector2 vel, walkingDir;
    public ParticleSystem shield;
   
    private Vector2 fast;
    public Vector3 dir,pos,dir2,pos2;
    public bool entering;
    void Start()
    {
        
    }
//REMINDER: Speed is 20.
    // Update is called once per frame
    void Update()
    {
        if (GMController.gm.tutdone & !GMController.gm.playerDead & !GMController.gm.dialogue & !GMController.gm.ui.showmenu)
        {
            TempMovement();
            if (Input.GetMouseButtonDown(0) & !GMController.gm.ani.attacking) AttackCode();
        }

        if (GMController.gm.dialogue) rb.velocity = Vector2.zero;
    }

    // when called this function plays the sword animation
    void AttackCode()
    {
        StartCoroutine(GMController.gm.ani.SpatulaSwipe());
        int ran = Random.Range(0, TempSound.soundtemp.swordclips.Length);
        
        TempSound.soundtemp.tempstorage[3].PlayOneShot(TempSound.soundtemp.swordclips[ran]);
    }
    
    void TempMovement()
    {
        vel = rb.velocity;
        Vector2 sidespeed = transform.right * speed;
        Vector2 fowardspeed = new Vector2(0,1) * speed;
        
        if (Input.GetKey(KeyCode.W))
        {
            walkingDir.y = 10;
            vel.y = fowardspeed.y;
        }

        else if (Input.GetKey(KeyCode.S))
        {
            walkingDir.y = -10;
            vel.y = -fowardspeed.y;
        }

        else
        {
            walkingDir.y = 0;
            vel.y = 0;
        }

        if (Input.GetKey(KeyCode.A))
        {
            walkingDir.x = -10;
            vel.x = -sidespeed.x;
        }

        else if (Input.GetKey(KeyCode.D))
        {
            walkingDir.x = 10;
            vel.x = sidespeed.x;
        }
        else
        {
            walkingDir.x = 0;
            vel.x = 0;
        }
        
        rb.velocity = Vector2.Lerp(rb.velocity, vel, movedelay * Time.deltaTime);
        // rb.velocity = vel;
    }

    // unused projectile function. Will proably scrap this function or recycle it for a future function
    void Shoot()
    {
        
        timer -= shootdelay * Time.deltaTime;
        if (timer <= 0)
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                GMController.gm.dir = new Vector2(0, 1);
                  Instantiate(GMController.gm.oc.pellet, transform.position, Quaternion.Euler(0,0,0));
                  timer = 5;
            }
            
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                GMController.gm.dir = new Vector2(0, -1);
                Instantiate(GMController.gm.oc.pellet, transform.position, Quaternion.Euler(0,0,0));;
                timer = 5;
            }
            
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                GMController.gm.dir = new Vector2(1, 0);
                Instantiate(GMController.gm.oc.pellet, transform.position, Quaternion.Euler(0,0,90));
                timer = 5;
            }
            
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                GMController.gm.dir = new Vector2(-1, 0);
                Instantiate(GMController.gm.oc.pellet, transform.position, Quaternion.Euler(0,0,0));
                timer = 5;
            }
        }
        
    }

    // this function lowers the player's hp and turns on the iframes function
    public void Playerhurt()
    {
        if (!GMController.gm.playerDead)
        {
            SoundControl.Soundcntrl.CharaDamagePlay();
            GMController.gm.ui.health.health--;
            GMController.gm.playerhurt = true;
            StartCoroutine(GMController.gm.IFrames());
        }
    }

    void MoveToRoom(Collider2D col, DoorScript ds)
    {
        if (col.gameObject.CompareTag("Door") & !entering)
        {
                
            if (!ds.bossdoor)
            {

                dir = col.gameObject.GetComponent<DoorScript>().dir;
                dir2 = col.gameObject.GetComponent<DoorScript>().dir;
                StartCoroutine(EnterRoom());
            }
            else
            {
                // SceneManager.LoadScene("EndPlayTest");

                Vector3 loc = GMController.gm.info.bossdoors[0].GetComponentInParent<BossRoomController>().transform
                    .position;
                transform.position = GMController.gm.info.startingloc[0].position;
                // cam.transform.SetParent(transform);
                // cam.transform.localPosition = new Vector3(0, 0, -10);
            }
        }

        if (col.gameObject.CompareTag("Room"))
            cam.transform.position = new Vector3(col.gameObject.transform.position.x,
                col.gameObject.transform.position.y, -10);

        if (col.gameObject.CompareTag("Exit"))
        {
            Vector3 loc = GMController.gm.info.bossdoors[1].GetComponentInParent<RoomController>().transform
                .position;
            transform.position = GMController.gm.info.startingloc[1].position;
            cam.transform.SetParent(null);
            cam.transform.position = new Vector3(loc.x, loc.y, -10);
        }
    }

    // changes the cam position and the player position to move to the room the player has entered
    IEnumerator EnterRoom()
    {
        entering = true;
        
        yield return new WaitForSeconds(.1f);
        
        if(dir.y > 0 || dir.y < 0)  pos = cam.transform.position + dir;
        else if(dir.y == 0) pos = cam.transform.position + (dir * 1.8f);
        
         pos2 = transform.position + (dir2 / 1.5f);
         // cam.transform.position = new Vector3(pos.x,pos.y, -10);
         transform.position = new Vector3(pos2.x, pos2.y, 0);
        // yield return new WaitUntil(Arrived);
        entering = false;
    }
    
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        
        RoomController rc = col.gameObject.GetComponent<RoomController>();
        DoorScript ds = col.gameObject.GetComponent<DoorScript>();
        BossRoomController br = col.gameObject.GetComponent<BossRoomController>();

        if (!GMController.gm.testscene)
        {
            MoveToRoom(col,ds);
        }


        if (col.gameObject.CompareTag("Room"))
        {
            GMController.gm.virtualCam.Follow = col.gameObject.transform;
        }

        if (br != null)
        {
            GMController.gm.virtualCam.GetComponent<CinemachineConfiner>().m_BoundingShape2D = br.polyCollider;
            GMController.gm.virtualCam.Follow = transform;
        }


    }

    private void OnTriggerExit2D(Collider2D col)
    {
        BossRoomController br = col.gameObject.GetComponent<BossRoomController>();
        if (br != null)
        {
            GMController.gm.virtualCam.GetComponent<CinemachineConfiner>().m_BoundingShape2D = null;

        }
    }

    // public bool Arrived()
    // {
    //     if (cam.transform.position != pos)
    //     {
    //         return false;
    //     }
    //     else
    //     {
    //         return true;
    //     }
    // }
    private void OnCollisionEnter2D(Collision2D col)
    {
       
    }
}
