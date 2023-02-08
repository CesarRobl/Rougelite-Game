using System;
using System.Collections;
using System.Collections.Generic;
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

    public int roomint, roommax,playerhealth;
    private int maxhealth;
    public float pelletspeed, hurtdelay;
    private float timer;
    private Vector3 pos;
    public Vector2 dir;
    public bool playerhurt;
    private bool spawnedboss;
    void Start()
    {
        timer = hurtdelay;
        maxhealth = playerhealth;
        Cursor.visible = true;
        gm = this;
    }

    // Update is called once per frame
    void Update()
    {
       if(!spawnedboss)Invoke("SpawnBossRoom", 1f);
        if (Input.GetKeyDown(KeyCode.R)) SceneManager.LoadScene(0);
        if(playerhurt) IFrames();
        if(playerhealth <= 0) PlayerDie();
        if (playerhealth > maxhealth) playerhealth = maxhealth;
        
    }

    private void FixedUpdate()
    {
        FollowCursor();
    }

    // prevents the player from getting hurt from the samething multiple times in a single second
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
                Vector3 rot = transform.rotation.eulerAngles;
                string roomname = rc[i].gameObject.name;
               
                if (roomname == "U(Clone)") rot.z = 90;
                else if (roomname == "L(Clone)") rot.z = 180;
                else if (roomname == "D(Clone)") rot.z = 270;

                Debug.Log(roomname + " " +rot.z);
                Instantiate(Roomlist.rl.bossroom, rc[i].transform.position, Quaternion.Euler(rot));
               
                Destroy(rc[i].gameObject);
                rc.Remove(rc[i]);
                spawnedboss = true;
            }
            
        }
           
    }

    float SetBossRot(float z ,string roomname)
    {
        if (roomname == "U(Clone)") z = 90;
        else if (roomname == "L(Clone)") z = 180;
        else if (roomname == "D(Clone)") z = 270;
            
           
        
        
        return z;
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
