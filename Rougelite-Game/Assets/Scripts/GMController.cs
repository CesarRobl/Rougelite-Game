using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GMController : MonoBehaviour
{
   
    public static GMController gm;
    public TempPlayer temp;
    public Transform player;
    public ObjectController oc;
    public List<RoomController> rc;

    public int roomint, roommax,playerhealth;
    public float pelletspeed;
    public Vector2 dir;
    private bool spawnedboss;
    void Start()
    {
        gm = this;
    }

    // Update is called once per frame
    void Update()
    {
       if(!spawnedboss)Invoke("SpawnBossRoom", 1f);
        if (Input.GetKeyDown(KeyCode.R)) SceneManager.LoadScene(0);
        if(playerhealth <= 0) PlayerDie();
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
        Destroy(enemy);
    }
}
