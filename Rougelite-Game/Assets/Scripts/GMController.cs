using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GMController : MonoBehaviour
{
   
    public static GMController gm;
    public TempPlayer temp;
    public Transform player;
    public ObjectController oc;

    public float pelletspeed;
    public Vector2 dir;
    void Start()
    {
        gm = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
