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
    public List<StageController> sc;

    public int roomint, roommax;
    public float pelletspeed;
    public Vector2 dir;
    void Start()
    {
        gm = this;
    }

    // Update is called once per frame
    void Update()
    {
        StopRoomGen();
        if (Input.GetKeyDown(KeyCode.R)) SceneManager.LoadScene(0);
    }

    //Stops room generation once the roommax is hit
    void StopRoomGen()
    {
        
            foreach (var i in sc)
            {
                i.spawned = true;
            }
        
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
