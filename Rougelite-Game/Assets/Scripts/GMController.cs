using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class GMController : MonoBehaviour
{
   
    public static GMController gm;
    public TempPlayer temp;
    public Transform player;
    public ObjectController oc;
    public AniController ani;
    public List<RoomController> rc;
    public GameObject crosshair;
    public GameObject arrow;
    [SerializeField] private Transform holder;
    [SerializeField] private Transform sword;
    [HideInInspector]public RoomInfo info;
    
    public int roomint, roommax,playerhealth;
    private int maxhealth;
    public float pelletspeed, hurtdelay, maxforce;
    private float timer;
    private Vector3 pos;
    public Vector2 dir;
    public bool playerhurt;
    [HideInInspector]public bool spawnedboss;
    
    void Start()
    {
        info = GetComponent<RoomInfo>();
        timer = hurtdelay;
        maxhealth = playerhealth;
        Cursor.visible = true;
        gm = this;
    }

    // Update is called once per frame
    void Update()
    {
       if(!spawnedboss)Invoke("SpawnBossRoom", 1f);
        if (Input.GetKeyDown(KeyCode.R)) SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        if(playerhurt) IFrames();
        if(playerhealth <= 0) PlayerDie();
        if (playerhealth > maxhealth) playerhealth = maxhealth;
        Holder();
        
    }

    private void FixedUpdate()
    {
        FollowCursor();
    }

    // prevents the player from getting hurt from the same thing multiple times in a single second
    void IFrames()
    {
        if (timer <= 0)
        {
            timer = hurtdelay;
            playerhurt = false;
        }
        else timer -= Time.deltaTime; 
    }

    void FollowCursor()
    {
         pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        crosshair.gameObject.transform.position = new Vector3(pos.x, pos.y, 0);
        
        Vector2 dir = pos - arrow.transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        arrow.transform.rotation = Quaternion.Slerp(arrow.transform.rotation, rotation, 10f * Time.deltaTime);
    }
    
    void SpawnBossRoom()
    {
        for (int i = 0; i < rc.Count; i++)
        {
            if (i == rc.Count - 1 & !spawnedboss)
            { 
                rc[i].gameObject.SetActive(false);
                Instantiate(Roomlist.rl.bossroom, rc[i].transform.position, Quaternion.identity);
               
                Destroy(rc[i].gameObject);
                rc.Remove(rc[i]);
                spawnedboss = true;
            }
            rc[i].Invoke("CheckDoor", .5f);
        }
           
    }

    float SetBossRot(float z ,string roomname)
    {
        if (roomname == "U(Clone)") z = 90;
        else if (roomname == "L(Clone)") z = 180;
        else if (roomname == "D(Clone)") z = 270;
            
           
        
        
        return z;
    }
    
    // use the holder position for the sword
    void Holder()
    {
        holder.position = new Vector2(gm.temp.transform.position.x, gm.temp.transform.position.y);
    }
    public void PlayerDie()
    { 
        SceneManager.LoadScene(0);
    }
    public void Die(GameObject enemy)
    {
        int rand = Random.Range(0, 100);
        if (rand >= 50)
        {
            Instantiate(oc.Heathdrop,enemy.transform.position,Quaternion.identity);
        }
        Destroy(enemy);
    }
}
