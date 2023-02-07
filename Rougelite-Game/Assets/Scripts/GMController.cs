using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GMController : MonoBehaviour
{
   
    public static GMController gm;
    public TempPlayer temp;
    public Transform player;
    public ObjectController oc;

    public List<StageController> sc;

    public float pelletspeed;
    public Vector2 dir;

    public int roomint, roommax;
    void Start()
    {
        gm = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // If roomint hits roommax run this function and stop room gen
    void StopRoomGen()
    {
        foreach (var i in sc)
        {
            if(roomint > roommax) i.spawned = true;
        }
    }
}
